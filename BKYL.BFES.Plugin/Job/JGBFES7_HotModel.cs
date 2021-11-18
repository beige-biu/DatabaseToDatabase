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

namespace BKYL.BFES.Plugin
{
    class JGBFES7_HotModel : BaseJob
    {
        C_HotModel m_HotModel;
        public override void InitTask()
        {
        }
        public override void RunTask(DateTime currentTime)
        {
            //DateTime dtStart = new DateTime(2019, 6, 18, 0, 0, 0);
            //DateTime dtEnd = new DateTime(2019, 6, 21, 0, 0, 0);
            //TimeSpan ti = dtEnd - dtStart;
            //for (int i = 0; i < ti.TotalSeconds;)
            //{
            //    try
            //    {
            //        i += 900;
            //        currentTime = dtStart.AddSeconds(i);
            int IsStopBf = 0;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                IsStopBf = idatabase.GetInt("select C_FLBF from DT_TC05M where timestamp = (select max(timestamp) from DT_TC05M)");
            }
            if(IsStopBf!=0)//高炉休风
            {
                m_HotModel = new C_HotModel(S_DBSource);
                m_HotModel.Calcular(currentTime);
            }
            //}
            //catch (Exception ee)
            //{
            //    //输入错误到界面
            //    GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + ee.Message, System.Drawing.Color.Red);
            //    //throw;
            //    }

            //}

        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
    }
    public class C_HotModel
    {
        DatabaseSource m_dbs;
        PRIMARYACTIONS m_PRIMARYACTIONS = new PRIMARYACTIONS();
        public C_HotModel(DatabaseSource dbs)
        {
            m_dbs = dbs;
        }
        DateTime CurrentTime;
        #region 参数
        /// <summary>
        /// 最接近当前时间的透气性指数
        /// </summary>
        public double ITZ15M { get; set; }
        /// <summary>
        /// 最接近上次出铁时间的透气性指数
        /// </summary>
        public double ITZ15MB1 { get; set; }
        /// <summary>
        /// 最接近上上次出铁时间的透气性指数
        /// </summary>
        public double ITZ15MB2 { get; set; }
        /// <summary>
        /// 最接近当前时间的料速
        /// </summary>
        public double VB15M { get; set; }
        /// <summary>
        /// 最接近上次出铁时间的料速
        /// </summary>
        public double VB15MB1 { get; set; }
        /// <summary>
        /// 最接近上上次出铁时间的料速
        /// </summary>
        public double VB15MB2 { get; set; }

        /// <summary>
        /// 最接近当前时间的风量
        /// </summary>
        public double BV15M { get; set; }
        /// <summary>
        /// 最接近上次出铁时间的风量
        /// </summary>
        public double BV15MB1 { get; set; }
        /// <summary>
        /// 最接近上上次出铁时间的风量
        /// </summary>
        public double BV15MB2 { get; set; }

        /// <summary>
        /// 最接近当前时间的顶温
        /// </summary>
        public double TTOP15M { get; set; }
        /// <summary>
        /// 最接近上次出铁时间的顶温
        /// </summary>
        public double TTOP15MB1 { get; set; }
        /// <summary>
        /// 最接近上上次出铁时间的顶温
        /// </summary>
        public double TTOP15MB2 { get; set; }
        /// <summary>
        /// 最接近当前时间的煤气利用率
        /// </summary>
        public double COR15M { get; set; }
        /// <summary>
        /// 最接近上次出铁时间的煤气利用率
        /// </summary>
        public double COR15MB1 { get; set; }
        /// <summary>
        /// 最接近上上次出铁时间的煤气利用率
        /// </summary>
        public double COR15MB2 { get; set; }

        /// <summary>
        /// 透气性指数比上次铁高
        /// </summary>
        public bool ITZ_STATUS1 = false;
        /// <summary>
        /// 透气性指数比上次铁低
        /// </summary>
        public bool ITZ_STATUS2 = false;
        /// <summary>
        /// 透气性指数比上次铁高很多
        /// </summary>
        public bool ITZ_STATUS3 = false;
        /// <summary>
        /// 透气性指数比上次铁低很多
        /// </summary>
        public bool ITZ_STATUS4 = false;
        /// <summary>
        /// 透气性指数上次比上上次铁高
        /// </summary>
        public bool ITZ_STATUS5 = false;
        /// <summary>
        /// 透气性指数上次比上上次铁低
        /// </summary>
        public bool ITZ_STATUS6 = false;



