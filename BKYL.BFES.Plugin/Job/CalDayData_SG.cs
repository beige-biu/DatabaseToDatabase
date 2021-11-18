using BFES.DataAccess;
using BFES.Models;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base.Utils;
using BKYL.Jobs.Base;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKYL.BFES.Plugin
{
    //山钢高炉炉况检查数据计算JOB
    public class CalDayData_SG : BaseJob
    {
        List<CONFIG_BFANADATA> ConfigListModel = null;
        string log = "";

        Dictionary<string, string> dicName = new Dictionary<string, string>();

        public override void InitTask()
        {
            #region TC0087
            dicName.Add("TIMESTAMP", "TIMESTAMP");
            dicName.Add("IR_CHE_OP_AC_DT", "IR_CHE_OP_AC_DT");
            dicName.Add("SNDR_INFORM_EDIT_DATE", "SNDR_INFORM_EDIT_DATE");
            dicName.Add("GAS_NUMBER1", "GAS_NUMBER1");
            dicName.Add("GAS_NUMBER2", "GAS_NUMBER2");
            dicName.Add("GAS_NUMBER3", "GAS_NUMBER3");
            dicName.Add("GAS_NUMBER4", "GAS_NUMBER4");
            dicName.Add("GAS_NUMBER5", "GAS_NUMBER5");
            dicName.Add("GAS_NUMBER6", "GAS_NUMBER6");
            dicName.Add("GAS_NUMBER7", "GAS_NUMBER7");
            dicName.Add("GAS_NUMBER8", "GAS_NUMBER8");
            dicName.Add("GAS_NUMBER9", "GAS_NUMBER9");
            dicName.Add("GAS_NUMBER10", "GAS_NUMBER10");
            dicName.Add("GAS_NUMBER11", "GAS_NUMBER11");
            dicName.Add("GAS_NUMBER12", "GAS_NUMBER12");
            dicName.Add("GAS_NUMBER13", "GAS_NUMBER13");
            dicName.Add("GAS_NUMBER14", "GAS_NUMBER14");
            dicName.Add("GAS_NUMBER15", "GAS_NUMBER15");
            dicName.Add("GAS_NUMBER16", "GAS_NUMBER16");
            dicName.Add("GAS_NUMBER17", "GAS_NUMBER17");
            dicName.Add("GAS_NUMBER18", "GAS_NUMBER18");
            dicName.Add("GAS_NUMBER19", "GAS_NUMBER19");
            dicName.Add("GAS_NUMBER20", "GAS_NUMBER20");
            dicName.Add("GAS_NUMBER21", "GAS_NUMBER21");
            dicName.Add("GAS_NUMBER22", "GAS_NUMBER22");
            dicName.Add("GAS_NUMBER23", "GAS_NUMBER23");
            dicName.Add("GAS_NUMBER24", "GAS_NUMBER24");
            dicName.Add("GAS_NUMBER25", "GAS_NUMBER25");
            dicName.Add("GAS_NUMBER26", "GAS_NUMBER26");
            dicName.Add("GAS_NUMBER27", "GAS_NUMBER27");
            dicName.Add("GAS_NUMBER28", "GAS_NUMBER28");
            dicName.Add("GAS_NUMBER29", "GAS_NUMBER29");
            dicName.Add("GAS_NUMBER30", "GAS_NUMBER30");
            dicName.Add("GAS_NUMBER31", "GAS_NUMBER31");
            dicName.Add("GAS_NUMBER32", "GAS_NUMBER32");
            dicName.Add("GAS_NUMBER33", "GAS_NUMBER33");
            dicName.Add("GAS_NUMBER34", "GAS_NUMBER34");
            dicName.Add("GAS_NUMBER35", "GAS_NUMBER35");
            dicName.Add("GAS_NUMBER36", "GAS_NUMBER36");
            dicName.Add("GAS_NUMBER37", "GAS_NUMBER37");
            dicName.Add("GAS_NUMBER38", "GAS_NUMBER38");
            dicName.Add("GAS_NUMBER39", "GAS_NUMBER39");
            dicName.Add("GAS_NUMBER40", "GAS_NUMBER40");
            dicName.Add("GAS_NUMBER41", "GAS_NUMBER41");
            dicName.Add("GAS_NUMBER42", "GAS_NUMBER42");
            dicName.Add("GAS_NUMBER43", "GAS_NUMBER43");
            dicName.Add("GAS_NUMBER44", "GAS_NUMBER44");
            dicName.Add("GAS_NUMBER45", "GAS_NUMBER45");
            dicName.Add("GAS_NUMBER46", "GAS_NUMBER46");
            dicName.Add("GAS_NUMBER47", "GAS_NUMBER47");
            dicName.Add("GAS_NUMBER48", "GAS_NUMBER48");
            dicName.Add("GAS_NUMBER49", "GAS_NUMBER49");
            dicName.Add("GAS_NUMBER50", "GAS_NUMBER50");
            dicName.Add("GAS_NUMBER51", "GAS_NUMBER51");
            dicName.Add("GAS_NUMBER52", "GAS_NUMBER52");
            dicName.Add("GAS_NUMBER53", "GAS_NUMBER53");
            dicName.Add("GAS_NUMBER54", "GAS_NUMBER54");
            dicName.Add("GAS_NUMBER55", "GAS_NUMBER55");
            dicName.Add("GAS_NUMBER56", "GAS_NUMBER56");
            dicName.Add("GAS_NUMBER57", "GAS_NUMBER57");
            dicName.Add("GAS_NUMBER58", "GAS_NUMBER58");
            dicName.Add("GAS_NUMBER59", "GAS_NUMBER59");
            dicName.Add("GAS_NUMBER60", "GAS_NUMBER60");
            dicName.Add("GAS_NUMBER61", "GAS_NUMBER61");
            dicName.Add("GAS_NUMBER62", "GAS_NUMBER62");
            dicName.Add("GAS_NUMBER63", "GAS_NUMBER63");
            dicName.Add("GAS_NUMBER64", "GAS_NUMBER64");
            dicName.Add("GAS_NUMBER65", "GAS_NUMBER65");
            dicName.Add("GAS_NUMBER66", "GAS_NUMBER66");
            dicName.Add("GAS_NUMBER67", "GAS_NUMBER67");
            dicName.Add("GAS_NUMBER68", "GAS_NUMBER68");
            dicName.Add("GAS_NUMBER69", "GAS_NUMBER69");
            dicName.Add("GAS_NUMBER70", "GAS_NUMBER70");

            #endregion
        }

        public override void RunTask(DateTime currentTime)
        {
            #region 字段名
            string[] ColumnArry = { "TIMESTAMP",
                                    "IR_CHE_OP_AC_DT",
                                    "SNDR_INFORM_EDIT_DATE",
                                    "GAS_NUMBER1",
                                    "GAS_NUMBER2",
                                    "GAS_NUMBER3",
                                    "GAS_NUMBER4",
                                    "GAS_NUMBER5",
                                    "GAS_NUMBER6",
                                    "GAS_NUMBER7",
                                    "GAS_NUMBER8",
                                    "GAS_NUMBER9",
                                    "GAS_NUMBER10",
                                    "GAS_NUMBER11",
                                    "GAS_NUMBER12",
                                    "GAS_NUMBER13",
                                    "GAS_NUMBER14",
                                    "GAS_NUMBER15",
                                    "GAS_NUMBER16",
                                    "GAS_NUMBER17",
                                    "GAS_NUMBER18",
                                    "GAS_NUMBER19",
                                    "GAS_NUMBER20",
                                    "GAS_NUMBER21",
                                    "GAS_NUMBER22",
                                    "GAS_NUMBER23",
                                    "GAS_NUMBER24",
                                    "GAS_NUMBER25",
                                    "GAS_NUMBER26",
                                    "GAS_NUMBER27",
                                    "GAS_NUMBER28",
                                    "GAS_NUMBER29",
                                    "GAS_NUMBER30",
                                    "GAS_NUMBER31",
                                    "GAS_NUMBER32",
                                    "GAS_NUMBER33",
                                    "GAS_NUMBER34",
                                    "GAS_NUMBER35",
                                    "GAS_NUMBER36",
                                    "GAS_NUMBER37",
                                    "GAS_NUMBER38",
                                    "GAS_NUMBER39",
                                    "GAS_NUMBER40",
                                    "GAS_NUMBER41",
                                    "GAS_NUMBER42",
                                    "GAS_NUMBER43",
                                    "GAS_NUMBER44",
                                    "GAS_NUMBER45",
                                    "GAS_NUMBER46",
                                    "GAS_NUMBER47",
                                    "GAS_NUMBER48",
                                    "GAS_NUMBER49",
                                    "GAS_NUMBER50",
                                    "GAS_NUMBER51",
                                    "GAS_NUMBER52",
                                    "GAS_NUMBER53",
                                    "GAS_NUMBER54",
                                    "GAS_NUMBER55",
                                    "GAS_NUMBER56",
                                    "GAS_NUMBER57",
                                    "GAS_NUMBER58",
                                    "GAS_NUMBER59",
                                    "GAS_NUMBER60",
                                    "GAS_NUMBER61",
                                    "GAS_NUMBER62",
                                    "GAS_NUMBER63",
                                    "GAS_NUMBER64",
                                    "GAS_NUMBER65",
                                    "GAS_NUMBER66",
                                    "GAS_NUMBER67",
                                    "GAS_NUMBER68",
                                    "GAS_NUMBER69",
                                    "GAS_NUMBER70"
                                    };
            string[] ValueArry =  {  "TIMESTAMP",
                                    "IR_CHE_OP_AC_DT",
                                    "SNDR_INFORM_EDIT_DATE",
                                    "GAS_NUMBER1",
                                    "GAS_NUMBER2",
                                    "GAS_NUMBER3",
                                    "GAS_NUMBER4",
                                    "GAS_NUMBER5",
                                    "GAS_NUMBER6",
                                    "GAS_NUMBER7",
                                    "GAS_NUMBER8",
                                    "GAS_NUMBER9",
                                    "GAS_NUMBER10",
                                    "GAS_NUMBER11",
                                    "GAS_NUMBER12",
                                    "GAS_NUMBER13",
                                    "GAS_NUMBER14",
                                    "GAS_NUMBER15",
                                    "GAS_NUMBER16",
                                    "GAS_NUMBER17",
                                    "GAS_NUMBER18",
                                    "GAS_NUMBER19",
                                    "GAS_NUMBER20",
                                    "GAS_NUMBER21",
                                    "GAS_NUMBER22",
                                    "GAS_NUMBER23",
                                    "GAS_NUMBER24",
                                    "GAS_NUMBER25",
                                    "GAS_NUMBER26",
                                    "GAS_NUMBER27",
                                    "GAS_NUMBER28",
                                    "GAS_NUMBER29",
                                    "GAS_NUMBER30",
                                    "GAS_NUMBER31",
                                    "GAS_NUMBER32",
                                    "GAS_NUMBER33",
                                    "GAS_NUMBER34",
                                    "GAS_NUMBER35",
                                    "GAS_NUMBER36",
                                    "GAS_NUMBER37",
                                    "GAS_NUMBER38",
                                    "GAS_NUMBER39",
                                    "GAS_NUMBER40",
                                    "GAS_NUMBER41",
                                    "GAS_NUMBER42",
                                    "GAS_NUMBER43",
                                    "GAS_NUMBER44",
                                    "GAS_NUMBER45",
                                    "GAS_NUMBER46",
                                    "GAS_NUMBER47",
                                    "GAS_NUMBER48",
                                    "GAS_NUMBER49",
                                    "GAS_NUMBER50",
                                    "GAS_NUMBER51",
                                    "GAS_NUMBER52",
                                    "GAS_NUMBER53",
                                    "GAS_NUMBER54",
                                    "GAS_NUMBER55",
                                    "GAS_NUMBER56",
                                    "GAS_NUMBER57",
                                    "GAS_NUMBER58",
                                    "GAS_NUMBER59",
                                    "GAS_NUMBER60",
                                    "GAS_NUMBER61",
                                    "GAS_NUMBER62",
                                    "GAS_NUMBER63",
                                    "GAS_NUMBER64",
                                    "GAS_NUMBER65",
                                    "GAS_NUMBER66",
                                    "GAS_NUMBER67",
                                    "GAS_NUMBER68",
                                    "GAS_NUMBER69",
                                    "GAS_NUMBER70"
                                    };
            #endregion
            GetCONFIG_BFANADATAList();
            if (ConfigListModel == null || ConfigListModel.Count == 0)
            {
                return;
            }
            List<Models.SG.TC0087> ResultList = new List<Models.SG.TC0087>();
            //currentTime = new DateTime (2019,10,10,23,30,00);
            DateTime TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(-0).AddHours(-3.5);
           
            int DealDayNumb = 0;//延迟天数，用于批量处理历史数据
            List<CONFIG_BFANADATA> ListModel = ConfigListModel.FindAll(match => match.DEALTYPE == 0 || match.DEALTYPE == 1);

            #region 处理数据
            for (int i = 0; i <= DealDayNumb; i++)
            {
                try
                {
                    DateTime DataEndTime = TodayTime.AddDays(-i).AddHours(0);
                    Models.SG.TC0087 bf_ana = null;
                    bf_ana = new Models.SG.TC0087();
                    bf_ana = Cal_BFANADATA(ListModel, DataEndTime);
                    if (bf_ana != null)
                    {
                        ResultList.Add(bf_ana);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            #endregion
            #region 插入数据
            if (ResultList != null && ResultList.Count > 0)
            {
                try
                {
                    using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                    {
                        DateTime endTime = ResultList.Max(m => m.TIMESTAMP);
                        DateTime startTime = ResultList.Min(m => m.TIMESTAMP);
                        int delete = 0;
                        idatabase.BeginTran();
                        delete = idatabase.ExecuteCommand($"delete from TC0087 where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)}");
                        log = $"TC0087成功删除{delete}条记录\r\n";
                        idatabase.BulkInsert<Models.SG.TC0087>("TC0087", ColumnArry, ValueArry, ResultList);
                        log += $"TC0087成功插入{ResultList.Count}条记录\r\n";
                        idatabase.CommitTran();
                        Outputlog(log);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ConfigListModel.Clear(); ConfigListModel = null;
                    ResultList.Clear(); ResultList = null;
                }
            }
            #endregion
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        /// <summary>
        /// 拼接数据源SQL
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ShortDate">数据开始时间，当日短型日期</param>
        /// <returns></returns>
        private string GetSqlByModel(CONFIG_BFANADATA model, DateTime endTime)
        {
            //数据结束日期
            DateTime startTime = endTime.AddDays(-1);

            string SQL = "";

            SQL = $"select {model.FIELDNAME} from {model.TABLENAME} where 1=1 and  {model.TIMESQL} ";
            if (!string.IsNullOrEmpty(model.FILTERSQL) && !string.IsNullOrWhiteSpace(model.FILTERSQL))
            {
                SQL += $"and {model.FILTERSQL}";
            }
            SQL = SQL.Replace("<%STARTTIME%>", DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle));
            SQL = SQL.Replace("<%ENDTIME%>", DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle));

            return SQL;

        }
        private Double CalValue(List<double> Data, CalType calType)
        {
            if (Data.Count > 0)
            {
                switch (calType)
                {
                    case CalType.Avg:
                        return Data.Average();
                    case CalType.Sum:
                        return Data.Sum();
                    case CalType.Std:
                        return CalStdev(Data);
                    case CalType.Max:
                        return Data.Max();
                    case CalType.Min:
                        return Data.Min();
                    case CalType.Ran:
                        return Data.Max() - Data.Min();
                    default:
                        throw new Exception("未知类型的计算"); ;
                }
            }
            else
            {
                return 0.0;
            }

        }
        private List<Double> Convert(DataTable dt)
        {
            List<Double> list = new List<double>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item[0].ToDouble(0));
                }
            }
            return list;
        }
        private Double CalStdev(List<Double> Data)
        {
            Double Temp = 0;
            Double Avg = Data.Average().ToDouble();
            for (int i = 0; i < Data.Count; i++)
            {
                Temp += Math.Pow((Avg - Data[i]), 2);
            }
            return Math.Pow(Temp / Data.Count, 0.5);
        }
        private void GetCONFIG_BFANADATAList()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                ConfigListModel = idatabase.GetList<CONFIG_BFANADATA>("select * from CONFIG_BFANADATA");
            }
        }
        private Models.SG.TC0087 Cal_BFANADATA(List<CONFIG_BFANADATA> ListModel, DateTime det)
        {
            DateTime InsertTime = det.ToDate();
            //List<double> datalist = null;
            DataTable dataTable = null;
            Models.SG.TC0087 ResultModel = null;
            ResultModel = new Models.SG.TC0087() { TIMESTAMP = InsertTime,IR_CHE_OP_AC_DT = InsertTime.ToString("yyyyMMddHHmmss"), SNDR_INFORM_EDIT_DATE= InsertTime.ToString("yyyyMMddHHmmss") };
            //处理DEALTYPE0与1类型
            foreach (CONFIG_BFANADATA model in ListModel)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                {
                    dataTable = idatabase.GetDataTable(GetSqlByModel(model, det));
                    if (dataTable != null & dataTable.Rows.Count != 0)
                    {
                        Double Value = CalValue(Convert(dataTable), (CalType)model.CALTYPE);
                        //Writelog(string.Format("Name：{0},Value：{1}.", dicName[model.TFIELDNAME], Value));
                        CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, dicName[model.TFIELDNAME], Math.Round(Value.ToDouble(), 3).ToString());
                        //var val=  CustomModelInfo.GetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, dicName[model.TFIELDNAME]);//如果是反射取数
                    }
                    //如果没有数据取前一天的数据
                    else if (model.DEALTYPE == 1)
                    {
                       // ResultModel = ReadBF_DAYDATE(model.TFIELDNAME, InsertTime);
                        //如果前边也没有从数据源取最新的
                        //if (ResultModel == null)
                       // {
                           // ResultModel = new Models.SG.TC0087() { TIMESTAMP=InsertTime, IR_CHE_OP_AC_DT = InsertTime.ToString() };
                            string sql = $"select {model.FIELDNAME} from {model.TABLENAME} where  {model.TIMEFIELD}=(select max({model.TIMEFIELD}) from {model.TABLENAME} where {model.FILTERSQL} )";
                            DataTable tableTemp = idatabase.GetDataTable(sql);
                            if (tableTemp != null & tableTemp.Rows.Count != 0)
                            {
                                Double Value = CalValue(Convert(tableTemp), (CalType)model.CALTYPE);
                                //Writelog(string.Format("Name：{0},Value：{1}.", dicName[model.TFIELDNAME], tableTemp.Rows[0][0].ToDouble()));
                                CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, dicName[model.TFIELDNAME], Math.Round(Value.ToDouble(), 3).ToString());
                            }
                       // }
                    }
                }
            }
            //处理自定义数据计算方式
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                //计算抗阻指数均值
                string sql = $"select a.A_BP,a.A_TPAVG,b.C_BVSG from AV05M_HIST a LEFT JOIN (select C_BVSG,timestamp from TC05M )b on a.timestamp =b.timestamp where  a.timestamp >={DataBaseFactory.ConvertTimeString(det.AddHours(-8), DataBaseType.Oracle)} and  a.timestamp <{DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)} and A_BP>0 and C_BVSG >0";
                DataTable tableTemp = idatabase.GetDataTable(sql);
                if (tableTemp != null && tableTemp.Rows.Count > 0)
                {
                    List<Double> list = new List<double>();
                    foreach (DataRow item in tableTemp.Rows)
                    {
                        list.Add((Math.Pow(item[0].ToDouble(0), 2) - Math.Pow(item[1].ToDouble(0), 2)) / Math.Pow(item[2].ToDouble(0), 1.7) * 100);
                    }
                    Double Value = CalValue(list, (CalType)1);
                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER40", Math.Round(Value.ToDouble(), 3).ToString());
                    //modelList.Add(model);

                    list.Clear();
                    list = null;
                }
                //计算日焦比和燃料比
                sql = $"select sum(I_COKE_IN) from MATERIAL_BALANCE_BAT t where timestamp >={DataBaseFactory.ConvertTimeString(det.AddDays(-1), DataBaseType.Oracle)} and  timestamp <{DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)}  ";
                double cokeweight = idatabase.GetDouble(sql);
                sql = $"select sum(I_COAL_IN) from MATERIAL_BALANCE_BAT t where timestamp >={DataBaseFactory.ConvertTimeString(det.AddDays(-1), DataBaseType.Oracle)} and  timestamp <{DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)}  ";
                double coalweight = idatabase.GetDouble(sql);
                sql = $"select sum(I_MHMT_OUT) from MATERIAL_BALANCE_BAT t where timestamp >={DataBaseFactory.ConvertTimeString(det.AddDays(-1), DataBaseType.Oracle)} and  timestamp <{DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)}  ";
                double ironweight = idatabase.GetDouble(sql);
                if (ironweight > 0)
                {
                    double Value = cokeweight * 1000 / ironweight;
                    double Value1 = (cokeweight + coalweight) * 1000 / ironweight;

                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER4", Math.Round(Value.ToDouble(), 3).ToString());
                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER5", Math.Round(Value1.ToDouble(), 3).ToString());
                }

                //计算炉喉钢砖均值
                sql = $"select t.address from UNIVERSALTE_MONITOR_CONFIG t where groupno = 15";

                DataTable tempTable = idatabase.GetDataTable(sql);
                string FieldName = "";
                if (tempTable != null && tempTable.Rows.Count > 0)
                {
                    foreach (DataRow item in tempTable.Rows)
                    {
                        FieldName += $"round(avg({item["address"].ToString()}),2),";
                    }
                    FieldName = FieldName.Remove(FieldName.Length - 1);

                    sql = $"select {FieldName} from AV05M_LININGTC_01H where  timestamp>{DataBaseFactory.ConvertTimeString(det.AddDays(-1), DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)}";
                    DataTable DTtemp = idatabase.GetDataTable(sql);
                    List<Double> list = new List<double>();
                    foreach (DataRow item in DTtemp.Rows)
                    {
                        for (int i = 0; i < DTtemp.Columns.Count; i++)
                        {
                            if (item[i].ToDouble(0) > 0)
                            {
                                list.Add(item[i].ToDouble());
                            }
                        }
                    }
                    Double Value = CalValue(list, (CalType)1);
                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER20", Math.Round(Value.ToDouble(), 3).ToString());

                    list.Clear();
                    list = null;
                }

                //计算18层壁体均值
                sql = $"select t.address from UNIVERSALTE_MONITOR_CONFIG t where groupno = 14";
                tempTable = idatabase.GetDataTable(sql);
                FieldName = "";
                if (tempTable != null && tempTable.Rows.Count > 0)
                {
                    foreach (DataRow item in tempTable.Rows)
                    {
                        FieldName += $"round(avg({item["address"].ToString()}),2),";
                    }
                    FieldName = FieldName.Remove(FieldName.Length - 1);

                    sql = $"select {FieldName} from AV05M_LININGTC_01H where  timestamp>{DataBaseFactory.ConvertTimeString(det.AddDays(-1), DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)}";
                    DataTable DTtemp = idatabase.GetDataTable(sql);
                    List<Double> list = new List<double>();
                    foreach (DataRow item in DTtemp.Rows)
                    {
                        for (int i = 0; i < DTtemp.Columns.Count; i++)
                        {
                            if (item[i].ToDouble(0) > 0)
                            {
                                list.Add(item[i].ToDouble());
                            }
                        }
                    }
                    Double Value = CalValue(list, (CalType)1);
                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER21", Math.Round(Value.ToDouble(), 3).ToString());

                    list.Clear();
                    list = null;
                }

                //计算日补水量
                sql = $"select A_FT_ADDWATER from AV05M_HIST t where timestamp ={DataBaseFactory.ConvertTimeString(det.AddDays(-1), DataBaseType.Oracle)} ";
                double AddWaterLast = idatabase.GetDouble(sql);

                sql = $"select A_FT_ADDWATER from AV05M_HIST t where timestamp ={DataBaseFactory.ConvertTimeString(det.AddDays(0), DataBaseType.Oracle)} ";
                double AddWaterNow = idatabase.GetDouble(sql);
                if (AddWaterNow >= AddWaterLast)
                {
                    Double Value = AddWaterNow - AddWaterLast;
                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER44", Math.Round(Value.ToDouble(), 3).ToString());
                }

                //计算日返矿率kg/t   
                sql = $"select RETURNORE from AV05M_HIST t where timestamp ={DataBaseFactory.ConvertTimeString(det.AddMinutes(+10), DataBaseType.Oracle)} ";
                double RETURNORE = idatabase.GetDouble(sql);
                sql = $"select SINTER_SUM from AV05M_HIST t where timestamp ={DataBaseFactory.ConvertTimeString(det.AddMinutes(+10), DataBaseType.Oracle)} ";
                double SINTERTOTAL = idatabase.GetDouble(sql);
                if (SINTERTOTAL > 0)
                {
                    Double Value = RETURNORE * 1000 / SINTERTOTAL;
                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER61", Math.Round(Value.ToDouble(),3).ToString());
                }
                //计算日返焦率
                sql = $"select RETURNCOKE from AV05M_HIST t where timestamp ={DataBaseFactory.ConvertTimeString(det.AddMinutes(+10), DataBaseType.Oracle)} ";
                double RETURNCOKE = idatabase.GetDouble(sql);
                sql = $"select COKE_SUM from AV05M_HIST t where timestamp ={DataBaseFactory.ConvertTimeString(det.AddMinutes(+10), DataBaseType.Oracle)} ";
                double COKETOTAL = idatabase.GetDouble(sql);
                if (COKETOTAL > 0)
                {
                    Double Value = RETURNCOKE * 1000 / COKETOTAL;
                    CustomModelInfo.SetModelItemValue<Models.SG.TC0087>(ResultModel, "GAS_NUMBER62", Math.Round(RETURNCOKE.ToDouble(), 3).ToString());
                }

            }
            return ResultModel;
        }
        private void GetCustomModel(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                //string sql = $"select a.A_BP,a.A_TPAVG,b.C_BVSG from AV05M a LEFT JOIN (select C_BVSG,timestamp from TC05M )b on a.timestamp =b.timestamp where  a.timestamp <={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  a.timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and A_BP>0 and C_BVSG >0";
                //DataTable tableTemp = idatabase.GetDataTable(sql);

                //List<Double> list = new List<double>();
                //if (tableTemp != null && tableTemp.Rows.Count > 0)
                //{
                //    foreach (DataRow item in tableTemp.Rows)
                //    {                      
                //        list.Add((Math.Pow(item[0].ToDouble(0),2)-Math.Pow(item[1].ToDouble(0),2))/Math.Pow(item[2].ToDouble(0),1.7)*100);
                //    }
                //    Double Value = CalValue(list, (CalType)1);
                //    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, dicName[model.TFIELDNAME], Value.ToDouble());
                //}                
                ////读取铁水温度合格率数据
                //sql = $"select count(*) from TAPPING where  HMTP_TAPEND between 1490 and 1510 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                //hmtpnum = idatabase.GetInt(sql);
                //sql = $"select count(*) from TAPPING where  HMTP_TAPEND >0 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                //hmtpsum = idatabase.GetInt(sql);
            }
        }
        private Models.AG.BF_ANADATA_08H GetCustomCal(DateTime dt)
        {
            DateTime InsertTime = dt.ToDate();
            //List<Models.AG.BF_ANADATA_08H> modelList = new List<Models.AG.BF_ANADATA_08H>();
            Models.AG.BF_ANADATA_08H model = null;
            model = new Models.AG.BF_ANADATA_08H();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                //计算煤气阻力系数均值与标准差
                string sql = $"select a.A_BP,a.A_TPAVG,b.C_BVSG from AV05M a LEFT JOIN (select C_BVSG,timestamp from TC05M )b on a.timestamp =b.timestamp where  a.timestamp >={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  a.timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and A_BP>0 and C_BVSG >0";
                DataTable tableTemp = idatabase.GetDataTable(sql);
                if (tableTemp != null && tableTemp.Rows.Count > 0)
                {
                    List<Double> list = new List<double>();
                    foreach (DataRow item in tableTemp.Rows)
                    {
                        list.Add((Math.Pow(item[0].ToDouble(0), 2) - Math.Pow(item[1].ToDouble(0), 2)) / Math.Pow(item[2].ToDouble(0), 1.7) * 100);
                    }
                    Double Value = CalValue(list, (CalType)1);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_K", Value.ToDouble());
                    //modelList.Add(model);

                    Value = CalValue(list, (CalType)3);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_K_STD", Value.ToDouble());
                    //modelList.Add(model);

                    list.Clear();
                    list = null;
                }
                //计算中心加焦比例
                sql = $"select a_circle1,a_circle1+a_circle2+a_circle3+a_circle4+a_circle5+a_circle6+a_circle7+a_circle8+a_circle9+a_circle10+a_circle11 from STUFF_MATRIX t where timestamp >={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and a_type=1 ";
                DataTable tableTemp1 = idatabase.GetDataTable(sql);
                if (tableTemp1 != null && tableTemp1.Rows.Count > 0)
                {
                    List<Double> list = new List<double>();
                    foreach (DataRow item in tableTemp1.Rows)
                    {
                        list.Add((item[0].ToDouble(0) / item[1].ToDouble(0)).ToDouble() * 100);
                    }
                    Double Value = CalValue(list, (CalType)1);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_CENTERCOKE", Value.ToDouble());
                    //modelList.Add(model);

                    Value = CalValue(list, (CalType)3);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_CENTERCOKE_STD", Value.ToDouble());
                    //modelList.Add(model);

                    list.Clear();
                    list = null;
                }
                //计算铁水温度均值
                //sql = $"select HMTP from LT_TAPPING where TAPSTART timestamp <={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and HMTP>0";
                //DataTable tableTemp = idatabase.GetDataTable(sql);
            }
            return model;
        }
        private Models.SG.TC0087 ReadBF_DAYDATE(string fieldname, DateTime dt)
        {
            Models.SG.TC0087 model = null;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                model = idatabase.GetSingle<Models.SG.TC0087>($"select {fieldname} from TC0087 where timestamp = (select max(timestamp) from TC0087 where timestamp<{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and {fieldname}>0)");
            }
            return model;
        }
    }
}
