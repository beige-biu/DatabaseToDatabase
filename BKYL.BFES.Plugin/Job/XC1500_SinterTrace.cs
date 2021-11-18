using BFES.DataAccess;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace BKYL.BFES.Plugin
{
    class XC1500_SinterTrace : BaseJob
    {

        int iCalDays;       //计算天数
        int iTimes_ToBin;   //取样点到仓的时间(s)
        Dictionary<string, string> DicNameCode; //CangInfo与BatchhisCode映射关系
        Dictionary<string, string> DicNameCangWei; //CangInfo与仓位映射关系
        List<CangInfo> CangList;
        List<L3MATERIAL> l3MatList;
        public string BFflag = "T7";
        public override void InitTask()
        {
            iCalDays = 3;
            iTimes_ToBin = 270;//皮带长431，速度1.6
        }

        public override void RunTask(DateTime currentTime)
        {
            // currentTime = "2019/7/31 23:00:00".ToDate();
            List<L3ANALYSI> L_L3ANALYSI = GetL3ANALYSI(currentTime.AddDays(-iCalDays), currentTime);
            l3MatList = GetL3Material();
            CangList = new List<CangInfo>();
            InitCangModel();//初始化仓上

            L_L3ANALYSI = L_L3ANALYSI.OrderByDescending(m => m.MAT_SAMPLE_TIME).ToList();
            List<int> L_Index = new List<int>();
            DateTime DT_TEMP = DateTime.MinValue;
            for (int i = 0; i < L_L3ANALYSI.Count; i++)
            {
                if (L_L3ANALYSI[i].MAT_SAMPLE_TIME == DT_TEMP)
                {
                    L_Index.Add(i);
                }
                DT_TEMP = (DateTime)L_L3ANALYSI[i].MAT_SAMPLE_TIME;
            }

            for (int i = L_Index.Count - 1; i >= 0; i--)
            {
                L_L3ANALYSI.RemoveAt(L_Index[i]);
            }

            L_L3ANALYSI = L_L3ANALYSI.OrderBy(m => m.MAT_SAMPLE_TIME).ToList();




            //

            List<SINTER_TRACE> L_SINTER_TRACE = new List<SINTER_TRACE>();
            foreach (L3ANALYSI L3Analysi in L_L3ANALYSI)
            {
                double dblSumWeight = 0;    //仓中烧结矿总量
                double dblSpeedSinter = 0;  //烧结矿排料速度
                double dblTimesToTop = 0;
                DateTime? sample_time = L3Analysi.MAT_SAMPLE_TIME;
                L3Analysi.MAT_SAMPLE_TIME = CalcMAT_SAMPLE_TIME(L3Analysi.MAT_SAMPLE_TIME);
                if (L3Analysi.MAT_SAMPLE_TIME == null)
                {
                    continue;
                }

                //烧结采样点到槽下所需要的时间
                DateTime dt_BinTime = ((DateTime)L3Analysi.MAT_SAMPLE_TIME).AddSeconds(iTimes_ToBin);

                #region 计算烧结矿槽下存储量 

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

                DataTable BatCode = GetBATCHHISCODE(dt_BinTime);
                DataTable BatCs = GetBATCHHIS_CS(dt_BinTime);

                if (BatCode == null || BatCode.Rows.Count == 0 || BatCs == null || BatCs.Rows.Count == 0)
                    continue;
                foreach (CangInfo item in CangList)
                {
                    string ColumnName = DicNameCode[item.CangName];
                    if (BatCode.Columns.Contains(ColumnName))
                    {
                        item.CODE = BatCode.Rows[0][ColumnName].ToInt();
                        //如果物料编码属于烧结矿
                        if (item.CODE >= 50 && item.CODE <= 59)
                        {
                            int index = l3MatList.FindIndex(m => m.CODE == item.CODE);
                            if (index > 0)
                                item.Density = l3MatList[index].DENSITY;
                            string CangColumnName = DicNameCangWei[item.CangName];
                            if (BatCs.Columns.Contains(CangColumnName))
                            {
                                item.CangWei = BatCs.Rows[0][CangColumnName].ToDouble();
                                item.Weight = GetWeight(item, item.CangWei);
                                dblSumWeight += item.Weight;
                            }
                        }
                    }
                }

                SINTER_TRACE sinterTrace = new SINTER_TRACE();

                #endregion

                dblSpeedSinter = CalSpeedSinter(dt_BinTime);
                dblTimesToTop = iTimes_ToBin / 3600 + (dblSpeedSinter > 0 ? dblSumWeight / dblSpeedSinter : 0);

                sinterTrace.MATERIAL_NAME = "400烧结矿";

                sinterTrace.TIMESTAMP = (DateTime)sample_time;
                sinterTrace.TIMES_TOTOP = dblTimesToTop;
                sinterTrace.TIME_TOBF = sinterTrace.TIMESTAMP.AddHours(sinterTrace.TIMES_TOTOP);
                sinterTrace.M_TFE_CONT = L3Analysi.M_TFE_CONT;
                sinterTrace.M_FEO_CONT = L3Analysi.M_FEO_CONT;
                sinterTrace.M_AL2O3_CONT = L3Analysi.M_AL2O3_CONT;
                sinterTrace.M_SIO2_CONT = L3Analysi.M_SIO2_CONT;
                sinterTrace.M_CAO_CONT = L3Analysi.M_CAO_CONT;
                sinterTrace.M_MGO_CONT = L3Analysi.M_MGO_CONT;
                sinterTrace.M_TI6Z3 = L3Analysi.M_TI6Z3;
                sinterTrace.M_RDI_3Z15 = L3Analysi.M_RDI_3Z15;
                sinterTrace.MR_R2 = L3Analysi.M_SIO2_CONT > 0 ? L3Analysi.M_CAO_CONT / L3Analysi.M_SIO2_CONT : 0;
                sinterTrace.M_SAMPLE_NO = L3Analysi.MAT_SAMPLE_NO;
                L_SINTER_TRACE.Add(sinterTrace);
            }
            SaveSINTER_TRACE(L_SINTER_TRACE, currentTime.AddDays(-iCalDays), currentTime);
        }
        private DateTime? CalcMAT_SAMPLE_TIME(DateTime? time)
        {
            try
            {
                if (time == null)
                    return null;
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    //是否在区间内
                    string strSql = string.Format($"select * from MES_T_MATERIAL_TEAM_YIELDGGL where ((TEAM_END_TIME > {UtilsConvert.GetOraString(time.ToDate())} and TEAM_BEGIN_TIME <  {UtilsConvert.GetOraString(time.ToDate())} ) or( TEAM_BEGIN_TIME <  {UtilsConvert.GetOraString(time.ToDate())} AND TEAM_END_TIME is null )) and wp_no='{BFflag}'");
                    var mes = idatabase.GetList< MES_T_MATERIAL_TEAM_YIELDGGL>(strSql);
                    if (mes != null&& mes.Count>0)
                        return time;
                    //是否在开始放料前
                    strSql = string.Format($"select * from MES_T_MATERIAL_TEAM_YIELDGGL where  TEAM_BEGIN_TIME =(SELECT MIN(TEAM_BEGIN_TIME) FROM MES_T_MATERIAL_TEAM_YIELDGGL WHERE TEAM_BEGIN_TIME> {UtilsConvert.GetOraString(time.ToDate())} AND  wp_no='{BFflag}')  ");
                     mes = idatabase.GetList<MES_T_MATERIAL_TEAM_YIELDGGL>(strSql);
                    if (mes != null && mes.Count > 0)
                        return mes[0].TEAM_BEGIN_TIME;
                }
                return null;
            }
            catch (Exception EE)
            {
                return null;
            }

        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        public List<L3ANALYSI> GetL3ANALYSI(DateTime StartTime, DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from L3ANALYSI where MAT_SAMPLE_TIME >= {0} and MAT_SAMPLE_TIME <= {1} and M_CODE = 50 and MAT_SAMPLE_NO like 'RSJ%' ",
                    UtilsConvert.GetOraString(StartTime), UtilsConvert.GetOraString(EndTime));
                return idatabase.GetList<L3ANALYSI>(strSql);
            }
        }
        public double CalSpeedSinter(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select round(nvl(avg(I_SINTER_WEIGHT),0),2) from MATERIAL_BALANCE_HOUR  where TIMESTAMP >= {0} and TIMESTAMP <= {1}",
                    UtilsConvert.GetOraString(EndTime.AddHours(-12)), UtilsConvert.GetOraString(EndTime));
                return idatabase.GetDouble(strSql);
            }
        }
        public void SaveSINTER_TRACE(List<SINTER_TRACE> L_SINTER_TRACE, DateTime startTime, DateTime endTime)
        {
            if (L_SINTER_TRACE.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        string deleteSQL = $"delete from SINTER_TRACE where timestamp >= {DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle)}" +
                                                 $"and timestamp <= {DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)}";
                        idatabase.BeginTran();
                        idatabase.ExecuteCommand(deleteSQL);
                        idatabase.InsertRange(L_SINTER_TRACE);
                        idatabase.CommitTran();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        idatabase.RollbackTran();
                    }
                }
            }
        }
        public DataTable GetBATCHHISCODE(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from BATCHHISCODE where TIMESTAMP " +
                    "= (select max(timestamp) from  BATCHHISCODE where timestamp <= {0})",
                    UtilsConvert.GetOraString(EndTime));
                return idatabase.GetDataTable(strSql);
            }
        }
        public DataTable GetBATCHHIS_CS(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
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
            Model = new CangInfo() { ID = 1, CangName = "8#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "9#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);
            Model = new CangInfo() { ID = 1, CangName = "10#烧结", DownL = 1, DownW = 1, DownH = 8.14, UpL = 8.1, UpW = 7.6, UpH = 6.8, Density = 1.85, ChaYi = 2.5 };
            CangList.Add(Model);

        }
        public List<L3MATERIAL> GetL3Material()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.GetList<L3MATERIAL>("select * from L3MATERIAL");
            }

        }
    }
}