        /// <summary>
        /// 风量比上次铁高
        /// </summary>
        public bool BV_STATUS1 = false;
        /// <summary>
        /// 风量比上次铁低
        /// </summary>
        public bool BV_STATUS2 = false;
        /// <summary>
        /// 风量比上次铁高很多
        /// </summary>
        public bool BV_STATUS3 = false;
        /// <summary>
        /// 风量比上次铁低很多
        /// </summary>
        public bool BV_STATUS4 = false;
        /// <summary>
        /// 风量上次比上上次铁低
        /// </summary>
        public bool BV_STATUS5 = false;
        /// <summary>
        /// 风量上次比上上次铁高
        /// </summary>
        public bool BV_STATUS6 = false;

        /// <summary>
        /// 料速比上次铁慢
        /// </summary>
        public bool VB_STATUS1 = false;
        /// <summary>
        /// 料速比上次铁快
        /// </summary>
        public bool VB_STATUS2 = false;
        /// <summary>
        /// 料速上次比上上次铁慢
        /// </summary>
        public bool VB_STATUS3 = false;
        /// <summary>
        /// 料速上次比上上次铁快
        /// </summary>
        public bool VB_STATUS4 = false;

        /// <summary>
        /// 顶温比上次铁低
        /// </summary>
        public bool TTOP_STATUS1 = false;
        /// <summary>
        /// 顶温比上次铁高
        /// </summary>
        public bool TTOP_STATUS2 = false;
        /// <summary>
        /// 顶温比上次铁高很多
        /// </summary>
        public bool TTOP_STATUS3 = false;
        /// <summary>
        /// 顶温上次比上上次铁低
        /// </summary>
        public bool TTOP_STATUS4 = false;
        /// <summary>
        /// 顶温上次比上上次铁高
        /// </summary>
        public bool TTOP_STATUS5 = false;

        /// <summary>
        /// 煤气利用率比上次铁低
        /// </summary>
        public bool COR_STATUS1 = false;
        /// <summary>
        /// 煤气利用率比上次铁高
        /// </summary>
        public bool COR_STATUS2 = false;
        /// <summary>
        /// 煤气利用率上次比上上次铁低
        /// </summary>
        public bool COR_STATUS3 = false;
        /// <summary>
        /// 煤气利用率上次比上上次铁高
        /// </summary>
        public bool COR_STATUS4 = false;

        /// <summary>
        /// 炉温向剧冷
        /// </summary>
        public bool Rule403B015 = false;
        /// <summary>
        /// 前次炉温向凉
        /// </summary>
        public bool Rule403B013 = false;
        /// <summary>
        /// 炉温向凉
        /// </summary>
        public bool Rule403B011 = false;
        /// <summary>
        /// 炉温向热
        /// </summary>
        public bool Rule403B012 = false;
        /// <summary>
        /// 前次炉温向热
        /// </summary>
        public bool Rule403B014 = false;
        /// <summary>
        /// 热状态指数的等级
        /// </summary>
        public int tmrnk_va11 = -9999;
        /// <summary>
        ///上次出铁的铁水温度(即当前时间15M表中的数据)
        /// </summary>
        public double HMTH15 { get; set; }
        /// <summary>
        /// 铁水温度的等级
        /// </summary>
        public int tmhmtrnk_va12 = -9999;

