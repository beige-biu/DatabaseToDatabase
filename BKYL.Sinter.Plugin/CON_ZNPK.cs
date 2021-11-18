
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
    public class CON_ZNPK : BaseJob
    {
        public override void InitTask()
        {
            //加载配置 MC_POPCAL_PAR
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            List<ORE_TRACK_BIGDATA> ORE_TRACK_BIGDATA_ = null;
            try
            {
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    int M_hour = 0;
                    int M_min = 0;
                    // 数据库查询
                    //if (M_hour == 8 && M_min == 5)
                    //{
                    //    ORE_TRACK_BIGDATA_ = GET_ORE_TRACK_BIGDATA(iDataBase);
                    //}
                    //else if (M_hour == 20 && M_min == 5)
                    //{
                    DateTime maxDate = iDataBase.Queryable<ORE_TRACK_BIGDATA>().Max(m => m.BEGIN_STORE_DATE).ToDate();//获取最大时间    
                    ORE_TRACK_BIGDATA_ = GET_ORE_TRACK_BIGDATA(maxDate,iDataBase);

                    //}
                }
                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {

                    DateTime end_time;
                    // 数据库查询
                    if (ORE_TRACK_BIGDATA_.Count > 0)
                    {
                        for (int i = 0; i < ORE_TRACK_BIGDATA_.Count; i++)
                        {
                            int a = GET_ORE_TRACK_BIGDATA_NUMBER(ORE_TRACK_BIGDATA_[i].BEGIN_STORE_DATE.ToDate(), iDataBase);
                            if (ORE_TRACK_BIGDATA_[i].END_TIME==null)
                            {
                                 end_time = DateTime.Now;

                            }else
                            {
                                end_time = ORE_TRACK_BIGDATA_[i].END_TIME.ToDate();
                            }
                            double M_SJKSUM = GET_T_BURN_COLE_PLC_1MIN(ORE_TRACK_BIGDATA_[i].START_TIME.ToDate(), end_time, iDataBase);//ORE_TRACK_BIGDATA_[i].END_TIME.ToDate()
                            double M_SJKRJ = GET_MC_POPCAL_OUT_CURVE(ORE_TRACK_BIGDATA_[i].START_TIME.ToDate(), end_time, iDataBase);//ORE_TRACK_BIGDATA_[i].END_TIME.ToDate()
                            if (a == 1)
                            {
                                Set_update(ORE_TRACK_BIGDATA_, M_SJKSUM, M_SJKRJ,i);
                            }
                            else if (a == 0)
                            {
                                ORE_TRACK_BIGDATA ORE_TRACK_BIGDATA_BD_ = null;
                                ORE_TRACK_BIGDATA_BD_ = new ORE_TRACK_BIGDATA();
                                ORE_TRACK_BIGDATA_BD_.ORDER_NO = ORE_TRACK_BIGDATA_[i].ORDER_NO;
                                ORE_TRACK_BIGDATA_BD_.BEGIN_STORE_DATE = ORE_TRACK_BIGDATA_[i].BEGIN_STORE_DATE;
                                ORE_TRACK_BIGDATA_BD_.START_TIME = ORE_TRACK_BIGDATA_[i].START_TIME;
                                ORE_TRACK_BIGDATA_BD_.END_TIME = ORE_TRACK_BIGDATA_[i].END_TIME.ToDate();
                                ORE_TRACK_BIGDATA_BD_.TFE = ORE_TRACK_BIGDATA_[i].TFE;
                                ORE_TRACK_BIGDATA_BD_.FEO = ORE_TRACK_BIGDATA_[i].FEO;
                                ORE_TRACK_BIGDATA_BD_.R = ORE_TRACK_BIGDATA_[i].R;
                                ORE_TRACK_BIGDATA_BD_.SIO2 = ORE_TRACK_BIGDATA_[i].SIO2;
                                ORE_TRACK_BIGDATA_BD_.AL2O3 = ORE_TRACK_BIGDATA_[i].AL2O3;
                                ORE_TRACK_BIGDATA_BD_.CAO = ORE_TRACK_BIGDATA_[i].CAO;
                                ORE_TRACK_BIGDATA_BD_.MGO = ORE_TRACK_BIGDATA_[i].MGO;
                                ORE_TRACK_BIGDATA_BD_.K2O = ORE_TRACK_BIGDATA_[i].K2O;
                                ORE_TRACK_BIGDATA_BD_.NA2O = ORE_TRACK_BIGDATA_[i].NA2O;
                                ORE_TRACK_BIGDATA_BD_.ZN = ORE_TRACK_BIGDATA_[i].ZN;
                                ORE_TRACK_BIGDATA_BD_.P = ORE_TRACK_BIGDATA_[i].P;
                                ORE_TRACK_BIGDATA_BD_.S = ORE_TRACK_BIGDATA_[i].S;
                                ORE_TRACK_BIGDATA_BD_.TI = ORE_TRACK_BIGDATA_[i].TI;
                                ORE_TRACK_BIGDATA_BD_.MNO = ORE_TRACK_BIGDATA_[i].MNO;
                                ORE_TRACK_BIGDATA_BD_.T_AS = ORE_TRACK_BIGDATA_[i].T_AS;
                                ORE_TRACK_BIGDATA_BD_.CR = ORE_TRACK_BIGDATA_[i].CR;
                                ORE_TRACK_BIGDATA_BD_.DRUM_INDEX = ORE_TRACK_BIGDATA_[i].DRUM_INDEX;
                                ORE_TRACK_BIGDATA_BD_.RI = ORE_TRACK_BIGDATA_[i].RI;
                                ORE_TRACK_BIGDATA_BD_.T_40MM = ORE_TRACK_BIGDATA_[i].T_40MM;
                                ORE_TRACK_BIGDATA_BD_.T_4025MM = ORE_TRACK_BIGDATA_[i].T_4025MM;
                                ORE_TRACK_BIGDATA_BD_.S25_16 = ORE_TRACK_BIGDATA_[i].S25_16;
                                ORE_TRACK_BIGDATA_BD_.T_1610MM = ORE_TRACK_BIGDATA_[i].T_1610MM;
                                ORE_TRACK_BIGDATA_BD_.T_105MM = ORE_TRACK_BIGDATA_[i].T_105MM;
                                ORE_TRACK_BIGDATA_BD_.T_5MM = ORE_TRACK_BIGDATA_[i].T_5MM;
                                ORE_TRACK_BIGDATA_BD_.LOW_TEMP_STRENGTH = ORE_TRACK_BIGDATA_[i].LOW_TEMP_STRENGTH;
                                ORE_TRACK_BIGDATA_BD_.LOW_TEMPERATURE_REDUCTION = ORE_TRACK_BIGDATA_[i].LOW_TEMPERATURE_REDUCTION;
                                ORE_TRACK_BIGDATA_BD_.LOW_TEMPE_ANTIWEAR = ORE_TRACK_BIGDATA_[i].LOW_TEMPE_ANTIWEAR;
                                ORE_TRACK_BIGDATA_BD_.SOFTENING_START = ORE_TRACK_BIGDATA_[i].SOFTENING_START;
                                ORE_TRACK_BIGDATA_BD_.SOFTENING_END = ORE_TRACK_BIGDATA_[i].SOFTENING_END;
                                ORE_TRACK_BIGDATA_BD_.SOFTENING_INTERVAL = ORE_TRACK_BIGDATA_[i].SOFTENING_INTERVAL;
                                ORE_TRACK_BIGDATA_BD_.MELTING_START = ORE_TRACK_BIGDATA_[i].MELTING_START;
                                ORE_TRACK_BIGDATA_BD_.MELTING_END = ORE_TRACK_BIGDATA_[i].MELTING_END;
                                ORE_TRACK_BIGDATA_BD_.MELTING_INTERVAL = ORE_TRACK_BIGDATA_[i].MELTING_INTERVAL;
                                ORE_TRACK_BIGDATA_BD_.PRESSURE_DIFF = ORE_TRACK_BIGDATA_[i].PRESSURE_DIFF;
                                ORE_TRACK_BIGDATA_BD_.SOFT_LAYER = ORE_TRACK_BIGDATA_[i].SOFT_LAYER;
                                ORE_TRACK_BIGDATA_BD_.COMPRESSIVE = ORE_TRACK_BIGDATA_[i].COMPRESSIVE;
                                ORE_TRACK_BIGDATA_BD_.DROPLET = ORE_TRACK_BIGDATA_[i].DROPLET;
                                ORE_TRACK_BIGDATA_BD_.SINTER_YIELD = M_SJKSUM;
                                ORE_TRACK_BIGDATA_BD_.DAY_YIELD_AVG = M_SJKRJ;
                                ORE_TRACK_BIGDATA_BD_.BC_SIN_MS_PV = ORE_TRACK_BIGDATA_[i].BC_SIN_MS_PV;
                                ORE_TRACK_BIGDATA_BD_.UTILIZATION_FACTOR = ORE_TRACK_BIGDATA_[i].UTILIZATION_FACTOR;
                                ORE_TRACK_BIGDATA_BD_.OPERATING_RATE = ORE_TRACK_BIGDATA_[i].OPERATING_RATE;
                                ORE_TRACK_BIGDATA_BD_.STEAM_YIELD = ORE_TRACK_BIGDATA_[i].STEAM_YIELD;
                                ORE_TRACK_BIGDATA_BD_.SECONDARY_MIXED_SIZE = ORE_TRACK_BIGDATA_[i].SECONDARY_MIXED_SIZE;
                                ORE_TRACK_BIGDATA_BD_.SECONDARY_MIX_H2O = ORE_TRACK_BIGDATA_[i].SECONDARY_MIX_H2O;
                                ORE_TRACK_BIGDATA_BD_.COKE_SIZE = ORE_TRACK_BIGDATA_[i].COKE_SIZE;
                                ORE_TRACK_BIGDATA_BD_.WATER = ORE_TRACK_BIGDATA_[i].WATER;
                                ORE_TRACK_BIGDATA_BD_.LAYER_THICKNESS = ORE_TRACK_BIGDATA_[i].LAYER_THICKNESS;
                                ORE_TRACK_BIGDATA_BD_.NEGATIVE = ORE_TRACK_BIGDATA_[i].NEGATIVE;
                                ORE_TRACK_BIGDATA_BD_.PS_MA_FAN_PV_FRE = ORE_TRACK_BIGDATA_[i].PS_MA_FAN_PV_FRE;
                                ORE_TRACK_BIGDATA_BD_.RETURN_RATIO = ORE_TRACK_BIGDATA_[i].RETURN_RATIO;
                                ORE_TRACK_BIGDATA_BD_.MQ_DH = ORE_TRACK_BIGDATA_[i].MQ_DH;
                                ORE_TRACK_BIGDATA_BD_.DN_DH = ORE_TRACK_BIGDATA_[i].DN_DH;
                                ORE_TRACK_BIGDATA_BD_.GT_RH = ORE_TRACK_BIGDATA_[i].GT_RH;
                                ORE_TRACK_BIGDATA_BD_.GT_RH_G = ORE_TRACK_BIGDATA_[i].GT_RH_G;
                                ORE_TRACK_BIGDATA_BD_.SSH_DH = ORE_TRACK_BIGDATA_[i].SSH_DH;
                                ORE_TRACK_BIGDATA_BD_.DH_WD = ORE_TRACK_BIGDATA_[i].DH_WD;
                                ORE_TRACK_BIGDATA_BD_.DYD_WD = ORE_TRACK_BIGDATA_[i].DYD_WD;
                                ORE_TRACK_BIGDATA_BD_.ZD_WD = ORE_TRACK_BIGDATA_[i].ZD_WD;
                                ORE_TRACK_BIGDATA_BD_.TL_R_NO = ORE_TRACK_BIGDATA_[i].TL_R_NO;
                                ORE_TRACK_BIGDATA_BD_.TL_R_SO2 = ORE_TRACK_BIGDATA_[i].TL_R_SO2;
                                ORE_TRACK_BIGDATA_BD_.TL_R_FC = ORE_TRACK_BIGDATA_[i].TL_R_FC;
                                ORE_TRACK_BIGDATA_BD_.TL_R_O2 = ORE_TRACK_BIGDATA_[i].TL_R_O2;
                                ORE_TRACK_BIGDATA_BD_.TL_R_SD = ORE_TRACK_BIGDATA_[i].TL_R_SD;
                                ORE_TRACK_BIGDATA_BD_.TL_R_LL = ORE_TRACK_BIGDATA_[i].TL_R_LL;
                                ORE_TRACK_BIGDATA_BD_.TL_R_TEMP = ORE_TRACK_BIGDATA_[i].TL_R_TEMP;
                                ORE_TRACK_BIGDATA_BD_.TL_C_NO = ORE_TRACK_BIGDATA_[i].TL_C_NO;
                                ORE_TRACK_BIGDATA_BD_.TL_C_SO2 = ORE_TRACK_BIGDATA_[i].TL_C_SO2;
                                ORE_TRACK_BIGDATA_BD_.TL_C_FC = ORE_TRACK_BIGDATA_[i].TL_C_FC;
                                ORE_TRACK_BIGDATA_BD_.TL_C_O2 = ORE_TRACK_BIGDATA_[i].TL_C_O2;
                                ORE_TRACK_BIGDATA_BD_.TL_C_SD = ORE_TRACK_BIGDATA_[i].TL_C_SD;
                                ORE_TRACK_BIGDATA_BD_.TL_C_LL = ORE_TRACK_BIGDATA_[i].TL_C_LL;
                                ORE_TRACK_BIGDATA_BD_.TL_C_TEMP = ORE_TRACK_BIGDATA_[i].TL_C_TEMP;
                                ORE_TRACK_BIGDATA_BD_.TL_C_NO_Z = ORE_TRACK_BIGDATA_[i].TL_C_NO_Z;
                                ORE_TRACK_BIGDATA_BD_.TL_C_SO2_Z = ORE_TRACK_BIGDATA_[i].TL_C_SO2_Z;
                                ORE_TRACK_BIGDATA_BD_.TL_C_FC_Z = ORE_TRACK_BIGDATA_[i].TL_C_FC_Z;
                                ORE_TRACK_BIGDATA_BD_.TL_T_NO = ORE_TRACK_BIGDATA_[i].TL_T_NO;
                                ORE_TRACK_BIGDATA_BD_.TL_T_SO2 = ORE_TRACK_BIGDATA_[i].TL_T_SO2;
                                ORE_TRACK_BIGDATA_BD_.TL_T_FC = ORE_TRACK_BIGDATA_[i].TL_T_FC;
                                ORE_TRACK_BIGDATA_BD_.TL_R_YL = ORE_TRACK_BIGDATA_[i].TL_R_YL;
                                ORE_TRACK_BIGDATA_BD_.TL_C_YL = ORE_TRACK_BIGDATA_[i].TL_C_YL;
                                if (Set_Admin_add(ORE_TRACK_BIGDATA_BD_))
                                {
                                    log = string.Format("保存料单追踪数据完成！");
                                    Outputlog(log);
                                }
                                else
                                {
                                    log = string.Format("保存料单追踪数据失败！");
                                    Outputlog(log);
                                }
                            }
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

        public bool Set_Admin_add(ORE_TRACK_BIGDATA model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
        public double GET_T_BURN_COLE_PLC_1MIN(DateTime  START_TIME,DateTime END_TIME, IDataBase iDataBase)
        {
            string sql = "";
            sql = "select nvl(sum(SPARE32),0) from  T_BURN_COLE_PLC_1MIN_HIST  where TIMESTAMP >  to_date('" + START_TIME + "','yyyy-mm-dd hh24:mi:ss')  and  TIMESTAMP <= to_date('" + END_TIME + "','yyyy-mm-dd hh24:mi:ss')";//TO_DATE("'+ORE_TRACK_BIGDATA_+'",'yyyy -mm-dd hh24:mi:ss'
            return iDataBase.GetDouble(sql);
        }

        public double GET_MC_POPCAL_OUT_CURVE(DateTime START_TIME,DateTime END_TIME, IDataBase iDataBase)
        {
            string sql = "";
            sql = "select round(nvl(avg(I_AL_OUTPUT),0),3) from  MC_POPCAL_OUT_CURVE  where I_AL_OUTPUT>0 and (TIMESTAMP >  to_date('" + START_TIME + "','yyyy-mm-dd hh24:mi:ss')  and  TIMESTAMP <= to_date('" + END_TIME + "','yyyy-mm-dd hh24:mi:ss'))";//TO_DATE("'+ORE_TRACK_BIGDATA_+'",'yyyy -mm-dd hh24:mi:ss'
            return iDataBase.GetDouble(sql);
        }
        public  int GET_ORE_TRACK_BIGDATA_NUMBER(DateTime BEGIN_STORE_DATE, IDataBase iDataBase)
        {
            string sql = "";
            sql = "select count(*) from ORE_TRACK_BIGDATA  where BEGIN_STORE_DATE= TO_DATE('" + BEGIN_STORE_DATE + "','yyyy-mm-dd hh24:mi:ss')";//TO_DATE("'+ORE_TRACK_BIGDATA_+'",'yyyy -mm-dd hh24:mi:ss'
            return iDataBase.GetInt(sql);
        }
        public bool Set_update(List<ORE_TRACK_BIGDATA> model,double M_SJKSUM,double M_SJKRJ,int i)
        {
            string log = "";
            bool listmodel = false;
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    iDataBase.BeginTran();
                    string sql = "";
                    sql = string.Format(@"update ORE_TRACK_BIGDATA set START_TIME = TO_DATE('" + model[i].START_TIME + "','yyyy-mm-dd hh24:mi:ss')"
                    + ",END_TIME=TO_DATE('" + model[i].END_TIME + "','yyyy-mm-dd hh24:mi:ss')"
                    + ",TFE= '" + model[i].TFE + "'"
                    + ",FEO= '" + model[i].FEO + "'"
                    + ",R= '" + model[i].R + "'"
                    + ",SIO2= '" + model[i].SIO2 + "'"
                    + ",AL2O3= '" + model[i].AL2O3 + "'"
                    + ",CAO= '" + model[i].CAO + "'"
                    + ",MGO= '" + model[i].MGO + "'"
                    + ",K2O= '" + model[i].K2O + "'"
                    + ",NA2O= '" + model[i].NA2O + "'"
                    + ",ZN= '" + model[i].ZN + "'"
                    + ",P= '" + model[i].P + "'"
                    + ",S= '" + model[i].S + "'"
                    + ",TI= '" + model[i].TI + "'"
                    + ",MNO= '" + model[i].MNO + "'"
                    + ",T_AS= '" + model[i].T_AS + "'"
                    + ",CR= '" + model[i].CR + "'"
                    + ",DRUM_INDEX= '" + model[i].DRUM_INDEX + "'"
                    + ",RI= '" + model[i].RI + "'"
                    + ",T_40MM= '" + model[i].T_40MM + "'"
                    + ",T_4025MM= '" + model[i].T_4025MM + "'"
                    + ",S25_16= '" + model[i].S25_16 + "'"
                    + ",T_1610MM= '" + model[i].T_1610MM + "'"
                    + ",T_105MM= '" + model[i].T_105MM + "'"
                    + ",T_5MM= '" + model[i].T_5MM + "'"
                    + ",LOW_TEMP_STRENGTH= '" + model[i].LOW_TEMP_STRENGTH + "'"
                    + ",LOW_TEMPERATURE_REDUCTION= '" + model[i].LOW_TEMPERATURE_REDUCTION + "'"
                    + ",LOW_TEMPE_ANTIWEAR= '" + model[i].LOW_TEMPE_ANTIWEAR + "'"
                    + ",SOFTENING_START= '" + model[i].SOFTENING_START + "'"
                    + ",SOFTENING_END= '" + model[i].SOFTENING_END + "'"
                    + ",SOFTENING_INTERVAL= '" + model[i].SOFTENING_INTERVAL + "'"
                    + ",MELTING_START= '" + model[i].MELTING_START + "'"
                    + ",MELTING_END= '" + model[i].MELTING_END + "'"
                    + ",MELTING_INTERVAL= '" + model[i].MELTING_INTERVAL + "'"
                    + ",PRESSURE_DIFF= '" + model[i].PRESSURE_DIFF + "'"
                    + ",SOFT_LAYER= '" + model[i].SOFT_LAYER + "'"
                    + ",COMPRESSIVE= '" + model[i].COMPRESSIVE + "'"
                    + ",DROPLET= '" + model[i].DROPLET + "'"
                    + ",SINTER_YIELD= '" + M_SJKSUM + "'"
                    + ",DAY_YIELD_AVG= '" + M_SJKRJ + "'"
                    + ",BC_SIN_MS_PV= '" + model[i].BC_SIN_MS_PV + "'"
                    + ",UTILIZATION_FACTOR= '" + model[i].UTILIZATION_FACTOR + "'"
                    + ",OPERATING_RATE= '" + model[i].OPERATING_RATE + "'"
                    + ",STEAM_YIELD= '" + model[i].STEAM_YIELD + "'"
                    + ",SECONDARY_MIXED_SIZE= '" + model[i].SECONDARY_MIXED_SIZE + "'"
                    + ",SECONDARY_MIX_H2O= '" + model[i].SECONDARY_MIX_H2O + "'"
                    + ",COKE_SIZE= '" + model[i].COKE_SIZE + "'"
                    + ",WATER= '" + model[i].WATER + "'"
                    + ",LAYER_THICKNESS= '" + model[i].LAYER_THICKNESS + "'"
                    + ",NEGATIVE= '" + model[i].NEGATIVE + "'"
                    + ",PS_MA_FAN_PV_FRE= '" + model[i].PS_MA_FAN_PV_FRE + "'"
                    + ",RETURN_RATIO= '" + model[i].RETURN_RATIO + "'"
                    + ",MQ_DH= '" + model[i].MQ_DH + "'"
                    + ",DN_DH= '" + model[i].DN_DH + "'"
                    + ",GT_RH_G= '" + model[i].GT_RH_G + "'"
                    + ",SSH_DH= '" + model[i].SSH_DH + "'"
                    + ",DH_WD= '" + model[i].DH_WD + "'"
                    + ",DYD_WD= '" + model[i].DYD_WD + "'"
                    + ",ZD_WD= '" + model[i].ZD_WD + "'"
                    + ",TL_R_NO= '" + model[i].TL_R_NO + "'"
                    + ",TL_R_SO2= '" + model[i].TL_R_SO2 + "'"
                    + ",TL_R_FC= '" + model[i].TL_R_FC + "'"
                    + ",TL_R_O2= '" + model[i].TL_R_O2 + "'"
                    + ",TL_R_SD= '" + model[i].TL_R_SD + "'"
                    + ",TL_R_LL= '" + model[i].TL_R_LL + "'"
                    + ",TL_R_TEMP= '" + model[i].TL_R_TEMP + "'"
                    + ",TL_C_NO= '" + model[i].TL_C_NO + "'"
                    + ",TL_C_SO2= '" + model[i].TL_C_SO2 + "'"
                    + ",TL_C_FC= '" + model[i].TL_C_FC + "'"
                    + ",TL_C_O2= '" + model[i].TL_C_O2 + "'"
                    + ",TL_C_SD= '" + model[i].TL_C_SD + "'"
                    + ",TL_C_LL= '" + model[i].TL_C_LL + "'"
                    + ",TL_C_TEMP= '" + model[i].TL_C_TEMP + "'"
                    + ",TL_C_NO_Z= '" + model[i].TL_C_NO_Z + "'"
                    + ",TL_C_SO2_Z= '" + model[i].TL_C_SO2_Z + "'"
                    + ",TL_C_FC_Z= '" + model[i].TL_C_FC_Z + "'"
                    + ",TL_T_NO= '" + model[i].TL_T_NO + "'"
                    + ",TL_T_SO2= '" + model[i].TL_T_SO2 + "'"
                    + ",TL_T_FC= '" + model[i].TL_T_FC + "'"
                    + ",TL_R_YL= '" + model[i].TL_R_YL + "'"
                    + ",TL_C_YL= '" + model[i].TL_C_YL + "'"
                    + "where ORDER_NO = '" + model[i].ORDER_NO + "'");
                    iDataBase.ExecuteCommand(string.Format(sql));
                    iDataBase.CommitTran();
                    log = string.Format("更新料单追踪数据完成！");
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

        public List< ORE_TRACK_BIGDATA> GET_ORE_TRACK_BIGDATA( DateTime maxDate, IDataBase iDataBase)
        {
            string sql = string.Format("select * from(select *  from ORE_TRACK_BIGDATA  where BEGIN_STORE_DATE <= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')order by BEGIN_STORE_DATE desc) where rownum <= 10", maxDate);
           return iDataBase.GetList<ORE_TRACK_BIGDATA>(sql);
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
