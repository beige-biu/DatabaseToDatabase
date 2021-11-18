using BFES.DataAccess;
using BFES.Models;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BKYL.BFES.Plugin
{

    class CalBigData : BaseJob
    {
        CalMatrixData m_CalMatrixData;
        CalMatrixData2 m_CalMatrixData2;
        CalTappingData m_CalTappingData;
        CalBatchhisDayWeight m_CalBatchhisDayWeight;
        CalMatrixData3 m_CalMatrixData3;
        public override void InitTask()
        {
            m_CalMatrixData = new CalMatrixData(base.S_DBSource);
            m_CalMatrixData2 = new CalMatrixData2(base.S_DBSource);
            m_CalTappingData = new CalTappingData(base.S_DBSource);
            m_CalBatchhisDayWeight = new CalBatchhisDayWeight(base.S_DBSource);
            m_CalMatrixData3 = new CalMatrixData3(base.S_DBSource);

            //string strTimeStart = "2019-04-22";
            //string strTimeEnd = "2019-05-09";
            //DateTime dtStartTime = strTimeStart.ToDate();
            //DateTime dtEndTime = strTimeEnd.ToDate();
            //for (DateTime i = dtStartTime; i <= dtEndTime; i=i.AddDays(1))
            //{
            //    m_CalTappingData.Calcular(i);
            //}
            //string strTimeStart = "2019-04-22";
            //DateTime dtStartTime = strTimeStart.ToDate();
            //m_CalMatrixData.Calcular(dtStartTime);
        }

        public override void RunTask(DateTime currentTime)
        {
            m_CalTappingData.Calcular(currentTime);
            m_CalBatchhisDayWeight.Calcular(currentTime);
            m_CalMatrixData.Calcular(currentTime);
            m_CalMatrixData2.Calcular(currentTime);
            m_CalMatrixData3.Calcular0(currentTime);
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }

    }
    /// <summary>
    /// 计算布料变化数据
    /// </summary>
    class CalMatrixData
    {

        STUFF_MATRIXDATA_CHANGE m_BigDataMatrixLast;
        STUFF_MATRIXDATA_CHANGE m_BigDataMatrixNow;
        public bool m_isFirstRun = true;
        public DatabaseSource m_dbs;
        List<L3MATERIAL_CONFIG> L_L3MATERIAL_CONFIG;
        BATCHHISCODE batchhisCode;
        bool IsChanged = false; //布料矩阵是否变化

        public CalMatrixData(DatabaseSource dbs)
        {
            m_dbs = dbs;
        }
        public bool InitData()
        {
            m_BigDataMatrixLast = new STUFF_MATRIXDATA_CHANGE();
            m_BigDataMatrixNow = new STUFF_MATRIXDATA_CHANGE();
            L_L3MATERIAL_CONFIG = new List<L3MATERIAL_CONFIG>();
            batchhisCode = new BATCHHISCODE();
            return true;
        }
        public bool Calcular(DateTime dtEnd)
        {
            if (m_isFirstRun)
            {
                InitData();
                m_isFirstRun = false;
            }
            DataTable dtSourceData = new DataTable();
            L_L3MATERIAL_CONFIG = GetL3MaterialConfig();
            batchhisCode = GetLastBatchhisCode(dtEnd);

            m_BigDataMatrixLast = GetLastDate(dtEnd);

            if (m_BigDataMatrixLast != null)
            {
                dtSourceData = GetSourceDate(m_BigDataMatrixLast.TIMESTAMP, dtEnd);
            }
            else
            {
                dtSourceData = GetSourceDate(DateTime.MinValue, dtEnd);
            }
            if (dtSourceData == null || dtSourceData.Rows.Count == 0)
            {
                return true;
            }
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                for (int i = 0; i < dtSourceData.Rows.Count; i++)
                {
                    if (AnalysisDate(dtSourceData.Rows[i], ref m_BigDataMatrixNow))
                    {
                        IsChanged = false; //默认没有变化

                        if (judgeIsChanged(m_BigDataMatrixLast, m_BigDataMatrixNow))
                        {

                            //如果布料制度没有变化
                            if (!IsChanged && m_BigDataMatrixLast != null)
                            {
                                m_BigDataMatrixNow.CHANGETIME = m_BigDataMatrixLast.CHANGETIME;
                            }
                            idatabase.Insert(m_BigDataMatrixNow);
                            //更新上次布料制度使用时间
                            if (m_BigDataMatrixLast != null)
                            {
                                double TimeTpan = m_BigDataMatrixNow.CHANGETIME.Subtract(m_BigDataMatrixLast.CHANGETIME).TotalHours;
                                idatabase.ExecuteCommand($"Update STUFF_MATRIXDATA_CHANGE set TIMESPAN = {TimeTpan} where TIMESTAMP={UtilsConvert.GetOraString(m_BigDataMatrixLast.TIMESTAMP)}");
                            }

                            //缓存上次的布料关键数据
                            m_BigDataMatrixLast.TIMESTAMP = m_BigDataMatrixNow.TIMESTAMP;
                            m_BigDataMatrixLast.WEIGHT_ORE = m_BigDataMatrixNow.WEIGHT_ORE;
                            m_BigDataMatrixLast.WEIGHT_COKE = m_BigDataMatrixNow.WEIGHT_COKE;
                            m_BigDataMatrixLast.CHARGE_MATRIX = m_BigDataMatrixNow.CHARGE_MATRIX;
                            m_BigDataMatrixLast.SET_LX_ORE = m_BigDataMatrixNow.SET_LX_ORE;
                            m_BigDataMatrixLast.SET_LX_COKE = m_BigDataMatrixNow.SET_LX_COKE;
                            m_BigDataMatrixLast.ANGLEDIFF_ORE = m_BigDataMatrixNow.ANGLEDIFF_ORE;
                            m_BigDataMatrixLast.ANGLEDIFF_ORE_COKE = m_BigDataMatrixNow.ANGLEDIFF_ORE_COKE;
                            m_BigDataMatrixLast.CENTRAL_COKE_RATIO = m_BigDataMatrixNow.CENTRAL_COKE_RATIO;
                            m_BigDataMatrixLast.MAXANGLE_ORE = m_BigDataMatrixNow.MAXANGLE_ORE;
                            m_BigDataMatrixLast.MAXANGLE_COKE = m_BigDataMatrixNow.MAXANGLE_COKE;
                            m_BigDataMatrixLast.CHANGETIME = m_BigDataMatrixNow.CHANGETIME;
                        }
                    }
                }
                //更新最新布料制度使用时间
                if (m_BigDataMatrixLast != null)
                {
                    double TimeTpan = dtEnd.Subtract(m_BigDataMatrixLast.CHANGETIME).TotalHours;
                    idatabase.ExecuteCommand($"Update STUFF_MATRIXDATA_CHANGE set TIMESPAN = {TimeTpan} where TIMESTAMP={UtilsConvert.GetOraString(m_BigDataMatrixLast.TIMESTAMP)}");
                }
            }

            return true;
        }
        public DataTable GetSourceDate(DateTime dtStart, DateTime dtEnd)
        {
            try
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    string strSql = "";
                    if (dtStart == DateTime.MinValue)
                    {
                        //strSql = $"select RECORD_TIME,ORE_WEIGHT,COKE_WEIGHT1,SET_LX,SET_COKELX,USER_REMARK from MES_DAY_BLJSDATA " +
                        //                      $"where RECORD_TIME = (select max(RECORD_TIME) from  MES_DAY_BLJSDATA where" +
                        //                      $" RECORD_TIME <= {UtilsConvert.GetOraString(dtEnd)})";
                        strSql = $"select * from MES_DAY_BLJSDATA " +
                                              $"where RECORD_TIME = (select max(RECORD_TIME) from  MES_DAY_BLJSDATA where" +
                                              $" RECORD_TIME <= {UtilsConvert.GetOraString(dtEnd)})";
                    }
                    else
                    {
                        //strSql = $"select RECORD_TIME,ORE_WEIGHT,COKE_WEIGHT1,SET_LX,SET_COKELX,USER_REMARK from MES_DAY_BLJSDATA " +
                        //                       $"where RECORD_TIME > {UtilsConvert.GetOraString(dtStart)} and"+
                        //                       $" RECORD_TIME <= {UtilsConvert.GetOraString(dtEnd)} order by RECORD_TIME";
                        strSql = $"select * from MES_DAY_BLJSDATA " +
                                              $"where RECORD_TIME > {UtilsConvert.GetOraString(dtStart)} and" +
                                              $" RECORD_TIME <= {UtilsConvert.GetOraString(dtEnd)} order by RECORD_TIME";
                    }

                    return idatabase.GetDataTable(strSql);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<L3MATERIAL_CONFIG> GetL3MaterialConfig()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                return idatabase.Queryable<L3MATERIAL_CONFIG>().ToList();
            }
        }
        public BATCHHISCODE GetLastBatchhisCode(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string sql = $"select * from BATCHHISCODE where timestamp = (select max(timestamp) from BATCHHISCODE where timestamp <= {UtilsConvert.GetOraString(dt)})";
                return idatabase.GetSingle<BATCHHISCODE>(sql);
            }
        }
        public STUFF_MATRIXDATA_CHANGE GetLastDate(DateTime dtEnd)
        {
            try
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    string strSql = $"select * from STUFF_MATRIXDATA_CHANGE " +
                        $"where TIMESTAMP =(select max(timestamp) from STUFF_MATRIXDATA_CHANGE where timestamp <= {UtilsConvert.GetOraString(dtEnd)})";
                    List<STUFF_MATRIXDATA_CHANGE> L_BMC = idatabase.GetList<STUFF_MATRIXDATA_CHANGE>(strSql);
                    if (L_BMC.Count > 0)
                    {
                        return L_BMC[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AnalysisDate(DataRow OneRowSourceData, ref STUFF_MATRIXDATA_CHANGE bmc_ref)
        {
            bmc_ref.TIMESTAMP = OneRowSourceData["RECORD_TIME"].ToDate(); //记录时间
            bmc_ref.CHANGETIME = GetChangedTime(OneRowSourceData["CHARGENUM"].ToInt(), bmc_ref.TIMESTAMP); //根据料批去找变料的时间
            bmc_ref.WEIGHT_ORE = OneRowSourceData["ORE_WEIGHT"].ToDouble(2);
            bmc_ref.WEIGHT_COKE = OneRowSourceData["COKE_WEIGHT1"].ToDouble(2);
            bmc_ref.SET_LX_ORE = OneRowSourceData["SET_LX"].ToDouble(2);
            bmc_ref.SET_LX_COKE = OneRowSourceData["SET_COKELX"].ToDouble(2);
            bmc_ref.COKE_NUT_WEIGHT = OneRowSourceData["CFD_WEIGHT"].ToDouble(2);
            bmc_ref.SINTER_WEIGHT = OneRowSourceData["SINTER_WEIGHT"].ToDouble(2);
            bmc_ref.GRADE_IN = OneRowSourceData["THERY_RLPW"].ToDouble(2);
            bmc_ref.WINJ = OneRowSourceData["JETCOKE_WEIGHT"].ToDouble(2);
            bmc_ref.BURDENRATIO = OneRowSourceData["THERY_GJFH"].ToDouble(2);
            bmc_ref.TOTAL_BURDENRATIO = OneRowSourceData["THERY_ZJDFH"].ToDouble(2);
            bmc_ref.WINJ_RATIO = OneRowSourceData["THERY_MB"].ToDouble(2);
            bmc_ref.COKE_NUT_RATIO = OneRowSourceData["THERY_JDB"].ToDouble(2);
            bmc_ref.THERY_FE = OneRowSourceData["THERY_FE"].ToDouble(2);
            bmc_ref.THERY_TJ = OneRowSourceData["THERY_TJ"].ToDouble(2);
            bmc_ref.SLAG_SIO2 = OneRowSourceData["SLAG_SIO"].ToDouble(2);
            bmc_ref.SLAG_AL2O3 = OneRowSourceData["SLAG_AL2O3"].ToDouble(2);
            bmc_ref.SLAG_CAO = OneRowSourceData["SLAG_CAO"].ToDouble(2);
            bmc_ref.SLAG_MGO = OneRowSourceData["SLAG_MGO"].ToDouble(2);
            bmc_ref.R2 = OneRowSourceData["R2"].ToDouble(2);
            bmc_ref.THERY_SLAG = OneRowSourceData["THERY_SLAG"].ToDouble(2);
            bmc_ref.THERY_YLZQ = OneRowSourceData["THERY_YLZQ"].ToDouble(2);
            bmc_ref.THERY_GJB = OneRowSourceData["THERY_GJB"].ToDouble(2);
            bmc_ref.THERY_KH = OneRowSourceData["THERY_KH"].ToDouble(2);
            bmc_ref.THERY_ZB = OneRowSourceData["THERY_ZB"].ToDouble(2);
            bmc_ref.FLUX_WEIGHT = OneRowSourceData["ZF1_WEIGHT"].ToDouble(2) + OneRowSourceData["ZF2_WEIGHT"].ToDouble(2); ;
            L3MATERIAL_CONFIG l3MaterialConfig = null;
            l3MaterialConfig = L_L3MATERIAL_CONFIG.Where(m => m.M_TYPE == 7).First();//球团

            if (l3MaterialConfig != null)
            {
                bmc_ref.PELLET_WEIGHT = 0;
                if (batchhisCode.KCODE7 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE7 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.PELLET_WEIGHT += OneRowSourceData["AF1_WEIGHT"].ToDouble(2);
                }
                if (batchhisCode.KCODE8 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE8 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.PELLET_WEIGHT += OneRowSourceData["AF2_WEIGHT"].ToDouble(2);
                }
                if (batchhisCode.KCODE9 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE9 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.PELLET_WEIGHT += OneRowSourceData["OF1_WEIGHT"].ToDouble(2);
                }
                if (batchhisCode.KCODE10 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE10 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.PELLET_WEIGHT += OneRowSourceData["OF2_WEIGHT"].ToDouble(2);
                }
            }
            l3MaterialConfig = L_L3MATERIAL_CONFIG.Single(m => m.M_TYPE == 8);//块矿
            if (l3MaterialConfig != null)
            {
                bmc_ref.CRUDEORE_WEIGHT = 0;
                if (batchhisCode.KCODE7 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE7 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.CRUDEORE_WEIGHT += OneRowSourceData["AF1_WEIGHT"].ToDouble(2);
                }
                if (batchhisCode.KCODE8 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE8 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.CRUDEORE_WEIGHT += OneRowSourceData["AF2_WEIGHT"].ToDouble(2);
                }
                if (batchhisCode.KCODE9 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE9 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.CRUDEORE_WEIGHT += OneRowSourceData["OF1_WEIGHT"].ToDouble(2);
                }
                if (batchhisCode.KCODE10 >= l3MaterialConfig.CODE_MIN && batchhisCode.KCODE10 <= l3MaterialConfig.CODE_MAX)
                {
                    bmc_ref.CRUDEORE_WEIGHT += OneRowSourceData["OF2_WEIGHT"].ToDouble(2);
                }
            }

            double dblSumWeight = bmc_ref.SINTER_WEIGHT + bmc_ref.PELLET_WEIGHT + bmc_ref.CRUDEORE_WEIGHT;
            if (dblSumWeight > 0)
            {
                bmc_ref.SINTER_RATIO = bmc_ref.SINTER_WEIGHT / dblSumWeight * 100;
                bmc_ref.PELLET_RATIO = bmc_ref.PELLET_WEIGHT / dblSumWeight * 100;
                bmc_ref.CRUDEORE_RATIO = bmc_ref.CRUDEORE_WEIGHT / dblSumWeight * 100;
            }

            bmc_ref.CHARGE_MATRIX = "";
            //K: 1:43()2:40(3)3:38(2)4:36(2)5:34(2)6:31(2)7:29(2)8:27()9:23()10:20(4)11:17() P: 1:43()2:38(2)3:36(2)4:34(2)5:32(2)6:29(2)7:29()8:25()9:20()10:19()11:19()
            string StuffMatrixStr = OneRowSourceData["USER_REMARK"].ToString();
            StuffMatrixStr = StuffMatrixStr.Remove(0, 3);
            string[] sArray = Regex.Split(StuffMatrixStr, "P:", RegexOptions.IgnoreCase);
            if (sArray.Length != 2)
            {
                return false;
            }
            string StuffMatrixStr_K = sArray[1].Trim();
            string StuffMatrixStr_J = sArray[0].Trim();
            double[] AngleArray_K = new double[11];
            double[] AngleArray_J = new double[11];
            double[] CircleArray_K = new double[11];
            double[] CircleArray_J = new double[11];
            for (int i = 1; i <= 11; i++)
            {
                string strTemp = string.Format("{0}:", i);
                int iIndex = StuffMatrixStr_K.IndexOf(strTemp);
                int iIndexOfLeftBracket = StuffMatrixStr_K.IndexOf('(', iIndex);
                double dblAngle = StuffMatrixStr_K.Substring(iIndex + strTemp.Length, iIndexOfLeftBracket - iIndex - strTemp.Length).ToDouble(2);
                int iIndexOfRightBracket = StuffMatrixStr_K.IndexOf(')', iIndexOfLeftBracket);
                double dblCircle = 0;
                if (iIndexOfRightBracket - iIndexOfLeftBracket > 1)
                {
                    dblCircle = StuffMatrixStr_K.Substring(iIndexOfLeftBracket + 1, iIndexOfRightBracket - iIndexOfLeftBracket - 1).ToDouble(2);
                }
                AngleArray_K[i - 1] = dblAngle;
                CircleArray_K[i - 1] = dblCircle;

                iIndex = StuffMatrixStr_J.IndexOf(strTemp);
                iIndexOfLeftBracket = StuffMatrixStr_J.IndexOf('(', iIndex);
                dblAngle = StuffMatrixStr_J.Substring(iIndex + strTemp.Length, iIndexOfLeftBracket - iIndex - strTemp.Length).ToDouble(2);
                iIndexOfRightBracket = StuffMatrixStr_J.IndexOf(')', iIndexOfLeftBracket);
                dblCircle = 0;
                if (iIndexOfRightBracket - iIndexOfLeftBracket > 1)
                {
                    dblCircle = StuffMatrixStr_J.Substring(iIndexOfLeftBracket + 1, iIndexOfRightBracket - iIndexOfLeftBracket - 1).ToDouble(2);
                }
                AngleArray_J[i - 1] = dblAngle;
                CircleArray_J[i - 1] = dblCircle;
            }
            string strMatrixK = "P:";
            string strMatrixJ = "K:";
            double dblAngleK_Min = double.MaxValue;
            double dblAngleK_Max = 0;
            double dblAngleJ_Min = double.MaxValue;
            double dblAngleJ_Max = 0;
            double dblCircleK_Sum = 0;
            double dblCircleJ_Sum = 0;
            double dblMulSum_K = 0;
            double dblMulSum_J = 0;
            double dblSumCircleLessThenMinAngelK_J = 0;
            for (int i = 0; i < 11; i++)
            {
                if (CircleArray_K[i] != 0)
                {
                    strMatrixK += $" {AngleArray_K[i]}({CircleArray_K[i]})";
                    if (dblAngleK_Min > AngleArray_K[i])
                    {
                        dblAngleK_Min = AngleArray_K[i];
                    }
                    if (dblAngleK_Max < AngleArray_K[i])
                    {
                        dblAngleK_Max = AngleArray_K[i];
                    }
                    dblCircleK_Sum += CircleArray_K[i];
                    dblMulSum_K = CircleArray_K[i] * AngleArray_K[i];
                }
            }
            for (int i = 0; i < 11; i++)
            {
                if (CircleArray_J[i] != 0)
                {
                    strMatrixJ += $" {AngleArray_J[i]}({CircleArray_J[i]})";
                    if (dblAngleJ_Min > AngleArray_J[i])
                    {
                        dblAngleJ_Min = AngleArray_J[i];
                    }
                    if (dblAngleJ_Max < AngleArray_J[i])
                    {
                        dblAngleJ_Max = AngleArray_J[i];
                    }
                    dblMulSum_J = CircleArray_J[i] * AngleArray_J[i];
                    dblCircleJ_Sum += CircleArray_J[i];

                    if (dblAngleK_Min - AngleArray_J[i] >= 2)
                    {
                        dblSumCircleLessThenMinAngelK_J += CircleArray_J[i];
                    }
                }
            }
            bmc_ref.CHARGE_MATRIX = strMatrixJ + ";" + strMatrixK;
            bmc_ref.CENTRAL_COKE_RATIO = dblCircleJ_Sum == 0 ? 0 : 100 * dblSumCircleLessThenMinAngelK_J / dblCircleJ_Sum;
            bmc_ref.ANGLEDIFF_ORE_COKE = (dblCircleK_Sum == 0 || dblCircleJ_Sum == 0) ? 0 : dblMulSum_K / dblCircleK_Sum - dblMulSum_J / dblCircleJ_Sum;
            bmc_ref.ANGLEDIFF_ORE = dblAngleK_Max - dblAngleK_Min;
            bmc_ref.ANGLEDIFF_COKE = dblAngleJ_Max - dblAngleJ_Min;


            bmc_ref.MINANGLE_ORE = dblAngleK_Min;
            bmc_ref.MAXANGLE_ORE = dblAngleK_Max;
            bmc_ref.MINANGLE_COKE = dblAngleJ_Min;
            bmc_ref.MAXANGLE_COKE = dblAngleJ_Max;

            return true;
        }
        public bool judgeIsChanged(STUFF_MATRIXDATA_CHANGE smcOld, STUFF_MATRIXDATA_CHANGE smcNew)
        {
            if (smcOld == null)
            {
                //没有前次数据，直接返回true，将新数据入库
                return true;
            }
            if (smcOld.CHARGE_MATRIX != smcNew.CHARGE_MATRIX)
            {
                IsChanged = true; //布料制度变化了
                return true;
            }
            if (smcOld.WEIGHT_ORE != smcNew.WEIGHT_ORE)
            {
                return true;
            }
            if (smcOld.WEIGHT_COKE != smcNew.WEIGHT_COKE)
            {
                return true;
            }
            if (smcOld.SET_LX_ORE != smcNew.SET_LX_ORE)
            {
                return true;
            }
            if (smcOld.SET_LX_COKE != smcNew.SET_LX_COKE)
            {
                return true;
            }

            return false;
        }

        public DateTime GetChangedTime(int Charge, DateTime time)
        {
            int Hour = time.Hour;
            DateTime CalTime = time.ToString("yyyy-MM-dd").ToDate();

            if (Hour >= 20)
            {
                CalTime = CalTime.AddDays(1).AddHours(8);
            }
            else if (Hour < 8)
            {
                CalTime = CalTime.AddHours(8);
            }
            else
            {
                CalTime = CalTime.AddHours(20);
            }
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                return idatabase.GetDateTime($"select max(timestamp) from BATCHHIS t  where  timestamp<{UtilsConvert.GetOraString(CalTime)} and timestamp>={UtilsConvert.GetOraString(CalTime.AddHours(-12))} and a_charges={Charge} ");
            }

        }

    }
    /// <summary>
    /// 计算出铁相关数据
    /// </summary>
    class CalTappingData
    {
        DatabaseSource m_dbs;
        List<MES_DAY_IRON> SourceDataList = new List<MES_DAY_IRON>();
        public CalTappingData(DatabaseSource dbs)
        {
            m_dbs = dbs;
        }
        public bool Calcular(DateTime currentTime)
        {
            bool success = false;
            #region 处理时间
            DateTime TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate();
            if (currentTime.Hour >= 20 && currentTime.Hour < 24)
            {
                TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(1).AddHours(20);
            }
            else
            {
                TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddHours(20);
            }
            #endregion
            int DealDayNumb = 3; //处理3天的数据
                                 //如果读取原始数据没有记录，返回
            if (!ReadMesDayIron(TodayTime.AddDays(-DealDayNumb), TodayTime))
            {
                return success;
            }
            List<TAPPING_STATIS> reusltList = new List<TAPPING_STATIS>();
            TAPPING_STATIS tempModel = null;
            for (int i = 0; i < DealDayNumb; i++)
            {
                tempModel = CalOneDay(TodayTime.AddDays(-i));
                if (tempModel != null)
                {
                    reusltList.Add(tempModel);
                }
            }
            #region 插入
            if (reusltList.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    try
                    {
                        string deleteSQL = $"delete from TAPPING_STATIS where timestamp between {DataBaseFactory.ConvertTimeString(TodayTime.AddDays(1 - DealDayNumb), DataBaseType.Oracle)}" +
                                                 $"and {DataBaseFactory.ConvertTimeString(TodayTime, DataBaseType.Oracle)}";
                        idatabase.BeginTran();
                        idatabase.ExecuteCommand(deleteSQL);
                        idatabase.InsertRange(reusltList);
                        idatabase.CommitTran();
                        success = true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        idatabase.RollbackTran();
                        reusltList.Clear();
                        reusltList = null;
                    }
                }
            }
            #endregion
            return success;
        }
        public TAPPING_STATIS CalOneDay(DateTime endTime)
        {
            TAPPING_STATIS model = null;
            List<MES_DAY_IRON> sourceData = new List<MES_DAY_IRON>();
            sourceData = SourceDataList.FindAll(m => m.OUT_START_TIME > endTime.AddDays(-1) && m.OUT_START_TIME <= endTime && m.OUT_END_TIME != DateTime.MinValue && m.OUT_TIME != DateTime.MinValue);
            if (sourceData.Count > 0)
            {
                model = new TAPPING_STATIS();
                model.TIMESTAMP = endTime;
                List<MES_DAY_IRON> EastSourceData = sourceData.FindAll(m => m.IRON_MOUTH_NO == "东");
                if (EastSourceData != null && EastSourceData.Count > 0)
                {
                    model.E_HMTE = EastSourceData.Average(m => m.IRON_TEMP1);
                    model.E_HOLELENGTH = EastSourceData.Average(m => m.IRON_MOUTH_DEPTH);
                    //model.E_IMBROKENCOUNT = EastSourceData.Count(m => m.DAI_LIU == "是;
                    model.E_IMBROKENCOUNT = EastSourceData.Count(m => m.REMARK != null && m.REMARK.Contains("漏"));
                    model.E_SLADEWEIGHT = EastSourceData.Average(m => m.FRUN_DEGR);
                    model.E_IMBROKENRATE = EastSourceData.Average(m => m.IRON_MOUTH_DEPTH);
                    model.E_TAPCOUNT = EastSourceData.Count;
                    model.E_TAPTIMESPAN = EastSourceData.Average(m => m.OUT_END_TIME.Subtract(m.OUT_START_TIME).TotalMinutes);
                    model.E_SLAGTIMESPAN = EastSourceData.Average(m => m.OUT_TIME.Subtract(m.OUT_START_TIME).TotalMinutes);
                    model.E_IMBROKENRATE = model.E_IMBROKENCOUNT / model.E_TAPCOUNT;
                }
                List<MES_DAY_IRON> WestSourceData = sourceData.FindAll(m => m.IRON_MOUTH_NO == "西");
                if (WestSourceData != null && WestSourceData.Count > 0)
                {
                    model.W_HMTE = WestSourceData.Average(m => m.IRON_TEMP1);
                    model.W_HOLELENGTH = WestSourceData.Average(m => m.IRON_MOUTH_DEPTH);
                    // model.W_IMBROKENCOUNT = WestSourceData.Count(m => m.DAI_LIU == "是
                    model.W_IMBROKENCOUNT = WestSourceData.Count(m => m.REMARK != null && m.REMARK.Contains("漏"));
                    model.W_SLADEWEIGHT = WestSourceData.Average(m => m.FRUN_DEGR);
                    model.W_IMBROKENRATE = WestSourceData.Average(m => m.IRON_MOUTH_DEPTH);
                    model.W_TAPCOUNT = WestSourceData.Count;
                    model.W_TAPTIMESPAN = WestSourceData.Average(m => m.OUT_END_TIME.Subtract(m.OUT_START_TIME).TotalMinutes);
                    model.W_SLAGTIMESPAN = WestSourceData.Average(m => m.OUT_TIME.Subtract(m.OUT_START_TIME).TotalMinutes);
                    model.W_IMBROKENRATE = model.W_IMBROKENCOUNT / model.W_TAPCOUNT;
                }
            }
            return model;
        }

        public bool ReadMesDayIron(DateTime startTime, DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                SourceDataList = idatabase.Queryable<MES_DAY_IRON>().Where(m => m.OUT_START_TIME > startTime && m.OUT_START_TIME <= endTime).ToList();
            }
            if (SourceDataList.Count == 0)
            {
                return false;
            }
            return true;
        }

    }
    /// <summary>
    /// 计算投料平衡数据
    /// </summary>
    class CalBatchhisDayWeight
    {
        DatabaseSource m_dbs;
        Dictionary<string, string> DicInput = new Dictionary<string, string>(); //投料字典
        Dictionary<string, double> DicCang = new Dictionary<string, double>();//仓位阶段
        BATCHHISCODE CodeModel = new BATCHHISCODE();
        BATCHHIS_CS_WEIGHT Model = new BATCHHIS_CS_WEIGHT();
        BATCHHIS_CS_WEIGHT LastModel = new BATCHHIS_CS_WEIGHT();
        AV30S Av30s = new AV30S();
        List<MES_TYMIRE1_IOOP> TYMIRE1_IOOPList = new List<MES_TYMIRE1_IOOP>();
        public CalBatchhisDayWeight(DatabaseSource dbs)
        {
            m_dbs = dbs;
            DicInput.Add("D05010020134070G", "铜陵一级干熄焦");
            DicInput.Add("D05010020290000G", "外购一级焦炭");
            DicInput.Add("D05010020234070G", "铜陵一级焦碳");
            DicInput.Add("D0501003033408G", "强二级焦");
            DicInput.Add("D05010030234070G", "铜陵准一级焦");

            DicCang.Add("4米", 4);
            DicCang.Add("5米", 5);
            DicCang.Add("6米", 6);
            DicCang.Add("7.1米", 7.1);
            DicCang.Add("8米", 8);
            DicCang.Add("9米", 9);
            DicCang.Add("10米", 10);
            DicCang.Add("11米", 11);
        }
        public void Calcular(DateTime currentTime)
        {
            int Hour = currentTime.Hour;
            DateTime CalTime = currentTime.ToString("yyyy-MM-dd").ToDate();
            if (Hour > 20)
            {
                CalTime = CalTime.AddDays(1);
            }
            CalTime = CalTime.AddHours(20).AddDays(-1);

            ReadData(CalTime);
            List<BATCHHIS_INPUT> ModelList = CalInputWeight(CalTime); //计算BATCHHIS_INPUT

            CalCangWeight(CalTime); //计算BATCHHIS_CS_WEIGHT
            #region 插入

            if (ModelList.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    try
                    {
                        string deleteSQL = $"delete from BATCHHIS_INPUT where timestamp = {DataBaseFactory.ConvertTimeString(CalTime, DataBaseType.Oracle)}";
                        idatabase.BeginTran();
                        idatabase.ExecuteCommand(deleteSQL);
                        idatabase.InsertRange(ModelList);
                        idatabase.CommitTran();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        idatabase.RollbackTran();
                        ModelList.Clear();
                        ModelList = null;
                    }
                }
            }
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                try
                {
                    string deleteSQL = $"delete from BATCHHIS_CS_WEIGHT where timestamp = {DataBaseFactory.ConvertTimeString(CalTime, DataBaseType.Oracle)}";
                    idatabase.BeginTran();
                    idatabase.ExecuteCommand(deleteSQL);
                    idatabase.Insert<BATCHHIS_CS_WEIGHT>(Model);
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
            #endregion
        }
        public void ReadData(DateTime Time)
        {

            List<AV30S> listModel = new List<AV30S>();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                listModel = idatabase.Queryable<AV30S>().Where(m => m.TIMESTAMP <= Time && m.TIMESTAMP >= Time.AddDays(-1)).ToList();
                TYMIRE1_IOOPList = idatabase.GetList<MES_TYMIRE1_IOOP>($"select * from MES_TYMIRE1_IOOP where to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<={DataBaseFactory.ConvertTimeString(Time, DataBaseType.Oracle)} and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>{DataBaseFactory.ConvertTimeString(Time.AddDays(-1), DataBaseType.Oracle)} and BACKLOG_CODE='T6'");
                LastModel = idatabase.GetSingle<BATCHHIS_CS_WEIGHT>($"select * from BATCHHIS_CS_WEIGHT where timestamp = {DataBaseFactory.ConvertTimeString(Time.AddDays(-1), DataBaseType.Oracle)}");
                CodeModel = idatabase.GetSingle<BATCHHISCODE>($"select * from BATCHHISCODE t where timestamp=(select max(timestamp) from BATCHHISCODE where timestamp<={DataBaseFactory.ConvertTimeString(Time, DataBaseType.Oracle)}) ");
            }
            if (listModel.Count > 0)
            {
                Av30s = listModel.Find(m => m.TIMESTAMP == listModel.Max(match => match.TIMESTAMP));
            }
        }
        /// <summary>
        /// 计算投料
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<BATCHHIS_INPUT> CalInputWeight(DateTime time)
        {
            List<BATCHHIS_INPUT> ResultList = new List<BATCHHIS_INPUT>();
            BATCHHIS_INPUT Model = null;

            //            COKETOTAL
            if (TYMIRE1_IOOPList.Count > 0)
            {
                string code = "D05010020134070G";
                int index = TYMIRE1_IOOPList.FindIndex(m => m.MAT_PROD_CODE == code);
                if (index >= 0)
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE1", MAT_NAME = DicInput[code], MAT_WEIGHT = TYMIRE1_IOOPList.FindAll(m => m.MAT_PROD_CODE == code).Sum(mc => mc.CARRY_WGT) };
                    ResultList.Add(Model);
                }
                else
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE1", MAT_NAME = DicInput[code], MAT_WEIGHT = 0 };
                    ResultList.Add(Model);
                }
                code = "D05010020290000G";
                index = TYMIRE1_IOOPList.FindIndex(m => m.MAT_PROD_CODE == code);
                if (index >= 0)
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE2", MAT_NAME = DicInput[code], MAT_WEIGHT = TYMIRE1_IOOPList.FindAll(m => m.MAT_PROD_CODE == code).Sum(mc => mc.CARRY_WGT) };
                    ResultList.Add(Model);
                }
                else
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE2", MAT_NAME = DicInput[code], MAT_WEIGHT = 0 };
                    ResultList.Add(Model);
                }
                code = "D05010020234070G";
                index = TYMIRE1_IOOPList.FindIndex(m => m.MAT_PROD_CODE == code);
                if (index >= 0)
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE3", MAT_NAME = DicInput[code], MAT_WEIGHT = TYMIRE1_IOOPList.FindAll(m => m.MAT_PROD_CODE == code).Sum(mc => mc.CARRY_WGT) };
                    ResultList.Add(Model);
                }
                else
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE3", MAT_NAME = DicInput[code], MAT_WEIGHT = 0 };
                    ResultList.Add(Model);
                }
                code = "D0501003033408G";
                index = TYMIRE1_IOOPList.FindIndex(m => m.MAT_PROD_CODE == code);
                if (index >= 0)
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE4", MAT_NAME = DicInput[code], MAT_WEIGHT = TYMIRE1_IOOPList.FindAll(m => m.MAT_PROD_CODE == code).Sum(mc => mc.CARRY_WGT) };
                    ResultList.Add(Model);
                }
                else
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE4", MAT_NAME = DicInput[code], MAT_WEIGHT = 0 };
                    ResultList.Add(Model);
                }
                code = "D05010030234070G";
                index = TYMIRE1_IOOPList.FindIndex(m => m.MAT_PROD_CODE == code);
                if (index >= 0)
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE5", MAT_NAME = DicInput[code], MAT_WEIGHT = TYMIRE1_IOOPList.FindAll(m => m.MAT_PROD_CODE == code).Sum(mc => mc.CARRY_WGT) };
                    ResultList.Add(Model);
                }
                else
                {
                    Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKE5", MAT_NAME = DicInput[code], MAT_WEIGHT = 0 };
                    ResultList.Add(Model);
                }
            }
            if (ResultList.Count > 0)
            {
                Model = new BATCHHIS_INPUT() { TIMESTAMP = time, MAT_CODE = "COKETOTAL", MAT_NAME = "焦炭总量", MAT_WEIGHT = ResultList.Sum(m => m.MAT_WEIGHT) };
                ResultList.Add(Model);
            }
            return ResultList;
        }

        /// <summary>
        /// 计算BATCHHIS_CS_WEIGHT
        /// </summary>
        /// <param name="time"></param>
        public void CalCangWeight(DateTime time)
        {
            Model.TIMESTAMP = time;
            Model.A_SORE1_CS = Av30s.A_SORE1_CS;
            Model.A_SORE2_CS = Av30s.A_SORE2_CS;
            Model.A_SORE3_CS = Av30s.A_SORE3_CS;
            Model.A_SORE4_CS = Av30s.A_SORE4_CS;
            Model.A_SORE5_CS = Av30s.A_SORE5_CS;
            Model.A_SORE6_CS = Av30s.A_SORE6_CS;
            Model.A_SORE7_CS = Av30s.A_SORE7_CS;
            Model.A_SORE8_CS = Av30s.A_SORE8_CS;
            Model.A_SORE9_CS = Av30s.A_SORE9_CS;
            Model.A_SORE10_CS = Av30s.A_SORE10_CS;
            Model.A_SORE11_CS = Av30s.A_SORE11_CS;
            Model.A_SORE12_CS = Av30s.A_SORE12_CS;
            Model.A_SCKE1_CS = Av30s.A_SCKE1_CS;
            Model.A_SCKE2_CS = Av30s.A_SCKE2_CS;
            Model.A_SCKE3_CS = Av30s.A_SCKE3_CS;
            Model.A_SCKE4_CS = Av30s.A_SCKE4_CS;
            Model.A_SCKE5_CS = Av30s.A_SCKE5_CS;

            //Model.A_SORE1_CS = 10.46;
            //Model.A_SORE2_CS = 10.28;
            //Model.A_SORE3_CS = 9.96;
            //Model.A_SORE4_CS = 9.49;
            //Model.A_SORE5_CS = 9.53;
            //Model.A_SORE6_CS = 8.68;
            //Model.A_SORE7_CS = 8.75;
            //Model.A_SORE8_CS = 9.15;
            //Model.A_SORE9_CS = 8.55;
            //Model.A_SORE10_CS = 8.01;
            //Model.A_SORE11_CS = 8.42;
            //Model.A_SORE12_CS = 9;
            //Model.A_SCKE1_CS = 8.62;
            //Model.A_SCKE2_CS = 8.64;
            //Model.A_SCKE3_CS = 8.83;
            //Model.A_SCKE4_CS = 9.61;
            //Model.A_SCKE5_CS = Av30s.A_SCKE5_CS;

            Model.A_SORE1_WEIGHT = CalWight(Model.A_SORE1_CS, 1);
            Model.A_SORE2_WEIGHT = CalWight(Model.A_SORE2_CS, 2);
            Model.A_SORE3_WEIGHT = CalWight(Model.A_SORE3_CS, 3);
            Model.A_SORE4_WEIGHT = CalWight(Model.A_SORE4_CS, 4);
            Model.A_SORE5_WEIGHT = CalWight(Model.A_SORE5_CS, 5);
            Model.A_SORE6_WEIGHT = CalWight(Model.A_SORE6_CS, 6);
            Model.A_SORE7_WEIGHT = CalWight(Model.A_SORE7_CS, 7);
            Model.A_SORE8_WEIGHT = CalWight(Model.A_SORE8_CS, 8);
            Model.A_SORE9_WEIGHT = CalWight(Model.A_SORE9_CS, 9);
            Model.A_SORE10_WEIGHT = CalWight(Model.A_SORE10_CS, 10);
            Model.A_SORE11_WEIGHT = CalWight(Model.A_SORE11_CS, 11);
            Model.A_SORE12_WEIGHT = CalWight(Model.A_SORE12_CS, 12);
            Model.A_SCKE1_WEIGHT = CalWight(Model.A_SCKE1_CS, 13);
            Model.A_SCKE2_WEIGHT = CalWight(Model.A_SCKE2_CS, 14);
            Model.A_SCKE3_WEIGHT = CalWight(Model.A_SCKE3_CS, 15);
            Model.A_SCKE4_WEIGHT = CalWight(Model.A_SCKE4_CS, 16);
            Model.A_SCKE5_WEIGHT = CalWight(Model.A_SCKE5_CS, 17);

            Model.A_COKE1 = MerageWeight(37);
            Model.A_COKE2 = MerageWeight(35);
            Model.A_COKE3 = MerageWeight(31);
            Model.A_COKE4 = MerageWeight(44);
            Model.A_COKE5 = MerageWeight(32);

            Model.A_COKE_TOTALWEIGHT = Model.A_COKE1 + Model.A_COKE2 + Model.A_COKE3 + Model.A_COKE4 + Model.A_COKE5;
            if (LastModel != null)
            {
                Model.A_COKE_WEIGHTDIFF = Model.A_COKE_TOTALWEIGHT - LastModel.A_COKE_TOTALWEIGHT;

            }
        }
        public double CalWight(double CangWei, int Numb)
        {
            double weight = 0;
            //焦丁
            if (Numb == 1)
            {
                weight = CalLittleCoke(CangWei);
            }
            //烧结         
            if (Numb == 2)
            {
                weight = CalSinter1(CangWei);
            }
            else if (Numb == 3)
            {
                weight = CalSinter2_4_5(CangWei);
            }
            else if (Numb == 4)
            {
                weight = CalSinter3(CangWei);
            }
            else if (Numb == 5)
            {
                weight = CalSinter2_4_5(CangWei);
            }
            else if (Numb == 6)
            {
                weight = CalSinter2_4_5(CangWei);
            }
            //球团
            else if (Numb == 7)
            {
                weight = CalPellet12(CangWei);
            }
            else if (Numb == 8)
            {
                weight = CalPellet12(CangWei);
            }
            //块矿
            else if (Numb == 9)
            {
                weight = CalCrude12(CangWei);
            }
            else if (Numb == 10)
            {
                weight = CalCrude12(CangWei);
            }
            //杂矿
            else if (Numb == 11)
            {
                weight = CalFlux12(CangWei);
            }
            else if (Numb == 12)
            {
                weight = CalFlux12(CangWei);
            }
            //焦炭
            else if (Numb == 13)
            {
                weight = CalCoke1234(CangWei);
            }
            else if (Numb == 14)
            {
                weight = CalCoke1234(CangWei);
            }
            else if (Numb == 15)
            {
                weight = CalCoke1234(CangWei);
            }
            else if (Numb == 16)
            {
                weight = CalCoke1234(CangWei);
            }
            else if (Numb == 17)
            {
                weight = CalLittleCoke(CangWei);
            }
            return weight;
        }

        /// <summary>
        /// 烧结矿1仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalSinter1(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 158 + (Cangwei - DicCang["7.1米"]) * (383 - 210) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 130 + (Cangwei - DicCang["6米"]) * (158 - 130);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 110 + (Cangwei - DicCang["5米"]) * (130 - 110);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 96 + (Cangwei - DicCang["4米"]) * (110 - 96);
            }
            else
            {
                Volume = Cangwei / 4.0 * 96;
            }
            return Volume * 1.8;
        }
        /// <summary>
        /// 烧结矿2_4_5仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalSinter2_4_5(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 158 + (Cangwei - DicCang["7.1米"]) * (383 - 210) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 123 + (Cangwei - DicCang["6米"]) * (158 - 123);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 98 + (Cangwei - DicCang["5米"]) * (123 - 98);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 53 + (Cangwei - DicCang["4米"]) * (98 - 53);
            }
            else
            {
                Volume = Cangwei / 4.0 * 53;
            }
            return Volume * 1.8;
        }
        /// <summary>
        /// 烧结矿3仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalSinter3(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 109 + (Cangwei - DicCang["7.1米"]) * (245 - 140) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 91 + (Cangwei - DicCang["6米"]) * (109 - 91);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 77 + (Cangwei - DicCang["5米"]) * (91 - 77);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 53 + (Cangwei - DicCang["4米"]) * (77 - 53);
            }
            else
            {
                Volume = Cangwei / 4.0 * 67;
            }
            return Volume * 1.8;
        }
        /// <summary>
        /// 球团1、2仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalPellet12(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 109 + (Cangwei - DicCang["7.1米"]) * (245 - 140) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 81 + (Cangwei - DicCang["6米"]) * (109 - 81);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 59 + (Cangwei - DicCang["5米"]) * (81 - 59);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 43 + (Cangwei - DicCang["4米"]) * (59 - 43);
            }
            else
            {
                Volume = Cangwei / 4.0 * 43;
            }
            return Volume * 2.2;
        }
        /// <summary>
        /// 块矿1、2仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalCrude12(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 109 + (Cangwei - DicCang["7.1米"]) * (245 - 140) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 81 + (Cangwei - DicCang["6米"]) * (109 - 81);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 59 + (Cangwei - DicCang["5米"]) * (81 - 59);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 43 + (Cangwei - DicCang["4米"]) * (59 - 43);
            }
            else
            {
                Volume = Cangwei / 4.0 * 43;
            }
            return Volume * 2;
        }
        /// <summary>
        /// 杂矿1、2仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalFlux12(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 109 + (Cangwei - DicCang["7.1米"]) * (245 - 140) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 81 + (Cangwei - DicCang["6米"]) * (109 - 81);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 59 + (Cangwei - DicCang["5米"]) * (81 - 59);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 43 + (Cangwei - DicCang["4米"]) * (59 - 43);
            }
            else
            {
                Volume = Cangwei / 4.0 * 43;
            }
            return Volume * 1.5;
        }
        /// <summary>
        /// 焦炭1、2、3、4仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalCoke1234(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 158 + (Cangwei - DicCang["7.1米"]) * (383 - 210) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 123 + (Cangwei - DicCang["6米"]) * (158 - 123);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 98 + (Cangwei - DicCang["5米"]) * (123 - 98);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 53 + (Cangwei - DicCang["4米"]) * (98 - 53);
            }
            else
            {
                Volume = Cangwei / 4.0 * 53;
            }
            return Volume * 0.65;
        }
        /// <summary>
        /// 焦丁仓
        /// </summary>
        /// <param name="Cangwei"></param>
        /// <returns></returns>
        public double CalLittleCoke(double Cangwei)
        {
            double Volume = 0;
            if (Cangwei > DicCang["7.1米"])
            {
                Volume = 128 + (Cangwei - DicCang["7.1米"]) * (297 - 170) / 3.0;
            }
            else if (Cangwei > DicCang["6米"])
            {
                Volume = 107 + (Cangwei - DicCang["6米"]) * (128 - 107);
            }
            else if (Cangwei > DicCang["5米"])
            {
                Volume = 91 + (Cangwei - DicCang["5米"]) * (107 - 91);
            }
            else if (Cangwei > DicCang["4米"])
            {
                Volume = 39 + (Cangwei - DicCang["4米"]) * (91 - 39);
            }
            else
            {
                Volume = Cangwei / 4.0 * 39;
            }
            return Volume * 0.65;
        }
        //合并重量
        public double MerageWeight(int Code)
        {
            double Weight = 0;
            if (CodeModel.JCODE1 == Code)
            {
                Weight += Model.A_SCKE1_WEIGHT;
            }
            if (CodeModel.JCODE2 == Code)
            {
                Weight += Model.A_SCKE2_WEIGHT;
            }
            if (CodeModel.JCODE3 == Code)
            {
                Weight += Model.A_SCKE3_WEIGHT;
            }
            if (CodeModel.JCODE4 == Code)
            {
                Weight += Model.A_SCKE4_WEIGHT;
            }
            if (CodeModel.JCODE5 == Code)
            {
                Weight += Model.A_SCKE5_WEIGHT;
            }
            return Weight;
        }

    }

    class CalMatrixData2
    {
        public DatabaseSource m_dbs;
        public bool m_isFirstRun = true;
        STUFF_MATRIXDATA_CHANGE_C LastData;
        STUFF_MATRIXDATA_CHANGE_C NowData;
        List<STUFF_MATRIXDATA_CHANGE_C> L_NowDataList;
        List<L3MATERIAL_CONFIG> L_L3MATERIAL_CONFIG;
        //List<MODEL_VISCOSITY> m_L_MODEL_VISCOSITY;
        //List<MATERIAL_BALANCE_BAT> m_L_materialBalanceBat;
        //  BATCHHISCODE batchhisCode;
        int iBinCount = 17;
        // bool IsChanged = false; //布料矩阵是否变化
        string setSumWeightFieldName;
        List<BATCHHIS_DUMP> m_L_BatchhisDump_New;
        List<BATCHHIS> m_L_Batchhis_New;
        List<STUFF_MATRIX> m_L_StuffMatrix_Ore_New;
        List<STUFF_MATRIX> m_L_StuffMatrix_Coke_New;
        public string SetSumWeightFieldName
        {
            get
            {
                if (string.IsNullOrEmpty(setSumWeightFieldName))
                {
                    setSumWeightFieldName = "(weight1_set";
                    for (int i = 2; i <= iBinCount; i++)
                    {
                        setSumWeightFieldName += string.Format("+weight{0}_set", i);
                    }
                    setSumWeightFieldName += ") as setweight ";
                }

                return setSumWeightFieldName;
            }
        }

        public CalMatrixData2(DatabaseSource dbs)
        {
            m_dbs = dbs;
            LastData = new STUFF_MATRIXDATA_CHANGE_C();
            NowData = new STUFF_MATRIXDATA_CHANGE_C();
        }
        public bool InitData(DateTime dtEnd)
        {
            LastData = new STUFF_MATRIXDATA_CHANGE_C();
            NowData = new STUFF_MATRIXDATA_CHANGE_C();
            L_NowDataList = new List<STUFF_MATRIXDATA_CHANGE_C>();
            L_L3MATERIAL_CONFIG = new List<L3MATERIAL_CONFIG>();
            //   batchhisCode = new BATCHHISCODE();

            m_L_BatchhisDump_New = new List<BATCHHIS_DUMP>();
            m_L_StuffMatrix_Ore_New = new List<STUFF_MATRIX>();
            m_L_StuffMatrix_Coke_New = new List<STUFF_MATRIX>();
            m_L_Batchhis_New = new List<BATCHHIS>();
            //m_L_MODEL_VISCOSITY = new List<MODEL_VISCOSITY>();
            //m_L_materialBalanceBat = new List<MATERIAL_BALANCE_BAT>();
            LastData = GetLastDate(dtEnd);
            return true;
        }
        public bool Calcular(DateTime dtEnd)
        {
            // dtEnd = new DateTime(2019, 4, 25, 2, 0, 28);
            if (m_isFirstRun)
            {
                InitData(dtEnd);
                m_isFirstRun = false;
            }
            L_L3MATERIAL_CONFIG = GetL3MaterialConfig();
            //  batchhisCode = GetLastBatchhisCode(dtEnd);
            if (GetNewDataList(LastData == null ? dtEnd.AddMonths(-2) : LastData.TIMESTAMP, dtEnd))
            {
                STUFF_MATRIX stuffMatrixOre = null;
                STUFF_MATRIX stuffMatrixCoke = null;
                BATCHHIS_DUMP batchhisDumpOre = null;
                BATCHHIS_DUMP batchhisDumpCoke = null;
                //MODEL_VISCOSITY modelViscosity = null;
                //MATERIAL_BALANCE_BAT materialBalacceBat = null;
                BATCHHIS batchhis = null;
                for (int i = 0; i < m_L_StuffMatrix_Ore_New.Count; i++)
                {
                    stuffMatrixOre = m_L_StuffMatrix_Ore_New[i];
                    stuffMatrixCoke = m_L_StuffMatrix_Coke_New.Where(m => m.A_CHARGE == m_L_StuffMatrix_Ore_New[i].A_CHARGE && m.TIMESTAMP >= m_L_StuffMatrix_Ore_New[i].TIMESTAMP.AddHours(-1)).Count() > 0
                        ? m_L_StuffMatrix_Coke_New.Where(m => m.A_CHARGE == m_L_StuffMatrix_Ore_New[i].A_CHARGE && m.TIMESTAMP >= m_L_StuffMatrix_Ore_New[i].TIMESTAMP.AddHours(-1)).First() : null;
                    batchhisDumpOre = m_L_BatchhisDump_New.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).Count() > 0
                        ? m_L_BatchhisDump_New.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).First() : null;
                    if (stuffMatrixCoke != null)
                    {
                        batchhisDumpCoke = m_L_BatchhisDump_New.Where(m => m.TIMESTAMP == stuffMatrixCoke.A_BATCHHISDUMPTIME).Count() > 0
                            ? m_L_BatchhisDump_New.Where(m => m.TIMESTAMP == stuffMatrixCoke.A_BATCHHISDUMPTIME).First() : null;
                    }
                    batchhis = m_L_Batchhis_New.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).Count() > 0
                        ? m_L_Batchhis_New.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).First() : null;

                    //modelViscosity =  m_L_MODEL_VISCOSITY.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).Count() > 0
                    //    ? m_L_MODEL_VISCOSITY.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).First() : null;


                    //materialBalacceBat = m_L_materialBalanceBat.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).Count() > 0
                    //    ? m_L_materialBalanceBat.Where(m => m.TIMESTAMP == m_L_StuffMatrix_Ore_New[i].A_BATCHHISDUMPTIME).First() : null;

                    if (stuffMatrixOre != null && stuffMatrixCoke != null && batchhisDumpOre != null &&
                        batchhisDumpCoke != null && batchhis != null)
                    {

                        NowData = GetSmccData(stuffMatrixOre, stuffMatrixCoke, batchhisDumpOre, batchhisDumpCoke, batchhis);
                        if (NowData != null)
                        {
                            // IsChanged = false; //默认没有变化
                            if (judgeIsChanged(LastData, NowData))
                            {
                                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                                {
                                    if (LastData != null)
                                    {
                                        //更新上次布料制度使用时间
                                        double TimeTpan = NowData.TIMESTAMP.Subtract(LastData.TIMESTAMP).TotalHours;
                                        idatabase.ExecuteCommand($"Update STUFF_MATRIXDATA_CHANGE_C set TIMESPAN = {TimeTpan} where TIMESTAMP={UtilsConvert.GetOraString(LastData.TIMESTAMP)}");
                                        //更新上次布料制度平均负荷
                                        DataTable dt = idatabase.GetDataTable($"select timestamp,min_fh,max_fh,avg_fh,center_fh," +
                                            $"mid_fh,margin_fh from ONLINEFABRIC_FH where timestamp  = (select max(timestamp) " +
                                            $"from ONLINEFABRIC_FH where timestamp <= {UtilsConvert.GetOraString(stuffMatrixOre.TIMESTAMP)})");
                                        if (dt.Rows.Count > 0)
                                        {
                                            idatabase.ExecuteCommand($"Update STUFF_MATRIXDATA_CHANGE_C set CENTER_FH = {dt.Rows[0]["center_fh"]}," +
                                                $"MID_FH = {dt.Rows[0]["mid_fh"]},MARGIN_FH = {dt.Rows[0]["margin_fh"]} " +
                                                $"where TIMESTAMP={UtilsConvert.GetOraString(LastData.TIMESTAMP)}");
                                        }
                                    }
                                    else
                                    {
                                        LastData = new STUFF_MATRIXDATA_CHANGE_C();
                                    }



                                    idatabase.Insert<STUFF_MATRIXDATA_CHANGE_C>(NowData);

                                    //缓存上次的布料关键数据

                                    LastData.TIMESTAMP = NowData.TIMESTAMP;
                                    LastData.WEIGHT_ORE = NowData.WEIGHT_ORE;
                                    LastData.WEIGHT_COKE = NowData.WEIGHT_COKE;
                                    LastData.CHARGE_MATRIX = NowData.CHARGE_MATRIX;
                                    LastData.SET_LX_ORE = NowData.SET_LX_ORE;
                                    LastData.SET_LX_COKE = NowData.SET_LX_COKE;
                                    LastData.ANGLEDIFF_ORE = NowData.ANGLEDIFF_ORE;
                                    LastData.ANGLEDIFF_ORE_COKE = NowData.ANGLEDIFF_ORE_COKE;
                                    LastData.CENTRAL_COKE_RATIO = NowData.CENTRAL_COKE_RATIO;
                                    LastData.MAXANGLE_ORE = NowData.MAXANGLE_ORE;
                                    LastData.MAXANGLE_COKE = NowData.MAXANGLE_COKE;
                                    LastData.CHANGETIME = NowData.CHANGETIME;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }


        public STUFF_MATRIXDATA_CHANGE_C GetLastDate(DateTime dtEnd)
        {
            try
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    string strSql = $"select * from STUFF_MATRIXDATA_CHANGE_C " +
                        $"where TIMESTAMP =(select max(timestamp) from STUFF_MATRIXDATA_CHANGE_C where timestamp <= {UtilsConvert.GetOraString(dtEnd)})";
                    List<STUFF_MATRIXDATA_CHANGE_C> L_BMCC = idatabase.GetList<STUFF_MATRIXDATA_CHANGE_C>(strSql);
                    if (L_BMCC.Count > 0)
                    {
                        return L_BMCC[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetNewDataList(DateTime dtStart, DateTime dtEnd)
        {
            m_L_BatchhisDump_New.Clear();
            m_L_StuffMatrix_Coke_New.Clear();
            m_L_BatchhisDump_New.Clear();
            m_L_Batchhis_New.Clear();
            // m_L_MODEL_VISCOSITY.Clear();

            List<STUFF_MATRIXDATA_CHANGE_C> L_SMCC = new List<STUFF_MATRIXDATA_CHANGE_C>();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string sql = string.Format("select * from STUFF_MATRIX where timestamp > {0} and timestamp <= {1} order by timestamp",
                    UtilsConvert.GetOraString(dtStart), UtilsConvert.GetOraString(dtEnd));

                List<STUFF_MATRIX> L_StuffMatrix = idatabase.GetList<STUFF_MATRIX>(sql);
                if (L_StuffMatrix == null || L_StuffMatrix.Count <= 0)
                {
                    return false;
                }
                sql = string.Format("select * from BATCHHIS_DUMP where timestamp in" +
                    "(select a_batchhisdumptime from STUFF_MATRIX where timestamp > {1} " +
                    "and timestamp <={2}) order by timestamp", SetSumWeightFieldName, UtilsConvert.GetOraString(dtStart),
                    UtilsConvert.GetOraString(dtEnd));
                m_L_BatchhisDump_New = idatabase.GetList<BATCHHIS_DUMP>(sql);
                if (m_L_BatchhisDump_New == null && m_L_BatchhisDump_New.Count <= 0)
                {
                    return false;
                }
                m_L_StuffMatrix_Ore_New = L_StuffMatrix.Where(m => m.A_TYPE == 0).ToList();
                m_L_StuffMatrix_Coke_New = L_StuffMatrix.Where(m => m.A_TYPE == 1).ToList();
                sql = string.Format("select * from BATCHHIS where timestamp in" +
                   "(select a_batchhisdumptime from STUFF_MATRIX where timestamp > {1} " +
                   "and timestamp <={2}) order by timestamp", SetSumWeightFieldName, UtilsConvert.GetOraString(dtStart),
                   UtilsConvert.GetOraString(dtEnd));
                m_L_Batchhis_New = idatabase.GetList<BATCHHIS>(sql);
                if (m_L_Batchhis_New == null || m_L_Batchhis_New.Count <= 0)
                {
                    return false;
                }
                //sql = string.Format("select * from MODEL_VISCOSITY where timestamp in" +
                //  "(select a_batchhisdumptime from STUFF_MATRIX where timestamp > {1} " +
                //  "and timestamp <={2}) order by timestamp", SetSumWeightFieldName, UtilsConvert.GetOraString(dtStart),
                //  UtilsConvert.GetOraString(dtEnd));
                //m_L_MODEL_VISCOSITY = idatabase.GetList<MODEL_VISCOSITY>(sql);
                //if (m_L_MODEL_VISCOSITY == null || m_L_MODEL_VISCOSITY.Count <= 0)
                //{
                //    return false;
                //}

            }
            return true;
        }





        public STUFF_MATRIXDATA_CHANGE_C GetSmccData(STUFF_MATRIX stuffMatrixOre, STUFF_MATRIX stuffMatrixCoke,
            BATCHHIS_DUMP batchhisDumpOre, BATCHHIS_DUMP batchhisDumpCoke, BATCHHIS batchhis)
        {
            STUFF_MATRIXDATA_CHANGE_C smcc = new STUFF_MATRIXDATA_CHANGE_C();

            string strMatrix = "";
            double SetWeightOre = 0;
            double SetWeightCoke = 0;
            double SetLxOre = 0;
            double SetLxCoke = 0;

            SetLxOre = stuffMatrixOre.A_LX;
            SetLxCoke = stuffMatrixCoke.A_LX;
            //计算矿批重
            //SetWeightOre += batchhisDumpOre.WEIGHT1_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT2_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT3_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT4_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT5_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT6_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT7_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT8_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT9_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT10_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT11_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT12_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT13_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT14_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT15_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT16_SET;
            //SetWeightOre += batchhisDumpOre.WEIGHT17_SET;
            //计算焦批重
            SetWeightCoke += batchhisDumpCoke.WEIGHT1_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT2_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT3_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT4_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT5_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT6_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT7_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT8_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT9_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT10_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT11_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT12_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT13_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT14_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT15_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT16_SET;
            SetWeightCoke += batchhisDumpCoke.WEIGHT17_SET;
            SetWeightCoke = Math.Round(SetWeightCoke, 2);
            #region 获取batchhis数据
            smcc.GRADE_IN = batchhis.A_FEK;
            smcc.COKE_NUT_RATIO = batchhis.A_COKE_NUT_RATE;
            smcc.THERY_SLAG = batchhis.A_MSLAG;
            smcc.THERY_GJB = batchhis.A_K1;
            smcc.THERY_FE = batchhis.A_MHTM;
            // smcc.WINJ_RATIO = batchhis.A_M;
            if (batchhis.A_MHTM > 0)
            {
                smcc.THERY_ZB = 100 * batchhis.A_MSLAG / batchhis.A_MHTM;
            }


            #endregion


            #region 布料矩阵
            strMatrix = "C:";
            strMatrix += stuffMatrixCoke.A_CIRCLE1 > 0 ? $" {stuffMatrixCoke.A_ANGLE1:#.#}({stuffMatrixCoke.A_CIRCLE1:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE2 > 0 ? $" {stuffMatrixCoke.A_ANGLE2:#.#}({stuffMatrixCoke.A_CIRCLE2:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE3 > 0 ? $" {stuffMatrixCoke.A_ANGLE3:#.#}({stuffMatrixCoke.A_CIRCLE3:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE4 > 0 ? $" {stuffMatrixCoke.A_ANGLE4:#.#}({stuffMatrixCoke.A_CIRCLE4:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE5 > 0 ? $" {stuffMatrixCoke.A_ANGLE5:#.#}({stuffMatrixCoke.A_CIRCLE5:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE6 > 0 ? $" {stuffMatrixCoke.A_ANGLE6:#.#}({stuffMatrixCoke.A_CIRCLE6:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE7 > 0 ? $" {stuffMatrixCoke.A_ANGLE7:#.#}({stuffMatrixCoke.A_CIRCLE7:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE8 > 0 ? $" {stuffMatrixCoke.A_ANGLE8:#.#}({stuffMatrixCoke.A_CIRCLE8:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE9 > 0 ? $" {stuffMatrixCoke.A_ANGLE9:#.#}({stuffMatrixCoke.A_CIRCLE9:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE10 > 0 ? $" {stuffMatrixCoke.A_ANGLE10:#.#}({stuffMatrixCoke.A_CIRCLE10:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE11 > 0 ? $" {stuffMatrixCoke.A_ANGLE11:#.#}({stuffMatrixCoke.A_CIRCLE11:#})" : "";
            strMatrix += ";O:";
            strMatrix += stuffMatrixOre.A_CIRCLE1 > 0 ? $" {stuffMatrixOre.A_ANGLE1:#.#}({stuffMatrixOre.A_CIRCLE1:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE2 > 0 ? $" {stuffMatrixOre.A_ANGLE2:#.#}({stuffMatrixOre.A_CIRCLE2:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE3 > 0 ? $" {stuffMatrixOre.A_ANGLE3:#.#}({stuffMatrixOre.A_CIRCLE3:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE4 > 0 ? $" {stuffMatrixOre.A_ANGLE4:#.#}({stuffMatrixOre.A_CIRCLE4:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE5 > 0 ? $" {stuffMatrixOre.A_ANGLE5:#.#}({stuffMatrixOre.A_CIRCLE5:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE6 > 0 ? $" {stuffMatrixOre.A_ANGLE6:#.#}({stuffMatrixOre.A_CIRCLE6:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE7 > 0 ? $" {stuffMatrixOre.A_ANGLE7:#.#}({stuffMatrixOre.A_CIRCLE7:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE8 > 0 ? $" {stuffMatrixOre.A_ANGLE8:#.#}({stuffMatrixOre.A_CIRCLE8:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE9 > 0 ? $" {stuffMatrixOre.A_ANGLE9:#.#}({stuffMatrixOre.A_CIRCLE9:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE10 > 0 ? $" {stuffMatrixOre.A_ANGLE10:#.#}({stuffMatrixOre.A_CIRCLE10:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE11 > 0 ? $" {stuffMatrixOre.A_ANGLE11:#.#}({stuffMatrixOre.A_CIRCLE11:#})" : "";

            double dblAngleK_Min = double.MaxValue;
            double dblAngleK_Max = 0;
            double dblAngleJ_Min = double.MaxValue;
            double dblAngleJ_Max = 0;
            double dblCircleK_Sum = 0;
            double dblCircleJ_Sum = 0;
            double dblMulSum_K = 0;
            double dblMulSum_J = 0;
            double dblSumCircleLessThenMinAngelK_J = 0;

            int iCanUsedCount = 0;
            if (stuffMatrixOre.A_CIRCLE1 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE1 ? stuffMatrixOre.A_ANGLE1 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE1 ? stuffMatrixOre.A_ANGLE1 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE1;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE1 * stuffMatrixOre.A_ANGLE1;
            }
            if (stuffMatrixOre.A_CIRCLE2 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE2 ? stuffMatrixOre.A_ANGLE2 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE2 ? stuffMatrixOre.A_ANGLE2 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE2;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE2 * stuffMatrixOre.A_ANGLE2;
            }
            if (stuffMatrixOre.A_CIRCLE3 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE3 ? stuffMatrixOre.A_ANGLE3 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE3 ? stuffMatrixOre.A_ANGLE3 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE3;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE3 * stuffMatrixOre.A_ANGLE3;
            }
            if (stuffMatrixOre.A_CIRCLE4 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE4 ? stuffMatrixOre.A_ANGLE4 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE4 ? stuffMatrixOre.A_ANGLE4 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE4;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE4 * stuffMatrixOre.A_ANGLE4;
            }
            if (stuffMatrixOre.A_CIRCLE5 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE5 ? stuffMatrixOre.A_ANGLE5 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE5 ? stuffMatrixOre.A_ANGLE5 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE5;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE5 * stuffMatrixOre.A_ANGLE5;
            }
            if (stuffMatrixOre.A_CIRCLE6 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE6 ? stuffMatrixOre.A_ANGLE6 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE6 ? stuffMatrixOre.A_ANGLE6 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE6;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE6 * stuffMatrixOre.A_ANGLE6;
            }
            if (stuffMatrixOre.A_CIRCLE7 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE7 ? stuffMatrixOre.A_ANGLE7 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE7 ? stuffMatrixOre.A_ANGLE7 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE7;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE7 * stuffMatrixOre.A_ANGLE7;
            }
            if (stuffMatrixOre.A_CIRCLE8 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE8 ? stuffMatrixOre.A_ANGLE8 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE8 ? stuffMatrixOre.A_ANGLE8 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE8;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE8 * stuffMatrixOre.A_ANGLE8;
            }
            if (stuffMatrixOre.A_CIRCLE9 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE9 ? stuffMatrixOre.A_ANGLE9 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE9 ? stuffMatrixOre.A_ANGLE9 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE9;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE9 * stuffMatrixOre.A_ANGLE9;
            }
            if (stuffMatrixOre.A_CIRCLE10 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE10 ? stuffMatrixOre.A_ANGLE10 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE10 ? stuffMatrixOre.A_ANGLE10 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE10;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE10 * stuffMatrixOre.A_ANGLE10;
            }
            if (stuffMatrixOre.A_CIRCLE11 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE11 ? stuffMatrixOre.A_ANGLE11 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE11 ? stuffMatrixOre.A_ANGLE11 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE11;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE11 * stuffMatrixOre.A_ANGLE11;
            }

            if (iCanUsedCount <= 0)
            {
                return null;
            }
            iCanUsedCount = 0;
            if (stuffMatrixCoke.A_CIRCLE1 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE1 ? stuffMatrixCoke.A_ANGLE1 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE1 ? stuffMatrixCoke.A_ANGLE1 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE1;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE1 * stuffMatrixCoke.A_ANGLE1;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE1 >= 2 ? stuffMatrixCoke.A_CIRCLE1 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE2 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE2 ? stuffMatrixCoke.A_ANGLE2 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE2 ? stuffMatrixCoke.A_ANGLE2 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE2;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE2 * stuffMatrixCoke.A_ANGLE2;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE2 >= 2 ? stuffMatrixCoke.A_CIRCLE2 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE3 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE3 ? stuffMatrixCoke.A_ANGLE3 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE3 ? stuffMatrixCoke.A_ANGLE3 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE3;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE3 * stuffMatrixCoke.A_ANGLE3;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE3 >= 2 ? stuffMatrixCoke.A_CIRCLE3 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE4 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE4 ? stuffMatrixCoke.A_ANGLE4 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE4 ? stuffMatrixCoke.A_ANGLE4 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE4;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE4 * stuffMatrixCoke.A_ANGLE4;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE4 >= 2 ? stuffMatrixCoke.A_CIRCLE4 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE5 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE5 ? stuffMatrixCoke.A_ANGLE5 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE5 ? stuffMatrixCoke.A_ANGLE5 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE5;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE5 * stuffMatrixCoke.A_ANGLE5;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE5 >= 2 ? stuffMatrixCoke.A_CIRCLE5 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE6 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE6 ? stuffMatrixCoke.A_ANGLE6 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE6 ? stuffMatrixCoke.A_ANGLE6 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE6;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE6 * stuffMatrixCoke.A_ANGLE6;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE6 >= 2 ? stuffMatrixCoke.A_CIRCLE6 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE7 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE7 ? stuffMatrixCoke.A_ANGLE7 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE7 ? stuffMatrixCoke.A_ANGLE7 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE7;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE7 * stuffMatrixCoke.A_ANGLE7;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE7 >= 2 ? stuffMatrixCoke.A_CIRCLE7 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE8 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE8 ? stuffMatrixCoke.A_ANGLE8 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE8 ? stuffMatrixCoke.A_ANGLE8 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE8;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE8 * stuffMatrixCoke.A_ANGLE8;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE8 >= 2 ? stuffMatrixCoke.A_CIRCLE8 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE9 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE9 ? stuffMatrixCoke.A_ANGLE9 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE9 ? stuffMatrixCoke.A_ANGLE9 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE9;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE9 * stuffMatrixCoke.A_ANGLE9;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE9 >= 2 ? stuffMatrixCoke.A_CIRCLE9 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE10 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE10 ? stuffMatrixCoke.A_ANGLE10 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE10 ? stuffMatrixCoke.A_ANGLE10 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE10;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE10 * stuffMatrixCoke.A_ANGLE10;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE10 >= 2 ? stuffMatrixCoke.A_CIRCLE10 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE11 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE11 ? stuffMatrixCoke.A_ANGLE11 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE11 ? stuffMatrixCoke.A_ANGLE11 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE11;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE11 * stuffMatrixCoke.A_ANGLE11;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE11 >= 2 ? stuffMatrixCoke.A_CIRCLE11 : 0;
            }
            if (iCanUsedCount <= 0)
            {
                return null;
            }
            #endregion


            L3MATERIAL_CONFIG l3MaterialConfig = null;
            l3MaterialConfig = L_L3MATERIAL_CONFIG.Where(m => m.M_TYPE == 7).First();//球团
            smcc.PELLET_WEIGHT = GetOneKindMaterialWeight(batchhisDumpOre, batchhisDumpCoke, l3MaterialConfig);
            l3MaterialConfig = L_L3MATERIAL_CONFIG.Single(m => m.M_TYPE == 8);//块矿
            smcc.CRUDEORE_WEIGHT = GetOneKindMaterialWeight(batchhisDumpOre, batchhisDumpCoke, l3MaterialConfig);
            l3MaterialConfig = L_L3MATERIAL_CONFIG.Single(m => m.M_TYPE == 6);//烧结
            smcc.SINTER_WEIGHT = GetOneKindMaterialWeight(batchhisDumpOre, batchhisDumpCoke, l3MaterialConfig);
            l3MaterialConfig = L_L3MATERIAL_CONFIG.Single(m => m.M_TYPE == 4);//焦丁
            smcc.COKE_NUT_WEIGHT = GetOneKindMaterialWeight(batchhisDumpOre, batchhisDumpCoke, l3MaterialConfig);
            l3MaterialConfig = L_L3MATERIAL_CONFIG.Single(m => m.M_TYPE == 3);//溶剂
            smcc.FLUX_WEIGHT = GetOneKindMaterialWeight(batchhisDumpOre, batchhisDumpCoke, l3MaterialConfig);
            //矿批重
            SetWeightOre = smcc.SINTER_WEIGHT + smcc.PELLET_WEIGHT + smcc.CRUDEORE_WEIGHT;
            SetWeightOre = Math.Round(SetWeightOre, 2);
            if (SetWeightOre > 0)
            {
                smcc.SINTER_RATIO = smcc.SINTER_WEIGHT / SetWeightOre * 100;
                smcc.PELLET_RATIO = smcc.PELLET_WEIGHT / SetWeightOre * 100;
                smcc.CRUDEORE_RATIO = smcc.CRUDEORE_WEIGHT / SetWeightOre * 100;
            }


            smcc.BURDENRATIO = SetWeightOre / SetWeightCoke;

            SetWeightCoke += smcc.COKE_NUT_WEIGHT;
            SetWeightCoke = Math.Round(SetWeightCoke, 2);

            if (SetWeightCoke > 0)
            {
                smcc.TOTAL_BURDENRATIO = SetWeightOre / SetWeightCoke;
            }
            else
            {
                smcc.TOTAL_BURDENRATIO = 0;
            }
            smcc.TIMESTAMP = stuffMatrixOre.TIMESTAMP;
            smcc.CHARGE_MATRIX = strMatrix;
            smcc.CHANGETIME = stuffMatrixOre.TIMESTAMP;
            smcc.CENTRAL_COKE_RATIO = dblCircleJ_Sum == 0 ? 0 : 100 * dblSumCircleLessThenMinAngelK_J / dblCircleJ_Sum;
            smcc.ANGLEDIFF_ORE_COKE = (dblCircleK_Sum == 0 || dblCircleJ_Sum == 0) ? 0 : dblMulSum_K / dblCircleK_Sum - dblMulSum_J / dblCircleJ_Sum;
            smcc.ANGLEDIFF_ORE = dblAngleK_Max - dblAngleK_Min;
            smcc.ANGLEDIFF_COKE = dblAngleJ_Max - dblAngleJ_Min;
            smcc.MINANGLE_ORE = dblAngleK_Min;
            smcc.MAXANGLE_ORE = dblAngleK_Max;
            smcc.MINANGLE_COKE = dblAngleJ_Min;
            smcc.MAXANGLE_COKE = dblAngleJ_Max;
            smcc.SET_LX_COKE = SetLxCoke;
            smcc.SET_LX_ORE = SetLxOre;
            smcc.WEIGHT_COKE = SetWeightCoke;
            smcc.WEIGHT_ORE = SetWeightOre;
            return smcc;
        }

        double GetOneKindMaterialWeight(BATCHHIS_DUMP batchhisDumpOre, BATCHHIS_DUMP batchhisDumpCoke, L3MATERIAL_CONFIG l3MaterialConfig)
        {
            int iDebugMinVal = 0;
            double weight = 0;
            if (l3MaterialConfig != null)
            {
                if (batchhisDumpOre.CODE1 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE1 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT1_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT1_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT1_SET;
                    weight += batchhisDumpCoke.WEIGHT1_SET;
                }

                if (batchhisDumpOre.CODE2 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE2 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT2_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT2_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT2_SET;
                    weight += batchhisDumpCoke.WEIGHT2_SET;
                }

                if (batchhisDumpOre.CODE3 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE3 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT3_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT3_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT3_SET;
                    weight += batchhisDumpCoke.WEIGHT3_SET;
                }

                if (batchhisDumpOre.CODE4 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE4 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT4_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT4_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT4_SET;
                    weight += batchhisDumpCoke.WEIGHT4_SET;
                }

                if (batchhisDumpOre.CODE5 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE5 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT5_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT5_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT5_SET;
                    weight += batchhisDumpCoke.WEIGHT5_SET;
                }

                if (batchhisDumpOre.CODE6 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE6 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT6_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT6_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT6_SET;
                    weight += batchhisDumpCoke.WEIGHT6_SET;
                }

                if (batchhisDumpOre.CODE7 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE7 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT7_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT7_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT7_SET;
                    weight += batchhisDumpCoke.WEIGHT7_SET;
                }

                if (batchhisDumpOre.CODE8 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE8 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT8_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT8_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT8_SET;
                    weight += batchhisDumpCoke.WEIGHT8_SET;
                }

                if (batchhisDumpOre.CODE9 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE9 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT9_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT9_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT9_SET;
                    weight += batchhisDumpCoke.WEIGHT9_SET;
                }

                if (batchhisDumpOre.CODE10 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE10 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT10_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT10_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT10_SET;
                    weight += batchhisDumpCoke.WEIGHT10_SET;
                }

                if (batchhisDumpOre.CODE11 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE11 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT11_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT11_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT11_SET;
                    weight += batchhisDumpCoke.WEIGHT11_SET;
                }

                if (batchhisDumpOre.CODE12 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE12 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT12_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT12_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT12_SET;
                    weight += batchhisDumpCoke.WEIGHT12_SET;
                }

                if (batchhisDumpOre.CODE13 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE13 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT13_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT13_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT13_SET;
                    weight += batchhisDumpCoke.WEIGHT13_SET;
                }

                if (batchhisDumpOre.CODE14 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE14 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT14_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT14_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT14_SET;
                    weight += batchhisDumpCoke.WEIGHT14_SET;
                }

                if (batchhisDumpOre.CODE15 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE15 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT15_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT15_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT15_SET;
                    weight += batchhisDumpCoke.WEIGHT15_SET;
                }

                if (batchhisDumpOre.CODE16 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE16 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT16_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT16_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT16_SET;
                    weight += batchhisDumpCoke.WEIGHT16_SET;
                }

                if (batchhisDumpOre.CODE17 >= l3MaterialConfig.CODE_MIN && batchhisDumpOre.CODE17 <= l3MaterialConfig.CODE_MAX)
                {
                    if (batchhisDumpOre.WEIGHT17_SET < iDebugMinVal)
                    {
                        batchhisDumpOre.WEIGHT17_SET = 0;
                    }
                    weight += batchhisDumpOre.WEIGHT17_SET;
                    weight += batchhisDumpCoke.WEIGHT17_SET;
                }
            }
            return weight;
        }
        public bool judgeIsChanged(STUFF_MATRIXDATA_CHANGE_C smcOld, STUFF_MATRIXDATA_CHANGE_C smcNew)
        {
            if (smcOld == null)
            {
                //没有前次数据，直接返回true，将新数据入库
                return true;
            }
            if (smcOld.CHARGE_MATRIX != smcNew.CHARGE_MATRIX)
            {
                //IsChanged = true; //布料制度变化了
                //Log.WriteLine($"CHARGE_MATRIX", "DEBUGLOG");
                //Log.WriteLine($" 前次：{smcOld.TIMESTAMP.ToString()}:{smcOld.CHARGE_MATRIX}", "DEBUGLOG");
                //Log.WriteLine($" 本次：{smcNew.TIMESTAMP.ToString()}:{smcNew.CHARGE_MATRIX}", "DEBUGLOG");
                return true;
            }
            if (Math.Abs(smcOld.WEIGHT_ORE - smcNew.WEIGHT_ORE) > 0.1)
            {
                //Log.WriteLine($"WEIGHT_ORE", "DEBUGLOG");
                //Log.WriteLine($" 前次：{smcOld.TIMESTAMP.ToString()}:{smcOld.WEIGHT_ORE}", "DEBUGLOG");
                //Log.WriteLine($" 本次：{smcNew.TIMESTAMP.ToString()}:{smcNew.WEIGHT_ORE}", "DEBUGLOG");
                return true;
            }
            if (Math.Abs(smcOld.WEIGHT_COKE - smcNew.WEIGHT_COKE) > 0.00001)
            {
                //Log.WriteLine($"WEIGHT_COKE", "DEBUGLOG");
                //Log.WriteLine($" 前次：{smcOld.TIMESTAMP.ToString()}:{smcOld.WEIGHT_COKE}", "DEBUGLOG");
                //Log.WriteLine($" 本次：{smcNew.TIMESTAMP.ToString()}:{smcNew.WEIGHT_COKE}", "DEBUGLOG");
                return true;
            }
            if (Math.Abs(smcOld.SET_LX_ORE - smcNew.SET_LX_ORE) > 0.00001)
            {
                //Log.WriteLine($"SET_LX_ORE", "DEBUGLOG");
                //Log.WriteLine($" 前次：{smcOld.TIMESTAMP.ToString()}:{smcOld.SET_LX_ORE}", "DEBUGLOG");
                //Log.WriteLine($" 本次：{smcNew.TIMESTAMP.ToString()}:{smcNew.SET_LX_ORE}", "DEBUGLOG");
                return true;
            }
            if (Math.Abs(smcOld.SET_LX_COKE - smcNew.SET_LX_COKE) > 0.00001)
            {
                //Log.WriteLine($"SET_LX_COKE", "DEBUGLOG");
                //Log.WriteLine($" 前次：{smcOld.TIMESTAMP.ToString()}:{smcOld.SET_LX_COKE}", "DEBUGLOG");
                //Log.WriteLine($" 本次：{smcNew.TIMESTAMP.ToString()}:{smcNew.SET_LX_COKE}", "DEBUGLOG");
                return true;
            }

            return false;
        }

        public List<L3MATERIAL_CONFIG> GetL3MaterialConfig()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                return idatabase.Queryable<L3MATERIAL_CONFIG>().ToList();
            }
        }
        public BATCHHISCODE GetLastBatchhisCode(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string sql = $"select * from BATCHHISCODE where timestamp = (select max(timestamp) from BATCHHISCODE where timestamp <= {UtilsConvert.GetOraString(dt)})";
                return idatabase.GetSingle<BATCHHISCODE>(sql);
            }
        }
    }
    class CalMatrixData3
    {
        public DatabaseSource m_dbs;
        public bool m_isFirstRun = true;
        List<STUFF_MATRIX> m_L_StuffMatrix_Ore_New;
        List<STUFF_MATRIX> m_L_StuffMatrix_Coke_New;
        STUFF_MATRIXDATA_CHANGE_M LastData;
        public CalMatrixData3(DatabaseSource dbs)
        {
            m_dbs = dbs;
        }
        public bool InitData(DateTime dtEnd)
        {
            m_L_StuffMatrix_Ore_New = new List<STUFF_MATRIX>();
            m_L_StuffMatrix_Coke_New = new List<STUFF_MATRIX>();
            LastData = GetLastDate(dtEnd);
            if (isWriteLog)
            {
                Log.WriteLine("InitData()执行完成!", logfieldname);
            }
            return true;
        }
        bool isWriteLog = false;
        string logfieldname = "CalMatrixData3";
        public bool Calcular0(DateTime dtEnd)
        {

            //dtEnd = new DateTime(2019, 10, 10, 0, 0, 0);
            if (m_isFirstRun)
            {
                InitData(dtEnd);
                m_isFirstRun = false;
            }
            DateTime dtBegin;
            if (LastData != null)
            {
                if (LastData.TIME_END != null)
                {
                    dtBegin = (DateTime)LastData.TIME_END;
                    LastData = null;
                }
                else
                {
                    dtBegin = LastData.TIME_BEGIN;
                }
            }
            else
            {
                dtBegin = dtEnd.AddMonths(-3);
            }
            if (GetNewDataList(dtBegin, dtEnd))
            {
                STUFF_MATRIX stuffMatrixOre = null;
                STUFF_MATRIX stuffMatrixCoke = null;
                for (int i = 0; i < m_L_StuffMatrix_Ore_New.Count; i++)
                {
                    try
                    {
                        stuffMatrixOre = m_L_StuffMatrix_Ore_New[i];
                        stuffMatrixCoke = m_L_StuffMatrix_Coke_New.Where(m => m.A_CHARGE == stuffMatrixOre.A_CHARGE && m.TIMESTAMP >= stuffMatrixOre.TIMESTAMP.AddHours(-1)).Count() > 0
                           ? m_L_StuffMatrix_Coke_New.Where(m => m.A_CHARGE == stuffMatrixOre.A_CHARGE && m.TIMESTAMP >= stuffMatrixOre.TIMESTAMP.AddHours(-1)).First() : null;
                        if (stuffMatrixOre != null && stuffMatrixCoke != null)
                        {
                            STUFF_MATRIXDATA_CHANGE_M NowData = GetMatrixData(stuffMatrixOre, stuffMatrixCoke);
                            if (NowData != null)
                            {
                                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                                {
                                    bool isStopWin = false;
                                    DateTime dtStopTime = new DateTime();
                                    if (LastData != null)
                                    {
                                        string strSql = "select timestamp,C_FLBF from DT_TC05M where " +
                                   $"timestamp >= {UtilsConvert.GetOraString(LastData.TIME_BEGIN)} and timestamp <= {UtilsConvert.GetOraString(NowData.TIME_BEGIN)} order by timestamp asc ";
                                        DataTable dtFlbf = idatabase.GetDataTable(strSql);
                                       
                                        foreach (DataRow item in dtFlbf.Rows)
                                        {
                                            if (item["C_FLBF"].ToInt() == 0)
                                            {
                                                dtStopTime = item["timestamp"].ToDate();
                                                isStopWin = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (isStopWin)
                                    {
                                        if (LastData != null)
                                        {
                                            if (isWriteLog)
                                            {
                                                Log.WriteLine("检测到休风标志!", logfieldname);
                                            }
                                            //若前次数据不为空，需要更新数据库前次数据
                                            DateTime startTime = LastData.TIME_BEGIN;
                                            DateTime endTime = dtStopTime; 
                                            if (GetOtherData(startTime, endTime, ref LastData))
                                            {
                                                //if (LastData.THERY_MB > 250 || LastData.THERY_GJB < 200 /*|| LastData.TIMESPAN < 1*/)
                                                //{
                                                //    if (isWriteLog)
                                                //    {
                                                //        Log.WriteLine($"新记录不符合入库条件，删除数据!TIME_BEGIN = {LastData.TIME_BEGIN}", logfieldname);
                                                //    }
                                                //    idatabase.ExecuteCommand($"delete from STUFF_MATRIXDATA_CHANGE_M  where TIME_begin = { UtilsConvert.GetOraString(LastData.TIME_BEGIN) }");
                                                //}
                                                //else
                                                //{
                                                bool status = idatabase.Update<STUFF_MATRIXDATA_CHANGE_M>(LastData, m => m.TIME_BEGIN == LastData.TIME_BEGIN);
                                                if (status && isWriteLog)
                                                {
                                                    Log.WriteLine($"更新数据成功,TIME_BEGIN = {LastData.TIME_BEGIN}", logfieldname);
                                                }
                                                else if (isWriteLog)
                                                {
                                                    Log.WriteLine($"更新数据失败,TIME_BEGIN = {LastData.TIME_BEGIN}", logfieldname);
                                                }
                                                // }
                                            }
                                            else
                                            {
                                                idatabase.ExecuteCommand($"delete from STUFF_MATRIXDATA_CHANGE_M  where TIME_begin = { UtilsConvert.GetOraString(LastData.TIME_BEGIN) }");
                                            }
                                            LastData = null;
                                        }
                                    }
                                    if ((LastData != null && LastData.CHARGE_MATRIX != NowData.CHARGE_MATRIX) || LastData == null)
                                    {

                                        // 没有前次数据或者有前次数据且矩阵变化了
                                        if (LastData != null)
                                        {
                                            if (isWriteLog)
                                            {
                                                Log.WriteLine("检测到矩阵变化!", logfieldname);
                                            }
                                            //若前次数据不为空，需要更新数据库前次数据
                                            DateTime startTime = LastData.TIME_BEGIN;
                                            DateTime endTime = NowData.TIME_BEGIN;
                                            if (GetOtherData(startTime, endTime, ref LastData))
                                            {
                                                //if (LastData.THERY_MB > 250 || LastData.THERY_GJB < 200 /*|| LastData.TIMESPAN < 1*/)
                                                //{
                                                //    if (isWriteLog)
                                                //    {
                                                //        Log.WriteLine($"新记录不符合入库条件，删除数据!TIME_BEGIN = {LastData.TIME_BEGIN}", logfieldname);
                                                //    }
                                                //    idatabase.ExecuteCommand($"delete from STUFF_MATRIXDATA_CHANGE_M  where TIME_begin = { UtilsConvert.GetOraString(LastData.TIME_BEGIN) }");
                                                //}
                                                //else
                                                //{
                                                bool status = idatabase.Update<STUFF_MATRIXDATA_CHANGE_M>(LastData, m => m.TIME_BEGIN == LastData.TIME_BEGIN);
                                                if (status && isWriteLog)
                                                {
                                                    Log.WriteLine($"更新数据成功,TIME_BEGIN = {LastData.TIME_BEGIN}", logfieldname);
                                                }
                                                else if (isWriteLog)
                                                {
                                                    Log.WriteLine($"更新数据失败,TIME_BEGIN = {LastData.TIME_BEGIN}", logfieldname);
                                                }
                                                // }
                                            }
                                            else
                                            {
                                                idatabase.ExecuteCommand($"delete from STUFF_MATRIXDATA_CHANGE_M  where TIME_begin = { UtilsConvert.GetOraString(LastData.TIME_BEGIN) }");
                                            }
                                        }
                                        //插入新数据、之后更新LastData数据
                                        idatabase.Insert(NowData);
                                        LastData = NowData;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            return true;
        }
        public bool Calcular(DateTime dtEnd)
        {
            // dtEnd = new DateTime(2019, 10, 10, 0, 0, 0);
            if (m_isFirstRun)
            {
                InitData(dtEnd);
                m_isFirstRun = false;
            }
            DateTime dtBegin;
            if (LastData != null)
            {
                if (LastData.TIME_END != null)
                {
                    dtBegin = (DateTime)LastData.TIME_END;
                    LastData = null;
                }
                else
                {
                    dtBegin = LastData.TIME_BEGIN;
                }
            }
            else
            {
                dtBegin = dtEnd.AddMonths(-3);
            }
            if (GetNewDataList(dtBegin, dtEnd))
            {
                STUFF_MATRIX stuffMatrixOre = null;
                STUFF_MATRIX stuffMatrixCoke = null;
                for (int i = 0; i < m_L_StuffMatrix_Ore_New.Count; i++)
                {
                    try
                    {
                        stuffMatrixOre = m_L_StuffMatrix_Ore_New[i];
                        stuffMatrixCoke = m_L_StuffMatrix_Coke_New.Where(m => m.A_CHARGE == stuffMatrixOre.A_CHARGE && m.TIMESTAMP >= stuffMatrixOre.TIMESTAMP.AddHours(-1)).Count() > 0
                           ? m_L_StuffMatrix_Coke_New.Where(m => m.A_CHARGE == stuffMatrixOre.A_CHARGE && m.TIMESTAMP >= stuffMatrixOre.TIMESTAMP.AddHours(-1)).First() : null;
                        if (stuffMatrixOre != null && stuffMatrixCoke != null)
                        {
                            STUFF_MATRIXDATA_CHANGE_M NowData = GetMatrixData(stuffMatrixOre, stuffMatrixCoke);
                            if (NowData != null)
                            {
                                if ((LastData != null && LastData.CHARGE_MATRIX != NowData.CHARGE_MATRIX) || LastData == null)
                                {
                                    using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                                    {
                                        bool isContinue = false;
                                        // 没有前次数据或者有前次数据且矩阵变化了
                                        if (LastData != null)
                                        {
                                            //若前次数据不为空，需要更新数据库前次数据
                                            DateTime startTime = LastData.TIME_BEGIN;
                                            DateTime endTime = NowData.TIME_BEGIN;
                                            if (GetOtherData(startTime, endTime, ref LastData))
                                            {
                                                if (LastData.THERY_MB > 250 || LastData.THERY_GJB < 200 || LastData.TIMESPAN < 1)
                                                {
                                                    idatabase.ExecuteCommand($"delete from STUFF_MATRIXDATA_CHANGE_M  where TIME_begin = { UtilsConvert.GetOraString(LastData.TIME_BEGIN) }");
                                                    if (LastData.THERY_MB > 250 || LastData.THERY_GJB < 200)
                                                    {
                                                        //如果当前需要更新的记录计算值异常，则不进行上一次记录和新记录矩阵是否相同的判断，直接开始新的记录
                                                        isContinue = false;
                                                    }
                                                    else if (LastData.TIMESPAN < 1)
                                                    {
                                                        STUFF_MATRIXDATA_CHANGE_M lastLastData = GetLastDate(LastData.TIME_BEGIN);
                                                        if (lastLastData.CHARGE_MATRIX == NowData.CHARGE_MATRIX)
                                                        {
                                                            //如果当前需要更新的记录持续时间小于1小时且上一次更新完成的记录和下一次的矩阵相同，则当前需要更新的记录删除，上一次记录继续计算
                                                            LastData = lastLastData;
                                                            LastData.TIME_END = null;//置空，保证下一次计算从上一条记录的开始时间开始计算
                                                            isContinue = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    object status = idatabase.Update<STUFF_MATRIXDATA_CHANGE_M>(LastData, m => m.TIME_BEGIN == LastData.TIME_BEGIN);
                                                }
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            //插入新数据、之后更新LastData数据
                                            //如果不是从上一条数据开始计算，则计算新的纪录
                                            idatabase.Insert(NowData);
                                            LastData = NowData;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }
            return true;
        }

        private bool GetOtherData(DateTime startTime, DateTime endTime, ref STUFF_MATRIXDATA_CHANGE_M lastData)
        {
            //TIME_END    N DATE    Y 结束使用时间
            //TIMESPAN N   NUMBER(10, 2)    Y 矩阵使用时间
            //CHARGE_END N   NUMBER(10)  Y 结束使用批次
            //THERY_FE N   NUMBER(10, 2)    Y 产铁量
            //THERY_FE_DAY N   NUMBER(10, 2)    Y 折合日产铁量
            //WEIGHT_ORE N   NUMBER(10, 2)    Y 平均矿石批重
            //WEIGHT_COKE N   NUMBER(10, 2)    Y 平均焦炭批重
            //CENTER_FH N   NUMBER(10, 2)    Y 中心负荷
            //MID_FH N   NUMBER(10, 2)    Y 平台负荷
            //MARGIN_FH N   NUMBER(10, 2)    Y 边缘负荷
            //THERY_RLB N   NUMBER(10, 2)    Y 理论燃料比
            //THERY_GJB N   NUMBER(10, 2)    Y 理论焦比
            //THERY_MB N   NUMBER(10, 2)    Y 理论煤比 
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string strSql = "";
                lastData.TIME_END = endTime;
                lastData.TIMESPAN = (endTime - startTime).TotalSeconds;

                DateTime CalStarTime = startTime;
                if (lastData.TIMESPAN < 3600)
                {
                    CalStarTime = endTime.AddHours(-1);
                }
                //string strSql = "select timestamp,C_FLBF from DT_TC05M where " +
                //    $"timestamp >= {UtilsConvert.GetOraString(startTime)} and timestamp <= {UtilsConvert.GetOraString(endTime)} order by timestamp asc ";
                //DataTable dtFlbf = idatabase.GetDataTable(strSql);

                //去掉休风时长
                //DateTime? dtLastStopTime = null;
                //foreach (DataRow item in dtFlbf.Rows)
                //{
                //    if (dtLastStopTime == null && item["C_FLBF"].ToInt() == 0)
                //    {
                //        dtLastStopTime = item["timestamp"].ToDate();
                //    }
                //    else if (dtLastStopTime != null && item["C_FLBF"].ToInt() == 0)
                //    {
                //        lastData.TIMESPAN -= ((TimeSpan)(item["timestamp"].ToDate() - dtLastStopTime)).TotalSeconds;
                //        dtLastStopTime = item["timestamp"].ToDate();
                //    }
                //    else if (item["C_FLBF"].ToInt() != 0)
                //    {
                //        dtLastStopTime = null;
                //    }
                //}
                lastData.TIMESPAN = lastData.TIMESPAN / 3600;
                if (lastData.TIMESPAN <= 0)
                {
                    return false;
                }
                lastData.CHARGE_END = GetRealChargeFromBatchhisDumpByTime(endTime);
                strSql = "select sum(I_MHMT_OUT) as I_MHMT_OUT,sum(I_COKE_IN) as I_COKE_IN,sum(I_COAL_IN) as I_COAL_IN," +
                    "sum(I_COKE_NUT_WEIGHT) as I_COKE_NUT_WEIGHT,round(avg(I_ORE_IN),2) as I_ORE_IN_V,round(avg(I_COKE_IN),2) as I_COKE_IN_V   from MATERIAL_BALANCE_BAT_HIST where " +
              $"timestamp >= (select max(A_BATCHHISDUMPTIME) from STUFF_MATRIX where timestamp <= {UtilsConvert.GetOraString(CalStarTime)}) and " +
              $"timestamp < (select max(A_BATCHHISDUMPTIME) from STUFF_MATRIX where timestamp <= {UtilsConvert.GetOraString(endTime)})";
                DataTable dtMbb = idatabase.GetDataTable(strSql);
                if (dtMbb != null && dtMbb.Rows.Count > 0)
                {
                    lastData.WEIGHT_COKE = dtMbb.Rows[0]["I_COKE_IN_V"].ToDouble();
                    lastData.WEIGHT_ORE = dtMbb.Rows[0]["I_ORE_IN_V"].ToDouble();
                    lastData.THERY_FE = dtMbb.Rows[0]["I_MHMT_OUT"].ToDouble();
                    lastData.THERY_FE_DAY = lastData.THERY_FE / (endTime - CalStarTime).TotalHours * 24;
                    if (lastData.THERY_FE <= 0)
                    {
                        lastData.THERY_GJB = 0;
                        lastData.THERY_MB = 0;
                        lastData.THERY_RLB = 0;
                    }
                    else
                    {
                        lastData.THERY_GJB = dtMbb.Rows[0]["I_COKE_IN"].ToDouble() / lastData.THERY_FE * 1000;
                        lastData.THERY_MB = dtMbb.Rows[0]["I_COAL_IN"].ToDouble() / lastData.THERY_FE * 1000;
                        lastData.THERY_RLB = (dtMbb.Rows[0]["I_COKE_NUT_WEIGHT"].ToDouble() + dtMbb.Rows[0]["I_COAL_IN"].ToDouble() + dtMbb.Rows[0]["I_COKE_IN"].ToDouble())
                            / lastData.THERY_FE * 1000;
                    }

                }
                else
                {
                    return false;
                }

                strSql = "select round(avg(CENTER_FH),2) as CENTER_FH,round(avg(MID_FH),2) as MID_FH,round(avg(MARGIN_FH),2) as MARGIN_FH from ONLINEFABRIC_FH where " +
             $"timestamp >=  {UtilsConvert.GetOraString(CalStarTime)} and  timestamp < {UtilsConvert.GetOraString(endTime)}";
                DataTable dtFh = idatabase.GetDataTable(strSql);
                if (dtFh != null && dtFh.Rows.Count > 0)
                {
                    lastData.CENTER_FH = dtFh.Rows[0]["CENTER_FH"].ToDouble();
                    lastData.MID_FH = dtFh.Rows[0]["MID_FH"].ToDouble();
                    lastData.MARGIN_FH = dtFh.Rows[0]["MARGIN_FH"].ToDouble();
                }
                else
                {
                    return false;
                }

                strSql = "select " +
                    "ROUND(NVL(avg(A_BV5M),0),2) as A_BV5M," +
                    "ROUND(NVL(avg(A_BP5M),0),2) as A_BP5M," +
                    "ROUND(NVL(avg(A_BT5M),0),2) as A_BT5M," +
                    "ROUND(NVL(avg(A_TP5M),0),2) as A_TP5M," +
                    "ROUND(NVL(avg(A_BVO),0),2) as A_BVO," +
                    "ROUND(NVL(avg(A_WINJ),0),2) as A_WINJ," +
                    "ROUND(NVL(avg(A_MOI),0),2) as A_MOI," +
                    "ROUND(NVL(avg(A_CO),0),2) as A_CO," +
                    "ROUND(NVL(avg(A_CO2),0),2) as A_CO2," +
                    "ROUND(NVL(avg(A_H2),0),2) as A_H2," +
                    "ROUND(NVL(avg(A_SPL11),0),2) as A_SPL11," +
                    "ROUND(NVL(avg(A_SPL12),0),2) as A_SPL12," +
                    "ROUND(NVL(avg(A_SPL13),0),2) as A_SPL13," +
                    "ROUND(NVL(avg(A_SPL14),0),2) as A_SPL14," +
                    "ROUND(NVL(avg(A_SPL21),0),2) as A_SPL21," +
                    "ROUND(NVL(avg(A_SPL22),0),2) as A_SPL22," +
                    "ROUND(NVL(avg(A_SPL23),0),2) as A_SPL23," +
                    "ROUND(NVL(avg(A_SPL24),0),2) as A_SPL24," +
                    "ROUND(NVL(avg(TE1301),0),2) as TE1301," +
                    "ROUND(NVL(avg(TE1302),0),2) as TE1302," +
                    "ROUND(NVL(avg(TE1303),0),2) as TE1303," +
                    "ROUND(NVL(avg(TE1304),0),2) as TE1304," +
                    "ROUND(NVL(avg(TE1305),0),2) as TE1305," +
                    "ROUND(NVL(avg(TE1306),0),2) as TE1306," +
                    "ROUND(NVL(avg(TE1307),0),2) as TE1307," +
                    "ROUND(NVL(avg(TE1308),0),2) as TE1308," +
                    "ROUND(NVL(avg(TE1309),0),2) as TE1309," +
                    "ROUND(NVL(avg(TE1310),0),2) as TE1310," +
                    "ROUND(NVL(avg(TE1311),0),2) as TE1311," +
                    "ROUND(NVL(avg(TE1312),0),2) as TE1312," +
                    "ROUND(NVL(avg(TE1313),0),2) as TE1313," +
                    "ROUND(NVL(avg(TE1314),0),2) as TE1314," +
                    "ROUND(NVL(avg(TE1315),0),2) as TE1315," +
                    "ROUND(NVL(avg(TE1316),0),2) as TE1316," +
                    "ROUND(NVL(avg(TE1317),0),2) as TE1317," +
                    "ROUND(NVL(avg(A_TTOPAVG),0),2) as A_TTOPAVG," +
                    "ROUND(NVL(avg(A_TTOP1),0),2) as A_TTOP1," +
                    "ROUND(NVL(avg(A_TTOP2),0),2) as A_TTOP2," +
                    "ROUND(NVL(avg(A_TTOP3),0),2) as A_TTOP3," +
                    "ROUND(NVL(avg(A_TTOP4),0),2) as A_TTOP4," +
                    "ROUND(NVL(avg(A_ETACO),0),2) as A_ETACO," +
                    "ROUND(NVL(avg(A_Z),0),2) as A_Z," +
                    "ROUND(NVL(avg(A_W),0),2) as A_W," +
                    "ROUND(NVL(avg(A_Z_W),0),2) as A_Z_W," +
                    "ROUND(NVL(avg(A_BVSG),0),2) as A_BVSG," +
                    "ROUND(NVL(avg(A_XBVSG),0),2) as A_XBVSG," +
                    "ROUND(NVL(avg(A_TF),0),2) as A_TF," +
                    "ROUND(NVL(avg(A_E),0),2) as A_E," +
                    "ROUND(NVL(avg(A_N2),0),2) as A_N2," +
                    "ROUND(NVL(avg(A_LESSCENTRE_DIFF),0),2) as A_LESSCENTRE_DIFF," +
                    "ROUND(NVL(avg(A_SIDE_AVG),0),2) as A_SIDE_AVG," +
                    "ROUND(NVL(avg(A_SIDE_DIFF),0),2) as A_SIDE_DIFF from av05m_HIST where " +
                    $"timestamp >=  {UtilsConvert.GetOraString(CalStarTime)} and  timestamp < {UtilsConvert.GetOraString(endTime)}";
                DataTable dtAv05m = idatabase.GetDataTable(strSql);
                if (dtAv05m != null && dtAv05m.Rows.Count > 0)
                {
                    lastData.A_BV5M = dtAv05m.Rows[0]["A_BV5M"].ToDouble();
                    lastData.A_BP5M = dtAv05m.Rows[0]["A_BP5M"].ToDouble();
                    lastData.A_BT5M = dtAv05m.Rows[0]["A_BT5M"].ToDouble();
                    lastData.A_TP5M = dtAv05m.Rows[0]["A_TP5M"].ToDouble();
                    lastData.A_BVO = dtAv05m.Rows[0]["A_BVO"].ToDouble();
                    lastData.A_WINJ = dtAv05m.Rows[0]["A_WINJ"].ToDouble();
                    lastData.A_MOI = dtAv05m.Rows[0]["A_MOI"].ToDouble();
                    lastData.A_CO = dtAv05m.Rows[0]["A_CO"].ToDouble();
                    lastData.A_CO2 = dtAv05m.Rows[0]["A_CO2"].ToDouble();
                    lastData.A_H2 = dtAv05m.Rows[0]["A_H2"].ToDouble();
                    lastData.A_SPL11 = dtAv05m.Rows[0]["A_SPL11"].ToDouble();
                    lastData.A_SPL12 = dtAv05m.Rows[0]["A_SPL12"].ToDouble();
                    lastData.A_SPL13 = dtAv05m.Rows[0]["A_SPL13"].ToDouble();
                    lastData.A_SPL14 = dtAv05m.Rows[0]["A_SPL14"].ToDouble();
                    lastData.A_SPL21 = dtAv05m.Rows[0]["A_SPL21"].ToDouble();
                    lastData.A_SPL22 = dtAv05m.Rows[0]["A_SPL22"].ToDouble();
                    lastData.A_SPL23 = dtAv05m.Rows[0]["A_SPL23"].ToDouble();
                    lastData.A_SPL24 = dtAv05m.Rows[0]["A_SPL24"].ToDouble();
                    lastData.TE1301 = dtAv05m.Rows[0]["TE1301"].ToDouble();
                    lastData.TE1302 = dtAv05m.Rows[0]["TE1302"].ToDouble();
                    lastData.TE1303 = dtAv05m.Rows[0]["TE1303"].ToDouble();
                    lastData.TE1304 = dtAv05m.Rows[0]["TE1304"].ToDouble();
                    lastData.TE1305 = dtAv05m.Rows[0]["TE1305"].ToDouble();
                    lastData.TE1306 = dtAv05m.Rows[0]["TE1306"].ToDouble();
                    lastData.TE1307 = dtAv05m.Rows[0]["TE1307"].ToDouble();
                    lastData.TE1308 = dtAv05m.Rows[0]["TE1308"].ToDouble();
                    lastData.TE1309 = dtAv05m.Rows[0]["TE1309"].ToDouble();
                    lastData.TE1310 = dtAv05m.Rows[0]["TE1310"].ToDouble();
                    lastData.TE1311 = dtAv05m.Rows[0]["TE1311"].ToDouble();
                    lastData.TE1312 = dtAv05m.Rows[0]["TE1312"].ToDouble();
                    lastData.TE1313 = dtAv05m.Rows[0]["TE1313"].ToDouble();
                    lastData.TE1314 = dtAv05m.Rows[0]["TE1314"].ToDouble();
                    lastData.TE1315 = dtAv05m.Rows[0]["TE1315"].ToDouble();
                    lastData.TE1316 = dtAv05m.Rows[0]["TE1316"].ToDouble();
                    lastData.TE1317 = dtAv05m.Rows[0]["TE1317"].ToDouble();
                    lastData.A_TTOPAVG = dtAv05m.Rows[0]["A_TTOPAVG"].ToDouble();
                    lastData.A_TTOP1 = dtAv05m.Rows[0]["A_TTOP1"].ToDouble();
                    lastData.A_TTOP2 = dtAv05m.Rows[0]["A_TTOP2"].ToDouble();
                    lastData.A_TTOP3 = dtAv05m.Rows[0]["A_TTOP3"].ToDouble();
                    lastData.A_TTOP4 = dtAv05m.Rows[0]["A_TTOP4"].ToDouble();
                    lastData.A_ETACO = dtAv05m.Rows[0]["A_ETACO"].ToDouble();
                    lastData.A_Z = dtAv05m.Rows[0]["A_Z"].ToDouble();
                    lastData.A_W = dtAv05m.Rows[0]["A_W"].ToDouble();
                    lastData.A_Z_W = dtAv05m.Rows[0]["A_Z_W"].ToDouble();
                    lastData.A_BVSG = dtAv05m.Rows[0]["A_BVSG"].ToDouble();
                    lastData.A_XBVSG = dtAv05m.Rows[0]["A_XBVSG"].ToDouble();
                    lastData.A_TF = dtAv05m.Rows[0]["A_TF"].ToDouble();
                    lastData.A_E = dtAv05m.Rows[0]["A_E"].ToDouble();
                    lastData.A_N2 = dtAv05m.Rows[0]["A_N2"].ToDouble();
                    lastData.A_LESSCENTRE_DIFF = dtAv05m.Rows[0]["A_LESSCENTRE_DIFF"].ToDouble();
                    lastData.A_SIDE_AVG = dtAv05m.Rows[0]["A_SIDE_AVG"].ToDouble();
                    lastData.A_SIDE_DIFF = dtAv05m.Rows[0]["A_SIDE_DIFF"].ToDouble();

                    //(1 - 0.21)*富氧量 / 60 / 风量
                    lastData.A_BVO_RATE = lastData.A_BV5M > 0 ? 100 * (1 - 0.21) * lastData.A_BVO / 60 / lastData.A_BV5M : 0;

                }
                else
                {
                    return false;
                }
                strSql = "select " +
                    "ROUND(NVL(avg(HEARTH_HL),0),2) as HEARTH_HL," +
                    "ROUND(NVL(avg(BOSH_HL),0),2) as BOSH_HL," +
                    "ROUND(NVL(avg(WAIST_HL),0),2) as WAIST_HL," +
                    "ROUND(NVL(avg(BOTTOMBODY_HL),0),2) as BOTTOMBODY_HL," +
                    "ROUND(NVL(avg(MIDBODY_HL),0),2) as MIDBODY_HL," +
                    "ROUND(NVL(avg(TOPBODY_HL),0),2) as TOPBODY_HL," +
                    "ROUND(NVL(avg(TUYERE_HL),0),2) as TUYERE_HL," +
                    "ROUND(NVL(avg(HEARTH_DIFFTE),0),2) as HEARTH_DIFFTE," +
                    "ROUND(NVL(avg(BOSH_DIFFTE),0),2) as BOSH_DIFFTE," +
                    "ROUND(NVL(avg(WAIST_DIFFTE),0),2) as WAIST_DIFFTE," +
                    "ROUND(NVL(avg(BOTTOMBODY_DIFFTE),0),2) as BOTTOMBODY_DIFFTE," +
                    "ROUND(NVL(avg(MIDBODY_DIFFTE),0),2) as MIDBODY_DIFFTE," +
                    "ROUND(NVL(avg(TOPBODY_DIFFTE),0),2) as TOPBODY_DIFFTE," +
                    "ROUND(NVL(avg(TUYERE_DIFFTE),0),2) as TUYERE_DIFFTE," +
                    "ROUND(NVL(avg(TOTAL_HL),0),2) as TOTAL_HL," +
                    "ROUND(NVL(avg(TOTAL_DIFFTE),0),2) as TOTAL_DIFFTE," +
                    "ROUND(NVL(avg(TOTAL_FT),0),2) as TOTAL_FT," +
                    "ROUND(NVL(avg(TE_IN),0),2) as TE_IN," +
                    "ROUND(NVL(avg(TE_OUT),0),2) as TE_OUT," +
                    "ROUND(NVL(avg(TOTAL_HL_AVG),0),2) as TOTAL_HL_AVG," +
                    "ROUND(NVL(avg(TOTAL_DIFFTE_AVG),0),2) as TOTAL_DIFFTE_AVG " +
                    " from wtd_hlsum_HIST where " +
                    $"timestamp >=  {UtilsConvert.GetOraString(CalStarTime)} and  timestamp < {UtilsConvert.GetOraString(endTime)}";
                DataTable dtWtdHlSum = idatabase.GetDataTable(strSql);
                if (dtWtdHlSum != null && dtWtdHlSum.Rows.Count > 0)
                {
                    lastData.HEARTH_HL = dtWtdHlSum.Rows[0]["HEARTH_HL"].ToDouble();
                    lastData.BOSH_HL = dtWtdHlSum.Rows[0]["BOSH_HL"].ToDouble();
                    lastData.WAIST_HL = dtWtdHlSum.Rows[0]["WAIST_HL"].ToDouble();
                    lastData.BOTTOMBODY_HL = dtWtdHlSum.Rows[0]["BOTTOMBODY_HL"].ToDouble();
                    lastData.MIDBODY_HL = dtWtdHlSum.Rows[0]["MIDBODY_HL"].ToDouble();
                    lastData.TOPBODY_HL = dtWtdHlSum.Rows[0]["TOPBODY_HL"].ToDouble();
                    lastData.TUYERE_HL = dtWtdHlSum.Rows[0]["TUYERE_HL"].ToDouble();
                    lastData.HEARTH_DIFFTE = dtWtdHlSum.Rows[0]["HEARTH_DIFFTE"].ToDouble();
                    lastData.BOSH_DIFFTE = dtWtdHlSum.Rows[0]["BOSH_DIFFTE"].ToDouble();
                    lastData.WAIST_DIFFTE = dtWtdHlSum.Rows[0]["WAIST_DIFFTE"].ToDouble();
                    lastData.BOTTOMBODY_DIFFTE = dtWtdHlSum.Rows[0]["BOTTOMBODY_DIFFTE"].ToDouble();
                    lastData.MIDBODY_DIFFTE = dtWtdHlSum.Rows[0]["MIDBODY_DIFFTE"].ToDouble();
                    lastData.TOPBODY_DIFFTE = dtWtdHlSum.Rows[0]["TOPBODY_DIFFTE"].ToDouble();
                    lastData.TUYERE_DIFFTE = dtWtdHlSum.Rows[0]["TUYERE_DIFFTE"].ToDouble();
                    lastData.TOTAL_HL = dtWtdHlSum.Rows[0]["TOTAL_HL"].ToDouble();
                    lastData.TOTAL_DIFFTE = dtWtdHlSum.Rows[0]["TOTAL_DIFFTE"].ToDouble();
                    lastData.TOTAL_FT = dtWtdHlSum.Rows[0]["TOTAL_FT"].ToDouble();
                    lastData.TE_IN = dtWtdHlSum.Rows[0]["TE_IN"].ToDouble();
                    lastData.TE_OUT = dtWtdHlSum.Rows[0]["TE_OUT"].ToDouble();
                    lastData.TOTAL_HL_AVG = dtWtdHlSum.Rows[0]["TOTAL_HL_AVG"].ToDouble();
                    lastData.TOTAL_DIFFTE_AVG = dtWtdHlSum.Rows[0]["TOTAL_DIFFTE_AVG"].ToDouble();
                }
                else
                {
                    return false;
                }
                return true;
            }
        }




        private STUFF_MATRIXDATA_CHANGE_M GetMatrixData(STUFF_MATRIX stuffMatrixOre, STUFF_MATRIX stuffMatrixCoke)
        {
            if ((stuffMatrixOre.TIMESTAMP - stuffMatrixOre.A_BATCHHISDUMPTIME).TotalSeconds > 2 * 3600)
            {
                return null;
            }
            STUFF_MATRIXDATA_CHANGE_M NowData = new STUFF_MATRIXDATA_CHANGE_M();
            NowData.TIME_BEGIN = stuffMatrixOre.TIMESTAMP;
            NowData.CHARGE_BEGIN = GetRealChargeFromBatchhisDumpByTime(stuffMatrixOre.TIMESTAMP);
            string strMatrix = "K:";
            strMatrix += stuffMatrixCoke.A_CIRCLE1 > 0 ? $" {stuffMatrixCoke.A_ANGLE1:#.#}({stuffMatrixCoke.A_CIRCLE1:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE2 > 0 ? $" {stuffMatrixCoke.A_ANGLE2:#.#}({stuffMatrixCoke.A_CIRCLE2:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE3 > 0 ? $" {stuffMatrixCoke.A_ANGLE3:#.#}({stuffMatrixCoke.A_CIRCLE3:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE4 > 0 ? $" {stuffMatrixCoke.A_ANGLE4:#.#}({stuffMatrixCoke.A_CIRCLE4:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE5 > 0 ? $" {stuffMatrixCoke.A_ANGLE5:#.#}({stuffMatrixCoke.A_CIRCLE5:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE6 > 0 ? $" {stuffMatrixCoke.A_ANGLE6:#.#}({stuffMatrixCoke.A_CIRCLE6:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE7 > 0 ? $" {stuffMatrixCoke.A_ANGLE7:#.#}({stuffMatrixCoke.A_CIRCLE7:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE8 > 0 ? $" {stuffMatrixCoke.A_ANGLE8:#.#}({stuffMatrixCoke.A_CIRCLE8:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE9 > 0 ? $" {stuffMatrixCoke.A_ANGLE9:#.#}({stuffMatrixCoke.A_CIRCLE9:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE10 > 0 ? $" {stuffMatrixCoke.A_ANGLE10:#.#}({stuffMatrixCoke.A_CIRCLE10:#})" : "";
            strMatrix += stuffMatrixCoke.A_CIRCLE11 > 0 ? $" {stuffMatrixCoke.A_ANGLE11:#.#}({stuffMatrixCoke.A_CIRCLE11:#})" : "";
            strMatrix += ";P:";
            strMatrix += stuffMatrixOre.A_CIRCLE1 > 0 ? $" {stuffMatrixOre.A_ANGLE1:#.#}({stuffMatrixOre.A_CIRCLE1:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE2 > 0 ? $" {stuffMatrixOre.A_ANGLE2:#.#}({stuffMatrixOre.A_CIRCLE2:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE3 > 0 ? $" {stuffMatrixOre.A_ANGLE3:#.#}({stuffMatrixOre.A_CIRCLE3:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE4 > 0 ? $" {stuffMatrixOre.A_ANGLE4:#.#}({stuffMatrixOre.A_CIRCLE4:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE5 > 0 ? $" {stuffMatrixOre.A_ANGLE5:#.#}({stuffMatrixOre.A_CIRCLE5:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE6 > 0 ? $" {stuffMatrixOre.A_ANGLE6:#.#}({stuffMatrixOre.A_CIRCLE6:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE7 > 0 ? $" {stuffMatrixOre.A_ANGLE7:#.#}({stuffMatrixOre.A_CIRCLE7:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE8 > 0 ? $" {stuffMatrixOre.A_ANGLE8:#.#}({stuffMatrixOre.A_CIRCLE8:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE9 > 0 ? $" {stuffMatrixOre.A_ANGLE9:#.#}({stuffMatrixOre.A_CIRCLE9:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE10 > 0 ? $" {stuffMatrixOre.A_ANGLE10:#.#}({stuffMatrixOre.A_CIRCLE10:#})" : "";
            strMatrix += stuffMatrixOre.A_CIRCLE11 > 0 ? $" {stuffMatrixOre.A_ANGLE11:#.#}({stuffMatrixOre.A_CIRCLE11:#})" : "";
            NowData.CHARGE_MATRIX = strMatrix;
            NowData.CHARGE_MATRIX_C_O = strMatrix.Replace("K", "C").Replace("P", "O");
            double dblAngleK_Min = double.MaxValue;
            double dblAngleK_Max = 0;
            double dblAngleJ_Min = double.MaxValue;
            double dblAngleJ_Max = 0;
            double dblCircleK_Sum = 0;
            double dblCircleJ_Sum = 0;
            double dblMulSum_K = 0;
            double dblMulSum_J = 0;
            double dblSumCircleLessThenMinAngelK_J = 0;

            int iCanUsedCount = 0;
            if (stuffMatrixOre.A_CIRCLE1 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE1 ? stuffMatrixOre.A_ANGLE1 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE1 ? stuffMatrixOre.A_ANGLE1 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE1;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE1 * stuffMatrixOre.A_ANGLE1;
            }
            if (stuffMatrixOre.A_CIRCLE2 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE2 ? stuffMatrixOre.A_ANGLE2 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE2 ? stuffMatrixOre.A_ANGLE2 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE2;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE2 * stuffMatrixOre.A_ANGLE2;
            }
            if (stuffMatrixOre.A_CIRCLE3 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE3 ? stuffMatrixOre.A_ANGLE3 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE3 ? stuffMatrixOre.A_ANGLE3 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE3;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE3 * stuffMatrixOre.A_ANGLE3;
            }
            if (stuffMatrixOre.A_CIRCLE4 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE4 ? stuffMatrixOre.A_ANGLE4 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE4 ? stuffMatrixOre.A_ANGLE4 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE4;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE4 * stuffMatrixOre.A_ANGLE4;
            }
            if (stuffMatrixOre.A_CIRCLE5 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE5 ? stuffMatrixOre.A_ANGLE5 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE5 ? stuffMatrixOre.A_ANGLE5 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE5;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE5 * stuffMatrixOre.A_ANGLE5;
            }
            if (stuffMatrixOre.A_CIRCLE6 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE6 ? stuffMatrixOre.A_ANGLE6 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE6 ? stuffMatrixOre.A_ANGLE6 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE6;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE6 * stuffMatrixOre.A_ANGLE6;
            }
            if (stuffMatrixOre.A_CIRCLE7 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE7 ? stuffMatrixOre.A_ANGLE7 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE7 ? stuffMatrixOre.A_ANGLE7 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE7;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE7 * stuffMatrixOre.A_ANGLE7;
            }
            if (stuffMatrixOre.A_CIRCLE8 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE8 ? stuffMatrixOre.A_ANGLE8 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE8 ? stuffMatrixOre.A_ANGLE8 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE8;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE8 * stuffMatrixOre.A_ANGLE8;
            }
            if (stuffMatrixOre.A_CIRCLE9 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE9 ? stuffMatrixOre.A_ANGLE9 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE9 ? stuffMatrixOre.A_ANGLE9 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE9;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE9 * stuffMatrixOre.A_ANGLE9;
            }
            if (stuffMatrixOre.A_CIRCLE10 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE10 ? stuffMatrixOre.A_ANGLE10 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE10 ? stuffMatrixOre.A_ANGLE10 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE10;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE10 * stuffMatrixOre.A_ANGLE10;
            }
            if (stuffMatrixOre.A_CIRCLE11 > 0)
            {
                iCanUsedCount++;
                dblAngleK_Min = dblAngleK_Min > stuffMatrixOre.A_ANGLE11 ? stuffMatrixOre.A_ANGLE11 : dblAngleK_Min;
                dblAngleK_Max = dblAngleK_Max < stuffMatrixOre.A_ANGLE11 ? stuffMatrixOre.A_ANGLE11 : dblAngleK_Max;
                dblCircleK_Sum += stuffMatrixOre.A_CIRCLE11;
                dblMulSum_K += stuffMatrixOre.A_CIRCLE11 * stuffMatrixOre.A_ANGLE11;
            }

            if (iCanUsedCount <= 0)
            {
                return null;
            }
            iCanUsedCount = 0;
            if (stuffMatrixCoke.A_CIRCLE1 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE1 ? stuffMatrixCoke.A_ANGLE1 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE1 ? stuffMatrixCoke.A_ANGLE1 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE1;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE1 * stuffMatrixCoke.A_ANGLE1;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE1 >= 2 ? stuffMatrixCoke.A_CIRCLE1 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE2 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE2 ? stuffMatrixCoke.A_ANGLE2 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE2 ? stuffMatrixCoke.A_ANGLE2 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE2;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE2 * stuffMatrixCoke.A_ANGLE2;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE2 >= 2 ? stuffMatrixCoke.A_CIRCLE2 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE3 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE3 ? stuffMatrixCoke.A_ANGLE3 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE3 ? stuffMatrixCoke.A_ANGLE3 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE3;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE3 * stuffMatrixCoke.A_ANGLE3;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE3 >= 2 ? stuffMatrixCoke.A_CIRCLE3 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE4 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE4 ? stuffMatrixCoke.A_ANGLE4 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE4 ? stuffMatrixCoke.A_ANGLE4 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE4;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE4 * stuffMatrixCoke.A_ANGLE4;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE4 >= 2 ? stuffMatrixCoke.A_CIRCLE4 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE5 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE5 ? stuffMatrixCoke.A_ANGLE5 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE5 ? stuffMatrixCoke.A_ANGLE5 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE5;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE5 * stuffMatrixCoke.A_ANGLE5;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE5 >= 2 ? stuffMatrixCoke.A_CIRCLE5 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE6 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE6 ? stuffMatrixCoke.A_ANGLE6 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE6 ? stuffMatrixCoke.A_ANGLE6 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE6;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE6 * stuffMatrixCoke.A_ANGLE6;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE6 >= 2 ? stuffMatrixCoke.A_CIRCLE6 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE7 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE7 ? stuffMatrixCoke.A_ANGLE7 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE7 ? stuffMatrixCoke.A_ANGLE7 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE7;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE7 * stuffMatrixCoke.A_ANGLE7;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE7 >= 2 ? stuffMatrixCoke.A_CIRCLE7 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE8 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE8 ? stuffMatrixCoke.A_ANGLE8 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE8 ? stuffMatrixCoke.A_ANGLE8 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE8;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE8 * stuffMatrixCoke.A_ANGLE8;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE8 >= 2 ? stuffMatrixCoke.A_CIRCLE8 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE9 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE9 ? stuffMatrixCoke.A_ANGLE9 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE9 ? stuffMatrixCoke.A_ANGLE9 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE9;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE9 * stuffMatrixCoke.A_ANGLE9;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE9 >= 2 ? stuffMatrixCoke.A_CIRCLE9 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE10 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE10 ? stuffMatrixCoke.A_ANGLE10 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE10 ? stuffMatrixCoke.A_ANGLE10 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE10;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE10 * stuffMatrixCoke.A_ANGLE10;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE10 >= 2 ? stuffMatrixCoke.A_CIRCLE10 : 0;
            }
            if (stuffMatrixCoke.A_CIRCLE11 > 0)
            {
                iCanUsedCount++;
                dblAngleJ_Min = dblAngleJ_Min > stuffMatrixCoke.A_ANGLE11 ? stuffMatrixCoke.A_ANGLE11 : dblAngleJ_Min;
                dblAngleJ_Max = dblAngleJ_Max < stuffMatrixCoke.A_ANGLE11 ? stuffMatrixCoke.A_ANGLE11 : dblAngleJ_Max;
                dblCircleJ_Sum += stuffMatrixCoke.A_CIRCLE11;
                dblMulSum_J += stuffMatrixCoke.A_CIRCLE11 * stuffMatrixCoke.A_ANGLE11;
                dblSumCircleLessThenMinAngelK_J += dblAngleK_Min - stuffMatrixCoke.A_ANGLE11 >= 2 ? stuffMatrixCoke.A_CIRCLE11 : 0;
            }
            if (iCanUsedCount <= 0)
            {
                return null;
            }
            NowData.CENTRAL_COKE_RATIO = dblCircleJ_Sum == 0 ? 0 : 100 * dblSumCircleLessThenMinAngelK_J / dblCircleJ_Sum;
            NowData.ANGLEDIFF_ORE_COKE = (dblCircleK_Sum == 0 || dblCircleJ_Sum == 0) ? 0 : dblMulSum_K / dblCircleK_Sum - dblMulSum_J / dblCircleJ_Sum;
            NowData.ANGLEDIFF_ORE = dblAngleK_Max - dblAngleK_Min;
            NowData.ANGLEDIFF_COKE = dblAngleJ_Max - dblAngleJ_Min;
            NowData.MINANGLE_ORE = dblAngleK_Min;
            NowData.MAXANGLE_ORE = dblAngleK_Max;
            NowData.MINANGLE_COKE = dblAngleJ_Min;
            NowData.MAXANGLE_COKE = dblAngleJ_Max;
            return NowData;
        }
        private int GetRealChargeFromBatchhisDumpByTime(DateTime dtime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string strSql = $"select CHARGE_NOZERO from BATCHHIS_DUMP  where timestamp =  (select max(A_BATCHHISDUMPTIME) from STUFF_MATRIX where timestamp <= {UtilsConvert.GetOraString(dtime)})";
                return idatabase.GetInt(strSql);
            }
        }
        public List<L3MATERIAL_CONFIG> GetL3MaterialConfig()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                return idatabase.Queryable<L3MATERIAL_CONFIG>().ToList();
            }
        }
        public bool GetNewDataList(DateTime dtStart, DateTime dtEnd)
        {

            m_L_StuffMatrix_Coke_New.Clear();
            m_L_StuffMatrix_Ore_New.Clear();
            List<STUFF_MATRIXDATA_CHANGE_M> L_SMCM = new List<STUFF_MATRIXDATA_CHANGE_M>();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                string sql = string.Format("select * from STUFF_MATRIX where timestamp > {0} and timestamp <= {1} order by timestamp",
                    UtilsConvert.GetOraString(dtStart), UtilsConvert.GetOraString(dtEnd));

                List<STUFF_MATRIX> L_StuffMatrix = idatabase.GetList<STUFF_MATRIX>(sql);


                m_L_StuffMatrix_Ore_New = L_StuffMatrix.Where(m => m.A_TYPE == 0).ToList();
                m_L_StuffMatrix_Coke_New = L_StuffMatrix.Where(m => m.A_TYPE == 1).ToList();
                if (m_L_StuffMatrix_Ore_New == null || m_L_StuffMatrix_Ore_New.Count <= 0 || m_L_StuffMatrix_Coke_New == null || m_L_StuffMatrix_Coke_New.Count <= 0)
                {
                    if (isWriteLog)
                    {
                        Log.WriteLine("获取矩阵数据失败!", logfieldname);
                    }
                    return false;
                }
            }
            return true;
        }
        public STUFF_MATRIXDATA_CHANGE_M GetLastDate(DateTime dtEnd)
        {
            try
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
                {
                    string strSql = $"select * from STUFF_MATRIXDATA_CHANGE_M " +
                        $"where time_begin =(select max(time_begin) from STUFF_MATRIXDATA_CHANGE_M where time_begin < {UtilsConvert.GetOraString(dtEnd)})";
                    List<STUFF_MATRIXDATA_CHANGE_M> L_BMCM = idatabase.GetList<STUFF_MATRIXDATA_CHANGE_M>(strSql);
                    if (L_BMCM.Count > 0)
                    {
                        return L_BMCM[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
