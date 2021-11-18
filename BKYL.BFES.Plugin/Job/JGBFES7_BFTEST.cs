using BFES.DataAccess;
using BFES.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using BKYL.BFES.Plugin.Models.JG;
using System.Reflection;
using BKYL.BFES.Plugin.Models;

namespace BKYL.BFES.Plugin 
{
    /// <summary>
    /// 计算高炉体检数据
    /// </summary>
    public class JGBFES7_BFTEST : BaseJob
    {
        public override void InitTask()
        {

        }
        public override void RunTask(DateTime currentTime)
        {

            string log = "";
            string[] columnNames = { "TIMESTAMP", "ID", "ID_DESC", "VALUE" };
            string[] dataFiledNameArr = { "TIMESTAMP", "ID", "ID_DESC", "VALUE" };
            TestData model = new TestData(base.S_DBSource);

            List<BF_TEST> ResultList = new List<Plugin.BF_TEST>();
            ResultList = model.Cal(currentTime);
            #region 插入数据
            if (ResultList.Count > 0)
            {
                try
                {
                    using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                    {

                        DateTime endTime = ResultList.Max(m => m.TIMESTAMP);
                        DateTime startTime = ResultList.Min(m => m.TIMESTAMP);
                        int delete = 0;
                        idatabase.BeginTran();
                        delete = idatabase.ExecuteCommand($"delete from BF_TEST where timestamp>={DataBaseFactory.ConvertTimeString(startTime, base.S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)}");
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
                    ResultList.Clear(); ResultList = null;
                }

            }
            #endregion
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
        }
    }
    public class TestData
    {
        DatabaseSource m_dbs;
        public TestData(DatabaseSource dbs)
        {
            m_dbs = dbs;
        }
        public List<BF_TEST> Cal(DateTime dt)
        {
          

            List<BF_TEST> ResultList = new List<BF_TEST>();
            DateTime TodayTime = dt.ToString("yyyy-MM-dd").ToDate();


            int DealDayNumb = 3; //处理3天的数据
            #region 处理数据
            for (int i = 1; i <= DealDayNumb; i++)
            {
                DateTime DataEndTime = TodayTime.AddDays(-i);
                try
                {
                    ResultList.AddRange(DealLtcReportData(DataEndTime));
                    ResultList.AddRange(DealEpxertAvg(DataEndTime));
                    ResultList.AddRange(DealJHY(DataEndTime));
                    ResultList.Add(DealReduceBv(DataEndTime));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            #endregion
            return ResultList;  
        }
        public List<BF_TEST> DealLtcReportData(DateTime dt)
        {
            BF_TEST temp = null;
            List<BF_TEST> resultList = new List<BF_TEST>();
            string sql = "";
            DataTable dTable = null;

            #region  LTCREPORT_GL_HUIZONG_YBTJ 10000000000001-10000000000018
            sql = $"select 产量,高炉锌负荷,碱负荷,焦丁比,硫负荷,炉渣含AL2O3,炉渣含MGO,煤比,燃料比,入炉焦比,入炉品位,生铁含S,钛负荷,综合负荷,综合焦比,全天风温,全天风压,全天风量 from LTCREPORT_GL_HUIZONG_YBTJ where timestamp = {DataBaseFactory.ConvertTimeString(dt, m_dbs.DBType)}";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                temp = new BF_TEST() { ID = "10000000000001", ID_DESC = "产量", TIMESTAMP = dt, VALUE = dTable.Rows[0][0].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000002", ID_DESC = "高炉锌负荷", TIMESTAMP = dt, VALUE = dTable.Rows[0][1].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000003", ID_DESC = "碱负荷", TIMESTAMP = dt, VALUE = dTable.Rows[0][2].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000004", ID_DESC = "焦丁比", TIMESTAMP = dt, VALUE = dTable.Rows[0][3].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000005", ID_DESC = "硫负荷", TIMESTAMP = dt, VALUE = dTable.Rows[0][4].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000006", ID_DESC = "炉渣含AL2O3", TIMESTAMP = dt, VALUE = dTable.Rows[0][5].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000007", ID_DESC = "炉渣含MGO", TIMESTAMP = dt, VALUE = dTable.Rows[0][6].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000008", ID_DESC = "煤比", TIMESTAMP = dt, VALUE = dTable.Rows[0][7].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000009", ID_DESC = "燃料比", TIMESTAMP = dt, VALUE = dTable.Rows[0][8].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000010", ID_DESC = "入炉焦比", TIMESTAMP = dt, VALUE = dTable.Rows[0][9].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000011", ID_DESC = "入炉品位", TIMESTAMP = dt, VALUE = dTable.Rows[0][10].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000012", ID_DESC = "生铁含S", TIMESTAMP = dt, VALUE = dTable.Rows[0][11].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000013", ID_DESC = "钛负荷", TIMESTAMP = dt, VALUE = dTable.Rows[0][12].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000014", ID_DESC = "综合负荷", TIMESTAMP = dt, VALUE = dTable.Rows[0][13].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000015", ID_DESC = "综合焦比", TIMESTAMP = dt, VALUE = dTable.Rows[0][14].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000016", ID_DESC = "全天风温", TIMESTAMP = dt, VALUE = dTable.Rows[0][15].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000017", ID_DESC = "全天风压", TIMESTAMP = dt, VALUE = dTable.Rows[0][16].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000018", ID_DESC = "全天风量", TIMESTAMP = dt, VALUE = dTable.Rows[0][17].ToDouble(0) };
                resultList.Add(temp);

                double Al2O3 = dTable.Rows[0][5].ToDouble(0);
                double MgO = dTable.Rows[0][6].ToDouble(0);
                if (Al2O3 > 0 && MgO > 0)
                {
                    temp = new BF_TEST() { ID = "10000000000019", ID_DESC = "镁铝比", TIMESTAMP = dt, VALUE = MgO / Al2O3 };
                    resultList.Add(temp);
                }
            }
            #endregion

            #region LTCREPORT_GL_HUIZONG_YRLZLBB_2 10000000000020
            sql = $"select 七高炉4烧结矿平均粒度 from LTCREPORT_GL_HUIZONG_YRLZLBB_2 where timestamp = {DataBaseFactory.ConvertTimeString(dt, m_dbs.DBType)}";
            dTable = null;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                temp = new BF_TEST() { ID = "10000000000020", ID_DESC = "七高炉4烧结矿平均粒度", TIMESTAMP = dt, VALUE = dTable.Rows[0][0].ToDouble(0) };
                resultList.Add(temp);
            }
            #endregion

            #region LTCREPORT_GL_HUIZONG_ZHRBB 10000000000021
            sql = $"select 硅偏差 from LTCREPORT_GL_HUIZONG_ZHRBB where timestamp = {DataBaseFactory.ConvertTimeString(dt, m_dbs.DBType)}";
            dTable = null;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                temp = new BF_TEST() { ID = "10000000000021", ID_DESC = "硅偏差", TIMESTAMP = dt, VALUE = dTable.Rows[0][0].ToDouble(0) };
                resultList.Add(temp);
            }
            #endregion

            #region LTCREPORT_BB_CTCZ 中铁水温度 10000000000022-10000000000023
            sql = $"select TSWDQ,TSWDH from LTCREPORT_BB_CTCZ where timestamp = {DataBaseFactory.ConvertTimeString(dt, m_dbs.DBType)}";
            dTable = null;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                double QTE = 0; double Qtemp; int QCNT = 0;
                double HTE = 0; double Htemp; int HCNT = 0;
                foreach (DataRow item in dTable.Rows)
                {
                    Qtemp = item[0].ToDouble(0);
                    QTE += Qtemp;
                    if (Qtemp > 0)
                    {
                        QCNT++;
                    }
                    Htemp = item[1].ToDouble(0);
                    HTE += Htemp;
                    if (Htemp > 0)
                    {
                        HCNT++;
                    }
                }
                if (QCNT > 0)
                {
                    temp = new BF_TEST() { ID = "10000000000022", ID_DESC = "前期铁水温度", TIMESTAMP = dt, VALUE = QTE / QCNT };
                    resultList.Add(temp);
                }
                if (HCNT > 0)
                {
                    temp = new BF_TEST() { ID = "10000000000023", ID_DESC = "后期铁水温度", TIMESTAMP = dt, VALUE = HTE / HCNT };
                    resultList.Add(temp);
                }

            }
            #endregion

