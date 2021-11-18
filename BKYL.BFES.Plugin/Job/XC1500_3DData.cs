using BFES.DataAccess;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BFES.DataAccess.Common;

namespace BKYL.BFES.Plugin
{
    class XC1500_3DData : BaseJob
    {
        CalMatSpeed MatSpeed;
        CalCangWeight CangWeight;
        public override void InitTask()
        {
            MatSpeed = new CalMatSpeed(base.S_DBSource, this);
            CangWeight = new CalCangWeight(base.S_DBSource);
        }
        public override void RunTask(DateTime currentTime)
        {
            //currentTime = "2020-10-21 13:00:00".ToDate();
            MatSpeed.Calcular(currentTime);
            CangWeight.Calcular(currentTime);
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }

    }
    class CalMatSpeed
    {
        DatabaseSource m_dbs;
        List<MATERIAL_BALANCE_BAT> materList;
        double HourBatReal = 0;
        private XC1500_3DData xC1500_3DData;
        public CalMatSpeed(DatabaseSource dbs)
        {
            m_dbs = dbs;
        }
        public CalMatSpeed(DatabaseSource dbs, XC1500_3DData xC1500_3DData) : this(dbs)
        {
            m_dbs = dbs;
            this.xC1500_3DData = xC1500_3DData;
        }
        public void Calcular(DateTime dt)
        {
            int i = dt.Minute % 5;
            dt = dt.AddMinutes(-i);
            int i3 = 0, i4 = 0, i5 = 0;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                i3 = idatabase.GetInt($"select count(*) from AV01H_VERTICAL where timestamp = {DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and id= 3 ");
                i4 = idatabase.GetInt($"select count(*) from AV01H_VERTICAL where timestamp = {DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and id= 4 ");
                i5 = idatabase.GetInt($"select count(*) from AV01H_VERTICAL where timestamp = {DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and id= 5 ");
            }
            if (i4 == 0)
                CalBatSpeed5M(dt);
            if (i5 == 0)
                CalRealFuel(dt);
            if (i3 == 0)
            {
                if (dt.Minute != 0 || dt.Second != 0)
                    return;
                CalBatSpeed01H(dt);
            }
        }
        /// <summary>
        /// 5分钟料速
        /// </summary>
        /// <param name="dt"></param>
        public void CalBatSpeed5M(DateTime dt)
        {
            xC1500_3DData.Writelog($"5分钟料速");
            try
            {
                double HourBat = 0;
                if (dt.Minute % 5 != 0 || dt.Second != 0)
                    return;
                DateTime EndTime = dt;
                DateTime StartTime = dt.AddHours(-1);
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    materList = idatabase.Queryable<MATERIAL_BALANCE_BAT>().Where(m => m.TIMESTAMP > StartTime && m.TIMESTAMP <= EndTime).OrderBy(m => m.TIMESTAMP, OrderByType.Desc).ToList();
                    xC1500_3DData.Writelog($"materList");
                }
                if (materList != null && materList.Count > 3)
                {

                    int LessLast = materList.Count - 1; //次最后一条记录，也是该小时的料批数
                    double Minute = (materList[0].TIMESTAMP - materList[LessLast].TIMESTAMP).TotalMinutes;
                    HourBat = Math.Round(60 * LessLast / Minute, 2);
                    HourBatReal = HourBat;
                }
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    idatabase.ExecuteCommand($"Insert into AV01H_VERTICAL (TIMESTAMP,ID,ID_DESC,VALUE) VALUES ({DataBaseFactory.ConvertTimeString(EndTime, DataBaseType.Oracle)},4,'实时小时料速',{HourBat})");
                }
            }
            catch (Exception ee)
            {
                xC1500_3DData.Writelog($"error：{ee.Message}");
            }
        }
        /// <summary>
        /// 小时料速
        /// </summary>
        /// <param name="dt"></param>
        public void CalBatSpeed01H(DateTime dt)
        {
            double HourBat = 0;
            if (dt.Minute != 0 || dt.Second != 0)
                return;
            xC1500_3DData.Writelog($"CalBatSpeed01H DateTime = {dt}");
            DateTime EndTime = dt;
            DateTime StartTime = dt.AddHours(-1);
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                materList = idatabase.Queryable<MATERIAL_BALANCE_BAT>().Where(m => m.TIMESTAMP > StartTime && m.TIMESTAMP <= EndTime).OrderBy(m => m.TIMESTAMP, OrderByType.Desc).ToList();
                xC1500_3DData.Writelog("idatabase.Queryable<MATERIAL_BALANCE_BAT>()");
            }
            if (materList != null && materList.Count > 3)
            {
                xC1500_3DData.Writelog($"materList = {materList.Count}");
                int LessLast = materList.Count - 1; //次最后一条记录，也是该小时的料批数
                double Minute = (materList[0].TIMESTAMP - materList[LessLast].TIMESTAMP).TotalMinutes;
                xC1500_3DData.Writelog($"Minute = {Minute}");
                HourBat = Math.Round(60 * LessLast / Minute, 4);
            }
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                idatabase.ExecuteCommand($"Insert into AV01H_VERTICAL (TIMESTAMP,ID,ID_DESC,VALUE) VALUES ({DataBaseFactory.ConvertTimeString(EndTime, DataBaseType.Oracle)},3,'小时料批',{HourBat})");
            }
        }
        /// <summary>
        /// 计算反应燃料比
        /// </summary>
        /// <param name="dt"></param>
        public void CalRealFuel(DateTime dt)
        {
            xC1500_3DData.Writelog($"计算反应燃料比");
            try
            {
                string TempSql = "";
                DateTime tuyere_fabrics_time = new DateTime();
                DateTime batchhis_time = new DateTime();
                DataTable DataT = new DataTable();
                TempBat model = new Plugin.TempBat();
                double Winj01h = 0;
                double RealFuel = 0;
                if (dt.Minute % 5 != 0 || dt.Second != 0)
                    return;
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    TempSql = $"select fabrics_time from MATERIAL_TRACK t where t.fabrics_time = (select max(fabrics_time) from MATERIAL_TRACK where flag=1 and type=0) ";
                    tuyere_fabrics_time = idatabase.GetDateTime(TempSql);
                    xC1500_3DData.Writelog($"tuyere_fabrics_time:{tuyere_fabrics_time}");
                    if (tuyere_fabrics_time != DateTime.MinValue)
                    {
                        TempSql = $"select a_batchhisdumptime from STUFF_MATRIX t where timestamp  ={DataBaseFactory.ConvertTimeString(tuyere_fabrics_time, DataBaseType.Oracle)} ";
                        batchhis_time = idatabase.GetDateTime(TempSql);
                        if (batchhis_time != DateTime.MinValue)
                        {
                            TempSql = $"select round(avg(i_coke_in),4) as COKE,round(avg(i_mhmt_out),4) as IRON,round(avg(i_coke_nut_weight),4) as NUTCOKE from material_balance_bat t where " +
                                      $"timestamp <={DataBaseFactory.ConvertTimeString(batchhis_time, DataBaseType.Oracle)} and timestamp>={DataBaseFactory.ConvertTimeString(batchhis_time.AddHours(-0.5), DataBaseType.Oracle)}";
                            model = idatabase.GetSingle<TempBat>(TempSql);
                        }
                    }
                    if (model.IRON > 0)
                    {
                        TempSql = $"select a_winj_01h from AV01H_WINJ t where timestamp = (select max(timestamp) from AV01H_WINJ where timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)})";
                        Winj01h = idatabase.GetDouble(TempSql);
                        if (HourBatReal > 0)
                        {
                            RealFuel = Math.Round((Winj01h / HourBatReal + model.COKE + model.NUTCOKE) / model.IRON * 1000, 2);
                        }
                    }
                    idatabase.ExecuteCommand($"Insert into AV01H_VERTICAL (TIMESTAMP,ID,ID_DESC,VALUE) VALUES ({DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)},5,'反应燃料比',{RealFuel})");
                }
            }
            catch (Exception ee)
            {
                xC1500_3DData.Writelog($"error：{ee.Message}");
            }
        }


    }
    class CalCangWeight
    {
        DatabaseSource m_dbs;
        List<CangInfo> CangList;
        Dictionary<string, string> DicNameCode; //CangInfo与BatchhisCode映射关系
        Dictionary<string, string> DicNameCangWei; //CangInfo与仓位映射关系
        List<L3MATERIAL> l3MatList;
        public CalCangWeight(DatabaseSource dbs)
        {
            m_dbs = dbs;
            #region
            DicNameCode = new Dictionary<string, string>();
            DicNameCode.Add("1#烧结", "CODE1");
            DicNameCode.Add("2#烧结", "CODE2");
            DicNameCode.Add("3#烧结", "CODE3");
            DicNameCode.Add("4#烧结", "CODE4");
            DicNameCode.Add("5#烧结", "CODE5");
            DicNameCode.Add("6#烧结", "CODE6");
            DicNameCode.Add("7#烧结", "CODE7");
            DicNameCode.Add("8#烧结", "CODE8");
            DicNameCode.Add("9#烧结", "CODE9");
            DicNameCode.Add("10#烧结", "CODE10");
            DicNameCode.Add("1#球团", "CODE11");
            DicNameCode.Add("2#球团", "CODE12");
            DicNameCode.Add("1#块矿", "CODE13");
            DicNameCode.Add("2#块矿", "CODE14");
            DicNameCode.Add("1#杂矿", "CODE15");
            DicNameCode.Add("2#杂矿", "CODE16");
            DicNameCode.Add("1#焦炭", "CODE18");
            DicNameCode.Add("2#焦炭", "CODE19");
            DicNameCode.Add("3#焦炭", "CODE20");
            DicNameCode.Add("4#焦炭", "CODE21");
            DicNameCode.Add("5#焦炭", "CODE22");
            DicNameCode.Add("6#焦炭", "CODE23");
            DicNameCode.Add("7#焦炭", "CODE24");


            DicNameCangWei = new Dictionary<string, string>();
            DicNameCangWei.Add("1#烧结", "SORE1_CS");
            DicNameCangWei.Add("2#烧结", "SORE2_CS");
            DicNameCangWei.Add("3#烧结", "SORE3_CS");
            DicNameCangWei.Add("4#烧结", "SORE4_CS");
            DicNameCangWei.Add("5#烧结", "SORE5_CS");
            DicNameCangWei.Add("6#烧结", "SORE6_CS");
            DicNameCangWei.Add("7#烧结", "SORE7_CS");
            DicNameCangWei.Add("8#烧结", "SORE8_CS");
            DicNameCangWei.Add("9#烧结", "SORE9_CS");
            DicNameCangWei.Add("10#烧结", "SORE10_CS");
            DicNameCangWei.Add("1#球团", "SORE11_CS");
            DicNameCangWei.Add("2#球团", "SORE12_CS");
            DicNameCangWei.Add("1#块矿", "SORE13_CS");
            DicNameCangWei.Add("2#块矿", "SORE14_CS");
            DicNameCangWei.Add("1#杂矿", "SORE15_CS");
            DicNameCangWei.Add("2#杂矿", "SORE16_CS");
            DicNameCangWei.Add("1#焦炭", "SCKE1_CS");
            DicNameCangWei.Add("2#焦炭", "SCKE2_CS");
            DicNameCangWei.Add("3#焦炭", "SCKE3_CS");
            DicNameCangWei.Add("4#焦炭", "SCKE4_CS");
            DicNameCangWei.Add("5#焦炭", "SCKE5_CS");
            DicNameCangWei.Add("6#焦炭", "SCKE6_CS");
            DicNameCangWei.Add("7#焦炭", "SCKE7_CS");
            #endregion
        }
        public void Calcular(DateTime dt)
        {
            // currentTime = "2019/7/31 23:00:00".ToDate();
            CangList = new List<CangInfo>();
            InitCangModel();
            DataTable BatCode = GetBATCHHISCODE(dt);
            l3MatList = GetL3Material();
            if (BatCode != null && BatCode.Rows.Count > 0)
            {
                foreach (CangInfo item in CangList)
                {
                    string ColumnName = DicNameCode[item.CangName];
                    if (BatCode.Columns.Contains(ColumnName))
                    {
                        item.CODE = BatCode.Rows[0][ColumnName].ToInt();

                        int index = l3MatList.FindIndex(m => m.CODE == item.CODE);
                        if (index > 0)
                        {
                            item.Density = l3MatList[index].DENSITY;
                            item.CODEDESC = l3MatList[index].NAME;
                        }
                    }
                }
            }
            DataTable BatCs = GetBATCHHIS_CS(dt);
            if (BatCs != null && BatCs.Rows.Count > 0)
            {
                foreach (CangInfo item in CangList)
                {
                    string ColumnName = DicNameCangWei[item.CangName];
                    if (BatCs.Columns.Contains(ColumnName))
                    {
                        item.CangWei = BatCs.Rows[0][ColumnName].ToDouble();
                        item.Weight = GetWeight(item, item.CangWei);
                    }
                }
            }
            var MergeResult = CangList.GroupBy(m => m.CODE).Select(a => new { Code = a.Key, Weight = a.Sum(x => x.Weight), Name = CangList.First(m => m.CODE == a.Key).CODEDESC }).ToList();
            string InsertSql = "";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                foreach (var item in MergeResult)
                {
                    InsertSql = $"Insert into BATCHHIS_CS_MERGEWEIGHT (timestamp,m_code,m_name,m_weight) values ({DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)},{item.Code},'{item.Name}',{item.Weight})";
                    idatabase.ExecuteCommand(InsertSql);
                }
            }
        }
        public DataTable GetBATCHHISCODE(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string strSql = string.Format("select * from BATCHHISCODE where TIMESTAMP " +
                    "= (select max(timestamp) from  BATCHHISCODE where timestamp <= {0})",
                    UtilsConvert.GetOraString(EndTime));
                return idatabase.GetDataTable(strSql);
            }
        }
        public DataTable GetBATCHHIS_CS(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string strSql = string.Format("select * from BATCHHIS_CS where TIMESTAMP " +
                    "= (select max(timestamp) from  BATCHHIS_CS where timestamp <= {0})",
                    UtilsConvert.GetOraString(EndTime));
                return idatabase.GetDataTable(strSql);
            }
        }
        public double GetWeight(CangInfo model, double dH)
        {
            double dTmp = 0;
            //台底长 台底宽 台高 方长 方宽 方高 堆比重 差异
            double dPL1 = 0, dPW1 = 0, dPH = 0, dCL = 0, dCW = 0, dCH = 0, dPro = 0, dDiff = 0;
            if (model == null)
                return 0;
            double Volume = 0;

            dPL1 = model.DownL;
            dPW1 = model.DownW;
            dPH = model.DownH;
            dCL = model.UpL;
            dCW = model.UpW;
            dCH = model.UpH;
            dPro = model.Density;
            dDiff = model.ChaYi;

          

            //差异料位 = 测量料位 + 差异
            dH = dH + dDiff;
            //台底面积 = 台底长 * 台底宽
            double dPS1 = dPL1 * dPW1;
            //台顶面积 = 台顶长 * 台顶宽 台高 方高
            double dPL2 = 0, dPW2 = 0, dPH1 = 0, dCH1 = 0;
            if (dH >= dPH) //当料位高于台高时
            {
                dPL2 = dCL;//台顶长 = 方长
                dPW2 = dCW;//台顶宽 = 方宽
                dPH1 = dPH;//台高 = 台高
                dCH1 = dH - dPH;//方高 = 料位 - 台高
            }
            else//当料位低于台高时
            {
                dTmp = (dCL - dPL1) / 2;
                //总高/当前高 = 总长/当前长
                dPL2 = (dTmp / (dPH / (dPH - dH)));
                dPL2 = dCL - (2 * dPL2);//台顶长

                dTmp = (dCW - dPW1) / 2;
                //总高/当前高 = 总宽/当前宽
                dPW2 = (dTmp / (dPH / (dPH - dH)));
                dPW2 = dCW - (2 * dPW2);//台顶宽

                dPH1 = dH;//台高 = 料位
                dCH1 = 0;//方高
            }
            double dPS2 = dPL2 * dPW2;
            //台体积 = 1/3 * (台底面积 + 台顶面积 + 开平方跟(台底面积 * 台顶面积)) * 台高
            double dPV = dPH1 * (dPS1 + dPS2 + Math.Sqrt(dPS1 * dPS2)) / 3;
            //方体积 = 方长 * 方宽 * 方高
            double dCV = dCL * dCW * dCH1;
            //总体积 = 台体积 + 方体积
            double dV = dPV + dCV;
            //重量 = 总体积 * 堆比重
            double dT = dV * dPro;
            return Math.Round(dT, 1);
        }

    
        public void InitCangModel()
        {
            CangInfo Model = null;
            Model = new CangInfo() { ID = 1, CangName = "1#焦炭", DownL = 1, DownW = 1, DownH = 6.2, UpL = 8.1, UpW = 6.6, UpH = 5.3, Density = 0.55, ChaYi = 2.8 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "2#焦炭", DownL = 1, DownW = 1, DownH = 6.2, UpL = 8.1, UpW = 6.6, UpH = 5.3, Density = 0.55, ChaYi = 2.8 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "3#焦炭", DownL = 1, DownW = 1, DownH = 6.2, UpL = 8.1, UpW = 6.6, UpH = 5.3, Density = 0.55, ChaYi = 2.8 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "4#焦炭", DownL = 1, DownW = 1, DownH = 6.2, UpL = 8.1, UpW = 6.6, UpH = 5.3, Density = 0.55, ChaYi = 2.8 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "5#焦炭", DownL = 1, DownW = 1, DownH = 6.2, UpL = 8.1, UpW = 6.6, UpH = 5.3, Density = 0.55, ChaYi = 2.8 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "6#焦炭", DownL = 1, DownW = 1, DownH = 6.2, UpL = 8.1, UpW = 6.6, UpH = 5.3, Density = 0.55, ChaYi = 2.8 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "7#焦炭", DownL = 1, DownW = 1, DownH = 6.2, UpL = 8.1, UpW = 6.6, UpH = 5.3, Density = 0.55, ChaYi = 2.8 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "1#球团", DownL = 0.9, DownW = 0.9, DownH = 4.4, UpL = 6.6, UpW = 4.6, UpH = 6.8, Density = 2, ChaYi = 2 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "2#球团", DownL = 0.9, DownW = 0.9, DownH = 4.4, UpL = 6.6, UpW = 4.6, UpH = 6.8, Density = 2, ChaYi = 2 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "1#块矿", DownL = 0.9, DownW = 0.9, DownH = 4.4, UpL = 6.6, UpW = 4.6, UpH = 6.8, Density = 2, ChaYi = 2 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "2#块矿", DownL = 0.9, DownW = 0.9, DownH = 4.4, UpL = 6.6, UpW = 4.6, UpH = 6.8, Density = 2, ChaYi = 2 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "1#杂矿", DownL = 0.9, DownW = 0.9, DownH = 5.6, UpL = 6.6, UpW = 4.6, UpH = 10.9, Density = 1.55, ChaYi = 1.9 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "2#杂矿", DownL = 0.9, DownW = 0.9, DownH = 5.6, UpL = 6.6, UpW = 4.6, UpH = 10.9, Density = 1.55, ChaYi = 1.9 };
            CangList.Add(Model);

            Model = new CangInfo() { ID = 1, CangName = "1#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "2#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "3#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "4#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "5#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "6#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "7#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "8#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 9.6, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "9#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 9.6, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "10#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 9.6, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);

        }
        public List<L3MATERIAL> GetL3Material()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                return idatabase.GetList<L3MATERIAL>("select * from L3MATERIAL");
            }

        }

    }

    class TempBat
    {
        public double COKE { get; set; }
        public double NUTCOKE { get; set; }
        public double IRON { get; set; }
    }
}

