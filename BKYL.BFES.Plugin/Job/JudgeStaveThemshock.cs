using BFES.DataAccess;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    class JudgeStaveThemshock : BaseJob
    {
        List<UNIVERSALTE_MONITOR_CONFIG> ConfigList = null;
        List<AV05M_LININGTC_ALARMINFO> LastResultList = null;
        List<AV05M_LININGTC_ALARMINFO> NewResultList = null;
        public override void InitTask()
        {

        }
        public override void RunTask(DateTime currentTime)
        {
             using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                ConfigList = idatabase.Queryable<UNIVERSALTE_MONITOR_CONFIG>().Where(model => model.POINTTYPE == 1).ToList();
                NewResultList = JudgeAlarm(currentTime); //本次满足报警的对象
                List<AV05M_LININGTC_ALARMINFO> UpdateList = null;
                DateTime dt = idatabase.GetDateTime(string.Format("select max(timestamp) from AV05M_LININGTC_ALARMINFO where timestamp<=to_date('{0}','yyyy-mm-dd HH24:mi:ss')", currentTime));
                try
                {
                    if (dt != null && DateTime.MinValue != dt)
                    {
                        LastResultList = idatabase.Queryable<AV05M_LININGTC_ALARMINFO>().Where(model => model.TIMESTAMP == dt).ToList();
                        if (NewResultList != null && NewResultList.Count > 0 && LastResultList != null && LastResultList.Count > 0)
                        {
                            //如果本次与上次时间相同，不计算
                            if (LastResultList[0].TIMESTAMP == NewResultList[0].TIMESTAMP)
                            {
                                LastResultList.Clear();
                                LastResultList = null;
                                NewResultList.Clear();
                                NewResultList = null;
                                return;
                            }
                            UpdateList = new List<AV05M_LININGTC_ALARMINFO>();
                            //本次需要更新的上次的结果
                            List<AV05M_LININGTC_ALARMINFO> tempList = new List<AV05M_LININGTC_ALARMINFO>();
                            //先将新的copy一份
                            foreach (AV05M_LININGTC_ALARMINFO item in NewResultList)
                            {
                                tempList.Add(item);
                            }
                            foreach (AV05M_LININGTC_ALARMINFO item in tempList)
                            {
                                //本次报警了，上次对应的点报警信息
                                AV05M_LININGTC_ALARMINFO model = LastResultList.Find(m => m.POINTNAME == item.POINTNAME && m.FLAG == 1);                                
                                if (model != null)
                                {
                                    model.REMAINTIME = (item.TIMESTAMP - model.STARTTIME).TotalMinutes;
                                    UpdateList.Add(model);
                                    int index = NewResultList.FindIndex(m=>m.POINTNAME==model.POINTNAME);
                                    if (index >= 0)
                                    {
                                        NewResultList.RemoveAt(index);
                                    }
                                }
                            }
                            LastResultList.Clear();
                            LastResultList = null;
                        }
                        //更新上次的
                        if (UpdateList != null && UpdateList.Count > 0)
                        {
                            foreach (var item in UpdateList)
                            {
                                idatabase.ExecuteCommand($"delete  from AV05M_LININGTC_ALARMINFO where POINTNAME = '{item.POINTNAME}' and TIMESTAMP={DataBaseFactory.ConvertTimeString(item.TIMESTAMP,DataBaseType.Oracle)}");
                                if (NewResultList != null && NewResultList.Count > 0)
                                {
                                    item.TIMESTAMP = NewResultList[0].TIMESTAMP;
                                }
                                else
                                {
                                    item.TIMESTAMP = currentTime;

                                }
                                idatabase.Insert<AV05M_LININGTC_ALARMINFO>(item);
                            }
                            GlobalObject.RichTextLog.AppendTextByAsync(string.Format("数据时间{0}更新成功，共计{1}条!", currentTime, UpdateList.Count), System.Drawing.Color.Green);
                            UpdateList.Clear();
                            UpdateList = null;
                        }

                    }
                    //插入本次发生热震的
                    if (NewResultList != null && NewResultList.Count > 0)
                    {
                        foreach (AV05M_LININGTC_ALARMINFO item in NewResultList)
                        {
                            idatabase.Insert<AV05M_LININGTC_ALARMINFO>(item);
                        }
                        GlobalObject.RichTextLog.AppendTextByAsync(string.Format("数据时间{0}插入成功，共计{1}条！", currentTime, NewResultList.Count), System.Drawing.Color.Green);
                        NewResultList.Clear();
                        NewResultList = null;
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        public DataTable GetDataTable(DateTime dt)
        {
            string sql = "";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                sql = string.Format("select * from AV05M_LININGTC where timestamp=(select max(timestamp) from AV05M_LININGTC where timestamp<=to_date('{0}','yyyy-mm-dd hh24:mi:ss'))", dt);
                return idatabase.GetDataTable(sql);
            }

        }
        public List<AV05M_LININGTC_ALARMINFO> JudgeAlarm(DateTime datetime)
        {
            List<AV05M_LININGTC_ALARMINFO> modellist = null;
            DataTable dt = GetDataTable(datetime);

            if (dt != null && dt.Rows.Count == 1)
            {
                modellist = new List<AV05M_LININGTC_ALARMINFO>();
                foreach (UNIVERSALTE_MONITOR_CONFIG item in ConfigList)
                {
                    AV05M_LININGTC_ALARMINFO model = new AV05M_LININGTC_ALARMINFO();

                    model.TIMESTAMP = dt.Rows[0][0].ToDate();
                    model.POINTNAME = item.POINTDESC;
                    model.HEIGHT = item.HEIGHT;
                    model.ANGLE = item.ANGLE;
                    model.DATAVALUE = dt.Rows[0][item.ADDRESS].ToDouble();


                    double value = dt.Rows[0][item.ADDRESS].ToDouble();
                    if (value >= item.ALARMHEIGHT)
                    {
                        model.FLAG = 1;
                        model.ALARMINFO = string.Format("{0}标高{1},测温点{2}温度值{3};", item.GROUPDESC, item.HEIGHT, item.POINTDESC, model.DATAVALUE);
                        model.STARTTIME = dt.Rows[0][0].ToDate();
                        modellist.Add(model);
                    }
                }
            }
            return modellist;
        }
    }
}
