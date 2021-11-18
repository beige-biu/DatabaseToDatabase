using BFES.DataAccess;
using BFES.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    /// <summary>
    /// 计算热面温度平均值
    /// </summary>
    public class Job_CalRMWD_Avg : BaseJob
    {
        Dictionary<int, string> sqlDic = new Dictionary<int, string>();

        List<OP_POINT_CONFIG> op_point_config_List = new List<OP_POINT_CONFIG>();
        string strGetMaxTime = "";
        string strGetMinTime = "";
        public override void InitTask()
        {
            //查询热面温度配置点表
            op_point_config_List = new List<OP_POINT_CONFIG>();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                string strSQL = "select * from OP_POINT_CONFIG";
                op_point_config_List = idatabase.GetList<OP_POINT_CONFIG>(strSQL);
            }
            var temp = op_point_config_List.GroupBy(k => k.SEG_ID);
            string SelectSql = "";
            foreach (var item in temp)
            {
                SelectSql = "";
                foreach (var son in item)
                {
                    SelectSql += "ROUND(NVL(AVG(POINT" + son.POINT_ID + "),0),2) as POINT" + son.POINT_ID + ",";
                }
                if (strGetMaxTime == "")
                {
                    strGetMaxTime = "select max(timestamp) from RMWD" + item.Key + "_HIST";
                    strGetMinTime = "select min(timestamp) from RMWD" + item.Key + "_HIST";
                }
                SelectSql = SelectSql.Remove(SelectSql.Length - 1, 1);
                sqlDic.Add(item.Key, SelectSql);
            }
        }

        public override void RunTask(DateTime currentTime)
        {
            DateTime TimeMax;
            DateTime TimeMin;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                TimeMax = idatabase.GetDateTime(strGetMaxTime);
                TimeMin = idatabase.GetDateTime(strGetMinTime);
            }
            if (TimeMax != DateTime.MinValue)
            {
                GetData(TimeMin, TimeMax);
            }
        }
        public void GetData(DateTime starttime, DateTime endtime)
        {
            var GroupByUpdatetime = op_point_config_List.GroupBy(k => k.UPDATETIME);
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                foreach (var GroupByUpdateItem in GroupByUpdatetime)
                {
                    var temp = GroupByUpdateItem.GroupBy(k => k.SEG_ID);
                    DateTime starttimetemp = starttime;
                    DateTime key = GroupByUpdateItem.Key.ToDate();
                    if (GroupByUpdateItem.Key != null && key > starttimetemp)
                        starttimetemp = key;
                    ///获取热面温度
                    ///
                    foreach (var item in sqlDic)
                    {
                        String SQL = String.Format("SELECT {0} FROM RMWD{1}_HIST where timestamp between to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') and to_date('{3}', 'yyyy-mm-dd hh24:mi:ss')", item.Value, item.Key, starttimetemp.ToString(), endtime.ToString());
                        string insertsql = "insert into RMWD{0}_AVG({1}) values({2})";
                        string Fields = "";
                        string Values = "";
                        DataTable dt = idatabase.GetDataTable(SQL);
                        try
                        {
                            var ocl = temp.First(k => k.Key == item.Key);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (var son in ocl)
                                {
                                    Fields += "POINT" + son.POINT_ID + ",";
                                    Values += Math.Round(dt.Rows[0]["POINT" + son.POINT_ID].ToDouble(), 1) + ",";
                                }
                            }
                            Fields = Fields.Remove(Fields.Length - 1, 1);
                            Values = Values.Remove(Values.Length - 1, 1);
                            idatabase.ExecuteCommand(string.Format("Delete RMWD{0}_AVG", item.Key));
                            idatabase.ExecuteCommand(string.Format(insertsql, item.Key, Fields, Values));
                        }
                        catch (Exception ee)
                        {
                            RunTaskException(DateTime.Now, ee);
                        }
                    }
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
    }
}
