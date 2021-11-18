using BFES.DataAccess;
using BKYL.Jobs.Base.Models;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Plugin
{
    class JudgeAlarm : BaseJob
    {
        public string log = "";
        List<CONFIG_ALARM> configAlarmArray = new List<CONFIG_ALARM>();
        List<ALARM_INFO> alarmInfoArray = new List<ALARM_INFO>();
        DateTime maxTimeOfConfigAlarm = new DateTime();
        Dictionary<string, DateTime> DicTableName_MaxTime = new Dictionary<string, DateTime>();
        Dictionary<string, string> DicTableName_SelectSql = new Dictionary<string, string>();
        public override void InitTask()
        {
            try
            {
                RefreshConfig();

            }
            catch (Exception ee)
            {
                throw ee;
            }

        }

        public override void RunTask(DateTime currentTime)
        {
            try
            {
                Outputlog("JudgeAlarm start");
                alarmInfoArray.Clear();

                string[] columnNames = { "TIMESTAMP", "ALARM_ID", "ALARM_DESC", "ALARM_TYPE", "ALARM_TYPE_DESC", "REMARK", "ALARM_LEVEL","ALARM_TIMESTAMP","ALARM_VALUE" };
                string[] dataFiledNameArr = { "TIMESTAMP", "ALARM_ID", "ALARM_DESC", "ALARM_TYPE", "ALARM_TYPE_DESC", "REMARK", "ALARM_LEVEL", "ALARM_TIMESTAMP", "ALARM_VALUE" };
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        if (maxTimeOfConfigAlarm != iDataBase.GetDateTime("select max(timestamp) from CONFIG_ALARM"))
                        {
                            RefreshConfig();
                        }
                        List<CONFIG_ALARM> _ConfigAlarmArray = (from n in configAlarmArray where ConvertDataTimeLong(currentTime) % n.FRE == 0 select n).ToList();//符合采集时间的所有记录
                        var configAlarmGroups = (_ConfigAlarmArray.GroupBy(m => m.TABLE_NAME)).ToList();//按表名称分组
                        foreach (var Groups in configAlarmGroups)
                        {
                            List<CONFIG_ALARM> OneGroup = Groups.ToList<CONFIG_ALARM>();
                            //获取数据
                            DataTable dt = iDataBase.GetDataTable(DicTableName_SelectSql[OneGroup[0].TABLE_NAME]);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow OneRow = dt.Rows[0];
                                if (DicTableName_MaxTime[OneGroup[0].TABLE_NAME] != OneRow[$"{OneGroup[0].TIME_FIELD}"].ToDate())
                                {
                                    foreach (CONFIG_ALARM item in OneGroup)
                                    {
                                        if (item.ISENABLE)
                                        {
                                            decimal realVal = OneRow[item.COLUMN_NAME].ToDecimal();
                                            decimal redVal_L = item.ALARM_RED_L;
                                            decimal yellowVal_L = item.ALARM_YELLOW_L;
                                            decimal yellowVal_H = item.ALARM_YELLOW_H;
                                            decimal redVal_H = item.ALARM_RED_H;
                                            decimal upLimit = item.UP_LIMIT;
                                            decimal downLimit = item.DOWN_LIMIT;
                                            ALARM_INFO alarmInfo = new ALARM_INFO();
                                            alarmInfo.TIMESTAMP = currentTime;
                                            alarmInfo.ALARM_ID = item.ALARM_ID;
                                            alarmInfo.ALARM_DESC = item.ALARM_DESC;
                                            alarmInfo.ALARM_TYPE = item.ALARM_TYPE;
                                            alarmInfo.ALARM_TYPE_DESC = item.ALARM_TYPE_DESC;
                                            alarmInfo.REMARK = string.Format($"低值报警值:{item.ALARM_RED_L}，低值预警值:{item.ALARM_YELLOW_L};高值预警值:{item.ALARM_YELLOW_H},高值报警值:{item.ALARM_RED_H}");
                                            alarmInfo.ALARM_TIMESTAMP = OneRow[$"{OneGroup[0].TIME_FIELD}"].ToDate();
                                            alarmInfo.ALARM_VALUE = realVal;
                                           
                                            if ((realVal <= upLimit && realVal >= downLimit) || (upLimit == downLimit ))
                                            {
                                                if (realVal > redVal_H && redVal_H != yellowVal_H)
                                                {
                                                    //高值红色报警
                                                    alarmInfo.ALARM_LEVEL = 4;
                                                    //alarmInfo.ALARM_DESC = item.ALARM_DESC+ "高值红色报警";
                                                    alarmInfoArray.Add(alarmInfo);

                                                }
                                                else if (realVal > yellowVal_H && redVal_H != yellowVal_H)
                                                {
                                                    //高值黄色报警
                                                    alarmInfo.ALARM_LEVEL = 3;
                                                    //alarmInfo.ALARM_DESC = item.ALARM_DESC + "高值黄色报警";
                                                    alarmInfoArray.Add(alarmInfo);
                                                }
                                                else if (realVal < redVal_L && redVal_L != yellowVal_L)
                                                {
                                                    //低值红色报警
                                                    alarmInfo.ALARM_LEVEL = 1;
                                                    //alarmInfo.ALARM_DESC = item.ALARM_DESC + "低值红色报警";
                                                    alarmInfoArray.Add(alarmInfo);
                                                }
                                                else if (realVal < yellowVal_L && redVal_L != yellowVal_L)
                                                {
                                                    //低值黄色报警
                                                    alarmInfo.ALARM_LEVEL = 2;
                                                    //alarmInfo.ALARM_DESC = item.ALARM_DESC + "低值黄色报警";
                                                    alarmInfoArray.Add(alarmInfo);
                                                }

                                            }
                                            DicTableName_MaxTime[OneGroup[0].TABLE_NAME] = dt.Rows[0][$"{OneGroup[0].TIME_FIELD}"].ToDate();
                                        }

                                    }
                                }
                            }
                        }
                        if (alarmInfoArray.Count > 0)
                        {
                            iDataBase.BeginTran();
                            iDataBase.BulkInsert<ALARM_INFO>("ALARM_INFO", columnNames, dataFiledNameArr, alarmInfoArray);
                            iDataBase.CommitTran();

                        }
                    }
                    catch (Exception ee)
                    {
                        throw ee;
                    }
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {

                Outputlog("JudgeAlarm finish");
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }

        public long ConvertDataTimeLong(DateTime dt)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 8, 0, 0)); // 当地时区
            return (long)(dt - startTime).TotalSeconds; // 相差秒数

        }


        public void RefreshConfig()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    Outputlog("JudgeAlarm：开始更新参数...");

                    DicTableName_SelectSql.Clear();
                    DicTableName_MaxTime.Clear();
                    configAlarmArray = iDataBase.GetList<CONFIG_ALARM>("select * from CONFIG_ALARM");
                    maxTimeOfConfigAlarm = iDataBase.GetDateTime("select max(timestamp) from CONFIG_ALARM");


                    var configAlarmGroups = (configAlarmArray.GroupBy(m => m.TABLE_NAME)).ToList();//按表名称分组
                    foreach (var GroupByTableNames in configAlarmGroups)
                    {
                        List<CONFIG_ALARM> OneGroupByTableName = GroupByTableNames.ToList<CONFIG_ALARM>();
                        string strTimeField = "timestamp";
                        if (OneGroupByTableName.Count > 0 && !string.IsNullOrEmpty(OneGroupByTableName[0].TIME_FIELD) )
                        {
                            strTimeField = OneGroupByTableName[0].TIME_FIELD;
                        }
                        string strSql = "select "+ strTimeField+",";
                        foreach (var item in OneGroupByTableName)
                        {
                            if (item.ISENABLE)
                            {
                                strSql += string.Format($"{item.COLUMN_NAME},");
                            }
                        }
                        strSql = strSql.Substring(0, strSql.LastIndexOf(","));
                        strSql += string.Format($" from {OneGroupByTableName[0].TABLE_NAME} where " +
                            $"{strTimeField} = (select  max({strTimeField}) from {OneGroupByTableName[0].TABLE_NAME})");
                        DicTableName_SelectSql.Add(OneGroupByTableName[0].TABLE_NAME, strSql);

                        strSql = string.Format($"select max(alarm_timestamp) from alarm_info");
                        DateTime dt =  iDataBase.GetDateTime(strSql);
                        if (!DicTableName_MaxTime.ContainsKey(OneGroupByTableName[0].TABLE_NAME))
                        {
                            DicTableName_MaxTime.Add(OneGroupByTableName[0].TABLE_NAME, dt);
                        }
                    }
                    Outputlog("JudgeAlarm：更新参数完成！");
                }
                catch (Exception ee)
                {
                    throw ee;
                }

            }
        }
    }
}


