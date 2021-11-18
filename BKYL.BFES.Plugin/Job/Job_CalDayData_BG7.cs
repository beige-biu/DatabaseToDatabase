using BFES.DataAccess;
using BFES.DataAccess.Common;
using BKYL.BFES.Plugin.BGBFES7;
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
    public class Job_CalDayData_BG7 : BaseJob
    {
        List<CONFIG_BFDAYDATA> ConfigListModel = null;
        string log = "";
        List<BKYL.BFES.Plugin.BGBFES7.AV05M_LININGTC> modelAV05M_LININGTC = null;
        List<MATERIAL_BALANCE_BAT> modelMATERIAL_BALANCE_BAT = null;
        MATERIAL_BALANCE_DAY materialDayModel = null;
        List<BGBFES7.L3ANALYSI> modelL3ANALYSI = null;
        List<BATCHHIS_TOTAL> BatchhisTotalList = null;
        List<HEARTH_01H> modelHEARTH_01H = null;
        double AddWater = 0; //补水量
        double PondYield = 0;//实际产量
        double DustWeight = 0; //灰量 kg
        double Ironprodratio = 0; //出铁率
        double Slagprodratio = 0; //出渣率
        double Fe_IN = 0;//入炉品位
        double sinterweight = 0;//烧结矿用量
        double pelletweight = 0;//球团矿用量
        double crudeweight = 0;//块矿用量
        DataTable A_BV = null; //风量
        DataTable A_BVO = null; //富氧
        DataTable HearthCebi = null;

        //double ReturnCoke = 0;
        //double ReturnOre = 0;
        double ConavgOre = 0;
        double ConsumOre = 0;
        double ConavgCoke = 0;
        double ConsumCoke = 0;
        double Consumbigcoke = 0;
        double Conavglitcoke = 0;
        double Consumlitcoke = 0;
        double AvgWinj = 0;
        double slags = 0;
        double irons = 0;
        int hmtpnum = 0;
        int hmtpsum = 0;
        int batnumday = 0;//利用铁间批数计算每日总批数


        double LotAlarm = 0; //槽下料位报警次数
        // List<BATCHHIS_CS> BatCs = null; 

        DataTable BatCs = null; //槽下料位
        DataTable batCode = null; //槽下物料编码
        double Winj = 0;
        double GradeWeight = 0; //一级品重量

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
            string[] columnNames = { "TIMESTAMP", "ID", "ID_DESC", "VALUE", "GROUP_ID" };
            string[] dataFiledNameArr = { "TIMESTAMP", "ID", "ID_DESC", "VALUE", "GROUP_ID" };

            List<BF_TEST> ResultList = new List<BF_TEST>();
            DateTime TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate();

            if (currentTime.Hour >= 20 && currentTime.Hour < 24)
            {
                TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddHours(20);
            }
            else
            {
                TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(-1).AddHours(20);
            }
            int DealDayNumb = 15; //处理3天的数据

            #region 处理数据
            for (int i = 0; i < DealDayNumb; i++)
            {
                DateTime DataEndTime = TodayTime.AddDays(-i);
                try
                {

                    BF_TEST bf_test = null;
                    #region 处理0类型的，求时间段的均值，最大值，最小值等
                    List<CONFIG_BFDAYDATA> ListModel1 = ConfigListModel.FindAll(match => match.DEALTYPE == 0);
                    //处理0与1类型
                    foreach (CONFIG_BFDAYDATA model in ListModel1)
                    {
                        bf_test = Cal_BF_TEST(model, DataEndTime);
                        if (bf_test != null)
                        {
                            ResultList.Add(bf_test);
                        }
                    }
                    #endregion               

                    #region 处理自定义计算方式
                    GetCustomModel(DataEndTime); //获取数据源对象
                    List<BF_TEST> CustomList = GetCustomCal(DataEndTime); //自定义数据
                    if (CustomList != null && CustomList.Count > 0)
                    {
                        ResultList.AddRange(CustomList);
                    }
                    CustomList.Clear();
                    CustomList = null;
                    #endregion
                }
                catch (Exception e)
                {
                    throw;
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
                catch (Exception)
                {
                    throw;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="DealType">数据类型，包括原料计算方法，基础数据计算方法，自定义等</param>
        /// <param name="calType">计算方法</param>
        /// <returns></returns>
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
        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取配置文件
        /// </summary>
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
                    ResultModel = new BF_TEST() { ID = model.D_ID, ID_DESC = model.D_DESC, VALUE = Value, TIMESTAMP = dt, GROUP_ID = 1 };
                }
            }
            return ResultModel;
        }
        private void GetCustomModel(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                modelAV05M_LININGTC = idatabase.Queryable<BKYL.BFES.Plugin.BGBFES7.AV05M_LININGTC>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                modelMATERIAL_BALANCE_BAT = idatabase.Queryable<MATERIAL_BALANCE_BAT>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                modelL3ANALYSI = idatabase.Queryable<BKYL.BFES.Plugin.BGBFES7.L3ANALYSI>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                //modelUniversConfig = idatabase.Queryable<BKYL.BFES.Plugin.BGBFES7.UNIVERSALTE_MONITOR_CONFIG>().ToList();
                modelHEARTH_01H = idatabase.Queryable<HEARTH_01H>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();

                //每日整罐产量
                string sql = $"select sum(TAPWREAL) from TAPPING t where   TAPSTART  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  TAPSTART <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                PondYield = idatabase.GetDouble(sql);
                //每日平均入炉品位
                sql = $"select round(avg(A_FEK),2) from BATCHHIS t where t.A_FEK >0 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                Fe_IN = idatabase.GetDouble(sql);
                //每日每批平均矿耗
                sql = $"select round(avg(I_ORE_IN),2) from MATERIAL_BALANCE_BAT t where t.I_ORE_IN >100 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ConavgOre = idatabase.GetDouble(sql);
                //每日每批平均焦丁量
                sql = $"select round(avg(I_COKE_NUT_WEIGHT),2) from MATERIAL_BALANCE_BAT t where t.I_COKE_NUT_WEIGHT >0 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                Conavglitcoke = idatabase.GetDouble(sql);

                //sql = $"select RETURN_ORE from RP_REPORT_MONTH_AUTO t where timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                //ReturnOre = idatabase.GetDouble(sql);
                ////返焦
                //sql = $"select RETURN_COKE from RP_REPORT_MONTH_AUTO t where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                //ReturnCoke = idatabase.GetDouble(sql);
                //每日每批平均焦耗
                sql = $"select round(avg(I_COKE_IN),2) from MATERIAL_BALANCE_BAT t where t.I_COKE_IN >20 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ConavgCoke = idatabase.GetDouble(sql);
                // 每日平均吨铁烧结矿用量
                sql = $"select round(avg(A_SINTER_AMOUNT),2) from BATCHHIS t where t.A_SINTER_AMOUNT >0 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                sinterweight = idatabase.GetDouble(sql);
                // 每日平均吨铁球团矿用量
                sql = $"select round(avg(A_PELLET_AMOUNT),2) from BATCHHIS t where t.A_PELLET_AMOUNT >0 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                pelletweight = idatabase.GetDouble(sql);
                // 每日平均吨铁块矿用量
                sql = $"select round(avg(A_CRUDE_AMOUNT),2) from BATCHHIS t where t.A_CRUDE_AMOUNT >0 and timestamp  >{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                crudeweight = idatabase.GetDouble(sql);

                //每日总料批数
                sql = $"select sum(tapbetween) from TAPPING t where  tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                batnumday = idatabase.GetInt(sql);
                //读取平均小时喷煤量
                sql = $"select round(AVG(A_WINJ),2)  from AV05M t where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} order by timestamp asc";
                AvgWinj = idatabase.GetDouble(sql);
                //读取渣中S和铁水中S
                sql = $"select round(avg(SLAGS),5) from TAPPING_SLAG_LAB where  SAMPLE_TAKETIME>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  SAMPLE_TAKETIME<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                slags = idatabase.GetDouble(sql);
                sql = $"select round(avg(HMS),5) from TAPPING_IRON_LAB where  SAMPTIME>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  SAMPTIME<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                irons = idatabase.GetDouble(sql);
                //读取铁水温度合格率数据
                sql = $"select count(*) from TAPPING where  HMTP between 1490 and 1510 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                hmtpnum = idatabase.GetInt(sql);
                sql = $"select count(*) from TAPPING where  HMTP >0 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
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
            PondYield = PondYield * 0.994;//每日产量用扣渣后计算
            //燃料比
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000003", ID_DESC = "燃料比", GROUP_ID = 1 };
            ConsumCoke = (ConavgCoke + Conavglitcoke) * batnumday;
            if (PondYield > 0)
            {
                model.VALUE = (ConsumCoke + AvgWinj * 24) * 1000 / PondYield;
                modelList.Add(model);
            }
            //大焦比
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000053", ID_DESC = "大焦比", GROUP_ID = 1 };
            Consumbigcoke = ConavgCoke * batnumday;
            if (PondYield > 0)
            {
                model.VALUE = Consumbigcoke * 1000 / PondYield;
                modelList.Add(model);
            }
            //全焦比
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000062", ID_DESC = "全焦比", GROUP_ID = 1 };
            ConsumCoke = (ConavgCoke + Conavglitcoke) * batnumday;
            if (PondYield > 0)
            {
                model.VALUE = ConsumCoke * 1000 / PondYield;
                modelList.Add(model);
            }
            //焦丁比
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000054", ID_DESC = "焦丁比", GROUP_ID = 1 };
            Consumlitcoke = Conavglitcoke * batnumday;
            if (PondYield > 0)
            {
                model.VALUE = Consumlitcoke * 1000 / PondYield;
                modelList.Add(model);
            }
            //煤比
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000055", ID_DESC = "煤比", GROUP_ID = 1 };
            if (PondYield > 0)
            {
                model.VALUE = AvgWinj * 24 * 1000 / PondYield;
                modelList.Add(model);
            }
            //入炉品位
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000058", ID_DESC = "入炉品位", GROUP_ID = 1 };
            if (PondYield > 0)
            {
                model.VALUE = Fe_IN;
                modelList.Add(model);
            }
            //烧结矿比例
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000060", ID_DESC = "烧结矿比例", GROUP_ID = 1 };
            if (sinterweight + pelletweight + crudeweight > 0)
            {
                model.VALUE = sinterweight / (sinterweight + pelletweight + crudeweight) * 100;
                modelList.Add(model);
            }
            //球团矿比例
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000061", ID_DESC = "球团矿比例", GROUP_ID = 1 };
            if (sinterweight + pelletweight + crudeweight > 0)
            {
                model.VALUE = pelletweight / (sinterweight + pelletweight + crudeweight) * 100;
                modelList.Add(model);
            }
            //块矿比例
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000063", ID_DESC = "块矿比例", GROUP_ID = 1 };
            if (sinterweight + pelletweight + crudeweight > 0)
            {
                model.VALUE = crudeweight / (sinterweight + pelletweight + crudeweight) * 100;
                modelList.Add(model);
            }

            //焦炭负荷
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000002", ID_DESC = "焦炭负荷", GROUP_ID = 1 };
            ConsumOre = ConavgOre * batnumday;
            ConsumCoke = ConavgCoke * batnumday;
            if (ConsumCoke > 0)
            {
                model.VALUE = ConsumOre / ConsumCoke;
                modelList.Add(model);
            }
            //LS硫分配系数
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000016", ID_DESC = "LS", GROUP_ID = 1 };
            if (irons > 0)
            {
                model.VALUE = slags / irons;
                modelList.Add(model);
            }
            //日铁水温度合格率
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000017", ID_DESC = "日铁水温度合格率", GROUP_ID = 1 };
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

            #region //壁体预警次数(16-18)
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000009", ID_DESC = "壁体预警次数(16-18)", GROUP_ID = 1 };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 13801; i <= 13822; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13901; i <= 13921; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 14001; i <= 14004; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion

            #region //壁体预警次数(12-15)
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000010", ID_DESC = "壁体预警次数(12-15)", GROUP_ID = 1 };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 13401; i <= 13406; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13408; i <= 13426; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13501; i <= 13525; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13601; i <= 13624; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13701; i <= 13717; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13719; i <= 13723; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion
            #region //壁体预警次数(7-11)
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000011", ID_DESC = "壁体预警次数(7-11)", GROUP_ID = 1 };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 12901; i <= 12929; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13001; i <= 13029; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13101; i <= 13129; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13201; i <= 13228; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 13301; i <= 13327; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion
            #region //壁体预警次数(1-5)
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000012", ID_DESC = "壁体预警次数(1-5)", GROUP_ID = 1 };
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                int alarmnum = 0;
                for (int i = 10301; i <= 10308; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 10801; i <= 10808; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 1131; i <= 1138; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 11501; i <= 11508; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 11701; i <= 11717; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 12001; i <= 12008; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                for (int i = 12201; i <= 12208; i++)
                {
                    sql = $"select count(*) from AV05M_LININGTC where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and TE04{i} >= (select alarmlow from UNIVERSALTE_MONITOR_CONFIG where address = 'TE04{i}')";
                    alarmnum = idatabase.GetInt(sql);
                    alarmnum += alarmnum;
                }
                model.VALUE = alarmnum;
                modelList.Add(model);
            }
            #endregion
            #region //环碳温度(6-8)
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000019", ID_DESC = "环碳温度(6-8)", GROUP_ID = 1 };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE0410707_2)
                            + modelHEARTH_01H.Average(m => m.TE0410706_2)
                            + modelHEARTH_01H.Average(m => m.TE0410705_2)
                            + modelHEARTH_01H.Average(m => m.TE0410704_2)
                            + modelHEARTH_01H.Average(m => m.TE0410703_2)
                            + modelHEARTH_01H.Average(m => m.TE0410702_2)
                            + modelHEARTH_01H.Average(m => m.TE0410701_2)
                            + modelHEARTH_01H.Average(m => m.TE0410712_2)
                            + modelHEARTH_01H.Average(m => m.TE0410711_2)
                            + modelHEARTH_01H.Average(m => m.TE0410710_2)
                            + modelHEARTH_01H.Average(m => m.TE0410709_2)
                            + modelHEARTH_01H.Average(m => m.TE0410708_2)
                            + modelHEARTH_01H.Average(m => m.TE0410913_2)
                            + modelHEARTH_01H.Average(m => m.TE0410912_2)
                            + modelHEARTH_01H.Average(m => m.TE0410911_2)
                            + modelHEARTH_01H.Average(m => m.TE0410910_2)
                            + modelHEARTH_01H.Average(m => m.TE0410909_2)
                            + modelHEARTH_01H.Average(m => m.TE0410908_2)
                            + modelHEARTH_01H.Average(m => m.TE0410907_2)
                            + modelHEARTH_01H.Average(m => m.TE0410906_2)
                            + modelHEARTH_01H.Average(m => m.TE0410905_2)
                            + modelHEARTH_01H.Average(m => m.TE0410904_2)
                            + modelHEARTH_01H.Average(m => m.TE0410903_2)
                            + modelHEARTH_01H.Average(m => m.TE0410902_2)
                            + modelHEARTH_01H.Average(m => m.TE0410901_2)
                            + modelHEARTH_01H.Average(m => m.TE0410924_2)
                            + modelHEARTH_01H.Average(m => m.TE0410923_2)
                            + modelHEARTH_01H.Average(m => m.TE0410922_2)
                            + modelHEARTH_01H.Average(m => m.TE0410921_2)
                            + modelHEARTH_01H.Average(m => m.TE0410920_2)
                            + modelHEARTH_01H.Average(m => m.TE0410919_2)
                            + modelHEARTH_01H.Average(m => m.TE0410918_2)
                            + modelHEARTH_01H.Average(m => m.TE0410917_2)
                            + modelHEARTH_01H.Average(m => m.TE0410916_2)
                            + modelHEARTH_01H.Average(m => m.TE0410915_2)
                            + modelHEARTH_01H.Average(m => m.TE0410914_2)
                            + modelHEARTH_01H.Average(m => m.TE0411015_2)
                            + modelHEARTH_01H.Average(m => m.TE0411014_2)
                            + modelHEARTH_01H.Average(m => m.TE0411013_2)
                            + modelHEARTH_01H.Average(m => m.TE0411012_2)
                            + modelHEARTH_01H.Average(m => m.TE0411010_2)
                            + modelHEARTH_01H.Average(m => m.TE0411009_2)
                            + modelHEARTH_01H.Average(m => m.TE0411008_2)
                            + modelHEARTH_01H.Average(m => m.TE0411007_2)
                            + modelHEARTH_01H.Average(m => m.TE0411006_2)
                            + modelHEARTH_01H.Average(m => m.TE0411004_2)
                            + modelHEARTH_01H.Average(m => m.TE0411003_2)
                            + modelHEARTH_01H.Average(m => m.TE0411002_2)
                            + modelHEARTH_01H.Average(m => m.TE0411001_2)
                            + modelHEARTH_01H.Average(m => m.TE0411028_2)
                            + modelHEARTH_01H.Average(m => m.TE0411027_2)
                            + modelHEARTH_01H.Average(m => m.TE0411026_2)
                            + modelHEARTH_01H.Average(m => m.TE0411024_2)
                            + modelHEARTH_01H.Average(m => m.TE0411023_2)
                            + modelHEARTH_01H.Average(m => m.TE0411022_2)
                            + modelHEARTH_01H.Average(m => m.TE0411021_2)
                            + modelHEARTH_01H.Average(m => m.TE0411020_2)
                            + modelHEARTH_01H.Average(m => m.TE0411018_2)
                            + modelHEARTH_01H.Average(m => m.TE0411017_2)
                            + modelHEARTH_01H.Average(m => m.TE0411016_2)) / 60;
            modelList.Add(model);
            #endregion

            #region //环碳温度(9-11)
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000020", ID_DESC = "环碳温度(9-11)", GROUP_ID = 1 };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE0411213_2)
                                + modelHEARTH_01H.Average(m => m.TE0411212_2)
                                + modelHEARTH_01H.Average(m => m.TE0411211_2)
                                + modelHEARTH_01H.Average(m => m.TE0411210_2)
                                + modelHEARTH_01H.Average(m => m.TE0411209_2)
                                + modelHEARTH_01H.Average(m => m.TE0411208_2)
                                + modelHEARTH_01H.Average(m => m.TE0411207_2)
                                + modelHEARTH_01H.Average(m => m.TE0411206_2)
                                + modelHEARTH_01H.Average(m => m.TE0411205_2)
                                + modelHEARTH_01H.Average(m => m.TE0411204_2)
                                + modelHEARTH_01H.Average(m => m.TE0411203_2)
                                + modelHEARTH_01H.Average(m => m.TE0411202_2)
                                + modelHEARTH_01H.Average(m => m.TE0411201_2)
                                + modelHEARTH_01H.Average(m => m.TE0411224_2)
                                + modelHEARTH_01H.Average(m => m.TE0411223_2)
                                + modelHEARTH_01H.Average(m => m.TE0411222_2)
                                + modelHEARTH_01H.Average(m => m.TE0411221_2)
                                + modelHEARTH_01H.Average(m => m.TE0411220_2)
                                + modelHEARTH_01H.Average(m => m.TE0411219_2)
                                + modelHEARTH_01H.Average(m => m.TE0411218_2)
                                + modelHEARTH_01H.Average(m => m.TE0411217_2)
                                + modelHEARTH_01H.Average(m => m.TE0411216_2)
                                + modelHEARTH_01H.Average(m => m.TE0411215_2)
                                + modelHEARTH_01H.Average(m => m.TE0411214_2)
                                + modelHEARTH_01H.Average(m => m.TE0411313_2)
                                + modelHEARTH_01H.Average(m => m.TE0411312_2)
                                + modelHEARTH_01H.Average(m => m.TE0411311_2)
                                + modelHEARTH_01H.Average(m => m.TE0411310_2)
                                + modelHEARTH_01H.Average(m => m.TE0411309_2)
                                + modelHEARTH_01H.Average(m => m.TE0411308_2)
                                + modelHEARTH_01H.Average(m => m.TE0411307_2)
                                + modelHEARTH_01H.Average(m => m.TE0411306_2)
                                + modelHEARTH_01H.Average(m => m.TE0411305_2)
                                + modelHEARTH_01H.Average(m => m.TE0411304_2)
                                + modelHEARTH_01H.Average(m => m.TE0411303_2)
                                + modelHEARTH_01H.Average(m => m.TE0411302_2)
                                + modelHEARTH_01H.Average(m => m.TE0411301_2)
                                + modelHEARTH_01H.Average(m => m.TE0411324_2)
                                + modelHEARTH_01H.Average(m => m.TE0411323_2)
                                + modelHEARTH_01H.Average(m => m.TE0411322_2)
                                + modelHEARTH_01H.Average(m => m.TE0411321_2)
                                + modelHEARTH_01H.Average(m => m.TE0411320_2)
                                + modelHEARTH_01H.Average(m => m.TE0411319_2)
                                + modelHEARTH_01H.Average(m => m.TE0411318_2)
                                + modelHEARTH_01H.Average(m => m.TE0411317_2)
                                + modelHEARTH_01H.Average(m => m.TE0411316_2)
                                + modelHEARTH_01H.Average(m => m.TE0411315_2)
                                + modelHEARTH_01H.Average(m => m.TE0411314_2)
                                + modelHEARTH_01H.Average(m => m.TE0411415_2)
                                + modelHEARTH_01H.Average(m => m.TE0411414_2)
                                + modelHEARTH_01H.Average(m => m.TE0411413_2)
                                + modelHEARTH_01H.Average(m => m.TE0411412_2)
                                + modelHEARTH_01H.Average(m => m.TE0411411_2)
                                + modelHEARTH_01H.Average(m => m.TE0411410_2)
                                + modelHEARTH_01H.Average(m => m.TE0411409_2)
                                + modelHEARTH_01H.Average(m => m.TE0411408_2)
                                + modelHEARTH_01H.Average(m => m.TE0411407_2)
                                + modelHEARTH_01H.Average(m => m.TE0411406_2)
                                + modelHEARTH_01H.Average(m => m.TE0411405_2)
                                + modelHEARTH_01H.Average(m => m.TE0411404_2)
                                + modelHEARTH_01H.Average(m => m.TE0411403_2)
                                + modelHEARTH_01H.Average(m => m.TE0411402_2)
                                + modelHEARTH_01H.Average(m => m.TE0411401_2)
                                + modelHEARTH_01H.Average(m => m.TE0411428_2)
                                + modelHEARTH_01H.Average(m => m.TE0411427_2)
                                + modelHEARTH_01H.Average(m => m.TE0411426_2)
                                + modelHEARTH_01H.Average(m => m.TE0411424_2)
                                + modelHEARTH_01H.Average(m => m.TE0411423_2)
                                + modelHEARTH_01H.Average(m => m.TE0411422_2)
                                + modelHEARTH_01H.Average(m => m.TE0411421_2)
                                + modelHEARTH_01H.Average(m => m.TE0411420_2)
                                + modelHEARTH_01H.Average(m => m.TE0411419_2)
                                + modelHEARTH_01H.Average(m => m.TE0411418_2)
                                + modelHEARTH_01H.Average(m => m.TE0411417_2)
                                + modelHEARTH_01H.Average(m => m.TE0411416_2)) / 75;
            modelList.Add(model);
            #endregion

            #region //环碳温度(12-14)
            model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000021", ID_DESC = "环碳温度(12-14)", GROUP_ID = 1 };
            model.VALUE = (modelHEARTH_01H.Average(m => m.TE0411613_2)
                        + modelHEARTH_01H.Average(m => m.TE0411612_2)
                        + modelHEARTH_01H.Average(m => m.TE0411611_2)
                        + modelHEARTH_01H.Average(m => m.TE0411610_2)
                        + modelHEARTH_01H.Average(m => m.TE0411609_2)
                        + modelHEARTH_01H.Average(m => m.TE0411608_2)
                        + modelHEARTH_01H.Average(m => m.TE0411607_2)
                        + modelHEARTH_01H.Average(m => m.TE0411606_2)
                        + modelHEARTH_01H.Average(m => m.TE0411605_2)
                        + modelHEARTH_01H.Average(m => m.TE0411604_2)
                        + modelHEARTH_01H.Average(m => m.TE0411603_2)
                        + modelHEARTH_01H.Average(m => m.TE0411602_2)
                        + modelHEARTH_01H.Average(m => m.TE0411601_2)
                        + modelHEARTH_01H.Average(m => m.TE0411624_2)
                        + modelHEARTH_01H.Average(m => m.TE0411623_2)
                        + modelHEARTH_01H.Average(m => m.TE0411622_2)
                        + modelHEARTH_01H.Average(m => m.TE0411621_2)
                        + modelHEARTH_01H.Average(m => m.TE0411620_2)
                        + modelHEARTH_01H.Average(m => m.TE0411619_2)
                        + modelHEARTH_01H.Average(m => m.TE0411618_2)
                        + modelHEARTH_01H.Average(m => m.TE0411617_2)
                        + modelHEARTH_01H.Average(m => m.TE0411616_2)
                        + modelHEARTH_01H.Average(m => m.TE0411615_2)
                        + modelHEARTH_01H.Average(m => m.TE0411614_2)
                        + modelHEARTH_01H.Average(m => m.TE0411813_2)
                        + modelHEARTH_01H.Average(m => m.TE0411812_2)
                        + modelHEARTH_01H.Average(m => m.TE0411811_1)
                        + modelHEARTH_01H.Average(m => m.TE0411810_2)
                        + modelHEARTH_01H.Average(m => m.TE0411809_2)
                        + modelHEARTH_01H.Average(m => m.TE0411808_2)
                        + modelHEARTH_01H.Average(m => m.TE0411807_2)
                        + modelHEARTH_01H.Average(m => m.TE0411806_2)
                        + modelHEARTH_01H.Average(m => m.TE0411805_2)
                        + modelHEARTH_01H.Average(m => m.TE0411804_2)
                        + modelHEARTH_01H.Average(m => m.TE0411803_2)
                        + modelHEARTH_01H.Average(m => m.TE0411802_2)
                        + modelHEARTH_01H.Average(m => m.TE0411801_2)
                        + modelHEARTH_01H.Average(m => m.TE0411824_2)
                        + modelHEARTH_01H.Average(m => m.TE0411823_2)
                        + modelHEARTH_01H.Average(m => m.TE0411822_2)
                        + modelHEARTH_01H.Average(m => m.TE0411821_2)
                        + modelHEARTH_01H.Average(m => m.TE0411820_2)
                        + modelHEARTH_01H.Average(m => m.TE0411819_2)
                        + modelHEARTH_01H.Average(m => m.TE0411818_2)
                        + modelHEARTH_01H.Average(m => m.TE0411817_2)
                        + modelHEARTH_01H.Average(m => m.TE0411816_2)
                        + modelHEARTH_01H.Average(m => m.TE0411815_2)
                        + modelHEARTH_01H.Average(m => m.TE0411814_2)
                        + modelHEARTH_01H.Average(m => m.TE0411904_2)
                        + modelHEARTH_01H.Average(m => m.TE0411903_2)
                        + modelHEARTH_01H.Average(m => m.TE0411902_2)
                        + modelHEARTH_01H.Average(m => m.TE0411901_2)
                        + modelHEARTH_01H.Average(m => m.TE0411908_2)
                        + modelHEARTH_01H.Average(m => m.TE0411907_2)
                        + modelHEARTH_01H.Average(m => m.TE0411906_2)
                        + modelHEARTH_01H.Average(m => m.TE0411905_2)) / 56;
            modelList.Add(model);
            #endregion

            //#region //环碳温度(7-8)
            //model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000022", ID_DESC = "环碳温度(7-8)" };
            //model.VALUE = (modelHEARTH_01H.Average(m => m.TE_114A8L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A9L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A7L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A5L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A6L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A13L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A14L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A12L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A10L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A11L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A4L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A1L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A2L1)
            //            + modelHEARTH_01H.Average(m => m.TE_114A3L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A3L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A4L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A1L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A2L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A7L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A8L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A5L1)
            //            + modelHEARTH_01H.Average(m => m.TE_115A6L1)) / 22;

            //modelList.Add(model);
            //#endregion

            //#region //环碳温度(9-13)
            //model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000023", ID_DESC = "环碳温度(9-13)" };
            //model.VALUE = (modelHEARTH_01H.Average(m => m.TE_116A1L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A2L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A3L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A4L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A5L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A6L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A7L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A8L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A9L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A10L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A11L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A12L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A13L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A14L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A15L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A16L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A17L1)
            //            + modelHEARTH_01H.Average(m => m.TE_116A18L1)) / 18;
            //#endregion
            //modelList.Add(model);

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
                model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000006", ID_DESC = "炉喉钢砖均值", GROUP_ID = 1 };
                model.VALUE = (modelAV05M_LININGTC.Average(m => m.TE0414001)
                        + modelAV05M_LININGTC.Average(m => m.TE0414002)
                        + modelAV05M_LININGTC.Average(m => m.TE0414003)
                        + modelAV05M_LININGTC.Average(m => m.TE0414004)) / 4;
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
        /// <summary>
        /// 读取一天统计的所有物料重量
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<BATCHHIS_TOTAL> ReadBatchhisTotal(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                return idatabase.GetList<BATCHHIS_TOTAL>($"select * from BATCHHIS_TOTAL where timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}" +
                    $" and timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)}").ToList();
            }
        }
        //根据排料重量计算的检化验项目值
        private double GetMaterAnaModel(List<BATCHHIS_TOTAL> ModelList, DateTime dt, string Item, int MatType)
        {
            double ItemValue = 0;
            List<BATCHHIS_TOTAL> TempModellist = new List<BATCHHIS_TOTAL>();
            double Weight = 0;
            double FenZi = 0;
            if (MatType == 5)
            {
                TempModellist = ModelList.FindAll(m => m.CODE >= 30 && m.CODE <= 49 && m.CODE != 39);
            }
            else if (MatType == 6)
            {
                TempModellist = ModelList.FindAll(m => m.CODE >= 50 && m.CODE <= 59);
            }
            else if (MatType == 7)
            {
                TempModellist = ModelList.FindAll(m => m.CODE >= 60 && m.CODE <= 79);
            }
            else if (MatType == 8)
            {
                TempModellist = ModelList.FindAll(m => m.CODE >= 80 && m.CODE <= 99);
            }
            else if (MatType == 2)
            {
                TempModellist = ModelList.FindAll(m => m.CODE >= 10 && m.CODE <= 19);
            }
            if (TempModellist.Count > 0)
            {
                foreach (BATCHHIS_TOTAL item in TempModellist)
                {
                    double value = GetItemValue(item.CODE, Item, dt);
                    if (value != 0)
                    {
                        Weight += item.WEIGHT.ToDouble(0);
                        FenZi += item.WEIGHT.ToDouble(0) * value;
                    }

                }
                if (Weight > 0)
                {
                    ItemValue = FenZi / Weight;
                }
            }
            return ItemValue;
        }
        //获取数据库化验项的值
        private double GetItemValue(int Code, string AnaItem, DateTime dt)
        {
            double Value = 0;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                //读取天平均，如果读取不到，读取最新的数据
                string sql = $"select round(avg({AnaItem}),4) from L3ANALYSI where timestamp<{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}" +
                             $" and timestamp>={DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and M_CODE={Code} and {AnaItem}>0";
                Value = idatabase.GetDouble(sql);
                if (Value == 0)
                {
                    sql = $"select {AnaItem} from L3ANALYSI where timestamp= (select max(timestamp) from  L3ANALYSI where" +
                          $" timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and M_CODE={Code} and {AnaItem}>0)" +
                             $" and M_CODE={Code}";
                    Value = idatabase.GetDouble(sql);
                }
            }
            return Value;
        }



    }

}