        public int[,] ACTTYPE_VA13 = new int[7, 8] {
            {-5,-5,-5,-4,-2,-1,0,1 },
            {-5,-5,-4,-3,-1,0,1,2 },
            {-5,-4,-3,-2,0,1,2,3 },
            {-4,-3,-2,-1,1,2,3,4 },
            {-3,-2,-1,0,2,3,4,5 },
            {-2,-1,0,1,3,4,5,5 },
            {-1,0,1,2,4,5,5,5 }
        };
        /// <summary>
        /// 本次热判断综合等级
        /// </summary>
        public int ACT_TM { get; set; }
        /// <summary>
        /// 上次热判断综合等级
        /// </summary>
        public int ACT_TM_PR { get; set; }
        /// <summary>
        /// 最大风温
        /// </summary>
        public double BT_MAX = 1250;
        /// <summary>
        /// 风口前理燃温度
        /// </summary>
        public double TEMP_FKLR { get; set; }
        /// <summary>
        /// 最大风温和当前风温差
        /// </summary>
        public double BTMAX_BT3M { get; set; }
        public bool IsAction = false;
        GSPAR400 m_GSPAR400 = new GSPAR400();
        #endregion
        #region 入库参数
        /// <summary>
        /// 现象编号
        /// </summary>
        public int R_MSGID { get; set; }
        /// <summary>
        /// 风温建议
        /// </summary>
        public double BT403C01 { get; set; }
        /// <summary>
        /// 喷煤建议
        /// </summary>
        public double PCI403C01 { get; set; }
        /// <summary>
        /// 富氧建议
        /// </summary>
        public double BVO403C01 { get; set; }
        /// <summary>
        /// 风量建议
        /// </summary>
        public double BV403C01 { get; set; }
        /// <summary>
        /// 附加焦建议
        /// </summary>
        public double BC403C01 { get; set; }
        /// <summary>
        /// 矿批建议
        /// </summary>
        public double CR403C01 { get; set; }