            return resultList;
        }
        public List<BF_TEST> DealEpxertAvg(DateTime dt)
        {
            BF_TEST temp = null;
            List<BF_TEST> resultList = new List<BF_TEST>();
            string sql = "";
            DataTable dTable = null;

            #region  AV05S_MONITOR 10000000000101-10000000000106
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                sql = "select round(avg(A_Z),2),round(avg(A_W),2),round(avg(A_Z_W),2),round(avg(A_ETACO),2),round(avg(A_TTOP_AVG),2),round(avg(A_H2),2) from AV05S_MONITOR t where A_GASCONVERT_FLAG=0 and A_W>0 and   " +
                $"timestamp>{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)}";
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                temp = new BF_TEST() { ID = "10000000000101", ID_DESC = "中心流指数Z值", TIMESTAMP = dt, VALUE = dTable.Rows[0][0].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000102", ID_DESC = "边缘流指数W值", TIMESTAMP = dt, VALUE = dTable.Rows[0][1].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000103", ID_DESC = "Z/W", TIMESTAMP = dt, VALUE = dTable.Rows[0][2].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000104", ID_DESC = "煤气利用率", TIMESTAMP = dt, VALUE = dTable.Rows[0][3].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000105", ID_DESC = "顶温", TIMESTAMP = dt, VALUE = dTable.Rows[0][4].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000114", ID_DESC = "炉顶煤气H2", TIMESTAMP = dt, VALUE = dTable.Rows[0][5].ToDouble(0) };
                resultList.Add(temp);
            }
            #endregion

            #region  TC05M 10000000000106-10000000000107
            dTable = null;
            sql = "select round(avg(C_BVSG),2),round(avg(C_E),2) from TC05M t where   " +
              $"timestamp>{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)}";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                temp = new BF_TEST() { ID = "10000000000106", ID_DESC = "实际炉腹煤气量", TIMESTAMP = dt, VALUE = dTable.Rows[0][0].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000107", ID_DESC = "鼓风动能", TIMESTAMP = dt, VALUE = dTable.Rows[0][1].ToDouble(0) };
                resultList.Add(temp);

            }
            #endregion

            #region  AV05M 10000000000108-10000000000112
            dTable = null;
            sql = "select round(avg(A_BV5M),2),round(avg(A_BVO5M),2),round(avg(A_TP5M),4),round(avg(A_DPB),4),round(avg(A_K),2)  from AV05M t where A_BV5M>0 and " +
              $"timestamp>{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)}";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                double bv = dTable.Rows[0][0].ToDouble(0);
                double bvo = dTable.Rows[0][1].ToDouble(0);
                temp = new BF_TEST() { ID = "10000000000108", ID_DESC = "富氧量", TIMESTAMP = dt, VALUE = bvo };
                resultList.Add(temp);
                if (bv > 0)
                {
                    temp = new BF_TEST() { ID = "10000000000109", ID_DESC = "富氧率", TIMESTAMP = dt, VALUE = bvo / 60 * 0.785 / bv*100 };
                    resultList.Add(temp);
                }
                temp = new BF_TEST() { ID = "10000000000110", ID_DESC = "顶压", TIMESTAMP = dt, VALUE = dTable.Rows[0][2].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000111", ID_DESC = "压差", TIMESTAMP = dt, VALUE = dTable.Rows[0][3].ToDouble(0) };
                resultList.Add(temp);
                temp = new BF_TEST() { ID = "10000000000112", ID_DESC = "透气性指数", TIMESTAMP = dt, VALUE = dTable.Rows[0][4].ToDouble(0) };
                resultList.Add(temp);

            }
            #endregion 

            #region 全炉热负荷 WTD_HLSUM 10000000000113
            dTable = null;
            sql = "select round(avg(TOTAL_HL),2) from WTD_HLSUM t where  TOTAL_HL>0 and  " +
              $"timestamp>{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and  timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)}";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
            }
            if (dTable != null && dTable.Rows.Count > 0)
            {
                temp = new BF_TEST() { ID = "10000000000113", ID_DESC = "全炉热负荷", TIMESTAMP = dt, VALUE = dTable.Rows[0][0].ToDouble(0)/1000 };
                resultList.Add(temp);
            }
            #endregion

