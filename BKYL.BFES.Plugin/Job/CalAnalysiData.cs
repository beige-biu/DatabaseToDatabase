using BFES.DataAccess;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    class CalAnalysiData : BaseJob
    {
        string strErrorMsg;
        string fieldNames;
        DataTable dt_L3Analysi;
        DataTable Dt_L3Analysi
        {
            get
            {
                if (dt_L3Analysi == null)
                {
                    dt_L3Analysi = new DataTable();
                }
                return dt_L3Analysi;
            }
            set
            {
                dt_L3Analysi = value;
            }
        }
        DataTable dt_L3AnalysiModel;
        DataTable Dt_L3AnalysiModel
        {
            get
            {
                if (dt_L3AnalysiModel == null)
                {
                    dt_L3AnalysiModel = new DataTable();
                }
                return dt_L3AnalysiModel;
            }
            set
            {
                dt_L3AnalysiModel = value;
            }
        }
        DateTime lastDataDate;
        List<L3MATERIAL> L_L3Material;
        public override void InitTask()
        {
             OutPutMsg("通用检化验处理计算开始初始化", Color.Green);
            strErrorMsg = "";
            fieldNames = "";
            Dt_L3Analysi = null;
            Dt_L3AnalysiModel = null;
            Dt_L3Analysi.TableName = "L3ANALYSI";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                L_L3Material = idatabase.GetList<L3MATERIAL>("select * from L3MATERIAL");
                lastDataDate = idatabase.GetDateTime("select max(timestamp) from L3ANALYSI_MODEL");
                if (L_L3Material != null && L_L3Material.Count > 0)
                {
                    string strSql = "select  column_name,data_type from user_tab_columns where table_name = upper('L3ANALYSI')";
                    DataTable dtTemp = idatabase.GetDataTable(strSql);
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow oneRow in dtTemp.Rows)
                        {
                            string fieldName = oneRow["column_name"].ToString();
                            string strTypeDesc = oneRow["data_type"].ToString();
                            Type type;
                            if (strTypeDesc == "DATE")
                            {
                                type = typeof(DateTime);
                            }
                            else if (strTypeDesc == "NUMBER")
                            {
                                type = typeof(double);
                            }
                            else
                            {
                                type = typeof(string);
                            }
                            Dt_L3Analysi.Columns.Add(new DataColumn(fieldName, type));
                            fieldNames += fieldName + ",";
                        }
                        fieldNames = fieldNames.Substring(0, fieldNames.Length - 1);
                        Dt_L3AnalysiModel = Dt_L3Analysi.Clone();
                        Dt_L3AnalysiModel.TableName = "L3ANALYSI_MODEL";
                        strSql = "select  column_name,data_type from user_tab_columns where table_name = upper('L3ANALYSI_MODEL')";
                        dtTemp = idatabase.GetDataTable(strSql);
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            foreach (DataRow oneRow in dtTemp.Rows)
                            {
                                string fieldName = oneRow["column_name"].ToString();
                                string strTypeDesc = oneRow["data_type"].ToString();
                                Type type;
                                if (strTypeDesc == "DATE")
                                {
                                    type = typeof(DateTime);
                                }
                                else if (strTypeDesc == "NUMBER")
                                {
                                    type = typeof(double);
                                }
                                else
                                {
                                    type = typeof(string);
                                }
                                if (!Dt_L3AnalysiModel.Columns.Contains(fieldName) || !(Dt_L3AnalysiModel.Columns[fieldName].DataType == type))
                                {
                                    strErrorMsg = $"L3ANALYSI_MODEL表字段{fieldName}:[{type.ToString()}]未在L3ANALYSI表中找到匹配项";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            strErrorMsg = $"未查询到L3ANALYSI_MODEL表字段信息";
                        }
                    }
                    else
                    {
                        strErrorMsg = $"未查询到L3ANALYSI表字段信息";
                    }
                }
                else
                {
                    strErrorMsg = $"未查询到L3MATERIAL表数据";
                }
            }
            if (string.IsNullOrEmpty(strErrorMsg))
            {
                OutPutMsg("通用检化验处理计算初始化完成",Color.Green);
            }
            else
            {
                Outputlog(strErrorMsg);
            }
        }

        public override void RunTask(DateTime currentTime)
        {
            if (string.IsNullOrEmpty(strErrorMsg))
            {
                dt_L3AnalysiModel.Clear();
                using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                {
                    DataTable dtTemp = idatabase.GetDataTable($"select {fieldNames} from L3ANALYSI where timestamp > {UtilsConvert.GetOraString(lastDataDate)} order by timestamp");
                    if (dtTemp != null)
                    {
                        Outputlog($"检测到{dtTemp.Rows.Count}条新数据！上次数据时间：{lastDataDate},当前时间：{currentTime}");
                        if (dtTemp.Rows.Count > 0)
                        {
                            lastDataDate = dtTemp.Rows[dtTemp.Rows.Count - 1]["timestamp"].ToDate();
                            foreach (DataRow row in dtTemp.Rows)
                            {
                                DataRow modelRow = dt_L3AnalysiModel.NewRow();

                                L3MATERIAL l3Material = L_L3Material.Find(m => m.CODE == row["M_CODE"].ToInt());
                                if (l3Material != null)
                                {
                                    if (GetModelData(l3Material, row, dt_L3AnalysiModel.Columns, ref modelRow))
                                    {
                                        dt_L3AnalysiModel.Rows.Add(modelRow);
                                    }
                                }
                            }
                            insertDataTableDataToDB(dt_L3AnalysiModel);
                        }
                    }
                InitTask();
                }
            }
            else
            {
                InitTask();
            }
        }

        private bool GetModelData(L3MATERIAL l3Material, DataRow newRow, DataColumnCollection columns, ref DataRow modelRow)
        {
            int calAvgNum = l3Material.CALAVG_NUM <= 0 ? 1 : l3Material.CALAVG_NUM;
            int CalDelayTimes = l3Material.CAL_DELAYTIME;
            bool isGetNewData = l3Material.QUERY_TYPE;
            DateTime newDateTime = newRow["TIMESTAMP"].ToDate();
            string strDeviationWhereSql = getDeviationWhereSql(l3Material, newDateTime);
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                string strSelectSql = $"select * from (select {fieldNames} from  L3ANALYSI  where M_CODE = {l3Material.CODE} and " +
                                            $"TIMESTAMP <= {UtilsConvert.GetOraString(newDateTime.AddHours(-CalDelayTimes))} and " +
                                            $"{strDeviationWhereSql} order by timestamp desc) where  rownum<= {calAvgNum}";
                DataTable dataTable = idatabase.GetDataTable(strSelectSql);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < columns.Count; i++)
                    {
                        string fieldName = columns[i].ColumnName;

                        if (columns[i].DataType == typeof(double))
                        {
                            modelRow[$"{fieldName}"] = dataTable.Compute($"avg({fieldName})", "1=1");
                        }
                        else
                        {
                            modelRow[$"{fieldName}"] = newRow[$"{fieldName}"];
                        }
                    }
                    return true;
                }
                else if(l3Material.QUERY_TYPE)
                {
                    for (int i = 0; i < columns.Count; i++)
                    {
                        string fieldName = columns[i].ColumnName;
                        modelRow[$"{fieldName}"] = newRow[$"{fieldName}"];
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string getDeviationWhereSql(L3MATERIAL l3Material, DateTime newDateTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                string strDeviationWhereSql = " 1=1 ";

                if (l3Material.DEVIATION_STATE && newDateTime != null)
                {
                    //启用偏离过滤条件
                    int iRecordCountLimit = l3Material.MODELDATA_NUM;
                    double dblPerLimit = l3Material.DEVIATION_PER;
                    l3Material.FIELDNAMES = l3Material.FIELDNAMES.Replace('，', ',');
                    l3Material.FIELDNAMES = l3Material.FIELDNAMES.Replace(" ", "");
                    string[] devFieldNames = l3Material.FIELDNAMES.Split(',');
                    if (devFieldNames.Count() > 0 && iRecordCountLimit > 0 && dblPerLimit > 0)
                    {
                        string strFieldNames = "";
                        String strname = "";
                        foreach (var fieldName in devFieldNames)
                        {
                            strFieldNames += $"Round(AVG({fieldName}),5) as {fieldName},";
                        }
                        foreach (var fieldName in devFieldNames)
                        {
                            strname += $"{fieldName},";
                        }
                        strFieldNames = strFieldNames.Substring(0, strFieldNames.Length - 1);
                        strname = strname.Substring(0, strname.Length - 1);
                        string strSql = $"select {strFieldNames} from （select {strname} from L3ANALYSI_MODEL where M_CODE = {l3Material.CODE} " +
                            $"and timestamp <= {UtilsConvert.GetOraString(newDateTime)} order by timestamp desc） where rownum<= {iRecordCountLimit}";
                        //string strSql = $"select {strFieldNames} from L3ANALYSI_MODEL where M_CODE = {l3Material.CODE} and rownum<= {iRecordCountLimit} " +
                        //    $"and timestamp <= {UtilsConvert.GetOraString(newDateTime)} order by timestamp desc";
                        DataTable deviationDataTable = idatabase.GetDataTable(strSql);
                        if (deviationDataTable.Rows.Count > 0)
                        {
                            foreach (var fieldName in devFieldNames)
                            {
                                double avgVal = deviationDataTable.Rows[0][fieldName].ToDouble();
                                double downLimit = avgVal * (1 - dblPerLimit / 100);
                                double upLimit = avgVal * (1 + dblPerLimit / 100);
                                if (downLimit > upLimit)
                                {
                                    downLimit += upLimit;
                                    upLimit = downLimit - upLimit;
                                    downLimit = downLimit - upLimit;
                                }
                                strDeviationWhereSql += $"and {fieldName} between {downLimit}  and {upLimit} ";
                            }
                        }
                    }
                }
                return strDeviationWhereSql;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        public void OutPutMsg(string strMsg,Color color)
        {
            GlobalObject.RichTextLog.AppendTextByAsync(strMsg, color);
        }
        public bool insertDataTableDataToDB(DataTable dataTable)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                try
                {
                    if (dataTable != null && dataTable.Rows.Count > 0 && !string.IsNullOrEmpty(dataTable.TableName))
                    {
                        string[] strFieldNames = new string[dataTable.Columns.Count];
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            strFieldNames[i] = dataTable.Columns[i].ColumnName;
                        }
                        idatabase.BeginTran();

                        idatabase.BulkInsert(dataTable.TableName, strFieldNames, strFieldNames, dataTable);

                        idatabase.CommitTran();
                        Outputlog($"成功插入{dataTable.Rows.Count}条数据！");
                    }
                }
                catch (Exception ex)
                {
                    idatabase.RollbackTran();
                    throw;
                }
            }
            return true;
        }
    }
}