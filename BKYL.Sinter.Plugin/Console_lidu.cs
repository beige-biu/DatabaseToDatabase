
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BFES.DataAccess;

using System.Data;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;
using MathNet.Numerics.Statistics;

namespace BKYL.Sinter.Plugin
{
    public class Console_lidu : BaseJob
    {
        //二混后平均粒度
        public double PICAL_BREATH_2M_GRIT_AVG_ = 0;

        public double Q = 0;
        public double K1 = 0;
        public double A = 0;
        public double P = 0;
        public double H = 0;
        public double K2 = 0;
        public double n = 0.6;
        public int ROW = 0;
        public static double[,] RELEVANCE_SZ_l;
        public override void InitTask()
        {
            //加载配置lihaisen
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    M_PICAL_PAR M_PICAL_PAR_ = GET_M_PICAL_PAR(iDataBase);
                    MC_MICAL_PAR MC_MICAL_PAR_ = GET_MC_MICAL_PAR(iDataBase);
                    MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN_ = GET_MD_PHY_PARTICLE_INFO_IN(M_PICAL_PAR_, iDataBase);

                    int currentNum = currentTime.Hour * 3600 + currentTime.Minute * 60 + currentTime.Second;
                    if (currentNum % (M_PICAL_PAR_.PAR_T2*60) == 0)
                    {
                        if (MD_PHY_PARTICLE_INFO_IN_ != null)
                        {
                            #region 透气性
                            //主抽温度,主抽1风量、主抽2风量、主抽1负压、主抽2负压
                            T_MA_PGD_PLC_1MIN T_MA_PGD_PLC_1MIN_ = GET_T_MA_PGD_PLC_1MIN(M_PICAL_PAR_, iDataBase);
                            //平均布料厚度
                            T_CLOTH_PLC_1MIN T_CLOTH_PLC_1MIN_ = GET_T_CLOTH_PLC_1MIN(M_PICAL_PAR_, iDataBase);
                            //目标点火温度（平均）、点火段温度1、点火段温度2、点火段温度3、台车速度
                            T_BURN_COLE_PLC_1MIN T_BURN_COLE_PLC_1MIN_ = GET_T_BURN_COLE_PLC_1MIN(M_PICAL_PAR_, iDataBase);
                            //垂直烧结速度
                            MC_BTPCAL_RESULT_1MIN MC_BTPCAL_RESULT_1MIN_ = GET_MC_BTPCAL_RESULT_1MIN(M_PICAL_PAR_, iDataBase);
                            ///透气性计算
                            double JPU = JPU_CAL(T_MA_PGD_PLC_1MIN_, T_CLOTH_PLC_1MIN_, M_PICAL_PAR_, MC_MICAL_PAR_);
                            #endregion
                            SET_SAVE(JPU, iDataBase, MC_MICAL_PAR_, M_PICAL_PAR_);
                            int M_ROW = Config_Sys_Value(iDataBase);
                            DataTable dt = Get_M_PICAL_BREATH_RESULT(M_ROW, iDataBase);
                            BEISHAO_JICHUSHUJU(dt);
                            if (Set_MD_PHY_PARTICLE_INFO_IN())
                            {
                                log = "更新标志完成";
                            }
                            else
                            {
                                log = "更新标志失败";
                            }
                            Outputlog(log);
                        }
                    }
                }
                Outputlog(log);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                //释放资源
                // log = null;
            }
        }
        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public M_PICAL_PAR GET_M_PICAL_PAR(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from M_PICAL_PAR");
            return iDataBase.GetSingle<M_PICAL_PAR>(str_sql);
        }

        public MC_MICAL_PAR GET_MC_MICAL_PAR(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from MC_MICAL_PAR");
            return iDataBase.GetSingle<MC_MICAL_PAR>(str_sql);
        }


        /// <summary>
        /// 读取二混粒度数据
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public MD_PHY_PARTICLE_INFO_IN GET_MD_PHY_PARTICLE_INFO_IN(M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-M_PICAL_PAR_.PAR_T2);
            string str_sql = string.Format("select nvl(round(avg(GRIT_LOW_1),3),0) as GRIT_LOW_1,nvl(round(avg(GRIT_1_3),3),0) as GRIT_1_3,nvl(round(avg(GRIT_UP_3),3),0) as GRIT_UP_3,nvl(round(avg(MATERIAL_TE),3),0) as MATERIAL_TE               from MD_PHY_PARTICLE_INFO_IN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and L2_CODE=901", End_Times, StaterTimes);
            return iDataBase.GetSingle<MD_PHY_PARTICLE_INFO_IN>(str_sql);
        }
        /// <summary>
        /// 目标点火温度（平均）、点火段温度1、点火段温度2、点火段温度3、台车速度
        /// </summary>
        /// <param name="M_PICAL_PAR_"></param>
        /// <param name="MC_MICAL_PAR_"></param>
        /// <param name="MC_MICAL_RESULT_"></param>
        /// <param name="MD_PHY_PARTICLE_INFO_IN_"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public T_BURN_COLE_PLC_1MIN GET_T_BURN_COLE_PLC_1MIN(M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            int SIN_U_TIME_PV = Convert.ToInt16(GetSIN_U_TIME_PV(iDataBase));
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-SIN_U_TIME_PV);
            string str_sql = string.Format("select  nvl(round(avg(BC_AIM_TE),3),0) as BC_AIM_TE , nvl(round(avg(BC_IG_01_TE),3),0) as BC_IG_01_TE  , nvl(round(avg(BC_IG_02_TE),3),0) as BC_IG_02_TE, nvl(round(avg(BC_IG_03_TE),3),0) as BC_IG_03_TE , nvl(round(avg(BC_SIN_MS_PV),3),0) as BC_SIN_MS_PV  from T_BURN_COLE_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_BURN_COLE_PLC_1MIN>(str_sql);
        }
        /// <summary>
        /// 主抽温度,主抽1风量、主抽2风量、主抽1负压、主抽2负压
        /// </summary>
        /// <param name="M_PICAL_PAR_"></param>
        /// <param name="MC_MICAL_PAR_"></param>
        /// <param name="MC_MICAL_RESULT_"></param>
        /// <param name="MD_PHY_PARTICLE_INFO_IN_"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public T_MA_PGD_PLC_1MIN GET_T_MA_PGD_PLC_1MIN(M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            int SIN_U_TIME_PV = Convert.ToInt16(GetSIN_U_TIME_PV(iDataBase));
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-SIN_U_TIME_PV);
            string str_sql = string.Format("select  nvl(round(avg((PS_MA_SB_1_FLUE_TE+PS_MA_SB_2_FLUE_TE)/2),3),0) as PS_MA_SB_1_FLUE_TE , nvl(round(avg(PS_MA_SB_1_FLUE_FT),3),0) as PS_MA_SB_1_FLUE_FT, nvl(round(avg(PS_MA_SB_2_FLUE_FT),3),0) as PS_MA_SB_2_FLUE_FT, nvl(round(avg(PS_MA_SB_1_FLUE_PT),3),0) as PS_MA_SB_1_FLUE_PT , nvl(round(avg(PS_MA_SB_2_FLUE_PT),3),0) as PS_MA_SB_2_FLUE_PT from T_MA_PGD_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_MA_PGD_PLC_1MIN>(str_sql);
        }

        /// <summary>
        /// 铺底料厚度,料厚
        /// </summary>
        /// <param name="M_PICAL_PAR_"></param>
        /// <param name="MC_MICAL_PAR_"></param>
        /// <param name="MC_MICAL_RESULT_"></param>
        /// <param name="MD_PHY_PARTICLE_INFO_IN_"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public T_CLOTH_PLC_1MIN GET_T_CLOTH_PLC_1MIN(M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            int SIN_U_TIME_PV = Convert.ToInt16(GetSIN_U_TIME_PV(iDataBase));
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-SIN_U_TIME_PV);
            string str_sql = string.Format("select nvl(round(avg(C_THICK_PV),3),0) as C_THICK_PV  from T_CLOTH_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_CLOTH_PLC_1MIN>(str_sql);
        }
        /// <summary>
        /// 垂直烧结速度
        /// </summary>
        /// <param name="M_PICAL_PAR_"></param>
        /// <param name="MC_MICAL_PAR_"></param>
        /// <param name="MC_MICAL_RESULT_"></param>
        /// <param name="MD_PHY_PARTICLE_INFO_IN_"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public MC_BTPCAL_RESULT_1MIN GET_MC_BTPCAL_RESULT_1MIN(M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-M_PICAL_PAR_.PAR_T2);
            string str_sql = string.Format("select  nvl(round(avg(BTPCAL_V),3),0) as BTPCAL_V  from MC_BTPCAL_RESULT_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')", End_Times, StaterTimes);
            return iDataBase.GetSingle<MC_BTPCAL_RESULT_1MIN>(str_sql);
        }
        /// <summary>
        /// 透气性计算
        /// </summary>
        /// <param name="T_MA_PGD_PLC_1MIN_"></param>
        /// <param name="T_CLOTH_PLC_1MIN_"></param>
        /// <param name="M_PICAL_PAR_"></param>
        /// <param name="MC_MICAL_PAR_"></param>
        /// <returns></returns>
        public double JPU_CAL(T_MA_PGD_PLC_1MIN T_MA_PGD_PLC_1MIN_, T_CLOTH_PLC_1MIN T_CLOTH_PLC_1MIN_, M_PICAL_PAR M_PICAL_PAR_, MC_MICAL_PAR MC_MICAL_PAR_)
        {
            double JPU = 0;
            double fenmu = 0;
            Q = 0;
            K1 = 0;
            A = 0;
            P = 0;
            H = 0;
            K2 = 0;
            n = 0.6;

            Q = T_MA_PGD_PLC_1MIN_.PS_MA_SB_1_FLUE_FT / 60 + T_MA_PGD_PLC_1MIN_.PS_MA_SB_2_FLUE_FT / 60;
            K1 = M_PICAL_PAR_.PAR_K1;
            A = (M_PICAL_PAR_.PAR_B * MC_MICAL_PAR_.PAR_LENGTH_SIN).ToDouble();
            P = Math.Abs((T_MA_PGD_PLC_1MIN_.PS_MA_SB_1_FLUE_PT + T_MA_PGD_PLC_1MIN_.PS_MA_SB_2_FLUE_PT) / 2);
            H = T_CLOTH_PLC_1MIN_.C_THICK_PV.ToDouble();
            K2 = M_PICAL_PAR_.PAR_K2;
            n = M_PICAL_PAR_.PAR_N;
            fenmu = K2 * A;
            if (fenmu > 0 && P > 0)
            {
                JPU = K2 * ((Q * (1 - K1)) / A) * (Math.Pow(H / (P * 1000), n));
            }
            return JPU;
        }
        public void SET_SAVE(double JPU, IDataBase iDataBase, MC_MICAL_PAR MC_MICAL_PAR_, M_PICAL_PAR M_PICAL_PAR_)
        {
            M_PICAL_BREATH_RESULT M_PICAL_BREATH_RESULT_ = null;
            string log = "";
            try
            {
                M_PICAL_BREATH_RESULT_ = new M_PICAL_BREATH_RESULT();
                M_PICAL_BREATH_RESULT_.TIMESTAMP = DateTime.Now;
                M_PICAL_BREATH_RESULT_.PICAL_JPU = JPU;
                ///
                #region 界面显示数据采集时间段
                #region  前九十分钟内
                int SIN_U_TIME_PV = Convert.ToInt16(GetSIN_U_TIME_PV(iDataBase));
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE16 = SIN_U_TIME_PV;
                M_PICAL_BREATH_RESULT_.PICAL_VER_SIN_SP = GetPICAL_VER_SIN_SP(SIN_U_TIME_PV, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_H = GetC_THICK_PV(SIN_U_TIME_PV, iDataBase);///2020-09-03修改
                T_BURN_COLE_PLC_1MIN T_BURN_COLE_PLC_1MIN_ = GetT_BURN_COLE_PLC_1MIN(SIN_U_TIME_PV, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_A = A;
                M_PICAL_BREATH_RESULT_.PICAL_P = P;
                M_PICAL_BREATH_RESULT_.PICAL_Q = Q;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_IG_01_TE = T_BURN_COLE_PLC_1MIN_.BC_IG_02_TE;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_IG_02_TE = T_BURN_COLE_PLC_1MIN_.BC_IG_03_TE;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE13 = T_BURN_COLE_PLC_1MIN_.BC_SIN_MS_PV;
                T_MA_PGD_PLC_1MIN T_MA_PGD_PLC_1MIN_ = GetT_MA_PGD_PLC_1MIN(SIN_U_TIME_PV, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_Q1 = T_MA_PGD_PLC_1MIN_.PS_MA_SB_1_FLUE_FT;
                M_PICAL_BREATH_RESULT_.PICAL_Q2 = T_MA_PGD_PLC_1MIN_.PS_MA_SB_2_FLUE_FT;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE14 = T_MA_PGD_PLC_1MIN_.PS_MA_SB_1_FLUE_TE;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE15 = T_MA_PGD_PLC_1MIN_.PS_MA_SB_2_FLUE_TE;
                M_PICAL_BREATH_RESULT_.PICAL_P1 = T_MA_PGD_PLC_1MIN_.PS_MA_SB_1_FLUE_PT;
                M_PICAL_BREATH_RESULT_.PICAL_P2 = T_MA_PGD_PLC_1MIN_.PS_MA_SB_2_FLUE_PT;
                #endregion
                #region  二混到烧结机台车耗时
                //二混到烧结机台车耗时
                double RHDSJJTCHS = (GetRHDSJJTCHS(iDataBase) + MC_MICAL_PAR_.PAR_CLOTH_U_TIME).ToDouble();
                T_BLEND_PLC_1MIN T_BLEND_PLC_1MIN_ = GetT_BLEND_PLC_1MIN(SIN_U_TIME_PV, RHDSJJTCHS, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_2M_FT_PV = T_BLEND_PLC_1MIN_.B_2M_FLOW_PV;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_2M_NEX_WAT = T_BLEND_PLC_1MIN_.B_2M_NEX_WATER_AVG;
                MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN_ = GetMD_PHY_PARTICLE_INFO_IN(SIN_U_TIME_PV, RHDSJJTCHS, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_BLE_TE = MD_PHY_PARTICLE_INFO_IN_.MATERIAL_TE;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_2M_GRIT_3 = MD_PHY_PARTICLE_INFO_IN_.GRIT_UP_3;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_2M_GRIT_1_3 = MD_PHY_PARTICLE_INFO_IN_.GRIT_1_3;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_2M_GRIT_1 = MD_PHY_PARTICLE_INFO_IN_.GRIT_LOW_3;
                #endregion
                #region  二混到一混倒推时间耗时
                //二混到一混倒推时间耗时
                double RHDYHDTSJHS = GetRHDYHDTSJHS(iDataBase);
                T_BLEND_PLC_1MIN T_BLEND_PLC_1MIN1_ = GetT_BLEND_PLC_1MIN(SIN_U_TIME_PV, RHDSJJTCHS, RHDYHDTSJHS, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_1M_FT_PV = T_BLEND_PLC_1MIN1_.B_1M_FT_PV;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_1M_NEX_WAT = T_BLEND_PLC_1MIN1_.B_1M_NEX_WATER_AVG;
                #endregion
                #region  一混到配料倒推耗时
                double YHDPLDTHS = GetYHDPLDTHS(iDataBase);
                T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_ = GetT_MATCH_PLC_1MIN(SIN_U_TIME_PV, RHDSJJTCHS, RHDYHDTSJHS, YHDPLDTHS, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE12 = T_MATCH_PLC_1MIN_.M_ACTUAL_W_1;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_COKE_BILL = T_MATCH_PLC_1MIN_.M_ACTUAL_W_2;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SRM_BILL = T_MATCH_PLC_1MIN_.M_ACTUAL_W_3;
                MC_SINCAL_result_1min MC_SINCAL_result_1min_ = GetMC_SINCAL_result_1min(SIN_U_TIME_PV, RHDSJJTCHS, RHDYHDTSJHS, YHDPLDTHS, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE2 = MC_SINCAL_result_1min_.SINCAL_SIN_PV_R;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE3 = MC_SINCAL_result_1min_.SINCAL_SIN_PV_MGO;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE4 = MC_SINCAL_result_1min_.SINCAL_MIX_PV_C;
                MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN1_ = GetMD_PHY_PARTICLE_INFO_IN(SIN_U_TIME_PV, RHDSJJTCHS, RHDYHDTSJHS, YHDPLDTHS, iDataBase);
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE5 = MD_PHY_PARTICLE_INFO_IN1_.GRIT_UP_3;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE6 = MD_PHY_PARTICLE_INFO_IN1_.GRIT_1_3;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE7 = MD_PHY_PARTICLE_INFO_IN1_.GRIT_LOW_3;
                #endregion
                #endregion

                M_PICAL_BREATH_RESULT_.PICAL_BREATH_IG_03_TE = (M_PICAL_BREATH_RESULT_.PICAL_BREATH_IG_02_TE + M_PICAL_BREATH_RESULT_.PICAL_BREATH_IG_01_TE) / 2;
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE17 = (M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE14 + M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE15) / 2;
            }
            catch (Exception ee)
            {

                throw;
            }
            if (Set_Admin_add(M_PICAL_BREATH_RESULT_))
            {
                log = "保存完成";
            }
            else
            {
                log = "保存失败";
            }
            Outputlog(log);
        }

        private MD_PHY_PARTICLE_INFO_IN GetMD_PHY_PARTICLE_INFO_IN(int sIN_U_TIME_PV, double rHDSJJTCHS, double rHDYHDTSJHS, double yHDPLDTHS, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - rHDYHDTSJHS - yHDPLDTHS);
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - sIN_U_TIME_PV - rHDYHDTSJHS - yHDPLDTHS);
            string str_sql = string.Format("select  count(*)  from MD_PHY_PARTICLE_INFO_IN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and L2_CODE=301", End_Times, StaterTimes);
            if (iDataBase.GetInt(str_sql) > 0)
            {
                str_sql = string.Format("select  nvl(round(avg(MATERIAL_TE),3),0) as MATERIAL_TE,nvl(round(avg(GRIT_UP_3),3),0) as GRIT_UP_3,nvl(round(avg(GRIT_1_3),3),0) as GRIT_1_3,nvl(round(avg(GRIT_LOW_3),3),0) as GRIT_LOW_3  from MD_PHY_PARTICLE_INFO_IN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and L2_CODE=301 ", End_Times, StaterTimes);
                return iDataBase.GetSingle<MD_PHY_PARTICLE_INFO_IN>(str_sql);
            }
            else
            {
                str_sql = string.Format("select  nvl(round(avg(MATERIAL_TE),3),0) as MATERIAL_TE,nvl(round(avg(GRIT_UP_3),3),0) as GRIT_UP_3,nvl(round(avg(GRIT_1_3),3),0) as GRIT_1_3,nvl(round(avg(GRIT_LOW_3),3),0) as GRIT_LOW_3  from MD_PHY_PARTICLE_INFO_IN where timestamp = (select max(timestamp) from MD_PHY_PARTICLE_INFO_IN where L2_CODE=301) and L2_CODE=301", End_Times, StaterTimes);
                return iDataBase.GetSingle<MD_PHY_PARTICLE_INFO_IN>(str_sql);
            }

        }

        private MC_SINCAL_result_1min GetMC_SINCAL_result_1min(int sIN_U_TIME_PV, double rHDSJJTCHS, double rHDYHDTSJHS, double yHDPLDTHS, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - rHDYHDTSJHS - yHDPLDTHS);
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - sIN_U_TIME_PV - rHDYHDTSJHS - yHDPLDTHS);
            string str_sql = string.Format("select  nvl(round(avg(SINCAL_SIN_PV_R),3),0) as SINCAL_SIN_PV_R,nvl(round(avg(SINCAL_SIN_PV_MGO),3),0) as SINCAL_SIN_PV_MGO,nvl(round(avg(SINCAL_MIX_PV_C),3),0) as SINCAL_MIX_PV_C  from MC_SINCAL_result_1min where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetSingle<MC_SINCAL_result_1min>(str_sql);
        }

        private T_MATCH_PLC_1MIN GetT_MATCH_PLC_1MIN(int sIN_U_TIME_PV, double rHDSJJTCHS, double rHDYHDTSJHS, double yHDPLDTHS, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - rHDYHDTSJHS - yHDPLDTHS);
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - sIN_U_TIME_PV - rHDYHDTSJHS - yHDPLDTHS);
            string str_sql = string.Format("select nvl(round(avg(((M_ACTUAL_W_10 + M_ACTUAL_W_11 + M_ACTUAL_W_16 +M_ACTUAL_W_17) /(M_ACTUAL_W_1 + M_ACTUAL_W_2 + M_ACTUAL_W_3+M_ACTUAL_W_4 + M_ACTUAL_W_5 + M_ACTUAL_W_6 +M_ACTUAL_W_7 + M_ACTUAL_W_8 + M_ACTUAL_W_9+M_ACTUAL_W_10 + M_ACTUAL_W_11 + M_ACTUAL_W_12 + M_ACTUAL_W_13 + M_ACTUAL_W_14 + M_ACTUAL_W_15 + M_ACTUAL_W_16 + M_ACTUAL_W_17 + M_ACTUAL_W_18 + M_ACTUAL_W_19)) *100),  3), 0) as M_ACTUAL_W_1,  nvl(round(avg(((M_ACTUAL_W_18 + M_ACTUAL_W_19) / (M_ACTUAL_W_1 + M_ACTUAL_W_2 + M_ACTUAL_W_3 + M_ACTUAL_W_4 + M_ACTUAL_W_5 + M_ACTUAL_W_6 + M_ACTUAL_W_7 + M_ACTUAL_W_8 + M_ACTUAL_W_9 + M_ACTUAL_W_10 + M_ACTUAL_W_11 + M_ACTUAL_W_12 + M_ACTUAL_W_13 + M_ACTUAL_W_14 + M_ACTUAL_W_15 + M_ACTUAL_W_16 + M_ACTUAL_W_17 + M_ACTUAL_W_18 + M_ACTUAL_W_19)) * 100),  3), 0) as M_ACTUAL_W_2,  nvl(round(avg(((M_ACTUAL_W_8 + M_ACTUAL_W_9) / (M_ACTUAL_W_1 + M_ACTUAL_W_2 + M_ACTUAL_W_3 + M_ACTUAL_W_4 + M_ACTUAL_W_5 + M_ACTUAL_W_6 + M_ACTUAL_W_7 + M_ACTUAL_W_8 + M_ACTUAL_W_9 + M_ACTUAL_W_10 + M_ACTUAL_W_11 + M_ACTUAL_W_12 + M_ACTUAL_W_13 + M_ACTUAL_W_14 + M_ACTUAL_W_15 + M_ACTUAL_W_16 + M_ACTUAL_W_17 + M_ACTUAL_W_18 + M_ACTUAL_W_19)) * 100),  3), 0) as M_ACTUAL_W_3  from T_MATCH_PLC_1MIN   where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_MATCH_PLC_1MIN>(str_sql);
        }

        private T_BLEND_PLC_1MIN GetT_BLEND_PLC_1MIN(int sIN_U_TIME_PV, double rHDSJJTCHS, double rHDYHDTSJHS, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - rHDYHDTSJHS);
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - sIN_U_TIME_PV - rHDYHDTSJHS);
            string str_sql = string.Format("select  nvl(round(avg(B_1M_FT_PV),3),0) as B_1M_FT_PV,nvl(round(avg(B_1M_NEX_WATER_AVG),3),0) as B_1M_NEX_WATER_AVG  from T_BLEND_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_BLEND_PLC_1MIN>(str_sql);
        }

        private MD_PHY_PARTICLE_INFO_IN GetMD_PHY_PARTICLE_INFO_IN(int sIN_U_TIME_PV, double rHDSJJTCHS, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS);
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - sIN_U_TIME_PV);
            string str_sql = string.Format("select  count(*)  from MD_PHY_PARTICLE_INFO_IN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and L2_CODE=901", End_Times, StaterTimes);
            if (iDataBase.GetInt(str_sql) > 0)
            {
                str_sql = string.Format("select  nvl(round(avg(MATERIAL_TE),3),0) as MATERIAL_TE,nvl(round(avg(GRIT_UP_3),3),0) as GRIT_UP_3,nvl(round(avg(GRIT_1_3),3),0) as GRIT_1_3,nvl(round(avg(GRIT_LOW_3),3),0) as GRIT_LOW_3  from MD_PHY_PARTICLE_INFO_IN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and L2_CODE=901 ", End_Times, StaterTimes);
                return iDataBase.GetSingle<MD_PHY_PARTICLE_INFO_IN>(str_sql);
            }
            else
            {
                str_sql = string.Format("select  nvl(round(avg(MATERIAL_TE),3),0) as MATERIAL_TE,nvl(round(avg(GRIT_UP_3),3),0) as GRIT_UP_3,nvl(round(avg(GRIT_1_3),3),0) as GRIT_1_3,nvl(round(avg(GRIT_LOW_3),3),0) as GRIT_LOW_3  from MD_PHY_PARTICLE_INFO_IN where timestamp = (select max(timestamp) from MD_PHY_PARTICLE_INFO_IN where L2_CODE=901) and L2_CODE=901", End_Times, StaterTimes);
                return iDataBase.GetSingle<MD_PHY_PARTICLE_INFO_IN>(str_sql);
            }

        }

        private T_BLEND_PLC_1MIN GetT_BLEND_PLC_1MIN(int sIN_U_TIME_PV, double rHDSJJTCHS, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS);
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV - rHDSJJTCHS - sIN_U_TIME_PV);
            string str_sql = string.Format("select  nvl(round(avg(B_2M_FLOW_PV),3),0) as B_2M_FLOW_PV,nvl(round(avg(B_2M_NEX_WATER_AVG),3),0) as B_2M_NEX_WATER_AVG  from T_BLEND_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_BLEND_PLC_1MIN>(str_sql);
        }

        private T_MA_PGD_PLC_1MIN GetT_MA_PGD_PLC_1MIN(int sIN_U_TIME_PV, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV);
            string str_sql = string.Format("select  nvl(round(avg(PS_MA_SB_1_FLUE_FT),3),0) as PS_MA_SB_1_FLUE_FT,nvl(round(avg(PS_MA_SB_2_FLUE_FT),3),0) as PS_MA_SB_2_FLUE_FT,nvl(round(avg(PS_MA_SB_1_FLUE_TE),3),0) as PS_MA_SB_1_FLUE_TE,nvl(round(avg(PS_MA_SB_2_FLUE_TE),3),0) as PS_MA_SB_2_FLUE_TE,nvl(round(avg(PS_MA_SB_1_FLUE_PT),3),0) as PS_MA_SB_1_FLUE_PT,nvl(round(avg(PS_MA_SB_2_FLUE_PT),3),0) as PS_MA_SB_2_FLUE_PT  from T_MA_PGD_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_MA_PGD_PLC_1MIN>(str_sql);
        }

        private T_BURN_COLE_PLC_1MIN GetT_BURN_COLE_PLC_1MIN(int sIN_U_TIME_PV, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-sIN_U_TIME_PV);
            string str_sql = string.Format("select  nvl(round(avg(BC_IG_02_TE),3),0) as BC_IG_02_TE,nvl(round(avg(BC_IG_03_TE),3),0) as BC_IG_03_TE,nvl(round(avg(BC_SIN_MS_PV),3),0) as BC_SIN_MS_PV  from T_BURN_COLE_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetSingle<T_BURN_COLE_PLC_1MIN>(str_sql);
        }

        private double? GetC_THICK_PV(int minutes, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-90);
            string str_sql = string.Format("select  nvl(round(avg(C_THICK_PV),3),0) as C_THICK_PV  from T_CLOTH_PLC_1MIN where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetDouble(str_sql);
        }

        private double? GetPICAL_VER_SIN_SP(int Minutes, IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-Minutes);
            string str_sql = string.Format("select  nvl(round(avg(BTPCAL_V),3),0) as BTPCAL_V  from MC_BTPCAL_result_1min where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') ", End_Times, StaterTimes);
            return iDataBase.GetDouble(str_sql);
        }

        private Double GetSIN_U_TIME_PV(IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-90);
            string str_sql = string.Format("select  nvl(round(avg(MICAL_SIN_U_TIME),3),0) as MICAL_SIN_U_TIME  from MC_MICAL_RESULT where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and DATANUM = 11", End_Times, StaterTimes);
            return iDataBase.GetDouble(str_sql);
        }
        private int GetRHDSJJTCHS(IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-90);
            string str_sql = string.Format("select  nvl(round(avg(MICAL_2M_1M_TIME+MICAL_BL_BUNK_TIME),3),0) as MICAL_SIN_U_TIME  from MC_MICAL_RESULT where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and DATANUM = 11", End_Times, StaterTimes);
            return iDataBase.GetInt(str_sql);
        }
        private double GetRHDYHDTSJHS(IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-90);
            string str_sql = string.Format("select  nvl(round(avg(MICAL_2M_1M_TIME),3),0) as MICAL_SIN_U_TIME  from MC_MICAL_RESULT where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and DATANUM = 11", End_Times, StaterTimes);
            return iDataBase.GetDouble(str_sql);
        }

        private double GetYHDPLDTHS(IDataBase iDataBase)
        {
            DateTime End_Times = DateTime.Now;
            DateTime StaterTimes = DateTime.Now.AddMinutes(-90);
            string str_sql = string.Format("select  nvl(round(avg(MICAL_1M_MAT_TIME),3),0) as MICAL_1M_MAT_TIME  from MC_MICAL_RESULT where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and DATANUM = 11", End_Times, StaterTimes);
            return iDataBase.GetDouble(str_sql);
        }

        public bool Set_Admin_add(M_PICAL_BREATH_RESULT model)
        {
            bool listmodel = false;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }

        public DataTable Get_M_PICAL_BREATH_RESULT(int a, IDataBase iDataBase)
        {
            string str_sql = string.Format(@"select * from  (select  nvl(PICAL_JPU, 0),
       nvl(PICAL_Q, 0),
       nvl(PICAL_P, 0),
       nvl(PICAL_BREATH_SRM_BILL, 0),
       nvl(PICAL_BREATH_COKE_BILL, 0),
       nvl(PICAL_BREATH_SPARE12, 0),
       nvl(PICAL_BREATH_SPARE2, 0),
       nvl(PICAL_BREATH_SPARE3, 0),
       nvl(PICAL_BREATH_SPARE4, 0),
       nvl(PICAL_BREATH_2M_NEX_WAT, 0),
       nvl(PICAL_H, 0),
       nvl(PICAL_BREATH_IG_03_TE, 0),
       nvl(PICAL_BREATH_SPARE13, 0),
       nvl(PICAL_BREATH_SPARE17, 0)
  from M_PICAL_BREATH_RESULT
 order by TIMESTAMP desc)
 where  rownum <'" + a + "'");
            return iDataBase.GetDataTable(str_sql);
        }
        public int Config_Sys_Value(IDataBase iDataBase)
        {
            string str_sql = string.Format(@"select VALUE from CONFIG_SYS_VALUE where id=3");
            return iDataBase.GetInt(str_sql);
        }
        public void BEISHAO_JICHUSHUJU(DataTable dt)
        {
            string log = "";
            try
            {
                RELEVANCE_SZ_l = new double[dt.Rows.Count, 14];
                double[,] RELEVANCE_SZ_2 = new double[dt.Rows.Count, 14];
                if (dt.Rows.Count > 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int i1 = 0; i1 < 14; i1++)
                        {
                            RELEVANCE_SZ_l[i, i1] = Convert.ToDouble(dt.Rows[i][i1].ToString());
                        }
                    }
                }
                List<double[]> list = new List<double[]>();
                for (int i = 0; i < 14; i++)
                {
                    double[] D = new double[dt.Rows.Count];
                    for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                    {
                        D[i1] = RELEVANCE_SZ_l[i1, i];
                    }
                    list.Add(D);
                }
                for (int i = 0; i < 14; i++)
                {
                    for (int i1 = 0; i1 < 14; i1++)
                    {
                        RELEVANCE_SZ_2[i, i1] = nanInfinity(Correlation.Spearman(list[i], list[i1]));
                    }
                }
                M_PICAL_BREATH_RESULT M_PICAL_BREATH_RESULT_ = null;
                M_PICAL_BREATH_RESULT_ = new M_PICAL_BREATH_RESULT();
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_BLEND = nanInfinity(RELEVANCE_SZ_2[1, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_FLUE_PT = nanInfinity(RELEVANCE_SZ_2[2, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_SINBACK = nanInfinity(RELEVANCE_SZ_2[3, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_FUEL = nanInfinity(RELEVANCE_SZ_2[4, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_FLUX = nanInfinity(RELEVANCE_SZ_2[5, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_R = nanInfinity(RELEVANCE_SZ_2[6, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_MGO = nanInfinity(RELEVANCE_SZ_2[7, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_COKE_GRI = nanInfinity(RELEVANCE_SZ_2[8, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_WAT = nanInfinity(RELEVANCE_SZ_2[9, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_THICK = nanInfinity(RELEVANCE_SZ_2[10, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_IG_TE = nanInfinity(RELEVANCE_SZ_2[11, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE11 = nanInfinity(RELEVANCE_SZ_2[12, 0]);
                M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_MA_TE = nanInfinity(RELEVANCE_SZ_2[13, 0]);
                if (Set_update(M_PICAL_BREATH_RESULT_))
                {
                    log = "更新完成";
                }
                else
                {
                    log = "更新失败";
                }
                Console.WriteLine("These folks work at {0}", "相关性计算完成!");

            }
            catch (Exception ee)
            {
                log = "读取原料成分维护表MATERIAL_ANA数据完成";
            }
        }
        public double nanInfinity(double num)
        {
            if (double.IsNaN(num))
            {
                num = 0;
            }
            if (double.IsInfinity(num))
            {
                num = 0;
            }
            return num;
        }

        public bool Set_update(M_PICAL_BREATH_RESULT M_PICAL_BREATH_RESULT_)
        {
            string log = "";
            bool listmodel = false;
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    iDataBase.BeginTran();
                    string sql = "";
                    sql = string.Format(@"update M_PICAL_BREATH_RESULT set PICAL_RELAT_BR_BL_BLEND ='" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_BLEND + "'"
                    + ",PICAL_RELAT_BR_FLUE_PT = '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_FLUE_PT + "'"
                    + ",PICAL_RELAT_BR_BL_SINBACK= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_SINBACK + "' "
                    + ",PICAL_RELAT_BR_BL_FUEL= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_FUEL + "'"
                    + ",PICAL_RELAT_BR_BL_FLUX= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_FLUX + "'"
                    + ",PICAL_RELAT_BR_R= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_R + "'"
                    + ",PICAL_RELAT_BR_BL_MGO= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_MGO + "'"
                    + ",PICAL_RELAT_BR_COKE_GRI= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_COKE_GRI + "'"
                    + ",PICAL_RELAT_BR_WAT= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_WAT + "'"
                    + ",PICAL_RELAT_BR_THICK= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_THICK + "'"
                    + ",PICAL_RELAT_BR_BL_IG_TE= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_BL_IG_TE + "'"
                    + ",PICAL_BREATH_SPARE11= '" + M_PICAL_BREATH_RESULT_.PICAL_BREATH_SPARE11 + "'"
                    + ",PICAL_RELAT_BR_MA_TE= '" + M_PICAL_BREATH_RESULT_.PICAL_RELAT_BR_MA_TE + "'  where timestamp = (select max(timestamp) from M_PICAL_BREATH_RESULT)");
                    iDataBase.ExecuteCommand(string.Format(sql));
                    iDataBase.CommitTran();
                    log = string.Format("更新相关性计算完成！");
                    Outputlog(log);
                    listmodel = true;
                    return listmodel;
                }
            }
            catch
            {
                return listmodel;
            }
        }

        public bool Set_MD_PHY_PARTICLE_INFO_IN()
        {
            string log = "";
            bool listmodel = false;
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    iDataBase.BeginTran();
                    string sql = "";
                    sql = "update MD_PHY_PARTICLE_INFO_IN set FLAG_GRIT =0";
                    iDataBase.ExecuteCommand(string.Format(sql));
                    iDataBase.CommitTran();
                    log = string.Format("更新标志为完成！");
                    Outputlog(log);
                    listmodel = true;
                    return listmodel;
                }
            }
            catch
            {
                return listmodel;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }


    }
    public class MC_SINCAL_result_1min
    {
        /// <summary>
        /// 混合料碱度(%)
        /// </summary>
        public double SINCAL_SIN_PV_R { get; set; }
        /// <summary>
        /// 混合料MgO(%)
        /// </summary>
        public double SINCAL_SIN_PV_MGO { get; set; }
        /// <summary>
        /// 混合料含碳(%)
        /// </summary>
        public double SINCAL_MIX_PV_C { get; set; }


    }


}
