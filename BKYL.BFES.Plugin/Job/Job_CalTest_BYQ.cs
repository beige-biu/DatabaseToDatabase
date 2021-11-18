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
using BKYL.BFES.Plugin;

namespace BKYL.Transfer.Plugin
{
    public class Job_CalTest_BYQ : BaseJob
    {
        List<CONFIG_BFDAYDATA> ConfigListModel = null;
        string log = "";
        //HEARTHBALANCE_IRONDIFF_DAY modelHEARTHBALANCE_IRONDIFF_DAY = null;
        List<BFES.Plugin.Models.BYQ.AV05M_LININGTC> modelAV05M_LININGTC = null;
        List<L3ANALYSI> modelL3ANALYSI = null;
        List<HEARTH_01H> modelHEARTH_01H = null;
        //List<UNIVERSALTE_MONITOR_CONFIG> modelUniversConfig = null;

        double ReturnCoke = 0;
        double ReturnOre = 0;
        double ConsumOre = 0;
        double ConsumCoke = 0;
        double slags = 0;
        double irons = 0;
        int hmtpnum = 0;
        int hmtpsum = 0;

        public override void InitTask()
        {
        }
        public override void RunTask(DateTime currentTime)
        {
            GetCONFIG_BFDAYDATAList();
            if (ConfigListModel == null || ConfigListModel.Count == 0)
            {
                return;
            }
            string[] columnNames = { "TIMESTAMP", "ID", "ID_DESC", "VALUE" };
            string[] dataFiledNameArr = { "TIMESTAMP", "ID", "ID_DESC", "VALUE" };

            List<BF_TEST> ResultList = new List<BF_TEST>();
            DateTime TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(-1);

            //if (currentTime.Hour >= 0 && currentTime.Hour < 24)
            //{
            //    TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddHours(20);
            //}
            //else
            //{
            //    TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(-1).AddHours(20);
            //}
            int DealDayNumb = 10;
            List<CONFIG_BFDAYDATA> ListModel = ConfigListModel.FindAll(match => match.DEALTYPE == 0 || match.DEALTYPE == 1);

            #region 处理数据
            for (int i = 0; i <= DealDayNumb; i++)
            {
                try
                {
                    DateTime DataEndTime = TodayTime.AddDays(-i);
                    BF_TEST bf_test = null;
                    //处理0与1类型
                    foreach (CONFIG_BFDAYDATA model in ListModel)
                    {
                        bf_test = Cal_BF_TEST(model, DataEndTime);
                        if (bf_test != null)
                        {
                            ResultList.Add(bf_test);
                        }
                    }
                    //处理自定义计算方式
                    GetCustomModel(DataEndTime); //获取数据源对象
                    List<BF_TEST> CustomList = GetCustomCal(DataEndTime); //自定义数据
                    if (CustomList != null && CustomList.Count > 0)
                    {
                        ResultList.AddRange(CustomList);
                    }
                    CustomList.Clear();
                    CustomList = null;

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
                        delete = idatabase.ExecuteCommand($"delete from BF_TEST where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)}");
                        log = $"BF_TEST成功删除{delete}条记录\r\n";
                        idatabase.BulkInsert<BF_TEST>("BF_TEST", columnNames, dataFiledNameArr, ResultList);
                        log += $"BF_TEST成功插入{ResultList.Count}条记录\r\n";
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
        private string GetSqlByModel(CONFIG_BFDAYDATA model, DateTime dtTime)
        {
            //数据结束日期
            DateTime startTime = dtTime.AddDays(-1);

            string SQL = "";

            SQL = $"select {model.FIELDNAME} from {model.TABLENAME} where 1=1 and  {model.TIMESQL} ";
            if (!string.IsNullOrEmpty(model.FILTERSQL) && !string.IsNullOrWhiteSpace(model.FILTERSQL))
            {
                SQL += $"and {model.FILTERSQL}";
            }
            SQL = SQL.Replace("<%STARTTIME%>", DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle));
            SQL = SQL.Replace("<%ENDTIME%>", DataBaseFactory.ConvertTimeString(dtTime, DataBaseType.Oracle));

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

            Double Avg = Data.Average();
            for (int i = 0; i < Data.Count; i++)
            {
                Temp += Math.Pow((Avg - Data[i]), 2);
            }
            return Math.Pow(Temp / Data.Count, 0.5);
        }
        private void GetCONFIG_BFDAYDATAList()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                ConfigListModel = idatabase.GetList<CONFIG_BFDAYDATA>("select * from CONFIG_BFDAYDATA");
            }
        }
        private BF_TEST Cal_BF_TEST(CONFIG_BFDAYDATA model, DateTime dt)
        {
            DateTime InsertTime = dt.ToString("yyyy-MM-dd").ToDate();
            DataTable dataTable = null;
            BF_TEST ResultModel = null;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                dataTable = idatabase.GetDataTable(GetSqlByModel(model, dt));
                if (dataTable != null & dataTable.Rows.Count != 0)
                {
                    Double Value = CalValue(Convert(dataTable), (CalType)model.CALTYPE);
                    ResultModel = new BF_TEST() { ID = model.D_ID, ID_DESC = model.D_DESC, VALUE = Value, TIMESTAMP = InsertTime };
                }
                //如果没有数据取前边的数据
                else if (model.DEALTYPE == 1)
                {
                    ResultModel = ReadBF_TEST(model.D_ID, InsertTime);
                    //如果前边也没有从数据源取最新的
                    if (ResultModel == null)
                    {
                        // timestamp between <%STARTTIME%> and <%ENDTIME%> and TOTAL_HL>0
                        string sql = $"select {model.FIELDNAME} from {model.TABLENAME} where  {model.TIMEFIELD}=(select max({model.TIMEFIELD}) from {model.TABLENAME} where {model.FILTERSQL} )";
                        DataTable tableTemp = idatabase.GetDataTable(sql);
                        if (tableTemp.Rows.Count > 0)
                        {
                            ResultModel = new BF_TEST() { ID = model.D_ID, ID_DESC = model.D_DESC, VALUE = tableTemp.Rows[0][0].ToDouble(), TIMESTAMP = InsertTime };
                        }

                    }

                }
            }
            return ResultModel;
        }
        private void GetCustomModel(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                modelAV05M_LININGTC = idatabase.Queryable<BFES.Plugin.Models.BYQ.AV05M_LININGTC>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                //modelMATERIAL_BALANCE_BAT = idatabase.Queryable<MATERIAL_BALANCE_BAT>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                modelL3ANALYSI = idatabase.Queryable<L3ANALYSI>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                //modelUniversConfig = idatabase.Queryable<UNIVERSALTE_MONITOR_CONFIG>().ToList();
                modelHEARTH_01H = idatabase.Queryable<HEARTH_01H>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                //根据每日矿耗、返矿计算返矿率
                string sql = $"select R_CRUDE_WEIGHT from RP3_OPTIMUM t where t.R_SINTER_WEIGHT >150 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ConsumOre = idatabase.GetDouble(sql);

                sql = $"select RETURN_ORE from RP_REPORT_MONTH_AUTO t where timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ReturnOre = idatabase.GetDouble(sql);
                //返焦
                sql = $"select RETURN_COKE from RP_REPORT_MONTH_AUTO t where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ReturnCoke = idatabase.GetDouble(sql);
                //每日焦耗
                sql = $"select R_FOCAL_AMOUNT from RP_REPORT_MONTH_MQ t where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ConsumCoke = idatabase.GetDouble(sql);
                //读取风量
                //sql = $"select A_BV5m  from AV05M t where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} order by timestamp asc";
                //读取渣中S和铁水中S
                sql = $"select round(avg(SLAGS),5) from TAPPING_SLAG_LAB where  SAMPLE_TAKETIME>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  SAMPLE_TAKETIME<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                slags = idatabase.GetDouble(sql);
                sql = $"select round(avg(HMS),5) from TAPPING_IRON_LAB where  TAPSTART>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  TAPSTART<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                irons = idatabase.GetDouble(sql);
                //读取铁水温度合格率数据
                sql = $"select count(*) from TAPPING where  HMTP_TAPEND between 1490 and 1510 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                hmtpnum = idatabase.GetInt(sql);
                sql = $"select count(*) from TAPPING where  HMTP_TAPEND >0 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                hmtpsum = idatabase.GetInt(sql);
            }
        }
        private List<BF_TEST> GetCustomCal(DateTime dt)
        {
            DateTime InsertTime = dt.ToString("yyyy-MM-dd").ToDate();
            List<BF_TEST> modelList = new List<BF_TEST>();
            BF_TEST model = null;
            string sql = null;
            //double ore = 0;
            //double Yield = 0;
            //if (modelHEARTHBALANCE_IRONDIFF_DAY != null)
            //{
            //ore = modelHEARTHBALANCE_IRONDIFF_DAY.SINTERWEIGHT.ToDouble() + modelHEARTHBALANCE_IRONDIFF_DAY.PELLETWEIGHT.ToDouble() + modelHEARTHBALANCE_IRONDIFF_DAY.CURDEWEIGHT.ToDouble();
            //燃料比
            //model = new BFES.Models.BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000003", ID_DESC = "操作燃料比" };
            //if (modelHEARTHBALANCE_IRONDIFF_DAY.YIELD.ToDouble(0) > 0)
            //{
            //    Yield = modelHEARTHBALANCE_IRONDIFF_DAY.YIELD.ToDouble();
            //    model.VALUE = (modelHEARTHBALANCE_IRONDIFF_DAY.COKEWEIGHT.ToDouble(0) + modelHEARTHBALANCE_IRONDIFF_DAY.LITTLEWEIGHT.ToDouble(0) + Winj) / modelHEARTHBALANCE_IRONDIFF_DAY.YIELD.ToDouble(0) * 1000;
            //    modelList.Add(model);
            //}

            //焦炭负荷
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000002", ID_DESC = "焦炭负荷" };
            if (ConsumCoke > 0)
            {
                model.VALUE = ConsumOre / ConsumCoke;
                modelList.Add(model);
            }
            //LS硫分配系数
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000016", ID_DESC = "LS" };
            if (irons > 0)
            {
                model.VALUE = slags / irons;
                modelList.Add(model);
            }
            //日铁水温度合格率
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000017", ID_DESC = "日铁水温度合格率" };
            if (hmtpsum > 0)
            {
                model.VALUE = 100 * hmtpnum / hmtpsum; 
                modelList.Add(model);
            }
            #region //返矿率(块矿+球团+烧结) 
            //if (ConsumOre > 0)
            //{
            //    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000042", ID_DESC = "返矿率" };
            //    model.VALUE = ReturnOre / ConsumOre * 100;
            //    modelList.Add(model);

            //}
            //#endregion
            //#region   //返焦率 
            //if (ConsumCoke > 0)
            //{
            //    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000039", ID_DESC = "返焦率" };
            //    model.VALUE = ReturnCoke / ConsumCoke * 100;
            //    modelList.Add(model);
            //}
            #endregion

            #region //壁体预警次数(14-16)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000009", ID_DESC = "壁体预警次数(14-16)" };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 5038; i <= 5061; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 5118; i <= 5141; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion

            #region //壁体预警次数(12-13)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000010", ID_DESC = "壁体预警次数(12-13)" };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 5062; i <= 5077; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 5142; i <= 5157; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion
            #region //壁体预警次数(10-11)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000011", ID_DESC = "壁体预警次数(10-11)" };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 5078; i <= 5093; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 5158; i <= 5173; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion
            #region //壁体预警次数(7-9)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000012", ID_DESC = "壁体预警次数(7-9)" };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 5094; i <= 5117; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 5174; i <= 5197; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion
            #region //环碳温度(1-2)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000019", ID_DESC = "环碳温度(1-2)" };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE_107A11L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A10L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A9L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A12L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A15L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A14L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A13L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A8L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A3L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A2L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A1L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A4L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A7L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A6L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A5L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A16L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A24L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A23L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A25L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A27L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A26L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A22L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A18L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A17L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A19L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A21L1)
                            + modelHEARTH_01H.Average(m => m.TE_107A20L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A3L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A4L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A1L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A2L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A36L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A15L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A14L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A13L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A16L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A19L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A18L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A17L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A12L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A7L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A6L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A5L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A8L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A11L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A10L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A9L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A20L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A31L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A30L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A29L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A32L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A35L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A34L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A33L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A28L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A23L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A22L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A21L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A24L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A27L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A26L1)
                            + modelHEARTH_01H.Average(m => m.TE_109A25L1)) / 63;
            modelList.Add(model);
            #endregion

            #region //环碳温度(3-4)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000020", ID_DESC = "环碳温度(3-4)" };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE_110A1L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A2L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A3L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A4L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A5L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A6L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A7L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A8L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A9L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A10L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A11L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A12L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A13L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A14L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A15L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A16L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A17L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A18L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A19L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A20L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A21L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A22L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A23L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A24L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A25L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A26L1)
                                + modelHEARTH_01H.Average(m => m.TE_110A27L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A1L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A2L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A3L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A4L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A5L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A6L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A7L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A8L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A9L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A10L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A11L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A12L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A13L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A14L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A15L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A16L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A17L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A18L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A19L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A20L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A21L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A22L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A23L1)
                                + modelHEARTH_01H.Average(m => m.TE_111A24L1)) / 51;
            modelList.Add(model);
            #endregion

            #region //环碳温度(5-6)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000021", ID_DESC = "环碳温度(5-6)" };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE_112A1L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A2L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A3L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A4L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A5L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A6L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A7L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A8L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A9L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A10L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A11L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A12L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A13L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A14L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A15L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A16L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A17L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A18L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A19L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A20L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A21L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A22L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A23L1)
                        + modelHEARTH_01H.Average(m => m.TE_112A24L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A1L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A2L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A3L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A6L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A7L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A8L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A9L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A12L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A13L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A14L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A15L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A16L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A19L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A20L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A21L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A22L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A25L1)
                        + modelHEARTH_01H.Average(m => m.TE_113A26L1)) / 42;
            modelList.Add(model);
            #endregion

            #region //环碳温度(7-8)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000022", ID_DESC = "环碳温度(7-8)" };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE_114A8L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A9L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A7L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A5L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A6L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A13L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A14L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A12L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A10L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A11L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A4L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A1L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A2L1)
                        + modelHEARTH_01H.Average(m => m.TE_114A3L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A3L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A4L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A1L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A2L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A7L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A8L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A5L1)
                        + modelHEARTH_01H.Average(m => m.TE_115A6L1)) / 22;

            modelList.Add(model);
            #endregion

            #region //环碳温度(9-13)
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000023", ID_DESC = "环碳温度(9-13)" };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE_116A1L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A2L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A3L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A4L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A5L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A6L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A7L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A8L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A9L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A10L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A11L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A12L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A13L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A14L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A15L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A16L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A17L1)
                        + modelHEARTH_01H.Average(m => m.TE_116A18L1)) / 18;

            modelList.Add(model);
            #endregion
            #region//炉身上壁体温度(15-16)
            //model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000010", ID_DESC = "炉身上壁体温度(15-16)" };
            //model.VALUE = (modelAV05M_LININGTC.Average(m => m.TE5046)
            //                + modelAV05M_LININGTC.Average(m => m.TE5047)
            //                + modelAV05M_LININGTC.Average(m => m.TE5048)
            //                + modelAV05M_LININGTC.Average(m => m.TE5049)
            //                + modelAV05M_LININGTC.Average(m => m.TE5050)
            //                + modelAV05M_LININGTC.Average(m => m.TE5051)
            //                + modelAV05M_LININGTC.Average(m => m.TE5052)
            //                + modelAV05M_LININGTC.Average(m => m.TE5053)
            //                + modelAV05M_LININGTC.Average(m => m.TE5126)
            //                + modelAV05M_LININGTC.Average(m => m.TE5127)
            //                + modelAV05M_LININGTC.Average(m => m.TE5128)
            //                + modelAV05M_LININGTC.Average(m => m.TE5129)
            //                + modelAV05M_LININGTC.Average(m => m.TE5130)
            //                + modelAV05M_LININGTC.Average(m => m.TE5131)
            //                + modelAV05M_LININGTC.Average(m => m.TE5132)
            //                + modelAV05M_LININGTC.Average(m => m.TE5133)
            //                + modelAV05M_LININGTC.Average(m => m.TE5038)
            //                + modelAV05M_LININGTC.Average(m => m.TE5039)
            //                + modelAV05M_LININGTC.Average(m => m.TE5040)
            //                + modelAV05M_LININGTC.Average(m => m.TE5041)
            //                + modelAV05M_LININGTC.Average(m => m.TE5042)
            //                + modelAV05M_LININGTC.Average(m => m.TE5043)
            //                + modelAV05M_LININGTC.Average(m => m.TE5044)
            //                + modelAV05M_LININGTC.Average(m => m.TE5045)
            //                + modelAV05M_LININGTC.Average(m => m.TE5118)
            //                + modelAV05M_LININGTC.Average(m => m.TE5119)
            //                + modelAV05M_LININGTC.Average(m => m.TE5120)
            //                + modelAV05M_LININGTC.Average(m => m.TE5121)
            //                + modelAV05M_LININGTC.Average(m => m.TE5122)
            //                + modelAV05M_LININGTC.Average(m => m.TE5123)
            //                + modelAV05M_LININGTC.Average(m => m.TE5124)
            //                + modelAV05M_LININGTC.Average(m => m.TE5125)) / 32;
            //#endregion
            //modelList.Add(model);

            //#region  //炉身中壁体温度(12-14)
            //model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000011", ID_DESC = "炉身中壁体温度(12-14)" };
            //model.VALUE = (modelAV05M_LININGTC.Average(m => m.TE5070)
            //            + modelAV05M_LININGTC.Average(m => m.TE5071)
            //            + modelAV05M_LININGTC.Average(m => m.TE5072)
            //            + modelAV05M_LININGTC.Average(m => m.TE5073)
            //            + modelAV05M_LININGTC.Average(m => m.TE5074)
            //            + modelAV05M_LININGTC.Average(m => m.TE5075)
            //            + modelAV05M_LININGTC.Average(m => m.TE5076)
            //            + modelAV05M_LININGTC.Average(m => m.TE5077)
            //            + modelAV05M_LININGTC.Average(m => m.TE5150)
            //            + modelAV05M_LININGTC.Average(m => m.TE5151)
            //            + modelAV05M_LININGTC.Average(m => m.TE5152)
            //            + modelAV05M_LININGTC.Average(m => m.TE5153)
            //            + modelAV05M_LININGTC.Average(m => m.TE5154)
            //            + modelAV05M_LININGTC.Average(m => m.TE5155)
            //            + modelAV05M_LININGTC.Average(m => m.TE5156)
            //            + modelAV05M_LININGTC.Average(m => m.TE5157)
            //            + modelAV05M_LININGTC.Average(m => m.TE5062)
            //            + modelAV05M_LININGTC.Average(m => m.TE5063)
            //            + modelAV05M_LININGTC.Average(m => m.TE5064)
            //            + modelAV05M_LININGTC.Average(m => m.TE5065)
            //            + modelAV05M_LININGTC.Average(m => m.TE5066)
            //            + modelAV05M_LININGTC.Average(m => m.TE5067)
            //            + modelAV05M_LININGTC.Average(m => m.TE5068)
            //            + modelAV05M_LININGTC.Average(m => m.TE5069)
            //            + modelAV05M_LININGTC.Average(m => m.TE5142)
            //            + modelAV05M_LININGTC.Average(m => m.TE5143)
            //            + modelAV05M_LININGTC.Average(m => m.TE5144)
            //            + modelAV05M_LININGTC.Average(m => m.TE5145)
            //            + modelAV05M_LININGTC.Average(m => m.TE5146)
            //            + modelAV05M_LININGTC.Average(m => m.TE5147)
            //            + modelAV05M_LININGTC.Average(m => m.TE5148)
            //            + modelAV05M_LININGTC.Average(m => m.TE5149)
            //            + modelAV05M_LININGTC.Average(m => m.TE5054)
            //            + modelAV05M_LININGTC.Average(m => m.TE5055)
            //            + modelAV05M_LININGTC.Average(m => m.TE5056)
            //            + modelAV05M_LININGTC.Average(m => m.TE5057)
            //            + modelAV05M_LININGTC.Average(m => m.TE5058)
            //            + modelAV05M_LININGTC.Average(m => m.TE5059)
            //            + modelAV05M_LININGTC.Average(m => m.TE5060)
            //            + modelAV05M_LININGTC.Average(m => m.TE5061)
            //            + modelAV05M_LININGTC.Average(m => m.TE5134)
            //            + modelAV05M_LININGTC.Average(m => m.TE5135)
            //            + modelAV05M_LININGTC.Average(m => m.TE5136)
            //            + modelAV05M_LININGTC.Average(m => m.TE5137)
            //            + modelAV05M_LININGTC.Average(m => m.TE5138)
            //            + modelAV05M_LININGTC.Average(m => m.TE5139)
            //            + modelAV05M_LININGTC.Average(m => m.TE5140)
            //            + modelAV05M_LININGTC.Average(m => m.TE5141)
            //               ) / 48;
            //#endregion
            //modelList.Add(model);

            //#region  //炉身下壁体温度(9-11)
            //model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000012", ID_DESC = "炉身下壁体温度(9-11)" };
            //model.VALUE = (modelAV05M_LININGTC.Average(m => m.TE5174)
            //        + modelAV05M_LININGTC.Average(m => m.TE5175)
            //        + modelAV05M_LININGTC.Average(m => m.TE5176)
            //        + modelAV05M_LININGTC.Average(m => m.TE5177)
            //        + modelAV05M_LININGTC.Average(m => m.TE5178)
            //        + modelAV05M_LININGTC.Average(m => m.TE5179)
            //        + modelAV05M_LININGTC.Average(m => m.TE5180)
            //        + modelAV05M_LININGTC.Average(m => m.TE5181)
            //        + modelAV05M_LININGTC.Average(m => m.TE5094)
            //        + modelAV05M_LININGTC.Average(m => m.TE5095)
            //        + modelAV05M_LININGTC.Average(m => m.TE5096)
            //        + modelAV05M_LININGTC.Average(m => m.TE5097)
            //        + modelAV05M_LININGTC.Average(m => m.TE5098)
            //        + modelAV05M_LININGTC.Average(m => m.TE5099)
            //        + modelAV05M_LININGTC.Average(m => m.TE5100)
            //        + modelAV05M_LININGTC.Average(m => m.TE5101)
            //        + modelAV05M_LININGTC.Average(m => m.TE5166)
            //        + modelAV05M_LININGTC.Average(m => m.TE5167)
            //        + modelAV05M_LININGTC.Average(m => m.TE5168)
            //        + modelAV05M_LININGTC.Average(m => m.TE5169)
            //        + modelAV05M_LININGTC.Average(m => m.TE5170)
            //        + modelAV05M_LININGTC.Average(m => m.TE5171)
            //        + modelAV05M_LININGTC.Average(m => m.TE5172)
            //        + modelAV05M_LININGTC.Average(m => m.TE5173)
            //        + modelAV05M_LININGTC.Average(m => m.TE5086)
            //        + modelAV05M_LININGTC.Average(m => m.TE5087)
            //        + modelAV05M_LININGTC.Average(m => m.TE5088)
            //        + modelAV05M_LININGTC.Average(m => m.TE5089)
            //        + modelAV05M_LININGTC.Average(m => m.TE5090)
            //        + modelAV05M_LININGTC.Average(m => m.TE5091)
            //        + modelAV05M_LININGTC.Average(m => m.TE5092)
            //        + modelAV05M_LININGTC.Average(m => m.TE5093)
            //        + modelAV05M_LININGTC.Average(m => m.TE5158)
            //        + modelAV05M_LININGTC.Average(m => m.TE5159)
            //        + modelAV05M_LININGTC.Average(m => m.TE5160)
            //        + modelAV05M_LININGTC.Average(m => m.TE5161)
            //        + modelAV05M_LININGTC.Average(m => m.TE5162)
            //        + modelAV05M_LININGTC.Average(m => m.TE5163)
            //        + modelAV05M_LININGTC.Average(m => m.TE5164)
            //        + modelAV05M_LININGTC.Average(m => m.TE5165)
            //        + modelAV05M_LININGTC.Average(m => m.TE5078)
            //        + modelAV05M_LININGTC.Average(m => m.TE5079)
            //        + modelAV05M_LININGTC.Average(m => m.TE5080)
            //        + modelAV05M_LININGTC.Average(m => m.TE5081)
            //        + modelAV05M_LININGTC.Average(m => m.TE5082)
            //        + modelAV05M_LININGTC.Average(m => m.TE5083)
            //        + modelAV05M_LININGTC.Average(m => m.TE5084)
            //        + modelAV05M_LININGTC.Average(m => m.TE5085)
            //               ) / 48;
            //#endregion
            //modelList.Add(model);

            //#region  //炉腰温度8
            //model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000013", ID_DESC = "炉腰温度8" };
            //model.VALUE = (modelAV05M_LININGTC.Average(m => m.TE5182)
            //        + modelAV05M_LININGTC.Average(m => m.TE5183)
            //        + modelAV05M_LININGTC.Average(m => m.TE5184)
            //        + modelAV05M_LININGTC.Average(m => m.TE5185)
            //        + modelAV05M_LININGTC.Average(m => m.TE5186)
            //        + modelAV05M_LININGTC.Average(m => m.TE5187)
            //        + modelAV05M_LININGTC.Average(m => m.TE5188)
            //        + modelAV05M_LININGTC.Average(m => m.TE5189)
            //        + modelAV05M_LININGTC.Average(m => m.TE5102)
            //        + modelAV05M_LININGTC.Average(m => m.TE5103)
            //        + modelAV05M_LININGTC.Average(m => m.TE5104)
            //        + modelAV05M_LININGTC.Average(m => m.TE5105)
            //        + modelAV05M_LININGTC.Average(m => m.TE5106)
            //        + modelAV05M_LININGTC.Average(m => m.TE5107)
            //        + modelAV05M_LININGTC.Average(m => m.TE5108)
            //        + modelAV05M_LININGTC.Average(m => m.TE5109)
            //               ) / 16;
            //#endregion
            //modelList.Add(model);

            //#region  //炉腹温度7

            //model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000014", ID_DESC = "炉腹温度7" };
            //model.VALUE = (modelAV05M_LININGTC.Average(m => m.TE5190)
            //        + modelAV05M_LININGTC.Average(m => m.TE5191)
            //        + modelAV05M_LININGTC.Average(m => m.TE5192)
            //        + modelAV05M_LININGTC.Average(m => m.TE5193)
            //        + modelAV05M_LININGTC.Average(m => m.TE5194)
            //        + modelAV05M_LININGTC.Average(m => m.TE5195)
            //        + modelAV05M_LININGTC.Average(m => m.TE5196)
            //        + modelAV05M_LININGTC.Average(m => m.TE5197)
            //        + modelAV05M_LININGTC.Average(m => m.TE5110)
            //        + modelAV05M_LININGTC.Average(m => m.TE5111)
            //        + modelAV05M_LININGTC.Average(m => m.TE5112)
            //        + modelAV05M_LININGTC.Average(m => m.TE5113)
            //        + modelAV05M_LININGTC.Average(m => m.TE5114)
            //        + modelAV05M_LININGTC.Average(m => m.TE5115)
            //        + modelAV05M_LININGTC.Average(m => m.TE5116)
            //        + modelAV05M_LININGTC.Average(m => m.TE5117)) / 16;
            //modelList.Add(model);
            #endregion
            #region  //炉喉钢砖均值
            if (modelAV05M_LININGTC != null && modelAV05M_LININGTC.Count > 0)
            {
                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000006", ID_DESC = "炉喉钢砖均值" };
                model.VALUE = (modelAV05M_LININGTC.Average(m => m.TE_5031)
                        + modelAV05M_LININGTC.Average(m => m.TE_5032)
                        + modelAV05M_LININGTC.Average(m => m.TE_5033)
                        + modelAV05M_LININGTC.Average(m => m.TE_5034)
                        + modelAV05M_LININGTC.Average(m => m.TE_5035)
                        + modelAV05M_LININGTC.Average(m => m.TE_5036)
                        + modelAV05M_LININGTC.Average(m => m.TE_5037)
                        + modelAV05M_LININGTC.Average(m => m.TE_5030)) / 8;
                #endregion
                modelList.Add(model);

                modelAV05M_LININGTC.Clear();
                modelAV05M_LININGTC = null;

            }

            //if (modelL3ANALYSI != null && modelL3ANALYSI.Count > 0)
            //{
            //    #region 焦炭平均粒度 M_SIZEABOVE80_CONT*80+M_SIZE60_80_CONT*70+M_SIZE40_60_CONT*50+M_SIZE30_40_CONT*35+M_SIZEUNDER30_CONT*30

            //    List<L3ANALYSI> CokeList = modelL3ANALYSI.FindAll(m => m.M_CODE >= 30 && m.M_CODE <= 49 && m.M_SIZE40_60_CONT > 0);
            //    double value = 0;
            //    if (CokeList != null && CokeList.Count > 0)
            //    {
            //        model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000038", ID_DESC = "焦炭平均粒级" };

            //        foreach (L3ANALYSI item in CokeList)
            //        {
            //            value += (item.M_SIZEABOVE80_CONT * 80 + item.M_SIZE60_80_CONT * 70 + item.M_SIZE40_60_CONT * 50 + item.M_SIZE30_40_CONT * 35 + item.M_SIZEUNDER30_CONT * 30) / 100;

            //        }
            //        model.VALUE = value / CokeList.Count;
            //        modelList.Add(model);
            //    }
            //    else
            //    {
            //        L3ANALYSI l3temp = null;

            //        using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            //        {
            //            sql = "select * from L3ANALYSI where timestamp = (select max(timestamp) from L3ANALYSI where  M_CODE>=30 and M_CODE<=49  and M_SIZE40_60_CONT>0)";
            //            l3temp = idatabase.GetSingle<L3ANALYSI>(sql);
            //        }
            //        if (l3temp != null)
            //        {
            //            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000038", ID_DESC = "焦炭平均粒级" };
            //            model.VALUE = (l3temp.M_SIZEABOVE80_CONT * 80 + l3temp.M_SIZE60_80_CONT * 70 + l3temp.M_SIZE40_60_CONT * 50 + l3temp.M_SIZE30_40_CONT * 35 + l3temp.M_SIZEUNDER30_CONT * 30) / 100;
            //            modelList.Add(model);
            //        }
            //    }
            //    #endregion

            //    #region //烧结矿平均粒度
            //    List<L3ANALYSI> SinterList = modelL3ANALYSI.FindAll(m => m.M_CODE >= 50 && m.M_CODE <= 59 && m.M_SINTER_SIZE10_16 > 0);
            //    value = 0;
            //    if (SinterList != null && SinterList.Count > 0)
            //    {
            //        model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000045", ID_DESC = "烧结平均粒级" };
            //        foreach (L3ANALYSI item in SinterList)
            //        {
            //            value += (item.M_SINTER_SIZEUNDER5 * 5 + item.M_SINTER_SIZE5_10 * 7.5 + item.M_SINTER_SIZE10_16 * 13 + item.M_SINTER_SIZE16_25 * 20.5 + item.M_SINTER_SIZE25_40 * 32.5 + item.M_SINTER_SIZEABOVE40 * 40) / 100;
            //        }
            //        model.VALUE = value / SinterList.Count;
            //        modelList.Add(model);
            //    }
            //    else
            //    {
            //        L3ANALYSI l3temp = null;

            //        using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            //        {
            //            sql = "select * from L3ANALYSI where timestamp = (select max(timestamp) from L3ANALYSI where  M_CODE>=50 and M_CODE<=59  and M_SINTER_SIZE10_16>0)";
            //            l3temp = idatabase.GetSingle<L3ANALYSI>(sql);
            //        }
            //        if (l3temp != null)
            //        {
            //            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000045", ID_DESC = "烧结平均粒级" };
            //            model.VALUE = (l3temp.M_SINTER_SIZEUNDER5 * 5 + l3temp.M_SINTER_SIZE5_10 * 7.5 + l3temp.M_SINTER_SIZE10_16 * 13 + l3temp.M_SINTER_SIZE16_25 * 20.5 + l3temp.M_SINTER_SIZE25_40 * 32.5 + l3temp.M_SINTER_SIZEABOVE40 * 40) / 100;
            //            modelList.Add(model);
            //        }

            //    }
            //    #endregion

                //modelL3ANALYSI.Clear();
                //modelL3ANALYSI = null;
            //}
            return modelList;
        }
        private BF_TEST ReadBF_TEST(string id, DateTime dt)
        {
            BF_TEST model = null;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                model = idatabase.GetSingle<BF_TEST>($"select * from BF_TEST where  id={id} and timestamp = (select max(timestamp) from BF_TEST where timestamp<{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and value>0 and id={id})");
            }
            if (model != null)
            {
                model.TIMESTAMP = dt;
            }
            return model;
        }

    }
    public enum CalType
    {
        Avg = 1,
        Sum = 2,
        Std = 3,
        Max = 4,
        Min = 5,
        Ran = 6
    }
}
