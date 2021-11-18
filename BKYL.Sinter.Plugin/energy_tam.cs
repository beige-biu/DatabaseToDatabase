
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BFES.DataAccess;

using System.Data;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;

namespace BKYL.Sinter.Plugin
{
    /// <summary>
    /// 读取能源消耗单耗
    /// </summary>
    public class energy_tam : BaseJob
    {
        public override void InitTask()
        {
            //加载配置 T_ENERGY_TEAM_CONSUMPTION
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
          
            try
            {
                //源数据库 表
                List<T_ENERGY_TEAM_CONSUMPTION> T_ENERGY_TEAM_CONSUMPTION_ = new List<T_ENERGY_TEAM_CONSUMPTION>();
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    int M_hour = 0;
                    int M_min = 0;
                   
                    DateTime endTime = DateTime.MinValue;
                    DateTime startTime = DateTime.MinValue;
                    M_hour = currentTime.Hour;
                    M_min= currentTime.Minute;
                    // 数据库查询
                    if (M_hour == 8 && M_min == 30)
                    {
                        endTime = currentTime;
                        startTime = currentTime.AddDays(-7);
                        T_ENERGY_TEAM_CONSUMPTION_ = GET_T_ENERGY_TEAM_CONSUMPTION(startTime,endTime,iDataBase);
                    }
                    else if (M_hour == 20 && M_min == 30)
                    {
                        endTime = currentTime;
                        startTime = currentTime.AddDays(-7);
                        T_ENERGY_TEAM_CONSUMPTION_ = GET_T_ENERGY_TEAM_CONSUMPTION(startTime, endTime, iDataBase);
                    }
                }

                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    // 数据库查询
                    if (T_ENERGY_TEAM_CONSUMPTION_.Count>0)
                    {
                        for (int i = 0; i < T_ENERGY_TEAM_CONSUMPTION_.Count; i++)
                        {
                            int M_count = GET_SELEET_T_ENERGY_TEAM_CONSUMPTION(T_ENERGY_TEAM_CONSUMPTION_[i].ID,iDataBase);
                            if (M_count == 0)
                            {
                                T_ENERGY_TEAM_CONSUMPTION T_ENERGY_TEAM_CONSUMPTION_MOD_ = null;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_ = new T_ENERGY_TEAM_CONSUMPTION();

                                T_ENERGY_TEAM_CONSUMPTION_MOD_.RECORD_DATE = T_ENERGY_TEAM_CONSUMPTION_[i].RECORD_DATE;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.SHIFTS = T_ENERGY_TEAM_CONSUMPTION_[i].SHIFTS;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.TAEM = T_ENERGY_TEAM_CONSUMPTION_[i].TAEM;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.WORK_BEGIN_TIME = T_ENERGY_TEAM_CONSUMPTION_[i].WORK_BEGIN_TIME;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.WORK_END_TIME = T_ENERGY_TEAM_CONSUMPTION_[i].WORK_END_TIME;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.MATERIAL_CODE = T_ENERGY_TEAM_CONSUMPTION_[i].MATERIAL_CODE;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.MATERIAL_NAME = T_ENERGY_TEAM_CONSUMPTION_[i].MATERIAL_NAME;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.CONSUMPTION_SUM = T_ENERGY_TEAM_CONSUMPTION_[i].CONSUMPTION_SUM;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.SPECIFIC_CONSUMPTION = T_ENERGY_TEAM_CONSUMPTION_[i].SPECIFIC_CONSUMPTION;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.ID = T_ENERGY_TEAM_CONSUMPTION_[i].ID;
                              if( Set_Admin_add(T_ENERGY_TEAM_CONSUMPTION_MOD_))
                                {
                                    log = string.Format("保存能源消耗完成！");
                                    Outputlog(log);
                                }
                              else
                                {
                                    log = string.Format("保存能源消耗失败！");
                                    Outputlog(log);
                                }
                            }
                            else if (M_count == 1)
                            {
                                T_ENERGY_TEAM_CONSUMPTION T_ENERGY_TEAM_CONSUMPTION_MOD_ = null;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_ = new T_ENERGY_TEAM_CONSUMPTION();

                                T_ENERGY_TEAM_CONSUMPTION_MOD_.RECORD_DATE = T_ENERGY_TEAM_CONSUMPTION_[i].RECORD_DATE;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.SHIFTS = T_ENERGY_TEAM_CONSUMPTION_[i].SHIFTS;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.TAEM = T_ENERGY_TEAM_CONSUMPTION_[i].TAEM;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.WORK_BEGIN_TIME = T_ENERGY_TEAM_CONSUMPTION_[i].WORK_BEGIN_TIME;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.WORK_END_TIME = T_ENERGY_TEAM_CONSUMPTION_[i].WORK_END_TIME;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.MATERIAL_CODE = T_ENERGY_TEAM_CONSUMPTION_[i].MATERIAL_CODE;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.MATERIAL_NAME = T_ENERGY_TEAM_CONSUMPTION_[i].MATERIAL_NAME;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.CONSUMPTION_SUM = T_ENERGY_TEAM_CONSUMPTION_[i].CONSUMPTION_SUM;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.SPECIFIC_CONSUMPTION = T_ENERGY_TEAM_CONSUMPTION_[i].SPECIFIC_CONSUMPTION;
                                T_ENERGY_TEAM_CONSUMPTION_MOD_.ID = T_ENERGY_TEAM_CONSUMPTION_[i].ID;
                                if(Set_update(T_ENERGY_TEAM_CONSUMPTION_MOD_))
                                {
                                    log = string.Format("更新能源消耗完成！");
                                    Outputlog(log);
                                }
                                else
                                {
                                    log = string.Format("更新能源消耗失败！");
                                    Outputlog(log);
                                }
                            }
                        }
                    }
                }

