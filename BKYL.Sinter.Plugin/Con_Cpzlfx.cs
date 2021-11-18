
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BFES.DataAccess;

using System.Data;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;

namespace BKYL.Sinter.Plugin
{
    /// <summary>
    /// 成品质量分析新
    /// </summary>
    public class Con_Cpzlfx : BaseJob
    {
        string log = "";

        public override void InitTask()
        {
            //加载配置 MC_POPCAL_PAR
        }

        public override void RunTask(DateTime currentTime)
        {
          
            try
            {
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    // 数据库查询

                }

                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    // 数据库查询
                    int Sign_=Sign(iDataBase);
                    int M_hour = 0;
                    M_hour = currentTime.Hour;
                    int M_Day = 0;
                    M_Day = currentTime.Day;
                    DateTime endTime = DateTime.MinValue;
                    DateTime startTime = DateTime.MinValue;
                    DateTime endTime_mon = DateTime.MinValue;
                    DateTime startTime_mon = DateTime.MinValue;
                    M_FPQCAL_RESULT FPQCAL_RESULT = null;
                    FPQCAL_RESULT = new M_FPQCAL_RESULT();
                    if (Sign_ == 0)
                    { return; }
                    if (Sign_==1)
                    {
                        //小于25号前的数据
                        if (M_Day <= 25)
                        {
                            endTime_mon = currentTime;
                            startTime_mon = currentTime.AddMonths(-1).ToString("yyyy-MM-25 20:01:00").ToDate();
                        }
                        //大于25号前的数据
                        if (M_Day > 25)
                        {
                            startTime_mon = currentTime.ToString("yyyy-MM-25 20:01:00").ToDate();
                            endTime_mon = currentTime.AddMonths(+1).ToString("yyyy-MM-25 20:00:00").ToDate();
                        }
                        //晚上20点之前的数据
                        if (M_hour <= 20)
                        {
                            endTime = currentTime;
                            startTime = currentTime.AddDays(-1).ToString("yyyy-MM-dd 20:01:00").ToDate();
                        }
                        //晚上20点到0点之间的数据
                        if (M_hour > 20 && M_hour < 0)
                        {
                            startTime =currentTime;
                            endTime = currentTime.AddDays(+1).ToString("yyyy-MM-dd 20:01:00").ToDate();
                        }

                      M_FPQCAL_PAR M_FPQCAL_PAR_MOD = M_FPQCAL_PAR_(iDataBase);
                      List<M_SINTER_ANA> M_SINTER_ANA_list = M_SINTER_ANA_(iDataBase, startTime, endTime);
                      //成品重量累计值
                      double M_SAP32 = GET_T_BURN_COLE_PLC_1MIN(iDataBase, startTime, endTime);
                      double M_M_TOTAL_SP_W = GET_T_MATCH_PLC_1MIN(iDataBase, startTime, endTime);
                      DataTable GetSINTER_ANA_ = GetSINTER_ANA(iDataBase,startTime, endTime);

                      DataTable GetSINTER_ANA__mon = GetSINTER_ANA(iDataBase, startTime_mon, endTime_mon);
                        //计算各种数据的月和
                      M_FPQCAL_RESULT M_FPQCAL_RESULT_SUM = GET_M_FPQCAL_RESULT_SUM(iDataBase, startTime_mon, endTime_mon);
                      double M_SAP31_MON = GET_T_BURN_COLE_PLC_1MIN(iDataBase, startTime_mon, endTime_mon);
                      double M_M_TOTAL_SP_W_MON = GET_T_MATCH_PLC_1MIN(iDataBase, startTime_mon, endTime_mon);
                      List<M_SINTER_ANA> M_SINTER_ANA_list_mon = M_SINTER_ANA_(iDataBase, startTime_mon, endTime_mon);
                      SET_M_FPQCAL_RESULT(GetSINTER_ANA__mon,M_SAP31_MON, M_M_TOTAL_SP_W_MON,M_FPQCAL_PAR_MOD, M_SINTER_ANA_list,M_SAP32, M_M_TOTAL_SP_W, GetSINTER_ANA_,M_FPQCAL_RESULT_SUM, M_SINTER_ANA_list_mon, ref FPQCAL_RESULT);
                        int jilu = get_M_FPQCAL_RESULT_int(DateTime.Now.ToString("yyyy-MM-dd 00:00:00").ToDate(), iDataBase);
                        //SET_delete(DateTime.Now.ToString("yyyy-MM-dd 00:00:00").ToDate());
                        UpdateM_SINTER_ANA();
                        if (jilu == 1)
                        {
                            Set_update(FPQCAL_RESULT);
                        }
                        else if (jilu == 0)
                        {
                            Set_Admin_add(FPQCAL_RESULT);
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
        public void SET_M_FPQCAL_RESULT(DataTable GetSINTER_ANA__mon, double M_SAP31_MON,double M_M_TOTAL_SP_W_MON,  M_FPQCAL_PAR M_FPQCAL_PAR_MOD, List<M_SINTER_ANA> M_SINTER_ANA_list,double M_SAP32,double M_M_TOTAL_SP_W , DataTable GraModel, M_FPQCAL_RESULT M_FPQCAL_RESULT_SUM,List<M_SINTER_ANA> M_SINTER_ANA_list_mon, ref M_FPQCAL_RESULT M_FPQCAL_RESULT_)
        {
            if (M_SINTER_ANA_list == null || M_SINTER_ANA_list.Count <= 0)
            {
                return;
            }
            double M_R = (((M_FPQCAL_PAR_MOD.PAR_DS_RANGE + M_FPQCAL_PAR_MOD.PAR_Q_B_RANGE) + (M_FPQCAL_PAR_MOD.PAR_DS_RANGE - M_FPQCAL_PAR_MOD.PAR_Q_B_RANGE)) / 2).ToDouble();
            double M_FEO = (((M_FPQCAL_PAR_MOD.PAR_FE_CENTER - M_FPQCAL_PAR_MOD.PAR_Q_FE_RANGE) + (M_FPQCAL_PAR_MOD.PAR_FE_CENTER - M_FPQCAL_PAR_MOD.PAR_Q_FE_RANGE)) / 2).ToDouble();
            double M_EQUITY = M_FPQCAL_PAR_MOD.PAR_Q_D_MIN.ToDouble();
            double M_TFE = (((M_FPQCAL_PAR_MOD.PAR_TFE_CENTER - M_FPQCAL_PAR_MOD.PAR_Q_TFE_RANGE) + (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER + M_FPQCAL_PAR_MOD.PAR_Q_TFE_RANGE)) / 2).ToDouble();

          for (int i=0;i<M_SINTER_ANA_list.Count;i++)
            {
               if( M_SINTER_ANA_list[i].C_R==0)
                {
                    M_SINTER_ANA_list[i].C_R = M_R;
                }
                if (M_SINTER_ANA_list[i].C_FEO== 0)
                {
                    M_SINTER_ANA_list[i].C_FEO = M_FEO;
                }
                if (M_SINTER_ANA_list[i].EQUITY == 0)
                {
                    M_SINTER_ANA_list[i].EQUITY = M_EQUITY;
                }

                if (M_SINTER_ANA_list[i].C_TFE == 0)
                {
                    M_SINTER_ANA_list[i].C_TFE = M_TFE;
                }
            }


            M_FPQCAL_RESULT_.TIMESTAMP =DateTime.Now.ToString("yyyy-MM-dd 00:00:00").ToDate();
            //计算日统计数据
            //计算合格率(一天满足合格条件数量)
            M_FPQCAL_RESULT_.FPQ_S_Q_RATE_DAY = M_SINTER_ANA_list.Count(m =>
                m.C_R >= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE - M_FPQCAL_PAR_MOD.PAR_Q_B_RANGE) && m.C_R <= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE + M_FPQCAL_PAR_MOD.PAR_Q_B_RANGE) && m.C_R>0 &&
                m.C_FEO >= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER - M_FPQCAL_PAR_MOD.PAR_Q_FE_RANGE) && m.C_FEO <= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER + M_FPQCAL_PAR_MOD.PAR_Q_FE_RANGE) && m.C_FEO > 0 &&  
                m.EQUITY >= M_FPQCAL_PAR_MOD.PAR_Q_D_MIN && m.EQUITY>0 &&
                m.C_TFE >= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER - M_FPQCAL_PAR_MOD.PAR_Q_TFE_RANGE) && m.C_TFE <= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER + M_FPQCAL_PAR_MOD.PAR_Q_TFE_RANGE) && m.C_TFE>0);

                int hgl = 0;
                foreach (var itemhgl in M_SINTER_ANA_list)
                {
                    bool flag = true;
                    if (itemhgl.C_R != 0)
                    {
                        if (itemhgl.C_R >= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE - M_FPQCAL_PAR_MOD.PAR_Q_B_RANGE) && itemhgl.C_R <= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE + M_FPQCAL_PAR_MOD.PAR_Q_B_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (itemhgl.C_FEO != 0)
                    {
                        if (itemhgl.C_FEO >= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER - M_FPQCAL_PAR_MOD.PAR_Q_FE_RANGE) && itemhgl.C_FEO <= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER + M_FPQCAL_PAR_MOD.PAR_Q_FE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.EQUITY != 0)
                    {
                        if (itemhgl.EQUITY >= M_FPQCAL_PAR_MOD.PAR_Q_D_MIN)
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.C_TFE != 0)
                    {
                        if (itemhgl.C_TFE >= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER - M_FPQCAL_PAR_MOD.PAR_Q_TFE_RANGE) && itemhgl.C_TFE <= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER + M_FPQCAL_PAR_MOD.PAR_Q_TFE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (flag)
                    { hgl++; }
                }
                M_FPQCAL_RESULT_.FPQ_S_Q_RATE_DAY = hgl;

                M_FPQCAL_RESULT_.FPQ_EXAMI_DAY = M_SINTER_ANA_list.Count;

                if (M_FPQCAL_RESULT_.FPQ_EXAMI_DAY > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_Q_RATE_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_Q_RATE_DAY / M_FPQCAL_RESULT_.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                }
                //计算一级品率
                M_FPQCAL_RESULT_.FPQ_S_FQ_RATE_DAY = M_SINTER_ANA_list.Count(m =>
                m.C_R >= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE - M_FPQCAL_PAR_MOD.PAR_FQ_B_RANGE) && m.C_R <= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE + M_FPQCAL_PAR_MOD.PAR_FQ_B_RANGE) && m.C_R>0&&
                m.C_FEO >= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER - M_FPQCAL_PAR_MOD.PAR_FQ_FE_RANGE) && m.C_FEO <= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER + M_FPQCAL_PAR_MOD.PAR_FQ_FE_RANGE) && m.C_R>0 &&
                m.EQUITY >= M_FPQCAL_PAR_MOD.PAR_FQ_D_MIN &&
                m.C_TFE >= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER - M_FPQCAL_PAR_MOD.PAR_FQ_TFE_RANGE) && m.C_TFE <= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER + M_FPQCAL_PAR_MOD.PAR_FQ_TFE_RANGE));

                hgl = 0;
                foreach (var itemhgl in M_SINTER_ANA_list)
                {
                    bool flag = true;
                    if (itemhgl.C_R != 0)
                    {
                        if (itemhgl.C_R >= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE - M_FPQCAL_PAR_MOD.PAR_FQ_B_RANGE) && itemhgl.C_R <= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE + M_FPQCAL_PAR_MOD.PAR_FQ_B_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (itemhgl.C_FEO != 0)
                    {
                        if (itemhgl.C_FEO >= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER - M_FPQCAL_PAR_MOD.PAR_FQ_FE_RANGE) && itemhgl.C_FEO <= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER + M_FPQCAL_PAR_MOD.PAR_FQ_FE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.EQUITY != 0)
                    {
                        if (itemhgl.EQUITY >= M_FPQCAL_PAR_MOD.PAR_FQ_D_MIN)
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.C_TFE != 0)
                    {
                        if (itemhgl.C_TFE >= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER - M_FPQCAL_PAR_MOD.PAR_FQ_TFE_RANGE) && itemhgl.C_TFE <= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER + M_FPQCAL_PAR_MOD.PAR_FQ_TFE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (flag)
                    { hgl++; }
                }
                M_FPQCAL_RESULT_.FPQ_S_FQ_RATE_DAY = hgl;

                if (M_FPQCAL_RESULT_.FPQ_EXAMI_DAY > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_FQ_RATE_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_FQ_RATE_DAY / M_FPQCAL_RESULT_.FPQ_EXAMI_DAY * 100).ToDouble(), 2);
                }

                //计算品位稳定率
                M_FPQCAL_RESULT_.FPQ_S_TF_S_R_DAY = M_SINTER_ANA_list.Count(m => m.C_TFE >= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER - M_FPQCAL_PAR_MOD.PAR_TATFE_RANGE) &&
                m.C_TFE <= (M_FPQCAL_PAR_MOD.PAR_TFE_CENTER + M_FPQCAL_PAR_MOD.PAR_TATFE_RANGE));

                int youxiaoCount = 0;
                youxiaoCount = M_SINTER_ANA_list.Count(m => m.C_TFE > 0);
                if (youxiaoCount > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_TF_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_TF_S_R_DAY / youxiaoCount * 100).ToDouble(), 2);
                }
                //计算碱度稳定率
                M_FPQCAL_RESULT_.FPQ_S_BA_S_R_DAY = M_SINTER_ANA_list.Count(m => m.C_R >= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE - M_FPQCAL_PAR_MOD.PAR_R_S_RANGE) && m.C_R <= (M_FPQCAL_PAR_MOD.PAR_DS_RANGE + M_FPQCAL_PAR_MOD.PAR_R_S_RANGE));

                youxiaoCount = M_SINTER_ANA_list.Count(m => m.C_R > 0);
                if (youxiaoCount > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_BA_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_BA_S_R_DAY / youxiaoCount * 100).ToDouble(), 2);
                }
                //计算FeO稳定率
                M_FPQCAL_RESULT_.FPQ_S_FEO_S_R_DAY = M_SINTER_ANA_list.Count(m => m.C_FEO >= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER - M_FPQCAL_PAR_MOD.PAR_FEO_S_RANGE) &&
                m.C_FEO <= (M_FPQCAL_PAR_MOD.PAR_FE_CENTER + M_FPQCAL_PAR_MOD.PAR_FEO_S_RANGE));

                youxiaoCount = M_SINTER_ANA_list.Count(m => m.C_FEO > 0);
                if (youxiaoCount > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_FEO_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_FEO_S_R_DAY / youxiaoCount * 100).ToDouble(), 2);
                }
                //计算MgO稳定率
                M_FPQCAL_RESULT_.FPQ_S_MG_S_R_DAY = M_SINTER_ANA_list.Count(m => m.C_MGO >= (M_FPQCAL_PAR_MOD.PAR_MGO_CENTER - M_FPQCAL_PAR_MOD.PAR_FEO_S_RANGE) &&
                m.C_MGO <= (M_FPQCAL_PAR_MOD.PAR_MGO_CENTER + M_FPQCAL_PAR_MOD.PAR_FEO_S_RANGE));

                youxiaoCount = M_SINTER_ANA_list.Count(m => m.C_MGO > 0);
                if (youxiaoCount > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_MG_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_MG_S_R_DAY / youxiaoCount * 100).ToDouble(), 2);
                }
                //计算低温还原粉化合格率
                M_FPQCAL_RESULT_.FPQ_S_RDI_S_R_DAY = M_SINTER_ANA_list.Count(m => m.RDI2 >= M_FPQCAL_PAR_MOD.PAR_Q_RDI_MIN);

                youxiaoCount = M_SINTER_ANA_list.Count(m => m.RDI2 > 0);
                if (youxiaoCount > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_RDI_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_RDI_S_R_DAY / youxiaoCount * 100).ToDouble(), 2);
                }

                //计算还原度合格率
                M_FPQCAL_RESULT_.FPQ_S_RI_S_R_DAY = M_SINTER_ANA_list.Count(m => m.RI >= M_FPQCAL_PAR_MOD.PAR_Q_RI_MIN);

                youxiaoCount = M_SINTER_ANA_list.Count(m => m.RI > 0);
                if (youxiaoCount > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_RI_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_RI_S_R_DAY / youxiaoCount * 100).ToDouble(), 2);
                }
                //计算转鼓强度合格率
                M_FPQCAL_RESULT_.FPQ_S_DRUM_S_R_DAY = M_SINTER_ANA_list.Count(m => m.EQUITY >= M_FPQCAL_PAR_MOD.PAR_Q_D_MIN);

                youxiaoCount = M_SINTER_ANA_list.Count(m => m.EQUITY > 0);
                if (youxiaoCount > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_DRUM_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_S_DRUM_S_R_DAY / youxiaoCount * 100).ToDouble(), 2);
                }
               //计算CaO合格率
               M_FPQCAL_RESULT_.FPQ_SPARE2 = M_SINTER_ANA_list.Count(m => m.C_CAO >= (M_FPQCAL_PAR_MOD.PAR_CAO_CENTER-M_FPQCAL_PAR_MOD.PAR_CAO_RANGE) && m.C_CAO <= (M_FPQCAL_PAR_MOD.PAR_CAO_CENTER + M_FPQCAL_PAR_MOD.PAR_CAO_RANGE));
                youxiaoCount = M_SINTER_ANA_list.Count(m => m.C_CAO > 0);
            if (youxiaoCount > 0)
            {
                M_FPQCAL_RESULT_.FPQ_SPARE4 = Math.Round((M_FPQCAL_RESULT_.FPQ_SPARE2 / youxiaoCount * 100).ToDouble(), 2);
            }
            //计算成品率
            M_FPQCAL_RESULT_.FPQ_FP_ACCU_W_DAY = M_SAP32;
                M_FPQCAL_RESULT_.FPQ_TOTAL_ACCU_W_DAY = M_M_TOTAL_SP_W;

                if (M_FPQCAL_RESULT_.FPQ_TOTAL_ACCU_W_DAY > 0)
                {
                    M_FPQCAL_RESULT_.FPQ_FP_S_R_DAY = Math.Round((M_FPQCAL_RESULT_.FPQ_FP_ACCU_W_DAY / M_FPQCAL_RESULT_.FPQ_TOTAL_ACCU_W_DAY * 100).ToDouble(), 2);
                }
                //计算平均粒度
                if (GraModel != null)
                {
                    int i = 0;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_LES_5_DAY = Math.Round(Convert.ToDouble(GraModel.Rows[0][i]),2);
                    }
                    else
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_LES_5_DAY = null;
                    }
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_5_10_DAY = Math.Round(Convert.ToDouble(GraModel.Rows[0][i]),2);
                    }
                    else
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_5_10_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_10_16_DAY = Math.Round(Convert.ToDouble(GraModel.Rows[0][i]),2);
                    }
                    else
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_10_16_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_16_25_DAY = Math.Round(Convert.ToDouble(GraModel.Rows[0][i]),2);
                    }
                    else
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_16_25_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_25_40_DAY = Math.Round(Convert.ToDouble(GraModel.Rows[0][i]),2);
                    }
                    else
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_25_40_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_B_40_DAY = Math.Round(Convert.ToDouble(GraModel.Rows[0][i]),2);
                    }
                    else
                    {
                        M_FPQCAL_RESULT_.FPQ_GRA_B_40_DAY = null;
                    }
                    M_FPQCAL_RESULT_.FPQ_GRA_AVG_DAY = Math.Round((
                        (5 * M_FPQCAL_RESULT_.FPQ_GRA_LES_5_DAY +
                        7.5 * M_FPQCAL_RESULT_.FPQ_GRA_5_10_DAY +
                        13 * M_FPQCAL_RESULT_.FPQ_GRA_10_16_DAY +
                        20.5 * M_FPQCAL_RESULT_.FPQ_GRA_16_25_DAY +
                        32.5 * M_FPQCAL_RESULT_.FPQ_GRA_25_40_DAY +
                        40 * M_FPQCAL_RESULT_.FPQ_GRA_B_40_DAY) / 100).ToDouble(), 2);
                }

            ///计算月统计数据
            //1、   合格率：月平均
            M_FPQCAL_RESULT_.FPQ_S_Q_RATE_MON = M_FPQCAL_RESULT_SUM.FPQ_S_Q_RATE_DAY + M_FPQCAL_RESULT_.FPQ_S_Q_RATE_DAY;
            M_FPQCAL_RESULT_.FPQ_Q_RATE_MON = Math.Round((M_FPQCAL_RESULT_.FPQ_S_Q_RATE_MON/M_SINTER_ANA_list_mon.Count*100).ToDouble(),2);
            //2、  一级品率：月平均
            M_FPQCAL_RESULT_.FPQ_S_FQ_RATE_MON = M_FPQCAL_RESULT_SUM.FPQ_S_FQ_RATE_DAY + M_FPQCAL_RESULT_.FPQ_S_FQ_RATE_DAY;
            M_FPQCAL_RESULT_.FPQ_FQ_RATE_MON = Math.Round((M_FPQCAL_RESULT_.FPQ_S_FQ_RATE_MON.ToDouble() / M_SINTER_ANA_list_mon.Count*100.ToDouble()),2);
            //3、品位稳定率：月平均
            M_FPQCAL_RESULT_.FPQ_S_TF_S_R_MON = M_FPQCAL_RESULT_SUM.FPQ_S_TF_S_R_DAY + M_FPQCAL_RESULT_.FPQ_S_TF_S_R_DAY;
            M_FPQCAL_RESULT_.FPQ_TF_S_R_MON = Math.Round((M_FPQCAL_RESULT_.FPQ_S_TF_S_R_MON.ToDouble() / M_SINTER_ANA_list_mon.Count(m => m.C_TFE > 0) * 100.ToDouble()),2);
            //4、碱度稳定率：月平均
            M_FPQCAL_RESULT_.FPQ_S_BA_S_R_MON = M_FPQCAL_RESULT_SUM.FPQ_S_BA_S_R_DAY + M_FPQCAL_RESULT_.FPQ_S_BA_S_R_DAY;
            M_FPQCAL_RESULT_.FPQ_BA_S_R_MON = Math.Round((M_FPQCAL_RESULT_.FPQ_S_BA_S_R_MON.ToDouble() / M_SINTER_ANA_list_mon.Count(m => m.C_R > 0)* 100.ToDouble()), 2);
            //5、FeO稳定率：月平均
            M_FPQCAL_RESULT_.FPQ_S_FEO_S_R_MON = M_FPQCAL_RESULT_SUM.FPQ_S_FEO_S_R_DAY + M_FPQCAL_RESULT_.FPQ_S_FEO_S_R_DAY;
            M_FPQCAL_RESULT_.FPQ_FEO_S_R_MON = Math.Round((M_FPQCAL_RESULT_.FPQ_S_FEO_S_R_MON.ToDouble() / M_SINTER_ANA_list_mon.Count(m => m.C_FEO > 0)*100),2);
            //6、MgO稳定率：月平均
            M_FPQCAL_RESULT_.FPQ_S_MG_S_R_MON = M_FPQCAL_RESULT_SUM.FPQ_S_MG_S_R_DAY + M_FPQCAL_RESULT_.FPQ_S_MG_S_R_DAY;
            M_FPQCAL_RESULT_.FPQ_MG_S_R_MON = Math.Round((M_FPQCAL_RESULT_.FPQ_S_FEO_S_R_MON.ToDouble() / M_SINTER_ANA_list_mon.Count(m => m.C_MGO > 0))*100,2);
            //7、成品率：月平均
            M_FPQCAL_RESULT_.FPQ_FP_ACCU_W_MON = M_SAP31_MON;
            M_FPQCAL_RESULT_.FPQ_TOTAL_ACCU_W_MON = M_M_TOTAL_SP_W_MON;
            M_FPQCAL_RESULT_.FPQ_FP_S_R_MON =Math.Round((M_FPQCAL_RESULT_.FPQ_FP_ACCU_W_MON.ToDouble()/ M_FPQCAL_RESULT_.FPQ_TOTAL_ACCU_W_MON.ToDouble())*100,2);
            //8、成品率：月平均
            M_FPQCAL_RESULT_.FPQ_SPARE3 = M_FPQCAL_RESULT_SUM.FPQ_SPARE2 + M_FPQCAL_RESULT_.FPQ_SPARE2;
            M_FPQCAL_RESULT_.FPQ_SPARE5 = Math.Round((M_FPQCAL_RESULT_.FPQ_SPARE3.ToDouble() / M_SINTER_ANA_list_mon.Count(m => m.C_CAO > 0))*100,2);

            if (GetSINTER_ANA__mon != null)
            {
                int i = 0;
                if (!Convert.IsDBNull(GetSINTER_ANA__mon.Rows[0][i]))
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_LES_5_MON = Math.Round(Convert.ToDouble(GetSINTER_ANA__mon.Rows[0][i]),2);
                }
                else
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_LES_5_MON = null;
                }
                if (!Convert.IsDBNull(GetSINTER_ANA__mon.Rows[0][i]))
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_5_10_MON = Math.Round(Convert.ToDouble(GetSINTER_ANA__mon.Rows[0][i]),2);
                }
                else
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_5_10_MON = null;
                }
                i++;
                if (!Convert.IsDBNull(GetSINTER_ANA__mon.Rows[0][i]))
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_10_16_MON = Math.Round(Convert.ToDouble(GetSINTER_ANA__mon.Rows[0][i]),2);
                }
                else
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_10_16_MON = null;
                }
                i++;
                if (!Convert.IsDBNull(GetSINTER_ANA__mon.Rows[0][i]))
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_16_25_MON = Math.Round(Convert.ToDouble(GetSINTER_ANA__mon.Rows[0][i]),2);
                }
                else
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_16_25_MON = null;
                }
                i++;
                if (!Convert.IsDBNull(GetSINTER_ANA__mon.Rows[0][i]))
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_25_40_MON = Math.Round(Convert.ToDouble(GetSINTER_ANA__mon.Rows[0][i]),2);
                }
                else
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_25_40_MON = null;
                }
                i++;
                if (!Convert.IsDBNull(GetSINTER_ANA__mon.Rows[0][i]))
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_B_40_MON = Math.Round(Convert.ToDouble(GetSINTER_ANA__mon.Rows[0][i]),2);
                }
                else
                {
                    M_FPQCAL_RESULT_.FPQ_GRA_B_40_MON = null;
                }

                M_FPQCAL_RESULT_.FPQ_GRA_AVG_MON = Math.Round((
                    (5 * M_FPQCAL_RESULT_.FPQ_GRA_LES_5_MON +
                    7.5 * M_FPQCAL_RESULT_.FPQ_GRA_5_10_MON +
                    13 * M_FPQCAL_RESULT_.FPQ_GRA_10_16_MON +
                    20.5 * M_FPQCAL_RESULT_.FPQ_GRA_16_25_MON +
                    32.5 * M_FPQCAL_RESULT_.FPQ_GRA_25_40_MON +
                    40 * M_FPQCAL_RESULT_.FPQ_GRA_B_40_MON) / 100).ToDouble(), 2);
            }
        }
        public int Sign(IDataBase iDataBase)
        {
                return iDataBase.GetInt("select spare10 from M_SINTER_ANA where timestamp = (select max(timestamp) from M_SINTER_ANA)  and  REOPTTIME = (select max(REOPTTIME) from M_SINTER_ANA)");
        }
        public List<M_SINTER_ANA> M_SINTER_ANA_(IDataBase iDataBase, DateTime startTime, DateTime endTime)
        {

              string sql= string.Format("select * from M_SINTER_ANA where REOPTTIME > {0} and  REOPTTIME <={1}",
               DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType));
              return iDataBase.GetList<M_SINTER_ANA>(sql);
        }
        public double GET_T_BURN_COLE_PLC_1MIN(IDataBase iDataBase, DateTime startTime, DateTime endTime)
        {
            string sql = string.Format("select sum(SPARE32) from T_BURN_COLE_PLC_1MIN where TIMESTAMP > {0} and  TIMESTAMP <={1}",
            DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType));
            return iDataBase.GetDouble(sql);
        }
        public double GET_T_MATCH_PLC_1MIN(IDataBase iDataBase, DateTime startTime, DateTime endTime)
        {
            string sql = string.Format("select round(sum(M_TOTAL_SP_W/60),3) from T_MATCH_PLC_1MIN where TIMESTAMP > {0} and  TIMESTAMP <={1}",
            DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType));
            return iDataBase.GetDouble(sql);
        }
        private DataTable GetSINTER_ANA(IDataBase iDataBase ,DateTime startTime, DateTime endTime)
        {
              return iDataBase.GetDataTable(string.Format("select round(avg(GRIT_SIN_1),3) as GRIT_SIN_1," +
                   "round(avg(GRIT_SIN_2),3) as GRIT_SIN_2," +
                   "round(avg(GRIT_SIN_3),3) as GRIT_SIN_3," +
                   "round(avg(GRIT_SIN_4),3) as GRIT_SIN_4," +
                   "round(avg(GRIT_SIN_5),3) as GRIT_SIN_5," +
                   "round(avg(GRIT_SIN_6),3) as GRIT_SIN_6 " +
                   "from M_SINTER_ANA where REOPTTIME >= {0} and  REOPTTIME <{1}",
                   DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType)));
        }
        public M_FPQCAL_PAR M_FPQCAL_PAR_(IDataBase iDataBase)
        {
                string sql = ("select * from M_FPQCAL_PAR where timestamp = (select max(timestamp) from M_FPQCAL_PAR)");
                return iDataBase.GetSingle<M_FPQCAL_PAR>(sql);
        }

        public M_FPQCAL_RESULT GET_M_FPQCAL_RESULT_SUM(IDataBase iDataBase, DateTime startTime, DateTime endTime)
        {
            string sql = string.Format("select sum(FPQ_S_Q_RATE_DAY) as FPQ_S_Q_RATE_DAY,sum(FPQ_S_FQ_RATE_DAY) as FPQ_S_FQ_RATE_DAY,sum(FPQ_S_TF_S_R_DAY) as FPQ_S_TF_S_R_DAY" +
              ",sum(FPQ_S_BA_S_R_DAY) as FPQ_S_BA_S_R_DAY,sum(FPQ_S_FEO_S_R_DAY) as FPQ_S_FEO_S_R_DAY,sum(FPQ_S_MG_S_R_DAY) as FPQ_S_MG_S_R_DAY,sum(FPQ_SPARE2)  as FPQ_SPARE2  from M_FPQCAL_RESULT where TIMESTAMP > {0} and  TIMESTAMP <={1}",
             DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType));
            return iDataBase.GetSingle<M_FPQCAL_RESULT>(sql);
        }

        public bool Set_Admin_add(M_FPQCAL_RESULT model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }

        public bool Set_update(M_FPQCAL_RESULT model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Update(model);
                return listmodel;
            }
        }
        private void UpdateM_SINTER_ANA()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    idatabase.BeginTran();
                    string sql = "";
                    // sql = string.Format("update M_SINTER_ANA set spare10 = 0 where timestamp = (select max(timestamp) from M_SINTER_ANA) and spare10 = 1");
                    sql = string.Format("update M_SINTER_ANA set spare10 = 0 where  spare10 = 1");
                    idatabase.ExecuteCommand(string.Format(sql));
                    idatabase.CommitTran();
                    log = string.Format("表M_SINTER_ANA的spare10标志重置成功！");
                    Outputlog(log);
                }

                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {

                }
            }
        }

       public int  get_M_FPQCAL_RESULT_int(DateTime time, IDataBase idatabase)
        {
            string  sql=  string.Format("select count(*) from M_FPQCAL_RESULT where timestamp = {0}", DataBaseFactory.ConvertTimeString(time.ToString("yyyy-MM-dd 00:00:00"), T_DBSource.DBType));
            return idatabase.GetInt(sql);
        }


        private void SET_delete(DateTime startTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    int delete = 0;
                    idatabase.BeginTran();
                    //string sql = string.Format("delete from M_FPQCAL_RESULT where timestamp >={0}", DataBaseFactory.ConvertTimeString(startTime.AddDays(-DateChange), S_DBSource.DBType));
                    delete = idatabase.ExecuteCommand(string.Format("delete from M_FPQCAL_RESULT where timestamp ={0}", DataBaseFactory.ConvertTimeString(startTime.ToString("yyyy-MM-dd 00:00:00"), T_DBSource.DBType)));
                    log = string.Format("M_FPQCAL_RESULT 成功删除{0}条记录！", delete);
                    delete = idatabase.ExecuteCommand(string.Format("delete from M_FPQCAL_RESULT_HIST where timestamp ={0}", DataBaseFactory.ConvertTimeString(startTime.ToString("yyyy-MM-dd 00:00:00"), T_DBSource.DBType)));
                    log = string.Format("M_FPQCAL_RESULT_HIST 成功删除{0}条记录！", delete);
                    Outputlog(log);
                    idatabase.CommitTran();

                    Outputlog(log);
                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
            }
            }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