            #region 炉身温度 壁体温度 10000000000130-10000000000141
            double value = -1;
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 1);
            if (value>0)
            {
                temp = new BF_TEST() { ID= "10000000000130",ID_DESC= "壁体六段(16.33m)",VALUE= value,TIMESTAMP =dt }; resultList.Add(temp);
            }
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 2);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000131", ID_DESC = "壁体七段(17.565m)", VALUE = value, TIMESTAMP = dt }; resultList.Add(temp);
            }
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 3);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000132", ID_DESC = "壁体八段(19.275m)", VALUE = value, TIMESTAMP = dt }; resultList.Add(temp);
            }
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 4);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000133", ID_DESC = "壁体九段(21.23m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 5);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000134", ID_DESC = "壁体十段(23.13m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 6);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000135", ID_DESC = "壁体十一段(25.03m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 7);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000136", ID_DESC = "壁体十二段(26.93m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            temp = null;
            value = CalAvgBTbyGroupNo(dt, 8);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000137", ID_DESC = "壁体十三段(28.83m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            value = CalAvgBTbyGroupNo(dt, 9);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000138", ID_DESC = "壁体十四段(30.73m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            value = CalAvgBTbyGroupNo(dt, 10);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000139", ID_DESC = "壁体十五段(32.63m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            value = CalAvgBTbyGroupNo(dt, 11);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000140", ID_DESC = "壁体十六段(34.53m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            value = CalAvgBTbyGroupNo(dt, 12);
            if (value > 0)
            {
                temp = new BF_TEST() { ID = "10000000000141", ID_DESC = "炉喉钢砖(38.25m)", VALUE = value, TIMESTAMP = dt };
                resultList.Add(temp);
            }
            #endregion

            return resultList;

        }
        
        public List<BF_TEST> DealJHY(DateTime dt)
        {
            List<BF_TEST> resultList = new List<BF_TEST>();
            double value = -1;
            BF_TEST temp = null;

            #region 焦炭 10000000000201-10000000000205
            int Code = 33;
            value = L3AndItmeValue(dt, Code, "M_M40");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000201", ID_DESC = "焦炭M40", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            value = L3AndItmeValue(dt, Code, "M_M10");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000202", ID_DESC = "焦炭M10", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            value = L3AndItmeValue(dt, Code, "M_CSR");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000203", ID_DESC = "焦炭CSR", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            value = L3AndItmeValue(dt, Code, "M_CRI");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000204", ID_DESC = "焦炭CRI", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            value = L3AndItmeValue(dt, Code, "M_COKEASH");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000205", ID_DESC = "焦炭灰分", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            #endregion

            #region 烧结矿 10000000000221-10000000000223
            Code = 51;
            value = L3AndItmeValue(dt, Code, "M_DI");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000221", ID_DESC = "烧结矿转鼓", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            value = L3AndItmeValue(dt, Code, "M_BASICITY");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000222", ID_DESC = "烧结矿碱度", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            value = L3AndItmeValue(dt, Code, "M_FEO_CONT");
            if (value > 0)
            {
                temp = new Plugin.BF_TEST() { ID = "10000000000223", ID_DESC = "烧结FeO", TIMESTAMP = dt, VALUE = value };
                resultList.Add(temp);
            }
            #endregion

            return resultList;
        }

        /// <summary>
        /// 计算减风次数 100000000000150
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public BF_TEST DealReduceBv(DateTime dt)
        {
            BF_TEST model = new Plugin.BF_TEST();
            string sql = $"select A_BV5m  from AV05M t where  timestamp>{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)} order by timestamp asc";
            DataTable table = new DataTable();

            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                table = idatabase.GetDataTable(sql);
            }
            if (table != null && table.Rows.Count > 0)
            {
                model = new BF_TEST() { TIMESTAMP = dt, ID = "10000000000150", ID_DESC = "减风次数(4500基准减10%)" };
                double TempValueLast = 0;
                double TempValueNow = 0;
                int Count = 0;
                for (int i = 0; i < table.Rows.Count - 1; i++)
                {
                    TempValueLast = table.Rows[i][0].ToDouble();
                    TempValueNow = table.Rows[i + 1][0].ToDouble();
                    if (TempValueLast >= 4050 && TempValueNow <= 4050)
                    {
                        Count++;
                    }
                }
                model.VALUE = Count;
            }
            return model;

        }
        /// <summary>
        /// 根据段位计算每段均值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="groupno"></param>
        /// <returns></returns>
        public double CalAvgBTbyGroupNo(DateTime dt,int groupno)
        {
            double result = 0;
            List<UNIVERSALTE_MONITOR_CONFIG> list = new List<Models.UNIVERSALTE_MONITOR_CONFIG>();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                list = idatabase.Queryable<UNIVERSALTE_MONITOR_CONFIG>().Where(m=>m.POINTTYPE==1&&m.GROUPNO== groupno).ToList();
            }
            if (list.Count>0)
            {
                int count = 0;
                double tempValue = 0;
                foreach (UNIVERSALTE_MONITOR_CONFIG item in list)
                {
                    string sql = $"select avg({item.ADDRESS})  from AV05M_LININGTC_01H t where {item.ADDRESS}>{item.TELOW} and {item.ADDRESS}<{item.HEIGHT} and "+
                                $"timestamp>{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)}";
                    using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                    {
                        tempValue = idatabase.GetDouble(sql);
                        if (tempValue>0)
                        {
                            count++;
                            result += tempValue;
                        }
                    }                        
                }
                if (count>0)
                {
                    result /= count;
                }
            }
            return result;
        }
        /// <summary>
        /// 读取检化验值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="CODE"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public double L3AndItmeValue(DateTime dt, int CODE, string FieldName)
        {
            DataTable dTable = new DataTable();
            double result = 0;
            string sql = $"select {FieldName} from L3ANALYSI where {FieldName}>0 and M_CODE ={CODE} and " +
                $"timestamp>{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and  " +
                $"timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)}";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                dTable = idatabase.GetDataTable(sql);
                if (dTable.Rows.Count > 0)
                {
                    double sum = 0;
                    foreach (DataRow item in dTable.Rows)
                    {
                        sum += item[0].ToDouble(0);
                    }
                    result = sum / dTable.Rows.Count;
                }
                else
                {
                    sql = $"select {FieldName} from L3ANALYSI where {FieldName}>0 and M_CODE ={CODE} and timestamp " +
                        $"= (select max(timestamp) from L3ANALYSI where {FieldName}>0 and M_CODE ={CODE} and timestamp<={DataBaseFactory.ConvertTimeString(dt.AddDays(1), DataBaseType.Oracle)})";
                    result = idatabase.GetDouble(sql);
                }
            }
            return result;
        }
    }
}
