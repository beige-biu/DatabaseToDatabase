using BFES.DataAccess;
using BFES.Models;
using BFES.Utils;
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
    class CalTenCross07DAVG : BaseJob
    {
        public List<CONFIG_TENCROSS_CAL07D> L_ConfigTenCrossCal07D = null;
        Dictionary<string, double> dic_SourceFieldName_Val = null;
        Dictionary<string, double> dic_TargetFieldName_Val = null;
        string selectSql;
        string insertSql;
        public override void InitTask()
        {
            try
            {
                selectSql = "";
                insertSql = "";
                L_ConfigTenCrossCal07D = new List<CONFIG_TENCROSS_CAL07D>();
                dic_SourceFieldName_Val = new Dictionary<string, double>();
                dic_TargetFieldName_Val = new Dictionary<string, double>();
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    L_ConfigTenCrossCal07D = iDataBase.GetList<CONFIG_TENCROSS_CAL07D>("select * from CONFIG_TENCROSS_CAL07D");
                    if (L_ConfigTenCrossCal07D.Count <= 0)
                    {
                        throw new Exception("CONFIG_TENCROSS_CAL07D表数据为空！");
                    }
                    else
                    {
                        selectSql = "select timestamp";
                        //list.OrderByDescending(i => i.a).ThenByDescending(i => i.b);
                        L_ConfigTenCrossCal07D = L_ConfigTenCrossCal07D.OrderBy(m => m.TARGET_FIELDNAME).ThenBy(m => m.SOURCE_FIELDNAME).ToList();
                        foreach (CONFIG_TENCROSS_CAL07D item in L_ConfigTenCrossCal07D)
                        {
                            item.SOURCE_FIELDNAME = item.SOURCE_FIELDNAME.ToUpper();
                            item.TARGET_FIELDNAME = item.TARGET_FIELDNAME.ToUpper();
                            double dblVal = double.MinValue;
                            if (dic_SourceFieldName_Val.TryGetValue(item.SOURCE_FIELDNAME, out dblVal))
                            {
                                continue;
                            }
                            dic_SourceFieldName_Val.Add(item.SOURCE_FIELDNAME, double.MinValue);
                            selectSql += ",";
                            selectSql += item.SOURCE_FIELDNAME;

                        }
                        selectSql += " from AV05S_MONITOR_01H where  timestamp > <%START_TIME%> and timestamp <= <%END_TIME%>";
                        insertSql = "insert into AV01H_TENCROSS_07DAVG (timestamp";
                        string strValFormat = "<%TIMESTAMP%>";
                        int iIndex = 1;
                        foreach (var item in L_ConfigTenCrossCal07D.GroupBy(m => m.TARGET_FIELDNAME))
                        {
                            dic_TargetFieldName_Val.Add(item.Key, double.MinValue);
                            insertSql += ",";
                            insertSql += item.Key;
                            strValFormat += ",";
                            strValFormat += string.Format("<%{0}_VAL%>", item.Key);
                        }
                        insertSql += ") values (" + strValFormat + ")";


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public override void RunTask(DateTime currentTime)
        {
            currentTime = DateTime.Now;
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    DateTime MaxTime = iDataBase.GetDateTime($"select max(timestamp) from AV01H_TENCROSS_07DAVG where " +
                        $"timestamp < {DataBaseFactory.ConvertTimeString(currentTime, S_DBSource.DBType)}");
                    if (MaxTime == DateTime.MinValue)
                    {
                        MaxTime = currentTime.AddHours(-1);
                    }
                    for (DateTime calTime = MaxTime.AddHours(1); calTime <= currentTime; calTime = calTime.AddHours(1))
                    {
                        Calculate(calTime);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }

        public void Calculate(DateTime CalTime)
        {

            bool CenterIsUsed = true;
            double XiShu = 0;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                DateTime AV05S_MONITOR_ENLABLETime;
                if (iDataBase.GetDouble("select A_CROSS1_0 from AV05S_MONITOR_ENLABLE") == 0)
                {
                    CenterIsUsed = false;
                    XiShu = iDataBase.GetDouble("select C_SMZ_T_MAX_MOD from bfpar");
                }

                AV05S_MONITOR_ENLABLETime = iDataBase.GetDateTime("select timestamp from AV05S_MONITOR_ENLABLE");

                DateTime startTime = CalTime.AddDays(-1);
                if (CenterIsUsed)
                {
                    if (CalTime.AddDays(-1) < AV05S_MONITOR_ENLABLETime && AV05S_MONITOR_ENLABLETime < CalTime)
                    {
                        startTime = AV05S_MONITOR_ENLABLETime;
                    }
                }

                string strSql = selectSql.Replace("<%START_TIME%>", DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)).
                    Replace("<%END_TIME%>", DataBaseFactory.ConvertTimeString(CalTime, S_DBSource.DBType));
                string strInsertSql = insertSql.Replace("<%TIMESTAMP%>", DataBaseFactory.ConvertTimeString(CalTime, S_DBSource.DBType));
                DataTable sourceDataDT = iDataBase.GetDataTable(strSql);
                if (sourceDataDT.Rows.Count <= 0)
                    return;
                foreach (var targetFieldName_Val in dic_TargetFieldName_Val)
                {
                    //如果是中心点并且中心点坏了则本次不计算
                    if (targetFieldName_Val.Key == "A_CROSS0" && !CenterIsUsed)
                    {
                        continue;
                    }
                    List<CONFIG_TENCROSS_CAL07D> L_ConfigTemp = L_ConfigTenCrossCal07D.Where(m => m.TARGET_FIELDNAME == targetFieldName_Val.Key).ToList();
                    int iAvailableCount = 0;
                    double sumVal = 0;
                    foreach (var configTemp in L_ConfigTemp)
                    {

                        foreach (DataRow item in sourceDataDT.Rows)
                        {
                            double dblVal = double.Parse(item[configTemp.SOURCE_FIELDNAME].ToString());
                            if (dblVal >= configTemp.SOURCE_DATE_MIN && dblVal <= configTemp.SOURCE_DATE_MAX)
                            {
                                iAvailableCount++;
                                sumVal += dblVal;
                            }
                        }
                    }
                    //如果是计算次中心，并且中心点坏了，则用次中心乘以系数代表中心点
                    if (targetFieldName_Val.Key == "A_CROSS1" && !CenterIsUsed)
                    {
                        strInsertSql = strInsertSql.Replace(string.Format("<%{0}_VAL%>", "A_CROSS0"), string.Format("{0:F2}", iAvailableCount == 0 ? 0 : sumVal / iAvailableCount * XiShu));
                    }

                    //dic_TargetFieldName_Val[targetFieldName_Val.Key] = iAvailableCount == 0 ? 0 : sumVal / iAvailableCount;
                    strInsertSql = strInsertSql.Replace(string.Format("<%{0}_VAL%>", targetFieldName_Val.Key), string.Format("{0:F2}", iAvailableCount == 0 ? 0 : sumVal / iAvailableCount));

                }

                try
                {
                    iDataBase.BeginTran();
                    iDataBase.ExecuteCommand(strInsertSql);
                    iDataBase.CommitTran();
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    throw ex;
                }
            }
        }
    }
}