                Outputlog(log);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                //释放资源
                // log = null;
            }
        }

        public List<T_ENERGY_TEAM_CONSUMPTION> GET_T_ENERGY_TEAM_CONSUMPTION(DateTime startTime, DateTime endTime, IDataBase iDataBase)
        {
             string sql =string.Format("select *  from T_ENERGY_TEAM_CONSUMPTION   where WORK_BEGIN_TIME >  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')  and  WORK_BEGIN_TIME <= to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')");
             return iDataBase.GetList<T_ENERGY_TEAM_CONSUMPTION>(sql);
        }
        
        public int GET_SELEET_T_ENERGY_TEAM_CONSUMPTION(int id, IDataBase iDataBase)
        {
            string sql = string.Format("select count(*)  from T_ENERGY_TEAM_CONSUMPTION   where id =  '" + id + "'");
            return iDataBase.GetInt(sql);
        }
        public bool Set_Admin_add(T_ENERGY_TEAM_CONSUMPTION model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }

        public bool Set_update(T_ENERGY_TEAM_CONSUMPTION T_ENERGY_TEAM_CONSUMPTION_MOD)
        {
            string log = "";
            bool listmodel = false;
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    iDataBase.BeginTran();
                    string sql = "";
                    sql = string.Format(@"update T_ENERGY_TEAM_CONSUMPTION set SHIFTS = '" + T_ENERGY_TEAM_CONSUMPTION_MOD.SHIFTS + "'"
                    + ",TAEM= '" + T_ENERGY_TEAM_CONSUMPTION_MOD.TAEM + "' "
                    + ",MATERIAL_CODE= '" + T_ENERGY_TEAM_CONSUMPTION_MOD.MATERIAL_CODE + "'"
                    + ",MATERIAL_NAME= '" + T_ENERGY_TEAM_CONSUMPTION_MOD.MATERIAL_NAME + "'"
                    + ",CONSUMPTION_SUM= '" + T_ENERGY_TEAM_CONSUMPTION_MOD.CONSUMPTION_SUM + "'"
                    + ",SPECIFIC_CONSUMPTION= '" + T_ENERGY_TEAM_CONSUMPTION_MOD.SPECIFIC_CONSUMPTION + "' where ID = '"+ T_ENERGY_TEAM_CONSUMPTION_MOD.ID + "'");
                    iDataBase.ExecuteCommand(string.Format(sql));
                    iDataBase.CommitTran();
                    log = string.Format("更新原料消耗完成！");
                    Outputlog(log);
                    listmodel = true;
                    return listmodel;
                }
            }
            catch
            {
                return listmodel;
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