        /// <summary>
        /// 描述
        /// </summary>
        public string R_MSGTEXT { get; set; }
        public double BT3M { get; private set; }
        /// <summary>
        /// 当前喷煤
        /// </summary>
        public double pci { get; set; }
        #endregion
        public void Calcular(DateTime dtEnd)
        {
            CurrentTime = dtEnd;
            Init();
            Calc();
            AnalysisPheomenon();
            InsertPheomenon();
        }
        public void Init()
        {

            //查询最接近的
            string sqlData = "select {0} from {1} where {2} = (select max({2}) from {1} where {2}<{3})";
            //上次的
            string sql_Time_up = "select {0} from {1} where {2} = (select max({2}) from {1} where {2} < (select max({2}) from {1} where {2}<{3}))";
            //上上次的
            string sql_Time_up_up = "select {0} from {1} where {2} = (select max({2}) from {1} where {2} < (select max({2}) from {1} where {2} < (select max({2}) from {1} where {2}<{3})))";
            //查询上次上上次的
            string select_up = "select {0} from {1} where {2} = (select min({2}) from {1} where {2} > {3}) ";

            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                m_GSPAR400 = idatabase.GetSingle<GSPAR400>("select * from GSPAR400");
                //查询上次出铁时间
                DateTime dt_up = idatabase.GetDateTime(string.Format(sql_Time_up, "M_TAPSTART", "V_TAPPING", "M_TAPSTART", UtilsConvert.GetOraString(CurrentTime)));
                //查询上上次出铁时间
                DateTime dt_up_up = idatabase.GetDateTime(string.Format(sql_Time_up_up, "M_TAPSTART", "V_TAPPING", "M_TAPSTART", UtilsConvert.GetOraString(CurrentTime)));
                //本次数据
                DataTable dt = idatabase.GetDataTable(string.Format(sqlData, "NVL(C_ITZ15M,0),NVL(C_BV15,0),NVL(C_VB15M,0),NVL(C_TTOP15M,0),NVL(C_COR15M,0)", "DT_TC15M", "TIMESTAMP", UtilsConvert.GetOraString(CurrentTime)));
                if (dt != null)
                {
                    //透气性指数
                    ITZ15M = dt.Rows[0][0].ToDouble();
                    //风量
                    BV15M = dt.Rows[0][1].ToDouble();
                    //料速
                    VB15M = dt.Rows[0][2].ToDouble();
                    //顶温
                    TTOP15M = dt.Rows[0][3].ToDouble();
                    //煤气利用率
                    COR15M = dt.Rows[0][4].ToDouble();
                }
                //上次出铁数据
                dt = idatabase.GetDataTable(string.Format(select_up, "NVL(C_ITZ15M,0),NVL(C_BV15,0),NVL(C_VB15M,0),NVL(C_TTOP15M,0),NVL(C_COR15M,0)", "DT_TC15M", "TIMESTAMP", UtilsConvert.GetOraString(dt_up)));
                if (dt != null)
                {
                    //透气性指数
                    ITZ15MB1 = dt.Rows[0][0].ToDouble();
                    //风量
                    BV15MB1 = dt.Rows[0][1].ToDouble();
                    //料速
                    VB15MB1 = dt.Rows[0][2].ToDouble();
                    //顶温
                    TTOP15MB1 = dt.Rows[0][3].ToDouble();
                    //煤气利用率
                    COR15MB1 = dt.Rows[0][4].ToDouble();
                }
                //上上次出铁数据
                dt = idatabase.GetDataTable(string.Format(select_up, "NVL(C_ITZ15M,0),NVL(C_BV15,0),NVL(C_VB15M,0),NVL(C_TTOP15M,0),NVL(C_COR15M,0)", "DT_TC15M", "TIMESTAMP", UtilsConvert.GetOraString(dt_up_up)));
                if (dt != null)
                {
                    //透气性指数
                    ITZ15MB2 = dt.Rows[0][0].ToDouble();
                    //风量
                    BV15MB2 = dt.Rows[0][1].ToDouble();
                    //料速
                    VB15MB2 = dt.Rows[0][2].ToDouble();
                    //顶温
                    TTOP15MB2 = dt.Rows[0][3].ToDouble();
                    //煤气利用率
                    COR15MB2 = dt.Rows[0][4].ToDouble();
                }
                //上次热判断等级
                // ACT_TM_PR = 
                //理然温度
                TEMP_FKLR = idatabase.GetDouble(string.Format(sqlData, "C_TF_CAL", "TC05M", "TIMESTAMP", UtilsConvert.GetOraString(CurrentTime)));
                dt = idatabase.GetDataTable(string.Format(sqlData, "NVL(A_BT5M,0),NVL(A_WINJ,0)", "AV05M", "TIMESTAMP", UtilsConvert.GetOraString(CurrentTime)));

                if (dt != null)
                {
                    //当前风温 BT3M
                    BT3M = dt.Rows[0][0].ToDouble();
                    //当前喷煤
                    pci = dt.Rows[0][1].ToDouble();
                }
                //最大温度和当前风温差
                BTMAX_BT3M = BT_MAX - BT3M;
                //当前铁水温度 DT_TC15M C_HMTH15
                HMTH15 = idatabase.GetDouble(string.Format(sqlData, "C_HMTH15", "DT_TC15M", "TIMESTAMP", UtilsConvert.GetOraString(CurrentTime)));
            }
        }
        public void Calc()
        {
            //透气性指数
            if (ITZ15M - ITZ15MB1 > m_GSPAR400.R_THER_MODEL_ITZ1)
                ITZ_STATUS1 = true;
            if (ITZ15M - ITZ15MB1 < m_GSPAR400.R_THER_MODEL_ITZ2)
                ITZ_STATUS2 = true;
            if (ITZ15M - ITZ15MB1 > m_GSPAR400.R_THER_MODEL_ITZ3)
                ITZ_STATUS3 = true;
            if (ITZ15M - ITZ15MB1 < m_GSPAR400.R_THER_MODEL_ITZ4)
                ITZ_STATUS4 = true;
            if (ITZ15MB1 - ITZ15MB2 > m_GSPAR400.R_THER_MODEL_ITZ1)
                ITZ_STATUS5 = true;
            if (ITZ15MB1 - ITZ15MB2 < m_GSPAR400.R_THER_MODEL_ITZ2)
                ITZ_STATUS6 = true;

            //风量
            if (BV15M - BV15MB1 < m_GSPAR400.R_THER_MODEL_BV1)
                BV_STATUS1 = true;
            if (BV15M - BV15MB1 > m_GSPAR400.R_THER_MODEL_BV2)
                BV_STATUS2 = true;
            if (BV15M - BV15MB1 < m_GSPAR400.R_THER_MODEL_BV3)
                BV_STATUS3 = true;
            if (BV15M - BV15MB1 > m_GSPAR400.R_THER_MODEL_BV4)
                BV_STATUS4 = true;
            if (BV15MB1 - BV15MB2 < m_GSPAR400.R_THER_MODEL_BV1)
                BV_STATUS5 = true;
            if (BV15MB1 - BV15MB2 > m_GSPAR400.R_THER_MODEL_BV2)
                BV_STATUS6 = true;

            //料速
            if (VB15M - VB15MB1 < m_GSPAR400.R_THER_MODEL_VB1)
                VB_STATUS1 = true;
            if (VB15M - VB15MB1 > m_GSPAR400.R_THER_MODEL_VB2)
                VB_STATUS2 = true;
            if (VB15MB1 - VB15MB2 < m_GSPAR400.R_THER_MODEL_VB1)
                VB_STATUS3 = true;
            if (VB15MB1 - VB15MB2 > m_GSPAR400.R_THER_MODEL_VB2)
                VB_STATUS4 = true;

            //顶温
            if (TTOP15M - TTOP15MB1 < m_GSPAR400.R_THER_MODEL_TTOP1)
                TTOP_STATUS1 = true;
            if (TTOP15M - TTOP15MB1 > m_GSPAR400.R_THER_MODEL_TTOP2)
                TTOP_STATUS2 = true;
            if (TTOP15M - TTOP15MB1 > m_GSPAR400.R_THER_MODEL_TTOP3)
                TTOP_STATUS3 = true;
            if (TTOP15MB1 - TTOP15MB2 < m_GSPAR400.R_THER_MODEL_TTOP1)
                TTOP_STATUS4 = true;
            if (TTOP15MB1 - TTOP15MB2 > m_GSPAR400.R_THER_MODEL_TTOP2)
                TTOP_STATUS5 = true;

            //煤气利用率
            if (COR15M - COR15MB1 < m_GSPAR400.R_THER_MODEL_COR1)
                COR_STATUS1 = true;
            if (COR15M - COR15MB1 > m_GSPAR400.R_THER_MODEL_COR2)
                COR_STATUS2 = true;
            if (COR15MB1 - COR15MB2 < m_GSPAR400.R_THER_MODEL_COR1)
                COR_STATUS3 = true;
            if (COR15MB1 - COR15MB2 > m_GSPAR400.R_THER_MODEL_COR2)
                COR_STATUS4 = true;

            if (ITZ_STATUS2 && BV_STATUS1 && VB_STATUS1 && TTOP_STATUS1 && COR_STATUS1)
                Rule403B015 = true;
            if (ITZ_STATUS5 && BV_STATUS6 && VB_STATUS4 && TTOP_STATUS4 && COR_STATUS3)
                Rule403B013 = true;
            if (ITZ_STATUS1 && BV_STATUS2 && VB_STATUS2 && TTOP_STATUS1 && COR_STATUS1)
                Rule403B011 = true;
            if (ITZ_STATUS2 && BV_STATUS1 && VB_STATUS1 && TTOP_STATUS2 && COR_STATUS2)
                Rule403B012 = true;
            if (ITZ_STATUS6 && BV_STATUS5 && VB_STATUS3 && TTOP_STATUS5 && COR_STATUS4)
                Rule403B014 = true;

            if (Rule403B011)
                tmrnk_va11 = 3;
            if (Rule403B013 && Rule403B011)
                tmrnk_va11 = 2;
            if (ITZ_STATUS3 && Rule403B013 && BV_STATUS4 && Rule403B011)
                tmrnk_va11 = 1;
            if (Rule403B015 && ITZ_STATUS4 && Rule403B013 && BV_STATUS3)
                tmrnk_va11 = 0;

            if (Rule403B012)
                tmrnk_va11 = 4;
            if (Rule403B014 && Rule403B014)
                tmrnk_va11 = 5;
            if (Rule403B012 && Rule403B014 && ITZ_STATUS4)
                tmrnk_va11 = 6;
            if (Rule403B012 && Rule403B014 && ITZ_STATUS4 && TTOP_STATUS3 && BV_STATUS3)
                tmrnk_va11 = 7;

            if (HMTH15 < m_GSPAR400.R_THER_MODEL_HMTH_1)
                tmhmtrnk_va12 = 0;
            else if (HMTH15 >= m_GSPAR400.R_THER_MODEL_HMTH_1 && HMTH15 < m_GSPAR400.R_THER_MODEL_HMTH_2)
                tmhmtrnk_va12 = 1;
            else if (HMTH15 >= m_GSPAR400.R_THER_MODEL_HMTH_2 && HMTH15 < m_GSPAR400.R_THER_MODEL_HMTH_3)
                tmhmtrnk_va12 = 2;
            else if (HMTH15 >= m_GSPAR400.R_THER_MODEL_HMTH_3 && HMTH15 <= m_GSPAR400.R_THER_MODEL_HMTH_4)
                tmhmtrnk_va12 = 3;
            else if (HMTH15 > m_GSPAR400.R_THER_MODEL_HMTH_4 && HMTH15 <= m_GSPAR400.R_THER_MODEL_HMTH_5)
                tmhmtrnk_va12 = 4;
            else if (HMTH15 > m_GSPAR400.R_THER_MODEL_HMTH_5 && HMTH15 <= m_GSPAR400.R_THER_MODEL_HMTH_6)
                tmhmtrnk_va12 = 5;
            else if (HMTH15 > m_GSPAR400.R_THER_MODEL_HMTH_6)
                tmhmtrnk_va12 = 6;

            if (tmrnk_va11 != -9999)
                ACT_TM = ACTTYPE_VA13[tmhmtrnk_va12, tmrnk_va11];


        }
        public void AnalysisPheomenon()
        {
            switch (ACT_TM)
            {
                case -5:
                    R_MSGID = 40305;
                    R_MSGTEXT = "炉温严重剧冷";
                    CR403C01 = 50;
                    BVO403C01 = -50;
                    BV403C01 = -6;
                    BT403C01 = BT_MAX;
                    BC403C01 = 2;

                    break;
                case -4:
                    R_MSGID = 40304;
                    R_MSGTEXT = "炉温严重冷行";
                    CR403C01 = 0.1;
                    BVO403C01 = -15;
                    BT403C01 = 20;
                    PCI403C01 = 2;

                    if (TEMP_FKLR > m_GSPAR400.R_THER_MODEL_FKLR1)
                    {
                        BT403C01 = 0;
                        PCI403C01 = 2;
                    }
                    else if (TEMP_FKLR <= m_GSPAR400.R_THER_MODEL_FKLR1 && TEMP_FKLR >= m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = 0;
                            PCI403C01 = 2;
                        }
                        else
                        {
                            BT403C01 = BT_MAX - BT3M;
                            PCI403C01 = 2;
                        }
                    }
                    else if (TEMP_FKLR < m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -pci;
                        }
                        else
                        {
                            BT403C01 = BT_MAX - BT3M;
                            PCI403C01 = -pci;
                        }
                    }
                    break;
                case -3:
                    R_MSGID = 40303;
                    R_MSGTEXT = "炉温冷行";
                    BT403C01 = 20;
                    PCI403C01 = 2;
                    if (TEMP_FKLR > m_GSPAR400.R_THER_MODEL_FKLR1)
                    {
                        BT403C01 = 0;
                        PCI403C01 = 2;
                    }
                    else if (TEMP_FKLR <= m_GSPAR400.R_THER_MODEL_FKLR1 && TEMP_FKLR >= m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = 0;
                            PCI403C01 = 2;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 40)
                        {
                            BT403C01 = 20;
                            PCI403C01 = (40 - (BT_MAX - BT3M)) * 0.05;
                        }
                        else if (BTMAX_BT3M >= 40)
                        {
                            BT403C01 = 20;
                            PCI403C01 = 2;
                        }
                    }
                    else if (TEMP_FKLR < m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 40)
                        {
                            BT403C01 = BT_MAX - BT3M;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M >= 40)
                        {
                            BT403C01 = 40;
                            PCI403C01 = -pci;
                        }
                    }
                    break;
                case -2:
                    R_MSGID = 40302;
                    R_MSGTEXT = "炉温向凉";
                    PCI403C01 = 0.5;
                    if (TEMP_FKLR > m_GSPAR400.R_THER_MODEL_FKLR1)
                    {
                        BT403C01 = 0;
                        PCI403C01 = 0.5;
                    }
                    else if (TEMP_FKLR <= m_GSPAR400.R_THER_MODEL_FKLR1 && TEMP_FKLR >= m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = 0;
                            PCI403C01 = 0.5;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 30)
                        {
                            BT403C01 = 0;
                            PCI403C01 = 0.5;
                        }
                        else if (BTMAX_BT3M >= 30)
                        {
                            BT403C01 = 0;
                            PCI403C01 = 0.5;
                        }
                    }
                    else if (TEMP_FKLR < m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 30)
                        {
                            BT403C01 = BT_MAX - BT3M;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M >= 30)
                        {
                            BT403C01 = 30;
                            PCI403C01 = -pci;
                        }
                    }
                    break;
                case -1:
                    R_MSGID = 40301;
                    R_MSGTEXT = "炉温轻微向凉";
                    break;
                case 1:
                    R_MSGID = 40311;
                    R_MSGTEXT = "炉温轻微向热";
                    break;
                case 2:
                    R_MSGID = 40312;
                    R_MSGTEXT = "炉温向热";
                    PCI403C01 = -0.5;

                    if (TEMP_FKLR > m_GSPAR400.R_THER_MODEL_FKLR2)
                    {
                        BT403C01 = 0;
                        PCI403C01 = 0;
                    }
                    else if (TEMP_FKLR <= m_GSPAR400.R_THER_MODEL_FKLR2 && TEMP_FKLR >= m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = -30;
                            PCI403C01 = 0;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 30 * 0.05)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M >= 30 * 0.05)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -0.5;
                        }
                    }
                    else if (TEMP_FKLR < m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = -30;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 30 * 0.05)
                        {
                            BT403C01 = -(30 * 0.05 - pci) / 0.05;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M >= 30 * 0.05)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -pci;
                        }
                    }
                    break;
                case 3:
                    R_MSGID = 40313;
                    R_MSGTEXT = "炉温热行";
                    PCI403C01 = 2;
                    if (TEMP_FKLR > m_GSPAR400.R_THER_MODEL_FKLR2)
                    {
                        BT403C01 = 0;
                        PCI403C01 = 0;
                    }
                    else if (TEMP_FKLR <= m_GSPAR400.R_THER_MODEL_FKLR2 && TEMP_FKLR >= m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = -40;
                            PCI403C01 = 0;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 40)
                        {
                            BT403C01 = -40;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M >= 40)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -2;
                        }
                    }
                    else if (TEMP_FKLR < m_GSPAR400.R_THER_MODEL_FKLR3)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = -40;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M > 0 && BTMAX_BT3M < 40)
                        {
                            BT403C01 = -(40 * 0.05 - pci) / 0.05;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M >= 40)
                        {
                            BT403C01 = 0;
                            PCI403C01 = -pci;
                        }
                    }
                    break;
                case 4:
                    R_MSGID = 40314;
                    R_MSGTEXT = "炉温严重热行";
                    BT403C01 = -20;
                    PCI403C01 = -2;
                    CR403C01 = -10;
                    if (TEMP_FKLR > m_GSPAR400.R_THER_MODEL_FKLR2)
                    {
                        BT403C01 = -20;
                        PCI403C01 = -2;
                    }
                    else if (TEMP_FKLR <= m_GSPAR400.R_THER_MODEL_FKLR2)
                    {
                        if (BTMAX_BT3M <= 0)
                        {
                            BT403C01 = -20;
                            PCI403C01 = -pci;
                        }
                        else if (BTMAX_BT3M > 0)
                        {
                            BT403C01 = -20;
                            PCI403C01 = -2;
                        }
                    }
                    break;
                case 5:
                    R_MSGID = 40315;
                    R_MSGTEXT = "炉温严重过热";
                    BT403C01 = -80;
                    PCI403C01 = -5;
                    break;
            }
        }
        public void InsertPheomenon()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(m_dbs))
            {
                if (ACT_TM != 0)
                {
                    string R_OPERATIONTEXT = "";
                    if (BT403C01 != 0)
                        if (BT403C01 == BT_MAX)
                            R_OPERATIONTEXT += "风温加到最大,";
                        else
                        {
                            if (BT403C01 > 0)
                                R_OPERATIONTEXT += "风温增加" + Math.Round(BT403C01, 2) + "℃,";
                            else
                                R_OPERATIONTEXT += "风温减少" + Math.Round(Math.Abs(BT403C01), 2) + "℃,";
                        }
                    if (PCI403C01 != 0)
                        if (PCI403C01 == -pci)
                        {
                            R_OPERATIONTEXT += "停煤,";
                        }
                        else
                        {
                            if (PCI403C01 > 0)
                                R_OPERATIONTEXT += "喷煤增加" + Math.Round(PCI403C01, 2) + "t/h,";
                            else
                                R_OPERATIONTEXT += "喷煤减少" + Math.Round(Math.Abs(PCI403C01), 2) + "t/h,";
                        }
                    if (BVO403C01 != 0)
                        if (BVO403C01 > 0)
                            R_OPERATIONTEXT += "富氧增加" + Math.Round(BVO403C01, 2) + "%,";
                        else
                            R_OPERATIONTEXT += "富氧减少" + Math.Round(Math.Abs(BVO403C01), 2) + "%,";
                    if (BV403C01 != 0)
                        if (BV403C01 > 0)
                            R_OPERATIONTEXT += "风量增加" + Math.Round(BV403C01, 2) + "%,";
                        else
                            R_OPERATIONTEXT += "风量减少" + Math.Round(Math.Abs(BV403C01), 2) + "%,";
                    if (BC403C01 != 0)
                        if (BC403C01 > 0)
                            R_OPERATIONTEXT += "附加焦增加" + Math.Round(BC403C01, 2) + ",";
                        else
                            R_OPERATIONTEXT += "附加焦减少" + Math.Round(Math.Abs(BC403C01), 2) + ",";
                    if (CR403C01 != 0)
                        if (CR403C01 > 0)
                            R_OPERATIONTEXT += "批矿增加" + Math.Round(CR403C01, 2) + "%,";
                        else
                            R_OPERATIONTEXT += "批矿减少" + Math.Round(Math.Abs(CR403C01), 2) + "%,";
                    if (R_OPERATIONTEXT.Length > 0)
                        R_OPERATIONTEXT = R_OPERATIONTEXT.Remove(R_OPERATIONTEXT.Length - 1);
                    m_PRIMARYACTIONS.TIMESTAMP = CurrentTime;
                    m_PRIMARYACTIONS.R_MSGID = R_MSGID;
                    m_PRIMARYACTIONS.R_MSGTEXT = R_MSGTEXT;
                    m_PRIMARYACTIONS.R_OPERATIONTEXT = R_OPERATIONTEXT;
                    m_PRIMARYACTIONS.R_BVACT = Math.Round(BC403C01, 2);
                    m_PRIMARYACTIONS.R_BTACT = Math.Round(BT403C01, 2);
                    m_PRIMARYACTIONS.R_O2ACT = Math.Round(BVO403C01, 2);
                    m_PRIMARYACTIONS.R_PCIACT = Math.Round(PCI403C01, 2);
                    m_PRIMARYACTIONS.R_BCACT = Math.Round(BC403C01, 2);
                    m_PRIMARYACTIONS.R_ORACT = Math.Round(CR403C01, 2);
                    m_PRIMARYACTIONS.EXIST = true;
                    m_PRIMARYACTIONS.R_HMTRNK = tmhmtrnk_va12 - 3;
                    if (tmrnk_va11 < 4)
                        m_PRIMARYACTIONS.R_TQRNK = tmrnk_va11 - 4;
                    else
                        m_PRIMARYACTIONS.R_TQRNK = tmrnk_va11 - 3;
                    m_PRIMARYACTIONS.R_ACT_TM = ACT_TM;
                    idatabase.BeginTran();
                    try
                    {
                        idatabase.Insert(m_PRIMARYACTIONS);
                    }
                    catch (Exception ee)
                    {
                        idatabase.RollbackTran();
                        return;
                    }
                    idatabase.CommitTran();
                }
            }
        }


    }
}
