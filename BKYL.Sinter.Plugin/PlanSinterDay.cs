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
    public class PlanSinterDay : BaseJob
    {
        //理论批重计算及插库
       
        DateTime timee = DateTime.Now.Date.ToString().ToDate().AddDays(-1).AddHours(20);
        DateTime timeb = DateTime.Now.Date.ToString().ToDate().AddDays(-2).AddHours(20);
        DateTime timeo = DateTime.Now.Date.ToString().ToDate();
        public override void InitTask()
        {
            //加载配置yanhao  /lihaiesn(修改)
        }

        public override void RunTask(DateTime currentTime)
        {
            return;//已经在产量模型中实现了zyh
            DateTime time = DateTime.Now.Date.ToString().ToDate();
            if (currentTime.Hour >= 20 && currentTime.Minute > 0)
            {
                timee = timee.AddDays(1);
                timeb = timeb.AddDays(1);
                timeo = timeo.AddDays(1);
            }
            string log = "";
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    //检测MD_PLAN_OUTPUT_IN 是否下发数据
                    MD_PLAN_OUTPUT_IN list = Get_MD_PLAN_OUTPUT_IN_modle(iDataBase);
                    MC_SINCAL_RESULT_1MIN list1 = Get_MC_SINCAL_RESULT_1MIN_modle(iDataBase);
                    List<T_MATCH_PLC_1MIN> list2 = Get_T_MATCH_PLC_1MIN(iDataBase);
                    double pOPCAL_D_SIN_RM = jsPOPCAL_D_SIN_RM(list2);
                    MC_POPCAL_PAR list3 = Get_MC_POPCAL_PAR_modle(iDataBase);
                    T_MATCH_PLC_1MIN list4 = GetT_MATCH_PLC_1MIN_modle(iDataBase);
                    SET_CAL_MC_POPCAL_RESULT_DAY(list, list1, list3, list4, iDataBase, pOPCAL_D_SIN_RM);
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
                log = null;
            }
        }

        /// <summary>
        /// 历史烧结返矿百分比平均值
        /// </summary>
        /// <param name="list2"></param>
        /// <returns></returns>
        private double jsPOPCAL_D_SIN_RM(List<T_MATCH_PLC_1MIN> list2)
        {
            double pjz = 0;           
            double mm = 0;
            double zz = 0;
            int numCount = 0;
            foreach (var item in list2)
            {
                mm = (item.M_ACTUAL_W_8 + item.M_ACTUAL_W_9);
                zz = (item.M_ACTUAL_W_1 + item.M_ACTUAL_W_2 + item.M_ACTUAL_W_3 + item.M_ACTUAL_W_4 + item.M_ACTUAL_W_5 + item.M_ACTUAL_W_6 + item.M_ACTUAL_W_7 + item.M_ACTUAL_W_8 + item.M_ACTUAL_W_9 + item.M_ACTUAL_W_10 + item.M_ACTUAL_W_11 + item.M_ACTUAL_W_12 + item.M_ACTUAL_W_13 + item.M_ACTUAL_W_14 + item.M_ACTUAL_W_15 + item.M_ACTUAL_W_16 + item.M_ACTUAL_W_17 + item.M_ACTUAL_W_18 + item.M_ACTUAL_W_19);
                if (mm != 0 && zz != 0)
                {
                    pjz += (mm / zz) * 100;
                    numCount++;
                }
            }
            if (numCount != 0)
            {
                return Math.Round(pjz / numCount,3);
            }
            else
            {
                return 0;
            }
        }

        public MD_PLAN_OUTPUT_IN Get_MD_PLAN_OUTPUT_IN_modle(IDataBase iDataBase)
        {
          string str_sql = string.Format("select * from MD_PLAN_OUTPUT_IN  where timestamp >  to_date('" + timee + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timeo + "','yyyy-mm-dd hh24:mi:ss') and state='0'");
          return iDataBase.GetSingle<MD_PLAN_OUTPUT_IN>(str_sql);//检测MD_PLAN_OUTPUT_IN 是否下发数据
        }
        public MC_SINCAL_RESULT_1MIN Get_MC_SINCAL_RESULT_1MIN_modle(IDataBase iDataBase)
        {
            //MC_SINCAL_result_1min     //SINCAL_MIX_PV_LOT                                                                                                       SINCAL_MIX_PV_LOT                                                                        
            string str_sql = string.Format(" select   round(NVL(avg(SINCAL_B_B_RATIO_PV),0),3) as SINCAL_B_B_RATIO_PV,  round(NVL(avg(SINCAL_MIX_PV_LOT),0),3) as SINCAL_MIX_PV_LOT, round(NVL(avg(SINCAL_MIX_SP_LOT),0),3) as SINCAL_MIX_SP_LOT, round(NVL(sum(SINCAL_OUTPUT_PV),0),3) as SINCAL_OUTPUT_PV from  MC_SINCAL_result_1min   where timestamp >  to_date('" + timeb.AddDays(-2) + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timee + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<MC_SINCAL_RESULT_1MIN>(str_sql);         
        }


        public List<T_MATCH_PLC_1MIN> Get_T_MATCH_PLC_1MIN(IDataBase iDataBase)
        {
            //T_MATCH_PLC_1MIN M_ACTUAL_W_8,M_ACTUAL_W_9,
            //(M_ACTUAL_W_T_8 +M_ACTUAL_W_T_9)/(M_ACTUAL_W_T_1 +M_ACTUAL_W_T_2+M_ACTUAL_W_T_3+M_ACTUAL_W_T_4+M_ACTUAL_W_T_5+M_ACTUAL_W_T_6+M_ACTUAL_W_T_7+M_ACTUAL_W_T_8+M_ACTUAL_W_T_9+M_ACTUAL_W_T_10+M_ACTUAL_W_T_11+M_ACTUAL_W_T_12+M_ACTUAL_W_T_13+M_ACTUAL_W_T_14+M_ACTUAL_W_T_15+M_ACTUAL_W_T_16+M_ACTUAL_W_T_17+M_ACTUAL_W_T_18+M_ACTUAL_W_T_19 ))*100
            //(M_ACTUAL_W_T_8 +M_ACTUAL_W_T_9)/(M_ACTUAL_W_T_1 +M_ACTUAL_W_T_2+M_ACTUAL_W_T_3+M_ACTUAL_W_T_4+M_ACTUAL_W_T_5+M_ACTUAL_W_T_6+M_ACTUAL_W_T_7+M_ACTUAL_W_T_8+M_ACTUAL_W_T_9+M_ACTUAL_W_T_10+M_ACTUAL_W_T_11+M_ACTUAL_W_T_12+M_ACTUAL_W_T_13+M_ACTUAL_W_T_14+M_ACTUAL_W_T_15+M_ACTUAL_W_T_16+M_ACTUAL_W_T_17+M_ACTUAL_W_T_18+M_ACTUAL_W_T_19 ))*100

            string str_col = "timestamp,M_ACTUAL_W_1,M_ACTUAL_W_2,M_ACTUAL_W_3,M_ACTUAL_W_4,M_ACTUAL_W_5,M_ACTUAL_W_6,M_ACTUAL_W_7,M_ACTUAL_W_8,M_ACTUAL_W_9,M_ACTUAL_W_10,M_ACTUAL_W_11,M_ACTUAL_W_12,M_ACTUAL_W_13,M_ACTUAL_W_14,M_ACTUAL_W_15,M_ACTUAL_W_16,M_ACTUAL_W_17,M_ACTUAL_W_18,M_ACTUAL_W_19";
            string str_sql = string.Format(" select {0} from  T_MATCH_PLC_1MIN   where timestamp >  to_date('" + timeb.AddDays(-2) + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timee + "','yyyy-mm-dd hh24:mi:ss')", str_col, timeb, timee);
            return iDataBase.GetList<T_MATCH_PLC_1MIN>(str_sql);
        }


        public MC_POPCAL_PAR Get_MC_POPCAL_PAR_modle(IDataBase iDataBase)
        {
         
              string  str_sql = string.Format(" select * from  MC_POPCAL_PAR ");
              return iDataBase.GetSingle<MC_POPCAL_PAR>(str_sql);
         
        }
        public T_MATCH_PLC_1MIN GetT_MATCH_PLC_1MIN_modle(IDataBase iDataBase)
        {     
               string  str_sql = string.Format(" select round(NVL(avg(M_TOTAL_SP_W),0),3) from T_MATCH_PLC_1MIN where timestamp = (select max(timestamp) from T_MATCH_PLC_1MIN)");//最大当前时间
               return iDataBase.GetSingle<T_MATCH_PLC_1MIN>(str_sql);
        }
        public void SET_CAL_MC_POPCAL_RESULT_DAY(MD_PLAN_OUTPUT_IN list, MC_SINCAL_RESULT_1MIN list1, MC_POPCAL_PAR list3, T_MATCH_PLC_1MIN list4, IDataBase idatabase, double pOPCAL_D_SIN_RM)
        {
            string log;
            MC_POPCAL_RESULT_DAY MC_POPCAL_RESULT_DAY_ = null;
            MC_POPCAL_RESULT_DAY_ = new MC_POPCAL_RESULT_DAY();
            if (list != null)
            {
                //检测到数据  开始计算公式SINCAL_MIX_PV_LOT
                try
                {
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_OUT_REA = idatabase.GetDouble(string.Format("select round(NVL(sum(SPARE32),0）,3) as SPARE32  from T_BURN_COLE_PLC_1MIN   where SPARE32>0 and  timestamp >  to_date('" + timeb.AddDays(-2) + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timee + "','yyyy-mm-dd hh24:mi:ss')"));

                    MC_POPCAL_RESULT_DAY_.POPCAL_D_ALL_OUT_PL = list.PLAN_OUTPUT_DAY.ToDouble();//POPCAL_D_ALL_OUT_PL
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_OUT_PL = list.PLAN_OUTPUT_DAY.ToDouble() / 2;//POPCAL_D_ALL_OUT_PL 夜班计划产量
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_SIN_RM = pOPCAL_D_SIN_RM;// list1.SINCAL_B_B_RATIO_PV.ToDouble();//POPCAL_D_SIN_RM
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_LOT = list1.SINCAL_MIX_PV_LOT.ToDouble();//list1.SINCAL_MIX_SP_LOT.ToDouble();//SINCAL_MIX_SP_LOT   
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_OUT_THR = list1.SINCAL_OUTPUT_PV.ToDouble();//POPCAL_D_OUT_THR 
                    double K = list3.PAR_K.ToDouble();
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_BW_ACT = list4.M_TOTAL_SP_W.ToDouble(); //POPCAL_D_BW_ACT
                    double c = 0;
                    if (MC_POPCAL_RESULT_DAY_.POPCAL_D_OUT_REA > 0)
                    {
                        c = MC_POPCAL_RESULT_DAY_.POPCAL_D_OUT_THR / MC_POPCAL_RESULT_DAY_.POPCAL_D_OUT_REA * K;
                    }
                    double a = MC_POPCAL_RESULT_DAY_.POPCAL_D_ALL_OUT_PL.ToDouble() / 24;
                    double b = 1 - (MC_POPCAL_RESULT_DAY_.POPCAL_D_SIN_RM / 100 + MC_POPCAL_RESULT_DAY_.POPCAL_D_LOT / 100);             
                    double result = c * a / b;
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_BW_THR = Math.Round(result, 3);  //求计算公式
                    MC_POPCAL_RESULT_DAY_.POPCAL_D_N_OUT_PL = list.PLAN_OUTPUT_DAY.ToDouble() / 2;

                    MC_POPCAL_RESULT_DAY_.TIMESTAMP = DateTime.Now;
                    if (Set_Admin_add(MC_POPCAL_RESULT_DAY_))
                    {
                        log = "保存完成";
                        update(1);
                    }
                    else
                    {
                        log = "保存失败";
                        update(2);
                    }
                 
                }
                catch (Exception ex)
                {
                    log = string.Format("执行失败。原因：{0}", ex.Message);
                }
            }
        }

        public bool Set_Admin_add(MC_POPCAL_RESULT_DAY model)
        {
            bool listmodel = false;          
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
        public void update(int a)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                string str_sql = string.Format("update  MD_PLAN_OUTPUT_IN  set state='"+ a + "' where  timestamp >  to_date('" + timee + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timeo + "','yyyy-mm-dd hh24:mi:ss') and state='0'");
                iDataBase.ExecuteCommand(str_sql);
            }
        }
    }
}
