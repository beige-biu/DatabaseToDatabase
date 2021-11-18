using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BFES.DataAccess;
using System.Data;
using BKYL.Sinter.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;

namespace BKYL.Sinter.Plugin
{
    public class Burntoreturn_Min : BaseJob
    {
       
        public override void InitTask()
        {
            //加载配置yanhao
        }
        public override void RunTask(DateTime currentTime)
        {
            string log = "";                                    
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
               
                try
                {
                    MC_SRM_RESULT MC_SRM_RESULT_ = GET_MC_SRM_RESULT(iDataBase);//结果表
                    MC_SRM_PAR MC_SRM_PAR_ = GET_MC_SRM_PAR(iDataBase);//配置表
                    DateTime maxDateplc = iDataBase.Queryable<T_MATCH_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间plc
                    T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_ = GET_T_MATCH_PLC_1MIN(iDataBase, maxDateplc);
                    MC_SINCAL_TRIG_1MIN MC_SINCAL_TRIG_1MIN_ = GET_MC_SINCAL_TRIG_1MIN(iDataBase);//SRMCAL_FUEL_BILL_SP
                    T_BURN_COLE_PLC_1MIN T_BURN_COLE_PLC_1MIN_ = GET_T_BURN_COLE_PLC_1MIN(iDataBase);//BC_SIN_MS_SP
                    CONFIG_SINCAL_VALUE CONFIG_SINCAL_VALUE_ = GET_CONFIG_SINCAL_VALUE(iDataBase);
                    MC_SINCAL_INTERFACE_TRIG_MIN MC_SINCAL_INTERFACE_TRIG_MIN_ = GET_MC_SINCAL_INTERFACE_TRIG_MIN(iDataBase);//SRMCAL_BILL_SP
                    M_MATERIAL_ANA M_MATERIAL_ANA_ = GET_M_MATERIAL_ANA(iDataBase);//获取仓号
                    T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_X = GET_T_MATCH_PLC_1MIN(iDataBase, M_MATERIAL_ANA_);//判断下料口是否启用ISQY
                    T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_M8 = GET_T_MATCH_PLC_1MIN_M8(iDataBase, M_MATERIAL_ANA_);//M_W_8
                    T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_W_LAST = GET_T_MATCH_PLC_1MIN_LAST(iDataBase,MC_SRM_PAR_);//W_LAST
                    double M_FPQCAL_PAR_ = M_FPQCAL_PAR(iDataBase);
                    double M_SINTER_ANA_ = M_SINTER_ANA(iDataBase);
                    MC_SINCAL_RESULT_1MIN MC_SINCAL_result_1min_ = MC_SINCAL_result_1min(iDataBase);

                    SET_SAVE(MC_SRM_RESULT_,T_MATCH_PLC_1MIN_X, T_MATCH_PLC_1MIN_M8, MC_SINCAL_INTERFACE_TRIG_MIN_,MC_SRM_PAR_,T_MATCH_PLC_1MIN_W_LAST,T_MATCH_PLC_1MIN_,MC_SINCAL_TRIG_1MIN_, T_BURN_COLE_PLC_1MIN_, CONFIG_SINCAL_VALUE_);
                    SET_SAVE1(M_FPQCAL_PAR_, M_SINTER_ANA_, MC_SINCAL_result_1min_);

                    Outputlog(log);
                }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.Message + ex.StackTrace);
                    throw;
                }
                finally
                {
                    log = null;
                }
            }
        }
        public void SET_SAVE(MC_SRM_RESULT MC_SRM_RESULT_,T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_X, T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_M8, MC_SINCAL_INTERFACE_TRIG_MIN MC_SINCAL_INTERFACE_TRIG_MIN_,MC_SRM_PAR MC_SRM_PAR_,T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_W_LAST, T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_, MC_SINCAL_TRIG_1MIN MC_SINCAL_TRIG_1MIN_, T_BURN_COLE_PLC_1MIN T_BURN_COLE_PLC_1MIN_, CONFIG_SINCAL_VALUE CONFIG_SINCAL_VALUE_)
        {
            string log = "";
            MC_SRM_RESULT_1MIN MC_SRM_RESULT_1MIN_M= null;
            MC_SRM_RESULT_1MIN_M = new MC_SRM_RESULT_1MIN();   
            MC_SRM_RESULT_1MIN_M.TIMESTAMP= DateTime.Now.ToString("yyyy-MM-dd HH:mm").ToDate();
            MC_SRM_RESULT_1MIN_M.A_TIME = MC_SRM_RESULT_.TIMESTAMP;
            MC_SRM_RESULT_1MIN_M.SRMCAL_FLAG = MC_SRM_RESULT_.SRMCAL_FLAG.ToInt();
            MC_SRM_RESULT_1MIN_M.SRMCAL_BILL_SP_A = Math.Round(MC_SRM_RESULT_.SRMCAL_BILL_SP_A.ToDouble(),3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_BILL_SP_NEW = Math.Round(MC_SRM_RESULT_.SRMCAL_BILL_SP_NEW.ToDouble(),3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_BILL_SP_OLD = Math.Round(MC_SRM_RESULT_.SRMCAL_BILL_SP_OLD.ToDouble(),3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_RS_FLAG = MC_SRM_RESULT_.SRMCAL_RS_FLAG;
            if (T_MATCH_PLC_1MIN_X.ISQY>1|| T_MATCH_PLC_1MIN_X.ISQY==1)
            {
                MC_SRM_RESULT_1MIN_M.SRMCAL_SIG = 1;
            }
            else
            {
                MC_SRM_RESULT_1MIN_M.SRMCAL_SIG = 0;
            }            
            MC_SRM_RESULT_1MIN_M.SRMCAL_W = Math.Round(T_MATCH_PLC_1MIN_M8.M_W_8,3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_BILL_SP = Math.Round(MC_SINCAL_INTERFACE_TRIG_MIN_.SINCAL_ORE_SP.ToDouble(),3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_W_AIM = Math.Round(MC_SRM_PAR_.PAR_W_AIM.ToDouble(),3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_W_LAST = Math.Round(T_MATCH_PLC_1MIN_W_LAST.M_W_8,3);
            double e = (T_MATCH_PLC_1MIN_M8.M_W_8 - MC_SRM_PAR_.PAR_W_AIM).ToDouble();
            MC_SRM_RESULT_1MIN_M.SRMCAL_E = Math.Round(e, 3);//保留3位小数e = Math.Round(e, 3);
            double ec = (T_MATCH_PLC_1MIN_M8.M_W_8 - T_MATCH_PLC_1MIN_W_LAST.M_W_8).ToDouble();
            MC_SRM_RESULT_1MIN_M.SRMCAL_EC = Math.Round(ec, 3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_TOTAL_SP_W = Math.Round(T_MATCH_PLC_1MIN_.M_TOTAL_SP_W,3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_TOTAL_PV_W = Math.Round(T_MATCH_PLC_1MIN_.M_TOTAL_PV_W,3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_FUEL_BILL_SP = Math.Round(MC_SINCAL_TRIG_1MIN_.SINCAL_FUEL_14_PV.ToDouble(),3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_SIN_MS_SP = Math.Round(T_BURN_COLE_PLC_1MIN_.BC_SIN_MS_SP,3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_SIN_MS_PV = Math.Round(T_BURN_COLE_PLC_1MIN_.BC_SIN_MS_PV,3);
            MC_SRM_RESULT_1MIN_M.SRMCAL_SL_FLAG = CONFIG_SINCAL_VALUE_.MCR_FLAG;
       
            if (Set_Admin_add(MC_SRM_RESULT_1MIN_M))
            {
                log = "保存完成";
            }
            else
            {
                log = "保存失败";
            }
            Outputlog(log);

        }


        public void SET_SAVE1(double M_FPQCAL_PAR_, double M_SINTER_ANA_, MC_SINCAL_RESULT_1MIN MC_SINCAL_result_1min_)
        {
            string log = "";
            M_ALI_CHART M_ALI_CHART_ = null;
            M_ALI_CHART_ = new M_ALI_CHART();
            M_ALI_CHART_.TIMESTAMP = DateTime.Now.ToString("yyyy-MM-dd HH:mm").ToDate();
            M_ALI_CHART_.PAR_FE_CENTER = M_FPQCAL_PAR_;
            M_ALI_CHART_.C_FEO = M_SINTER_ANA_;
            M_ALI_CHART_.SINCAL_C_A = MC_SINCAL_result_1min_.SINCAL_C_A;
            M_ALI_CHART_.SINCAL_C_DC = MC_SINCAL_result_1min_.SINCAL_C_DC;

            if (Set_Admin_add1(M_ALI_CHART_))
            {
                log = "保存完成";
            }
            else
            {
                log = "保存失败";
            }
            Outputlog(log);

        }
        public bool Set_Admin_add(MC_SRM_RESULT_1MIN model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }

        public bool Set_Admin_add1(M_ALI_CHART model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
        public MC_SRM_PAR GET_MC_SRM_PAR(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from MC_SRM_PAR");
            return iDataBase.GetSingle<MC_SRM_PAR>(str_sql);
        }
        public MC_SRM_RESULT GET_MC_SRM_RESULT(IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<MC_SRM_RESULT>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间
            string str_sql = string.Format("select * from MC_SRM_RESULT where timestamp  = to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss') ");
            return iDataBase.GetSingle<MC_SRM_RESULT>(str_sql);
        }
        public T_MATCH_PLC_1MIN GET_T_MATCH_PLC_1MIN(IDataBase iDataBase,DateTime maxDate)
        {
            string str_sql = string.Format("select * from T_MATCH_PLC_1MIN where timestamp  = to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<T_MATCH_PLC_1MIN>(str_sql);
        }
        public MC_SINCAL_TRIG_1MIN GET_MC_SINCAL_TRIG_1MIN(IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<MC_SINCAL_TRIG_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间
            string str_sql = string.Format("select NVL(sum (SINCAL_FUEL_14_PV+SINCAL_FUEL_15_PV),0)  as SINCAL_FUEL_14_PV from MC_SINCAL_TRIG_1MIN where timestamp  = to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss') ");
            return iDataBase.GetSingle<MC_SINCAL_TRIG_1MIN>(str_sql);
        }
        public T_BURN_COLE_PLC_1MIN GET_T_BURN_COLE_PLC_1MIN(IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<T_BURN_COLE_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间
            string str_sql = string.Format("select * from T_BURN_COLE_PLC_1MIN where timestamp  = to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<T_BURN_COLE_PLC_1MIN>(str_sql);
        }
        public CONFIG_SINCAL_VALUE GET_CONFIG_SINCAL_VALUE(IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<CONFIG_SINCAL_VALUE>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间
            string str_sql = string.Format("select * from CONFIG_SINCAL_VALUE where timestamp  = to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<CONFIG_SINCAL_VALUE>(str_sql);
        }
        public MC_SINCAL_INTERFACE_TRIG_MIN GET_MC_SINCAL_INTERFACE_TRIG_MIN(IDataBase iDataBase)
        { 
            string str_sql = string.Format("select * from  MC_SINCAL_INTERFACE_TRIG_MIN  where  sincal_cood='601' and blanking_id='8' and rownum=1 order by timestamp desc");
            return iDataBase.GetSingle<MC_SINCAL_INTERFACE_TRIG_MIN>(str_sql);
        }
        public M_MATERIAL_ANA GET_M_MATERIAL_ANA(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from M_MATERIAL_ANA  where  （l2_code > 600 and l2_code<699）and rownum=1  order by l2_code asc");
            return iDataBase.GetSingle<M_MATERIAL_ANA>(str_sql);
        }
        public T_MATCH_PLC_1MIN GET_T_MATCH_PLC_1MIN(IDataBase iDataBase, M_MATERIAL_ANA M_MATERIAL_ANA_)
        {
            DateTime maxDate = iDataBase.Queryable<T_MATCH_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间
            List<MC_SINCAL_CONFIG> list8 = null;  //烧返配置表
            int canghaon = M_MATERIAL_ANA_.BARNID.ToInt();
            string str_sql = string.Format("select * from MC_SINCAL_CONFIG t where t.bin_id='"+ canghaon + "'");
            list8 = iDataBase.GetList<MC_SINCAL_CONFIG>(str_sql);//从烧返配置表获取去信息
            string xlk1 = "M_SL_" + list8[0].BLANKING_ID.ToInt() + "";
            if (list8.Count > 1)
            {
                string xlk2 = "M_SL_" + list8[1].BLANKING_ID.ToInt() + "";
                xlk1 = xlk1 + "+" + xlk2;
            }
            str_sql = string.Format("select sum(" + xlk1 + ") as ISQY from T_MATCH_PLC_1MIN   where TIMESTAMP =to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<T_MATCH_PLC_1MIN>(str_sql);
                
        }
        public T_MATCH_PLC_1MIN GET_T_MATCH_PLC_1MIN_M8(IDataBase iDataBase, M_MATERIAL_ANA M_MATERIAL_ANA_)
        {
            DateTime maxDate = iDataBase.Queryable<T_MATCH_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间      
            int canghaon = M_MATERIAL_ANA_.BARNID.ToInt();
            string canhao = "M_W_" + canghaon + "";
            string str_sql = string.Format("select " + canhao + " as M_W_8 from T_MATCH_PLC_1MIN where TIMESTAMP =to_date('" + maxDate + "','yyyy-mm-dd hh24:mi:ss') ");
            return iDataBase.GetSingle<T_MATCH_PLC_1MIN>(str_sql);
           
        }
        public T_MATCH_PLC_1MIN GET_T_MATCH_PLC_1MIN_LAST(IDataBase iDataBase, MC_SRM_PAR MC_SRM_PAR_)
        {
            int EC_T = MC_SRM_PAR_.PAR_EC_T.ToInt();//倒退时间
            DateTime maxDate = iDataBase.Queryable<T_MATCH_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间 
            DateTime beforetime = maxDate.AddMinutes(-EC_T);
            string str_sql = string.Format(" select *  from T_MATCH_PLC_1MIN where TIMESTAMP =（select max(timestamp) from T_MATCH_PLC_1MIN where timestamp <= to_date('" + beforetime + "', 'yyyy-mm-dd hh24:mi:ss'))");//获取8号仓位里烧返数据 
            return iDataBase.GetSingle<T_MATCH_PLC_1MIN>(str_sql);
        }

        public double M_FPQCAL_PAR(IDataBase iDataBase)
        {
            string str_sql = string.Format(" select PAR_FE_CENTER  from M_FPQCAL_PAR where TIMESTAMP =（select max(timestamp) FROM M_FPQCAL_PAR)");
            return iDataBase.GetDouble(str_sql);
        }

        public double M_SINTER_ANA(IDataBase iDataBase)
        {
            string str_sql = string.Format(" select C_FEO  from M_SINTER_ANA where TIMESTAMP =（select max(timestamp) FROM M_SINTER_ANA)");
            return iDataBase.GetDouble(str_sql);
        }

        public MC_SINCAL_RESULT_1MIN MC_SINCAL_result_1min(IDataBase iDataBase)
        {
            string str_sql = string.Format(" select SINCAL_C_A,SINCAL_C_DC  from MC_SINCAL_RESULT_1MIN where TIMESTAMP =（select max(timestamp) FROM MC_SINCAL_RESULT_1MIN)");
            return iDataBase.GetSingle<MC_SINCAL_RESULT_1MIN>(str_sql);
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
