using BFES.DataAccess;
using BFES.DataAccess.Common;
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
    public class Job_CalDayData_AG : BaseJob
    {
        List<CONFIG_BFDAYDATA> ConfigListModel = null;
        string log = "";
        List<AV05M_LININGTC> modelAV05M_LININGTC = null;
        List<MATERIAL_BALANCE_BAT> modelMATERIAL_BALANCE_BAT = null;
        MATERIAL_BALANCE_DAY materialDayModel = null;
        List<L3ANALYSI> modelL3ANALYSI = null;
        List<BATCHHIS_TOTAL> BatchhisTotalList = null;
        double AddWater = 0; //补水量
        double PondYield = 0;//实际产量
        DataTable A_BV = null; //风量

        DataTable HearthCebi = null;


        double LotAlarm = 0; //槽下料位报警次数

        DataTable BatCs = null; //槽下料位
        DataTable batCode = null; //槽下物料编码
        double Winj = 0;
        double ReturnCoke = 0;
        double ReturnOre = 0;

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
            string[] columnNames = { "GROUP_ID", "TIMESTAMP", "ID", "ID_DESC", "VALUE" };
            string[] dataFiledNameArr = { "GROUP_ID", "TIMESTAMP", "ID", "ID_DESC", "VALUE" };

            List<BF_TEST> ResultList = new List<BF_TEST>();
            DateTime TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(-35);

            //if (currentTime.Hour >= 20 && currentTime.Hour < 24)
            //{
            //    TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddHours(20);
            //}
            //else
            //{
            //    TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(-1).AddHours(20);
            //}
            int DealDayNumb = 10; //处理N天的数据

            #region 处理数据
            for (int i = 0; i < DealDayNumb; i++)
            {
                try
                {
                    DateTime DataEndTime = TodayTime.AddDays(-i);
                    BF_TEST bf_test = null;
                    #region 处理0类型的，求时间段的均值，最大值，最小值等
                    List<CONFIG_BFDAYDATA> ListModel1 = ConfigListModel.FindAll(match => match.DEALTYPE == 0 || match.DEALTYPE == 1);
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
                    List<CONFIG_BFDAYDATA> ListModel2 = ConfigListModel.FindAll(match => match.DEALTYPE == 99);
                    foreach (CONFIG_BFDAYDATA model in ListModel2)
                    {
                        //GetCustomModel(DataEndTime); //获取数据源对象
                        //List<BF_TEST> CustomList = GetCustomCal(DataEndTime); //自定义数据
                        //if (CustomList != null && CustomList.Count > 0)
                        //{
                        //    ResultList.AddRange(CustomList);
                        //}
                        //CustomList.Clear();
                        //CustomList = null;
                    }
                    #endregion
                }
                catch (Exception)
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
                    ResultModel = new BF_TEST() { GROUP_ID = model.GROUP_ID, ID = model.D_ID, ID_DESC = model.D_DESC, VALUE = Value, TIMESTAMP = InsertTime };
                }
            }
            return ResultModel;
        }
        private void GetCustomModel(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource)) //AV01H_WINJ ,timestamp,a_winj_01h
            {
                string sql = "";
                //读取天累计喷煤量
                sql = $"select sum(a_winj_01h) from AV01H_WINJ where timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                Winj = idatabase.GetDouble(sql); //喷煤
                modelAV05M_LININGTC = idatabase.Queryable<AV05M_LININGTC>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                modelMATERIAL_BALANCE_BAT = idatabase.Queryable<MATERIAL_BALANCE_BAT>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();

                //读取一天物料平衡数据20点到20点物料统计
                sql = $"select * from MATERIAL_BALANCE_DAY where timestamp={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                materialDayModel = idatabase.GetSingle<MATERIAL_BALANCE_DAY>(sql);
                modelL3ANALYSI = idatabase.Queryable<L3ANALYSI>().Where(m => m.TIMESTAMP > dt.AddDays(-1) && m.TIMESTAMP <= dt).ToList();
                //返矿
                sql = $"select sum(net_wt) from MES_TYMIRD3_IOOP t where t.supply_dept_code=6220 and t.mat_prod_code= '4724' and  to_date(rec_create_time, 'yyyymmddhh24miss')>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  to_date(rec_create_time, 'yyyymmddhh24miss')<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ReturnOre = idatabase.GetDouble(sql);
                //返焦
                sql = $"select sum(net_wt) from MES_TYMIRD3_IOOP t where t.supply_dept_code=6220 and t.mat_prod_code= '487' and  to_date(rec_create_time, 'yyyymmddhh24miss')>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  to_date(rec_create_time, 'yyyymmddhh24miss')<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                ReturnCoke = idatabase.GetDouble(sql);
                //读取风量
                sql = $"select A_BV5m  from AV05M t where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} order by timestamp asc";
                A_BV = idatabase.GetDataTable(sql);
                //读取槽下料位
                sql = $"select *  from BATCHHIS_CS t where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} order by timestamp asc";
                BatCs = idatabase.GetDataTable(sql);
                //读取槽下物料编码
                sql = $"select *  from BATCHHISCODE t where  timestamp = (select max(timestamp) from BATCHHISCODE)";
                batCode = idatabase.GetDataTable(sql);
                //LotAlarm
                sql = $"select C_SLOT_ALARM from BFPAR t";
                LotAlarm = idatabase.GetDouble(sql);
                //补水量
                sql = $"select RSZGLJ+RSZGLJ2 from AV05M where timestamp =(select max(timestamp) from AV05M where timestamp<={ DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} )";
                double LastAdd = idatabase.GetDouble(sql);
                sql = $"select RSZGLJ+RSZGLJ2 from AV05M where timestamp =(select max(timestamp) from AV05M where timestamp<={ DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} )";
                double NowAdd = idatabase.GetDouble(sql);
                AddWater = NowAdd - LastAdd;
                //榜单产量20点至20点总重乘以渣系数0.99
                sql = $"select sum(net_weight)*0.99 from MES_TMMIRF9_IOOP t where to_date(ponder_time,'yyyymmddhh24miss')>{ DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} "
                       + $"    and    to_date(ponder_time,'yyyymmddhh24miss')<={ DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and unit_no='6' ";
                PondYield = idatabase.GetDouble(sql);
                BatchhisTotalList = ReadBatchhisTotal(dt);

                #region 读取侧壁温度点
                sql = $"select LEGENDNAME from HEARTHCONFIG p inner join (select max(b.depth) as depth, groupindex from HEARTHCONFIG b "
                        + "where b.groupindex in (select groupindex from hearthgroup t  where t.groupindex > = 7) group by b.groupindex) a "
                        + " on p.groupindex = a.groupindex   and p.depth = a.depth";

                DataTable tempTable = idatabase.GetDataTable(sql);
                string FieldName = "";
                if (tempTable != null && tempTable.Rows.Count > 0)
                {
                    foreach (DataRow item in tempTable.Rows)
                    {
                        FieldName += $"round(avg({item["LEGENDNAME"].ToString()}),2),";
                    }
                    FieldName = FieldName.Remove(FieldName.Length - 1);
                }
                sql = $"select {FieldName} from HEARTH where  timestamp>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";

                HearthCebi = idatabase.GetDataTable(sql);
                #endregion

            }
        }
        private List<BF_TEST> GetCustomCal(DateTime dt)
        {
            DateTime InsertTime = dt.ToString("yyyy-MM-dd").ToDate();
            List<BF_TEST> modelList = new List<BF_TEST>();
            BF_TEST model = null;
            double ore = 0;
            if (PondYield > 0)
            {
                //产量
                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000001", ID_DESC = "产量" };
                model.VALUE = PondYield;
                modelList.Add(model);

                //利用系数
                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000052", ID_DESC = "利用系数" };
                model.VALUE = PondYield / 1280.00;
                modelList.Add(model);

                //煤比
                model = new BF_TEST { TIMESTAMP = InsertTime, ID = "10000000000055", ID_DESC = "煤比" };
                model.VALUE = Winj / PondYield * 1000;
                modelList.Add(model);

                if (materialDayModel != null)
                {
                    ore = materialDayModel.I_ORE_IN.ToDouble();
                    //燃料比
                    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000003", ID_DESC = "操作燃料比" };
                    model.VALUE = (materialDayModel.I_COKE_IN + materialDayModel.I_COKE_NUT_WEIGHT + Winj) / PondYield * 1000;
                    modelList.Add(model);
                    //焦比
                    model = new BF_TEST { TIMESTAMP = InsertTime, ID = "10000000000053", ID_DESC = "焦比" };
                    model.VALUE = materialDayModel.I_COKE_IN / PondYield * 1000;
                    modelList.Add(model);
                    //焦丁比
                    model = new BF_TEST { TIMESTAMP = InsertTime, ID = "10000000000054", ID_DESC = "焦丁比" };
                    model.VALUE = materialDayModel.I_COKE_NUT_WEIGHT / PondYield * 1000;
                    modelList.Add(model);
                    //负荷
                    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000002", ID_DESC = "负荷" };
                    if ((materialDayModel.I_COKE_IN + materialDayModel.I_COKE_NUT_WEIGHT) > 0)
                    {
                        model.VALUE = ore / (materialDayModel.I_COKE_IN + materialDayModel.I_COKE_NUT_WEIGHT);
                        modelList.Add(model);
                    }
                    #region   //返焦粉率 
                    if (materialDayModel.I_COKE_IN.ToDouble(0) > 0)
                    {
                        model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000035", ID_DESC = "返焦粉率" };
                        model.VALUE = ReturnCoke / materialDayModel.I_COKE_IN.ToDouble(0) * 100;
                        modelList.Add(model);
                    }
                    #endregion
                    #region 计算入炉品位            
                    if (materialDayModel.I_ORE_IN > 0)
                    {
                        model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000058", ID_DESC = "入炉品位" };
                        model.VALUE = materialDayModel.I_MHMT_OUT * 0.945 / materialDayModel.I_ORE_IN * 100;
                        modelList.Add(model);
                    }
                    #endregion
                }
                #region //返粉率（块矿+球团+烧结） 
                if (ore > 0)
                {
                    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000040", ID_DESC = "返粉率" };
                    model.VALUE = ReturnOre / ore * 100;
                    modelList.Add(model);
                }
                #endregion


            }
            #region 软水总管
            model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000028", ID_DESC = "软水补水量", VALUE = AddWater };
            modelList.Add(model);
            #endregion
            if (modelMATERIAL_BALANCE_BAT != null && modelMATERIAL_BALANCE_BAT.Count > 0)
            {
                #region//Zn负荷 
                if (PondYield > 0)
                {
                    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000050", ID_DESC = "Zn负荷" };
                    model.VALUE = modelMATERIAL_BALANCE_BAT.Sum(m => m.ZNO_SINTER + m.ZNO_PELLET + m.ZNO_CRUDEORE + m.ZNO_COKE + m.ZNO_COAL) / PondYield * 1000 * 65 / 81.0; //单位g
                    modelList.Add(model);
                }
                #endregion
                #region    //(K2O+Na2O)负荷 
                if (PondYield > 0)
                {
                    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000051", ID_DESC = "(K2O+Na2O)负荷" };
                    model.VALUE = (modelMATERIAL_BALANCE_BAT.Sum(m => m.K2O_SINTER + m.K2O_PELLET + m.K2O_CRUDEORE + m.K2O_COKE
                    + m.K2O_COAL) + modelMATERIAL_BALANCE_BAT.Sum(m => m.NA2O_SINTER + m.NA2O_PELLET + m.NA2O_CRUDEORE + m.NA2O_COKE + m.K2O_COAL)) / PondYield;
                    modelList.Add(model);
                }

                #endregion

                modelMATERIAL_BALANCE_BAT.Clear();
                modelMATERIAL_BALANCE_BAT = null;

            }
            if (modelAV05M_LININGTC != null && modelAV05M_LININGTC.Count > 0)
            {
                #region  //10-12段

                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000009", ID_DESC = "炉身温度10~12" };
                model.VALUE = (modelAV05M_LININGTC.FindAll(m => m.CLP10_TI1 > 0).Average(m => m.CLP10_TI1)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP10_TI2 > 0).Average(m => m.CLP10_TI2)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP10_TI3 > 0).Average(m => m.CLP10_TI3)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP10_TI4 > 0).Average(m => m.CLP10_TI4)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP10_TI5 > 0).Average(m => m.CLP10_TI5)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP10_TI6 > 0).Average(m => m.CLP10_TI6)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP10_TI7 > 0).Average(m => m.CLP10_TI7)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP10_TI8 > 0).Average(m => m.CLP10_TI8)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI1 > 0).Average(m => m.CLP11_TI1)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI2 > 0).Average(m => m.CLP11_TI2)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI3 > 0).Average(m => m.CLP11_TI3)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI4 > 0).Average(m => m.CLP11_TI4)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI5 > 0).Average(m => m.CLP11_TI5)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI6 > 0).Average(m => m.CLP11_TI6)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI7 > 0).Average(m => m.CLP11_TI7)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP11_TI8 > 0).Average(m => m.CLP11_TI8)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP12_TI1 > 0).Average(m => m.CLP12_TI1)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP12_TI2 > 0).Average(m => m.CLP12_TI2)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP12_TI3 > 0).Average(m => m.CLP12_TI3)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP12_TI4 > 0).Average(m => m.CLP12_TI4)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP14_TI1 > 0).Average(m => m.CLP14_TI1)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP14_TI2 > 0).Average(m => m.CLP14_TI2)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP14_TI3 > 0).Average(m => m.CLP14_TI3)
                             + modelAV05M_LININGTC.FindAll(m => m.CLP14_TI4 > 0).Average(m => m.CLP14_TI4)) / 24;

                modelList.Add(model);
                #endregion

                #region  //炉腰温度7
                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000010", ID_DESC = "炉腰温度7" };
                model.VALUE = (modelAV05M_LININGTC.FindAll(m => m.CLP7_TI1 > 0).Average(m => m.CLP7_TI1)
                               + modelAV05M_LININGTC.FindAll(m => m.CLP7_TI2 > 0).Average(m => m.CLP7_TI2)
                               + modelAV05M_LININGTC.FindAll(m => m.CLP7_TI3 > 0).Average(m => m.CLP7_TI3)
                               + modelAV05M_LININGTC.FindAll(m => m.CLP7_TI4 > 0).Average(m => m.CLP7_TI4)
                               + modelAV05M_LININGTC.FindAll(m => m.CLP7_TI5 > 0).Average(m => m.CLP7_TI5)
                               + modelAV05M_LININGTC.FindAll(m => m.CLP7_TI6 > 0).Average(m => m.CLP7_TI6)
                               + modelAV05M_LININGTC.FindAll(m => m.CLP7_TI7 > 0).Average(m => m.CLP7_TI7)
                               + modelAV05M_LININGTC.FindAll(m => m.CLP7_TI8 > 0).Average(m => m.CLP7_TI8)
                               ) / 8;
                #endregion
                modelList.Add(model);

                #region  //炉腹温度6 

                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000011", ID_DESC = "炉腹温度6" };
                model.VALUE = (modelAV05M_LININGTC.FindAll(m => m.CLP6_TI1 > 0).Average(m => m.CLP6_TI1)
                              + modelAV05M_LININGTC.FindAll(m => m.CLP6_TI2 > 0).Average(m => m.CLP6_TI2)
                              + modelAV05M_LININGTC.FindAll(m => m.CLP6_TI3 > 0).Average(m => m.CLP6_TI3)
                              + modelAV05M_LININGTC.FindAll(m => m.CLP6_TI4 > 0).Average(m => m.CLP6_TI4)
                              + modelAV05M_LININGTC.FindAll(m => m.CLP6_TI5 > 0).Average(m => m.CLP6_TI5)
                              + modelAV05M_LININGTC.FindAll(m => m.CLP6_TI6 > 0).Average(m => m.CLP6_TI6)
                              + modelAV05M_LININGTC.FindAll(m => m.CLP6_TI7 > 0).Average(m => m.CLP6_TI7)
                              + modelAV05M_LININGTC.FindAll(m => m.CLP6_TI8 > 0).Average(m => m.CLP6_TI8)) / 8;
                #endregion
                modelList.Add(model);

                modelAV05M_LININGTC.Clear();
                modelAV05M_LININGTC = null;

            }
            if (modelL3ANALYSI != null && modelL3ANALYSI.Count > 0)
            {
                #region 焦炭平均粒度 M_SZ60*60+M_S40_60*50+M_S40_25*32.5+M_S_25*25

                List<L3ANALYSI> CokeList = modelL3ANALYSI.FindAll(m => m.M_CODE >= 30 && m.M_CODE <= 49 && m.M_SZ60 > 0);
                double value = 0;
                if (CokeList != null && CokeList.Count > 0)
                {
                    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000034", ID_DESC = "焦炭平均粒度" };

                    foreach (L3ANALYSI item in CokeList)
                    {
                        value += (item.M_SZ60 * 60 + item.M_S40_60 * 50 + item.M_S40_25 * 32.5 + item.M_S_25 * 25) / 100;
                    }
                    model.VALUE = value / CokeList.Count;
                    modelList.Add(model);
                }
                else
                {
                    L3ANALYSI l3temp = null;

                    using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                    {
                        string sql = "select * from L3ANALYSI where timestamp = (select max(timestamp) from L3ANALYSI where  M_CODE>=30 and M_CODE<=49  and M_S40_60>0)";
                        l3temp = idatabase.GetSingle<L3ANALYSI>(sql);
                    }
                    if (l3temp != null)
                    {
                        model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000034", ID_DESC = "焦炭平均粒度" };
                        model.VALUE = (l3temp.M_SZ60 * 60 + l3temp.M_S40_60 * 50 + l3temp.M_S40_25 * 32.5 + l3temp.M_S_25 * 25) / 100;
                        modelList.Add(model);
                    }
                }
                #endregion

                #region //块矿平均粒度 M_SZ10 M_S_6Z3
                List<L3ANALYSI> CurdeList = modelL3ANALYSI.FindAll(m => m.M_CODE >= 80 && m.M_CODE <= 99 && m.M_S_6Z3 > 0);
                value = 0;
                if (CurdeList != null && CurdeList.Count > 0)
                {
                    model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000045", ID_DESC = "块矿平均粒度" };
                    foreach (L3ANALYSI item in CurdeList)
                    {
                        value += (item.M_SZ10 * 12.5 + item.M_S_6Z3 * 6.3) / 100;
                    }
                    model.VALUE = value / CurdeList.Count;
                    modelList.Add(model);
                }
                else
                {
                    L3ANALYSI l3temp = null;

                    using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                    {
                        string sql = "select * from L3ANALYSI where timestamp = (select max(timestamp) from L3ANALYSI where  M_CODE>=80 and M_CODE<=99  and M_SZ10>0)";
                        l3temp = idatabase.GetSingle<L3ANALYSI>(sql);
                    }
                    if (l3temp != null)
                    {
                        model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000045", ID_DESC = "块矿平均粒度" };
                        model.VALUE = (l3temp.M_SZ10 * 12.5 + l3temp.M_S_6Z3 * 6.3) / 100;
                        modelList.Add(model);
                    }

                }

                #endregion

                modelL3ANALYSI.Clear();
                modelL3ANALYSI = null;
            }
            #region 减风次数
            if (A_BV != null && A_BV.Rows.Count > 0)
            {
                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000023", ID_DESC = "减风次数(10%,3000基准）" };
                double TempValueLast = 0;
                double TempValueNow = 0;
                int Count = 0;
                for (int i = 0; i < A_BV.Rows.Count - 1; i++)
                {
                    TempValueLast = A_BV.Rows[i][0].ToDouble();
                    TempValueNow = A_BV.Rows[i + 1][0].ToDouble();
                    if (TempValueLast >= 2700 && TempValueNow <= 2700)
                    {
                        Count++;
                    }
                }
                model.VALUE = Count;
                modelList.Add(model);
            }
            #endregion

            #region 槽下料位
            if (BatCs != null && BatCs.Rows.Count > 0 && batCode != null && batCode.Rows.Count > 0)
            {
                int CokeLw = 0;
                int SinterLw = 0;
                for (int i = 0; i < batCode.Columns.Count; i++)
                {
                    int Code = batCode.Rows[0][i].ToInt();
                    if (Code >= 30 && Code <= 49 && Code != 39)
                    {
                        double TempValueLast = 0;
                        double TempValueNow = 0;
                        for (int j = 0; j < BatCs.Rows.Count - 1; j++)
                        {
                            TempValueLast = BatCs.Rows[j][i].ToDouble();
                            TempValueNow = BatCs.Rows[j + 1][i].ToDouble();
                            if (TempValueLast >= LotAlarm && TempValueNow < LotAlarm)
                            {
                                CokeLw++;
                            }
                        }
                    }
                    if (Code >= 50 && Code <= 59)
                    {
                        double TempValueLast = 0;
                        double TempValueNow = 0;
                        for (int j = 0; j < BatCs.Rows.Count - 1; j++)
                        {
                            TempValueLast = BatCs.Rows[j][i].ToDouble();
                            TempValueNow = BatCs.Rows[j + 1][i].ToDouble();
                            if (TempValueLast >= LotAlarm && TempValueNow < LotAlarm)
                            {
                                SinterLw++;
                            }
                        }
                    }
                }
                modelList.Add(new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000024", ID_DESC = "焦炭槽位管理(低于8m次数)", VALUE = CokeLw });
                modelList.Add(new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000025", ID_DESC = "烧结槽位管理(低于8m次数)", VALUE = SinterLw });

            }
            #endregion

            #region 侧壁最高温度
            if (HearthCebi != null && HearthCebi.Rows.Count > 0)
            {
                model = new BF_TEST() { TIMESTAMP = InsertTime, ID = "10000000000026", ID_DESC = "侧壁最高温度", VALUE = 0 };
                double tempValue = 0;
                for (int i = 0; i < HearthCebi.Columns.Count; i++)
                {
                    tempValue = HearthCebi.Rows[0][i].ToDouble();
                    if (tempValue > model.VALUE)
                    {
                        model.VALUE = tempValue;
                    }
                }
                modelList.Add(model);
            }
            #endregion

            #region 处理检化验项目及炉料结构
            if (BatchhisTotalList != null && BatchhisTotalList.Count > 0)
            {
                #region 焦炭
                model = new BF_TEST() { ID = "10000000000029", ID_DESC = "焦炭M40", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_M40", 5), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000030", ID_DESC = "焦炭CRI", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_CRI", 5), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000031", ID_DESC = "焦炭CSR", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_CSR", 5), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000032", ID_DESC = "焦炭灰分", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_COKEASH", 5), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000033", ID_DESC = "焦炭M10", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_M10", 5), TIMESTAMP = InsertTime };
                modelList.Add(model);
                #endregion
                #region 烧结矿
                model = new BF_TEST() { ID = "10000000000036", ID_DESC = "烧结FeO", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_FEO_CONT", 6), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000037", ID_DESC = "碱度", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_BASICITY", 6), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000039", ID_DESC = "烧结转鼓指数（6.3mm）", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_TI6Z3", 6), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000038", ID_DESC = "烧结RDI（3.15mm）", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_RDI_3Z15", 6), TIMESTAMP = InsertTime };
                modelList.Add(model);
                #endregion
                #region 球团矿
                model = new BF_TEST() { ID = "10000000000041", ID_DESC = "球团FeO", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_FEO_CONT", 7), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000042", ID_DESC = "球团转鼓指数（6.3mm）", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_TI6Z3", 7), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000043", ID_DESC = "还原膨胀率", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_RSI", 7), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000044", ID_DESC = "球团还原度指数RI", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_RI", 7), TIMESTAMP = InsertTime };
                modelList.Add(model);
                #endregion
                #region 块矿
                model = new BF_TEST() { ID = "10000000000046", ID_DESC = "块矿低温还原强度（6.3mm）", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_RDIZ6Z3", 8), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000048", ID_DESC = "块矿水分", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_MOISTURE", 8), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000049", ID_DESC = "块矿还原度指数RI", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_RI", 8), TIMESTAMP = InsertTime };
                modelList.Add(model);
                #endregion
                #region 煤粉
                model = new BF_TEST() { ID = "10000000000056", ID_DESC = "煤粉固定碳", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_C_CONT", 2), TIMESTAMP = InsertTime };
                modelList.Add(model);
                model = new BF_TEST() { ID = "10000000000057", ID_DESC = "小于200目", VALUE = GetMaterAnaModel(BatchhisTotalList, dt, "M_SM_200", 2), TIMESTAMP = InsertTime };
                modelList.Add(model);

                #endregion
                #region 计算炉料结构
                double PelletWeight = 0;
                double CrudeWeight = 0;
                double SinterWeight = 0;
                double OreWeight = 0;
                foreach (BATCHHIS_TOTAL item in BatchhisTotalList)
                {
                    if (item.CODE >= 50 && item.CODE <= 59)
                    {
                        SinterWeight += item.WEIGHT.ToDouble();
                        OreWeight += item.WEIGHT.ToDouble();
                    }
                    if (item.CODE >= 60 && item.CODE <= 79)
                    {
                        PelletWeight += item.WEIGHT.ToDouble();
                        OreWeight += item.WEIGHT.ToDouble();
                    }
                    if (item.CODE >= 80 && item.CODE <= 99)
                    {
                        CrudeWeight += item.WEIGHT.ToDouble();
                        OreWeight += item.WEIGHT.ToDouble();
                    }
                }
                if (OreWeight > 0)
                {
                    model = new BF_TEST() { ID = "10000000000059", ID_DESC = "熟料率", VALUE = (SinterWeight + PelletWeight) / OreWeight * 100, TIMESTAMP = InsertTime };
                    modelList.Add(model);
                    model = new BF_TEST() { ID = "10000000000060", ID_DESC = "烧结比例", VALUE = SinterWeight / OreWeight * 100, TIMESTAMP = InsertTime };
                    modelList.Add(model);
                    model = new BF_TEST() { ID = "10000000000061", ID_DESC = "球团比例", VALUE = PelletWeight / OreWeight * 100, TIMESTAMP = InsertTime };
                    modelList.Add(model);
                }
                #endregion

            }
            #endregion




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
