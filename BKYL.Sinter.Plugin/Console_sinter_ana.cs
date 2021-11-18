using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BFES.DataAccess;
using System.Data;

using BKYL.Sinter.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System.Collections;

namespace BKYL.Sinter.Plugin
{
    /// <summary>
    /// J计算烧结智能配料中的成分设定值，实际值 LIHAISEN
    /// </summary>
    public class Console_sinter_ana : BaseJob
    {
        List<MC_SINCAL_INTERFACE_TRIG_MIN> interface_trig_min = new List<MC_SINCAL_INTERFACE_TRIG_MIN>();
        List<M_MATERIAL_ANA> manter_ana = new List<M_MATERIAL_ANA>();
        sinter_ana sinter_ana_ = null;
        sinter_ana_pv sinter_ana_pv_ = null;
        //MC_SINCAL_PAR MC_SINCAL_PAR_ = null;

        public override void InitTask()
        {
            //加载配置
        }
        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {

                    // 数据库查询
                    //读取
                    DateTime time = maxtimestamp("MC_SINCAL_INTERFACE_TRIG_MIN");
                    interface_trig_min.Clear();
                    interface_trig_min= Get_Read_data(time, iDataBase);
                    log = "读取智能配料界面一分钟结果表";
                    manter_ana.Clear();
                    manter_ana = Get_material_ana_data(iDataBase);

                    T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_ = GET_T_MATCH_PLC_1MIN(iDataBase);

                    MC_MICAL_RESULT MC_MICAL_RESULT_ = Get_MC_MICAL_RESULT(iDataBase);
                    log = "MC_SINCAL_INTERFACE_VA";
                    MC_SINCAL_INTERFACE_VA MC_SINCAL_INTERFACE_VA_= Get_MC_SINCAL_INTERFACE_VA(iDataBase);
                    MC_SINCAL_PAR MC_SINCAL_PAR_ = Get_MC_SINCAL_PAR(iDataBase);
                    log = "读取原料成分维护表MATERIAL_ANA数据完成";
                    T_PLC_5S Modle = Get_T_MATCH_PLC_1MIN_modle(iDataBase);
                    log = "读选仓健数据完成";
                    //MC_SINCAL_INTERFACE_AVG Modle_avg = Get_MC_SINCAL_INTERFACE_AVG_modle();
                    DateTime time1 = maxtimestamp("T_MATCH_PLC_1MIN");
                    MC_SINCAL_INTERFACE_AVG Modle_avg_PV =Get_MC_SINCAL_INTERFACE_AVG_PV_modle(time1, time1.AddMinutes(-5),iDataBase);
                    log = "读选平均值数据完成";
                    MC_SINCAL_RESULT_SP MC_SINCAL_RESULT_SP_= Get_Sp_Cal_Ana(interface_trig_min, manter_ana, Modle, MC_SINCAL_PAR_);
                    log = "计算设定成分完成";
                    MC_SINCAL_RESULT_PV MC_SINCAL_RESULT_PV_= Get_Pv_Cal_Ana(interface_trig_min, manter_ana, Modle, Modle_avg_PV, MC_SINCAL_PAR_);
                    log = "计算实际成分完成";
                    Save_Cal_min(manter_ana, Modle, interface_trig_min, MC_SINCAL_RESULT_SP_, MC_SINCAL_RESULT_PV_ , MC_SINCAL_INTERFACE_VA_, MC_MICAL_RESULT_, T_MATCH_PLC_1MIN_);

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
                DestroyIList(interface_trig_min);
                DestroyIList(manter_ana);
                if (sinter_ana_!=null )
                {
                    sinter_ana_ = null;
                }
                if (sinter_ana_pv_!=null)
                {
                    sinter_ana_pv_ = null;
                }
                 log = null;
            }
        }
        public void DestroyIList(IList list)
        {
            if (list != null)
            {
                list.Clear();
                list = null;
            }
        }
        public DateTime maxtimestamp(string tablename)
        {
            string Temp = "";
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                string strSQL = string.Format("select max(timestamp)  from {0}", tablename);
                Temp = iDataBase.GetString(strSQL);
                return  Convert.ToDateTime(Temp);
               
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="currentTime"></param>
        /// 
        public T_MATCH_PLC_1MIN GET_T_MATCH_PLC_1MIN(IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<T_MATCH_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();
            return iDataBase.Queryable<T_MATCH_PLC_1MIN>().Where(m => m.TIMESTAMP == maxDate).SingleOrDefault();
        }
       public MC_MICAL_RESULT Get_MC_MICAL_RESULT(IDataBase iDataBase)
        {
                DateTime maxDate = iDataBase.Queryable<MC_MICAL_RESULT>().Max(m => m.TIMESTAMP).ToDate();
                return iDataBase.Queryable<MC_MICAL_RESULT>().Where(m => m.TIMESTAMP == maxDate && m.DATANUM == 14).SingleOrDefault();
        }

        public List<MC_SINCAL_INTERFACE_TRIG_MIN> Get_Read_data(DateTime currentTime,IDataBase iDataBase)
        {
            string Temp = "";

                return iDataBase.Queryable<MC_SINCAL_INTERFACE_TRIG_MIN>().Where(m => m.TIMESTAMP == currentTime).OrderBy(M => M.BLANKING_ID).ToList();

        }
        public MC_SINCAL_INTERFACE_VA Get_MC_SINCAL_INTERFACE_VA(IDataBase iDataBase)
        {
                DateTime maxDate = iDataBase.Queryable<MC_SINCAL_INTERFACE_VA>().Max(m => m.TIMESTAMP).ToDate();
                return iDataBase.Queryable<MC_SINCAL_INTERFACE_VA>().Where(m => m.TIMESTAMP == maxDate).SingleOrDefault();
        }

        public MC_SINCAL_PAR Get_MC_SINCAL_PAR(IDataBase iDataBase)
        {

                DateTime maxDate = iDataBase.Queryable<MC_SINCAL_PAR>().Max(m => m.TIMESTAMP).ToDate();
                return iDataBase.Queryable<MC_SINCAL_PAR>().Where(m => m.TIMESTAMP == maxDate).SingleOrDefault();
        }
        public List<M_MATERIAL_ANA> Get_material_ana_data(IDataBase iDataBase)
        {
        
                return iDataBase.Queryable<M_MATERIAL_ANA>().OrderBy(M => M.BARNID).ToList();
        }
        public T_PLC_5S Get_T_MATCH_PLC_1MIN_modle(IDataBase iDataBase)
        {
            string Temp = "";
         
                string strSQL = "select max(timestamp)  from T_PLC_5S";
                Temp = iDataBase.GetString(strSQL);
                DateTime strtimestamp = Convert.ToDateTime(Temp);
                string sql = string.Format("select * from T_PLC_5S where timestamp =to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')", Temp);
                return iDataBase.GetSingle<T_PLC_5S>(sql);
      
        }
        /// <summary>
        /// 读取平均值
        /// </summary>
        /// <returns></returns>
        //public MC_SINCAL_INTERFACE_AVG Get_MC_SINCAL_INTERFACE_AVG_modle()
        //{
        //    using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
        //    {
        //        //string sql = string.Format(@"select Distinct (select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 1)  as sincal_ore_bk_sp1,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 2 ) as sincal_ore_bk_sp2,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 3 ) as sincal_ore_bk_sp3,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 4 ) as sincal_ore_bk_sp4,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 5) as sincal_ore_bk_sp5,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 6) as sincal_ore_bk_sp6,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 7) as sincal_ore_bk_sp7,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 8) as sincal_ore_bk_sp8,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 9) as sincal_ore_bk_sp9,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 10) as sincal_ore_bk_sp10,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 11) as sincal_ore_bk_sp11,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 12) as sincal_ore_bk_sp12,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 13) as sincal_ore_bk_sp13,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 14) as sincal_ore_bk_sp14,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 15) as sincal_ore_bk_sp15,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 16) as sincal_ore_bk_sp16,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 17) as sincal_ore_bk_sp17,(select ROUND(avg(sincal_ore_bk_sp),3)  from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 18) as sincal_ore_bk_sp18,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //        // where BLANKING_ID = 19) as sincal_ore_bk_sp19 from MC_SINCAL_INTERFACE_TRIG_MIN ");


        //        string sql = string.Format(@"select Distinct (select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 1 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN)) as sincal_ore_bk_sp1,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 2 and timestamp = (select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp2,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 3 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp3,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 4 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp4,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 5 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp5,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 6 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp6,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 7 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp7,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 8 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp8,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 9 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp9,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 10 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp10,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 11 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp11,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 12 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp12,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 13 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp13,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 14 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp14,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 15 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp15,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 16 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp16,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 17 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp17,(select ROUND(avg(sincal_ore_bk_sp),3)  from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 18 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp18,(select ROUND(avg(sincal_ore_bk_sp),3) from MC_SINCAL_INTERFACE_TRIG_MIN
        //         where BLANKING_ID = 19 and timestamp =(select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN))  as sincal_ore_bk_sp19 from MC_SINCAL_INTERFACE_TRIG_MIN ", end, start);
        //        return iDataBase.GetSingle<MC_SINCAL_INTERFACE_AVG>(sql);
        //    }
        //}

        public MC_SINCAL_INTERFACE_AVG Get_MC_SINCAL_INTERFACE_AVG_PV_modle(DateTime end, DateTime start,IDataBase iDataBase)
        {
         
                //string sql = string.Format(@"select Distinct (select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 1 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')) as Sincal_ore_bk_pv1,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 2 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv2,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 3 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv3,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 4 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv4,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 5 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv5,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 6 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv6,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 7 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv7,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 8 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv8,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 9 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv9,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 10 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv10,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 11 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv11,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 12 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv12,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 13 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv13,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 14 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv14,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 15 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv15,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 16 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv16,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 17 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv17,(select ROUND(avg(SINCAL_ORE_BK_PV),3)  from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 18 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv18,(select ROUND(avg(SINCAL_ORE_BK_PV),3) from MC_SINCAL_INTERFACE_TRIG_MIN
                // where BLANKING_ID = 19 and timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'))  as Sincal_ore_bk_pv19 from MC_SINCAL_INTERFACE_TRIG_MIN ", end, start);


                string sql = string.Format(@"select round(avg(M_ACTUAL_W_1),3) as sincal_ore_bk_pv1,round(avg(M_ACTUAL_W_2),3) as sincal_ore_bk_pv2,
                                                    round(avg(M_ACTUAL_W_3),3) as sincal_ore_bk_pv3,round(avg(M_ACTUAL_W_4),3) as sincal_ore_bk_pv4,
                                                    round(avg(M_ACTUAL_W_5),3) as sincal_ore_bk_pv5,round(avg(M_ACTUAL_W_6),3) as sincal_ore_bk_pv6,
                                                    round(avg(M_ACTUAL_W_7),3) as sincal_ore_bk_pv7,round(avg(M_ACTUAL_W_8),3) as sincal_ore_bk_pv8,
                                                    round(avg(M_ACTUAL_W_9),3) as sincal_ore_bk_pv9,round(avg(M_ACTUAL_W_10),3) as sincal_ore_bk_pv10,
                                                    round(avg(M_ACTUAL_W_11),3) as sincal_ore_bk_pv11,round(avg(M_ACTUAL_W_12),3) as sincal_ore_bk_pv12,
                                                    round(avg(M_ACTUAL_W_13),3) as sincal_ore_bk_pv13,round(avg(M_ACTUAL_W_14),3) as sincal_ore_bk_pv14,
                                                    round(avg(M_ACTUAL_W_15),3) as sincal_ore_bk_pv15,round(avg(M_ACTUAL_W_16),3) as sincal_ore_bk_pv16,
                                                    round(avg(M_ACTUAL_W_17),3) as sincal_ore_bk_pv17,round(avg(M_ACTUAL_W_18),3) as sincal_ore_bk_pv18,
                                                    round(avg(M_ACTUAL_W_19),3) as sincal_ore_bk_pv19
                                                      from T_MATCH_PLC_1MIN
                 where timestamp >to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and timestamp<=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                   ", end, start);
                return iDataBase.GetSingle<MC_SINCAL_INTERFACE_AVG>(sql);
        }
        /// <summary>
        /// 计算设定值成分
        /// </summary>
        /// <param name="interface_trig_min"></param>
        public MC_SINCAL_RESULT_SP Get_Sp_Cal_Ana(List<MC_SINCAL_INTERFACE_TRIG_MIN> interface_trig_min, List<M_MATERIAL_ANA> manter_ana, T_PLC_5S Modle, MC_SINCAL_PAR MC_SINCAL_PAR_)
        {
            MC_SINCAL_RESULT_SP MC_SINCAL_RESULT_SP_ = null;
           
            MC_SINCAL_RESULT_SP_ = new MC_SINCAL_RESULT_SP();
            sinter_ana_ = new sinter_ana();
            double[] M_belt_sl = new double[19];
            for (int j = 0; j < 19; j++)
            {
                M_belt_sl[j] = (int)CustomModelInfo.GetModelItemValue<T_PLC_5S>(Modle, "T_SL_" + (j + 1)+"_5S");
            }
            for (int i = 0; i < interface_trig_min.Count; i++)
            {

                if ((i != 17) && (i != 18))
                {
                    sinter_ana_.sumTFe += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TFE / 100).ToDouble();
                    sinter_ana_.sumFeO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_FEO / 100).ToDouble();
                    sinter_ana_.sumCaO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CAO / 100).ToDouble();
                    sinter_ana_.sumSiO2 += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_SIO2 / 100).ToDouble();
                    sinter_ana_.sumAl2O3 += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AL2O3 / 100).ToDouble();
                    sinter_ana_.sumMgO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MGO / 100).ToDouble();
                    sinter_ana_.sumS += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_S / 100).ToDouble();
                    sinter_ana_.sumP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_P / 100).ToDouble();
                    sinter_ana_.sumC += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();
                    sinter_ana_.sumMn += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MN / 100).ToDouble();
                    sinter_ana_.sumLOT += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_LOT / 100).ToDouble();
                    sinter_ana_.sumAsH += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumVOL += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_VOLATILES / 100).ToDouble();
                    sinter_ana_.sumTiO2 += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TIO2 / 100).ToDouble();
                    sinter_ana_.sumK2O += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K2O / 100).ToDouble();
                    sinter_ana_.sumNa2O += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA2O / 100).ToDouble();
                    sinter_ana_.sumPbO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PBO / 100).ToDouble();
                    sinter_ana_.sumZnO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZNO / 100).ToDouble();
                    sinter_ana_.sumF += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TI / 100).ToDouble();
                    sinter_ana_.sumAs += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AS / 100).ToDouble();
                    sinter_ana_.sumCu += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CU / 100).ToDouble();
                    sinter_ana_.sumPb += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PB / 100).ToDouble();
                    sinter_ana_.sumZn += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZN / 100).ToDouble();
                    sinter_ana_.sumK += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K / 100).ToDouble();
                    sinter_ana_.sumNa += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA / 100).ToDouble();
                    sinter_ana_.sumCr += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CR / 100).ToDouble();
                    sinter_ana_.sumNi += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NI / 100).ToDouble();

                    sinter_ana_.sumH2O_1 += (interface_trig_min[i].SINCAL_ORE_BK_SP * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_H2O / 100).ToDouble();
                    sinter_ana_.sumH2O_2 += (interface_trig_min[i].SINCAL_ORE_BK_SP * M_belt_sl[i] * interface_trig_min[i].SINCAL_ORE_H2O_PV / 100).ToDouble();
                    sinter_ana_.sumOther_C_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();
                    sinter_ana_.sumRemnant_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * (1 - manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_LOT / 100)).ToDouble();//总残存量

                }
                else
                {
                    sinter_ana_.sumTFe += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TFE / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumFeO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_FEO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumCaO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CAO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumSiO2 += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_SIO2 / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumAl2O3 += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AL2O3 / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumMgO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MGO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumS += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_S / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_P / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumMn += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MN / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumTiO2 += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TIO2 / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumK2O += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K2O / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumNa2O += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA2O / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumPbO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PBO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumZnO += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZNO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumF += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TI / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumAs += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AS / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumCu += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CU / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumPb += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PB / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumZn += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZN / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumK += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumNa += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumCr += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CR / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumNi += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NI / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();

                    sinter_ana_.sumH2O_1 += (interface_trig_min[i].SINCAL_ORE_BK_SP * M_belt_sl[i] / (1 - manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_H2O / 100) * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_H2O / 100).ToDouble();
                    sinter_ana_.sumH2O_2 += (interface_trig_min[i].SINCAL_ORE_BK_SP / (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * interface_trig_min[i].SINCAL_ORE_H2O_PV / 100).ToDouble();

                    sinter_ana_.sumC += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();
                    sinter_ana_.sumLOT += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * (1 - manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100)).ToDouble();
                    sinter_ana_.sumAsH += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_.sumVOL += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_VOLATILES / 100).ToDouble();

                    sinter_ana_.sumFuel_C_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();//燃料带入的C量
                    sinter_ana_.sumRemnant_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();//如果煤粉检化验有“灰分”，总烧损计算用该公式

                    sinter_ana_.sumFuel_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i]).ToDouble();
                }
                sinter_ana_.sumMix_Dry_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i]).ToDouble();//设定总干料量
                sinter_ana_.sumMix_Wet_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * M_belt_sl[i]).ToDouble();
                //sumDryBill +=Silo[i].bill_use * Silo[i].use;

                if (interface_trig_min[i].SINCAL_COOD > 199)//确保不是混匀矿
                {
                    if (interface_trig_min[i].BLANKING_ID.ToInt() == 18 || interface_trig_min[i].BLANKING_ID.ToInt() == 19)//为燃料仓
                    {
                        sinter_ana_.sumOther_SiO2_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_SIO2 / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    }
                    else
                    {
                        sinter_ana_.sumOther_SiO2_SP += (interface_trig_min[i].SINCAL_ORE_BK_SP * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_SIO2 / 100).ToDouble();
                    }
                }
            }
            /////
            sinter_ana_.sumOutput_SP = MC_SINCAL_PAR_.PAR_K.ToDouble()* (sinter_ana_.sumRemnant_SP - interface_trig_min[7].SINCAL_ORE_BK_SP * (1 - interface_trig_min[7].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[7] - interface_trig_min[8].SINCAL_ORE_BK_SP * (1 - interface_trig_min[8].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[8] - interface_trig_min[13].SINCAL_ORE_BK_SP * (1 - interface_trig_min[13].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[13] - interface_trig_min[14].SINCAL_ORE_BK_SP * (1 - interface_trig_min[14].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[14]).ToDouble();
            sinter_ana_.FuelCon_SP = sinter_ana_.sumFuel_SP / sinter_ana_.sumOutput_SP * 1000;
            //////////////////////////
            if (sinter_ana_.sumRemnant_SP > 0)
            {
                MC_SINCAL_RESULT_SP_.SP_TFE = sinter_ana_.sumTFe / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_FeO = sinter_ana_.m_SP * (sinter_ana_.sumC / sinter_ana_.sumMix_Dry_SP * 100);
                MC_SINCAL_RESULT_SP_.SP_CaO= sinter_ana_.sumCaO / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_SiO2 = sinter_ana_.sumSiO2 / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Al2O3 = sinter_ana_.sumAl2O3 / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_MgO = sinter_ana_.sumMgO / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_S = sinter_ana_.sumS / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_P = sinter_ana_.sumP / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Mn = sinter_ana_.sumMn / sinter_ana_.sumRemnant_SP * 100;

               if  (MC_SINCAL_RESULT_SP_.SP_SiO2>0)
                {
                    MC_SINCAL_RESULT_SP_.SP_R = MC_SINCAL_RESULT_SP_.SP_CaO / MC_SINCAL_RESULT_SP_.SP_SiO2;
                }
                MC_SINCAL_RESULT_SP_.SP_TiO2 = sinter_ana_.sumTiO2 / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_K2O = sinter_ana_.sumK2O / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Na2O = sinter_ana_.sumNa2O / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_PbO = sinter_ana_.sumPbO / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_ZnO = sinter_ana_.sumZnO / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_F = sinter_ana_.sumF / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_As = sinter_ana_.sumAs / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Cu = sinter_ana_.sumCu / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Pb = sinter_ana_.sumPb / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Zn = sinter_ana_.sumZn / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_K = sinter_ana_.sumK / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Na = sinter_ana_.sumNa / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Cr = sinter_ana_.sumCr / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SP_Ni = sinter_ana_.sumNi / sinter_ana_.sumRemnant_SP * 100;
                MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_LOT = sinter_ana_.sumLOT / sinter_ana_.sumMix_Dry_SP * 100;//混合料--干基设定烧损
                MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_H2O_1 = sinter_ana_.sumH2O_1 / sinter_ana_.sumMix_Wet_SP;   //混合料--采集水分计算的混合料原始水分含量
                MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_H2O_2 = sinter_ana_.sumH2O_2 / sinter_ana_.sumMix_Wet_SP; //混合料--设定水分计算的混合料原始水分含量
                MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_FEO = sinter_ana_.sumFeO / sinter_ana_.sumRemnant_SP * 100; //混合料--原料所含FeO含量
                MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_C = sinter_ana_.sumC / sinter_ana_.sumMix_Dry_SP * 100;             //混合料--干基设定固定碳
                MC_SINCAL_RESULT_SP_.SINCAL_NON_FUEL_SP_C = sinter_ana_.sumOther_C_SP / sinter_ana_.sumMix_Dry_SP * 100;    //混合料--非燃料带入固定碳含量
                MC_SINCAL_RESULT_SP_.SINCAL_FUEL_SP_C = sinter_ana_.sumFuel_C_SP / sinter_ana_.sumMix_Dry_SP * 100;    //混合料--燃料带入固定碳含量
                MC_SINCAL_RESULT_SP_.SINCAL_NON_FE_SP_SIO2 = sinter_ana_.sumOther_SiO2_SP / sinter_ana_.sumMix_Dry_SP * 100; //混合料--非铁料带入的SiO2含量量
                MC_SINCAL_RESULT_SP_.SINCAL_DRY_MIX_SP = sinter_ana_.sumMix_Dry_SP;                          //设定下料量计算总干料量
                MC_SINCAL_RESULT_SP_.SINCAL_OUTPUT_SP = sinter_ana_.sumOutput_SP / 60;                      //设定下料量计算的每分钟理论产量(单位：t/min)
                MC_SINCAL_RESULT_SP_.SINCAL_FUEL_CON_SP = nanInfinity(sinter_ana_. FuelCon_SP);                             //设定下料量计算的理论燃耗(干基)（单位：kg/t）
            }

            return MC_SINCAL_RESULT_SP_;
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
        public MC_SINCAL_RESULT_PV Get_Pv_Cal_Ana(List<MC_SINCAL_INTERFACE_TRIG_MIN> interface_trig_min, List<M_MATERIAL_ANA> manter_ana, T_PLC_5S Modle, MC_SINCAL_INTERFACE_AVG Modle_avg, MC_SINCAL_PAR MC_SINCAL_PAR_)
        {
            sinter_ana_pv_ = new sinter_ana_pv();
            MC_SINCAL_RESULT_PV MC_SINCAL_RESULT_PV_ = null;
            MC_SINCAL_RESULT_PV_ = new MC_SINCAL_RESULT_PV();
            double[] M_belt_sl = new double[19];
            double[] M_Mod_avg = new double[19];
            for (int j = 0; j < 19; j++)
            {
                M_belt_sl[j] = (int)CustomModelInfo.GetModelItemValue<T_PLC_5S>(Modle, "T_SL_" + (j + 1)+"_5S");
                if ((double)CustomModelInfo.GetModelItemValue<MC_SINCAL_INTERFACE_AVG>(Modle_avg, "sincal_ore_bk_pv" + (j + 1)) >=0)
                {
                    M_Mod_avg[j] = (double)CustomModelInfo.GetModelItemValue<MC_SINCAL_INTERFACE_AVG>(Modle_avg, "sincal_ore_bk_pv" + (j + 1));
                }
                else
                {
                    M_Mod_avg[j] = 0;
                }
            }
            for (int i = 0; i < interface_trig_min.Count; i++)
            {
                if ((i != 17) && (i != 18))
                {
                    sinter_ana_pv_.sumTFe += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TFE / 100).ToDouble();
                    sinter_ana_pv_.sumFeO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_FEO / 100).ToDouble();
                    sinter_ana_pv_.sumCaO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CAO / 100).ToDouble();
                    sinter_ana_pv_.sumSiO2 += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_SIO2 / 100).ToDouble();
                    sinter_ana_pv_.sumAl2O3 += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AL2O3 / 100).ToDouble();
                    sinter_ana_pv_.sumMgO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MGO / 100).ToDouble();
                    sinter_ana_pv_.sumS += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_S / 100).ToDouble();
                    sinter_ana_pv_.sumP += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_P / 100).ToDouble();
                    sinter_ana_pv_.sumC += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();
                    sinter_ana_pv_.sumMn += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MN / 100).ToDouble();
                    sinter_ana_pv_.sumLOT += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_LOT / 100).ToDouble();
                    sinter_ana_pv_.sumAsH += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumVOL += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_VOLATILES/ 100).ToDouble();
                    sinter_ana_pv_.sumTiO2 += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TIO2 / 100).ToDouble();
                    sinter_ana_pv_.sumK2O += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K2O / 100).ToDouble();
                    sinter_ana_pv_.sumNa2O += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA2O  / 100).ToDouble();
                    sinter_ana_pv_.sumPbO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PBO / 100).ToDouble();
                    sinter_ana_pv_.sumZnO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZNO / 100).ToDouble();
                    sinter_ana_pv_.sumF += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TI / 100).ToDouble();
                    sinter_ana_pv_.sumAs += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AS / 100).ToDouble();
                    sinter_ana_pv_.sumCu += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CU / 100).ToDouble();
                    sinter_ana_pv_.sumPb += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PB / 100).ToDouble();
                    sinter_ana_pv_.sumZn += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZN / 100).ToDouble();
                    sinter_ana_pv_.sumK += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K / 100).ToDouble();
                    sinter_ana_pv_.sumNa +=( M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA / 100).ToDouble();
                    sinter_ana_pv_.sumCr += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CR / 100).ToDouble();
                    sinter_ana_pv_.sumNi += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NI / 100).ToDouble();

                    sinter_ana_pv_.sumH2O_1 += (M_Mod_avg[i] * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_H2O / 100).ToDouble();
                    sinter_ana_pv_.sumH2O_2 += (M_Mod_avg[i] * M_belt_sl[i] * interface_trig_min[i].SINCAL_ORE_H2O_PV / 100).ToDouble();

                    sinter_ana_pv_.sumOther_C_PV += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();
                    sinter_ana_pv_.sumRemnant_PV += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * (1 - manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_LOT / 100)).ToDouble();

                }
                else
                {
                    sinter_ana_pv_.sumTFe += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TFE / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumFeO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_FEO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumCaO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CAO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumSiO2 += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_SIO2 / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumAl2O3 += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AL2O3 / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumMgO +=(M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MGO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumS += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_S / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumP += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_P / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumMn += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_MN / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumTiO2 += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TIO2 / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumK2O += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K2O / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumNa2O += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA2O / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumPbO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PBO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumZnO += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZNO / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumF += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_TI / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumAs += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_AS / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumCu += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CU / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumPb += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_PB / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumZn += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ZN / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumK += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_K / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumNa += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NA / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumCr += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_CR / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumNi += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_NI / 100 * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();

                    sinter_ana_pv_.sumC += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();
                    sinter_ana_pv_.sumLOT += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * (1 - manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100)).ToDouble();//燃料只有灰分检测，没有烧损检测
                    sinter_ana_pv_.sumH2O_1 +=( M_Mod_avg[i] * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_H2O / 100).ToDouble();
                    sinter_ana_pv_.sumH2O_2 +=( M_Mod_avg[i] * M_belt_sl[i] * interface_trig_min[i].SINCAL_ORE_H2O_PV / 100).ToDouble();
                    sinter_ana_pv_.sumAsH += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();
                    sinter_ana_pv_.sumVOL += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_VOLATILES / 100).ToDouble();

                    sinter_ana_pv_.sumRemnant_PV += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_ASH / 100).ToDouble();//默认燃料中有灰分检测，无烧损检测
                    sinter_ana_pv_.sumFuel_C_PV += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i] * manter_ana[interface_trig_min[i].SINCAL_ID.ToInt() - 1].C_C / 100).ToDouble();
                    sinter_ana_pv_.sumFuel_PV += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i]).ToDouble();

                }

                sinter_ana_pv_.sumMix_Dry_PV += (M_Mod_avg[i] * (1 - interface_trig_min[i].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[i]).ToDouble();
                sinter_ana_pv_.sumMix_Wet_PV += M_Mod_avg[i] * M_belt_sl[i];
        

            }

            sinter_ana_pv_.sumOutput_PV = MC_SINCAL_PAR_.PAR_K.ToDouble()* (sinter_ana_pv_.sumRemnant_PV - M_Mod_avg[7] * (1 - interface_trig_min[7].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[7] - M_Mod_avg[8] * (1 - interface_trig_min[8].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[8] - M_Mod_avg[13] * (1 - interface_trig_min[13].SINCAL_ORE_H2O_PV / 100) * M_Mod_avg[13] - M_Mod_avg[14] * (1 - interface_trig_min[14].SINCAL_ORE_H2O_PV / 100) * M_belt_sl[14]).ToDouble();
            sinter_ana_pv_.FuelCon_PV = sinter_ana_pv_.sumFuel_PV / sinter_ana_pv_.sumOutput_PV * 1000;

            if (sinter_ana_pv_.sumMix_Dry_PV > 0 && sinter_ana_pv_.sumDryBill > 0)
            {
                sinter_ana_pv_.Burn_Back_Ratio_PV = ((interface_trig_min[7].SINCAL_ORE_PV * M_belt_sl[7] + interface_trig_min[8].SINCAL_ORE_PV * M_belt_sl[8]) / sinter_ana_pv_.sumDryBill * 100).ToDouble();
                sinter_ana_pv_.BurnLoss_Ratio_PV = sinter_ana_pv_.sumLOT / sinter_ana_pv_.sumMix_Dry_PV * 100;
            }
            else
            {
                sinter_ana_pv_.Burn_Back_Ratio_PV = 0;
                sinter_ana_pv_.BurnLoss_Ratio_PV = 0;
            }

            if (sinter_ana_pv_.sumMix_Wet_PV > 0)
            {

                MC_SINCAL_RESULT_PV_.PV_TFE = sinter_ana_pv_.sumTFe / sinter_ana_pv_.sumRemnant_PV * 100;                        //烧结矿--PV_TFE
                MC_SINCAL_RESULT_PV_.PV_FeO = sinter_ana_pv_.m_PV * (sinter_ana_pv_.sumC / sinter_ana_pv_.sumMix_Dry_PV * 100);//烧结矿--PV_FeO
                MC_SINCAL_RESULT_PV_.PV_CaO= sinter_ana_pv_.sumCaO / sinter_ana_pv_.sumRemnant_PV * 100;                        //烧结矿--PV_CaO
                MC_SINCAL_RESULT_PV_.PV_SiO2= sinter_ana_pv_.sumSiO2 / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_SiO2
                MC_SINCAL_RESULT_PV_.PV_Al2O3 = sinter_ana_pv_.sumAl2O3 / sinter_ana_pv_.sumRemnant_PV * 100;                      //烧结矿--PV_Al2O3
                MC_SINCAL_RESULT_PV_.PV_MgO = sinter_ana_pv_.sumMgO / sinter_ana_pv_.sumRemnant_PV * 100;                        //烧结矿--PV_MgO
                MC_SINCAL_RESULT_PV_.PV_S = sinter_ana_pv_.sumS / sinter_ana_pv_.sumRemnant_PV * 100;                          //烧结矿--PV_S
                MC_SINCAL_RESULT_PV_.PV_P = sinter_ana_pv_.sumP / sinter_ana_pv_.sumRemnant_PV * 100;                          //烧结矿--PV_P
                MC_SINCAL_RESULT_PV_.PV_Mn= sinter_ana_pv_.sumMn / sinter_ana_pv_.sumRemnant_PV * 100;                         //烧结矿--PV_Mn

                if (MC_SINCAL_RESULT_PV_.PV_SiO2 > 0)
                {
                    MC_SINCAL_RESULT_PV_.PV_R = MC_SINCAL_RESULT_PV_.PV_CaO / MC_SINCAL_RESULT_PV_.PV_SiO2;                //烧结矿--PV_R
                }

                MC_SINCAL_RESULT_PV_.PV_TiO2= sinter_ana_pv_.sumTiO2 / sinter_ana_pv_.sumRemnant_PV * 100;                     //烧结矿--PV_TiO2
                MC_SINCAL_RESULT_PV_.PV_K2O = sinter_ana_pv_.sumK2O / sinter_ana_pv_.sumRemnant_PV * 100;                      //烧结矿--PV_K2O
                MC_SINCAL_RESULT_PV_.PV_Na2O = sinter_ana_pv_.sumNa2O / sinter_ana_pv_.sumRemnant_PV * 100;                     //烧结矿--PV_Na2O
                MC_SINCAL_RESULT_PV_.PV_PbO = sinter_ana_pv_.sumPbO / sinter_ana_pv_.sumRemnant_PV * 100;                      //烧结矿--PV_PbO
                MC_SINCAL_RESULT_PV_.PV_ZnO = sinter_ana_pv_.sumZnO / sinter_ana_pv_.sumRemnant_PV * 100;                      //烧结矿--PV_ZnO
                MC_SINCAL_RESULT_PV_.PV_F = sinter_ana_pv_.sumF / sinter_ana_pv_.sumRemnant_PV * 100;                        //烧结矿--PV_F
                MC_SINCAL_RESULT_PV_.PV_As = sinter_ana_pv_.sumAs / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_As
                MC_SINCAL_RESULT_PV_.PV_Cu = sinter_ana_pv_.sumCu / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_Cu
                MC_SINCAL_RESULT_PV_.PV_Pb = sinter_ana_pv_.sumPb / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_Pb
                MC_SINCAL_RESULT_PV_.PV_Zn = sinter_ana_pv_.sumZn / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_Zn
                MC_SINCAL_RESULT_PV_.PV_K = sinter_ana_pv_.sumK / sinter_ana_pv_.sumRemnant_PV * 100;                        //烧结矿--PV_K
                MC_SINCAL_RESULT_PV_.PV_Na= sinter_ana_pv_.sumNa / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_Na
                MC_SINCAL_RESULT_PV_.PV_Cr = sinter_ana_pv_.sumCr / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_Cr
                MC_SINCAL_RESULT_PV_.PV_Ni = sinter_ana_pv_.sumNi / sinter_ana_pv_.sumRemnant_PV * 100;                       //烧结矿--PV_Ni



                MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_LOT = sinter_ana_pv_.sumLOT / sinter_ana_pv_.sumMix_Dry_PV * 100;            //混合料--干基实际烧损
                MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_H2O_1 = sinter_ana_pv_.sumH2O_1 / sinter_ana_pv_.sumMix_Wet_PV;                //混合料--采集水分计算的混合料原始水分含量
                MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_H2O_2= sinter_ana_pv_.sumH2O_2 / sinter_ana_pv_.sumMix_Wet_PV;                //混合料--设定水分计算的混合料原始水分含量
                MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_FEO = sinter_ana_pv_.sumFeO / sinter_ana_pv_.sumRemnant_PV * 100;            //混合料--原料所含FeO含量
                MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_C = sinter_ana_pv_.sumC / sinter_ana_pv_.sumMix_Dry_PV * 100;              //混合料--干基设定固定碳
                MC_SINCAL_RESULT_PV_.SINCAL_NON_FUEL_PV_C = sinter_ana_pv_.sumOther_C_PV / sinter_ana_pv_.sumMix_Dry_PV * 100;     //混合料--非燃料带入固定碳含量
                MC_SINCAL_RESULT_PV_.SINCAL_FUEL_PV_C = sinter_ana_pv_.sumFuel_C_PV / sinter_ana_pv_.sumMix_Dry_PV * 100;      //混合料--燃料带入固定碳含量
                MC_SINCAL_RESULT_PV_.SINCAL_NON_FE_PV_SIO2 = sinter_ana_pv_.sumOther_SiO2_PV / sinter_ana_pv_.sumMix_Dry_PV * 100;  //混合料--非铁料带入的SiO2含量量
                MC_SINCAL_RESULT_PV_.SINCAL_DRY_MIX_PV = sinter_ana_pv_.sumMix_Dry_PV;                           //实际下料量计算总干料量
                MC_SINCAL_RESULT_PV_.SINCAL_OUTPUT_PV = sinter_ana_pv_.sumOutput_PV / 60;                       //实际下料量计算的每分钟理论产量(单位：t/min)
                MC_SINCAL_RESULT_PV_.SINCAL_FUEL_CON_PV = sinter_ana_pv_.FuelCon_PV;                              //实际下料量计算的理论燃耗(干基)（单位：kg/t）
                MC_SINCAL_RESULT_PV_.SINCAL_B_B_RATIO_PV = sinter_ana_pv_.Burn_Back_Ratio_PV;                     //实际下料计算烧返矿百分比（干） 
                MC_SINCAL_RESULT_PV_.SINCAL_BL_RATIO_PV = sinter_ana_pv_.BurnLoss_Ratio_PV;                      //实际下料计算综合烧损(%)

            }
            return MC_SINCAL_RESULT_PV_;
        }
        public void Save_Cal_min(List<M_MATERIAL_ANA> manter_ana, T_PLC_5S Modle, List<MC_SINCAL_INTERFACE_TRIG_MIN> interface_trig_min, MC_SINCAL_RESULT_SP MC_SINCAL_RESULT_SP_, MC_SINCAL_RESULT_PV MC_SINCAL_RESULT_PV_, MC_SINCAL_INTERFACE_VA MC_SINCAL_INTERFACE_VA_, MC_MICAL_RESULT MC_MICAL_RESULT_, T_MATCH_PLC_1MIN T_MATCH_PLC_1MIN_)
        {
            double FK_SUM = 0;
            double RL_SUM = 0;

            var qita = interface_trig_min.FindAll(m => m.SINCAL_COOD >= 200 && m.SINCAL_COOD <= 299);
            var qita1 = interface_trig_min.FindAll(m => m.SINCAL_COOD >= 300 && m.SINCAL_COOD <= 399);
            var qita2 = interface_trig_min.FindAll(m => m.SINCAL_COOD >= 600 && m.SINCAL_COOD <= 699).OrderBy(M=> M.BLANKING_ID).ToList();
            if (qita1.Count > 0)
            {
                for (int I = 0; I < qita1.Count; I++)
                {
                    RL_SUM += qita1[I].SINCAL_ORE_DRY_BILL.ToDouble();
                }
            }
            if (qita2.Count > 0)
            {
                FK_SUM += qita2[0].SINCAL_ORE_DRY_BILL.ToDouble();
            }
            string log = "";
            DateTime datetme;
            datetme = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":00");
            MC_SINCAL_RESULT_1MIN MC_SINCAL_RESULT_1MIN_ = null;
            MC_SINCAL_RESULT_1MIN_ = new MC_SINCAL_RESULT_1MIN();
            MC_SINCAL_RESULT_1MIN_.TIMESTAMP = datetme; //interface_trig_min[0].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_1 = Modle.T_CODE_1.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_2 = Modle.T_CODE_2.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_3 = Modle.T_CODE_3.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_4 = Modle.T_CODE_4.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_5 = Modle.T_CODE_5.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_6 = Modle.T_CODE_6.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_7 = Modle.T_CODE_7.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_8 = Modle.T_CODE_8.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_9 = Modle.T_CODE_9.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_10 = Modle.T_CODE_10.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_11 = Modle.T_CODE_11.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_12 = Modle.T_CODE_12.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_13 = Modle.T_CODE_13.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_14 = Modle.T_CODE_14.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_CODE_15 = Modle.T_CODE_15.ToString();
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB01_TIME = manter_ana[0].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB02_TIME = manter_ana[1].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB03_TIME = manter_ana[2].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB04_TIME = manter_ana[3].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB05_TIME = manter_ana[4].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB06_TIME = manter_ana[5].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB07_TIME = manter_ana[6].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB08_TIME = manter_ana[7].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB09_TIME = manter_ana[8].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB10_TIME = manter_ana[9].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB11_TIME = manter_ana[10].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB12_TIME = manter_ana[11].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB13_TIME = manter_ana[12].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB14_TIME = manter_ana[13].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_PB15_TIME = manter_ana[14].TIMESTAMP;
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_1 = Modle.T_SL_1_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_2 = Modle.T_SL_2_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_3 = Modle.T_SL_3_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_4 = Modle.T_SL_4_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_5 = Modle.T_SL_5_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_6 = Modle.T_SL_6_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_7 = Modle.T_SL_7_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_8 = Modle.T_SL_8_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_9 = Modle.T_SL_9_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_10 = Modle.T_SL_10_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_11 = Modle.T_SL_11_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_12 = Modle.T_SL_12_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_13 = Modle.T_SL_13_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_14 = Modle.T_SL_14_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_15 = Modle.T_SL_15_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_16 = Modle.T_SL_16_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_17 = Modle.T_SL_17_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_18 = Modle.T_SL_18_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SL_19 = Modle.T_SL_19_5S.ToInt();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_1 = interface_trig_min[0].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_2 = interface_trig_min[1].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_3 = interface_trig_min[2].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_4 = interface_trig_min[3].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_5 = interface_trig_min[4].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_6 = interface_trig_min[5].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_7 = interface_trig_min[6].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_8 = interface_trig_min[7].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_9 = interface_trig_min[8].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_10 = interface_trig_min[9].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_11 = interface_trig_min[10].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_12 = interface_trig_min[11].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_13 = interface_trig_min[12].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_14 = interface_trig_min[13].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_15 = interface_trig_min[14].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_16 = interface_trig_min[15].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_17 = interface_trig_min[16].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_18 = interface_trig_min[17].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SP_W_19 = interface_trig_min[18].SINCAL_ORE_BK_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_1 = Modle.T_ACTUAL_W_1_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_2 = Modle.T_ACTUAL_W_2_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_3 = Modle.T_ACTUAL_W_3_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_4 = Modle.T_ACTUAL_W_4_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_5 = Modle.T_ACTUAL_W_5_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_6 = Modle.T_ACTUAL_W_6_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_7 = Modle.T_ACTUAL_W_7_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_8 = Modle.T_ACTUAL_W_8_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_9 = Modle.T_ACTUAL_W_9_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_10 = Modle.T_ACTUAL_W_10_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_11 = Modle.T_ACTUAL_W_11_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_12 = Modle.T_ACTUAL_W_12_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_13 = Modle.T_ACTUAL_W_13_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_14 = Modle.T_ACTUAL_W_14_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_15 = Modle.T_ACTUAL_W_15_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_16 = Modle.T_ACTUAL_W_16_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_17 = Modle.T_ACTUAL_W_17_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_18 = Modle.T_ACTUAL_W_18_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_ACTUAL_W_19 = Modle.T_ACTUAL_W_19_5S;
            MC_SINCAL_RESULT_1MIN_.SINCAL_RETRODICT_TIME = MC_MICAL_RESULT_.MICAL_SAM_MAT_TIME;
            MC_SINCAL_RESULT_1MIN_.SINCAL_SUM_MIX_SP = T_MATCH_PLC_1MIN_.M_TOTAL_SP_W;
            MC_SINCAL_RESULT_1MIN_.SINCAL_SUM_MIX_PV = T_MATCH_PLC_1MIN_.M_TOTAL_PV_W;
            MC_SINCAL_RESULT_1MIN_.SINCAL_BLEND_ORE_BILL_SP = interface_trig_min[0].SINCAL_ORE_SP.ToDouble();

            if (qita.Count == 1)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_01 = qita[0].SINCAL_ORE_SP.ToDouble();
            }
            else if (qita.Count == 2)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_01 = qita[0].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_02 = qita[1].SINCAL_ORE_SP.ToDouble();
            }
            else if (qita.Count == 3)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_01 = qita[0].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_02 = qita[1].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_03 = qita[2].SINCAL_ORE_SP.ToDouble();
            }
            else if (qita.Count == 4)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_01 = qita[0].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_02 = qita[1].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_03 = qita[2].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_04 = qita[3].SINCAL_ORE_SP.ToDouble();
            }
            else if (qita.Count == 5)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_01 = qita[0].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_02 = qita[1].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_03 = qita[2].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_04 = qita[3].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_05 = qita[4].SINCAL_ORE_SP.ToDouble();
            }
            else if (qita.Count == 6)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_01 = qita[0].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_02 = qita[1].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_03 = qita[2].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_04 = qita[3].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_05 = qita[4].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_06 = qita[5].SINCAL_ORE_SP.ToDouble();
            }
            else if (qita.Count == 7)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_01 = qita[0].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_02 = qita[1].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_03 = qita[2].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_04 = qita[3].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_05 = qita[4].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_06 = qita[5].SINCAL_ORE_SP.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_SP_07 = qita[6].SINCAL_ORE_SP.ToDouble();
            }
            MC_SINCAL_RESULT_1MIN_.SINCAL_BRUN_SP_08 = interface_trig_min[7].SINCAL_ORE_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_STONE_SP_09 = interface_trig_min[9].SINCAL_ORE_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_STONE_SP_10 = interface_trig_min[11].SINCAL_ORE_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_ASH_DUST_SP_11 = interface_trig_min[13].SINCAL_ORE_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_SP_12 = interface_trig_min[15].SINCAL_ORE_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_SP_13 = interface_trig_min[16].SINCAL_ORE_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_BILL_SP_14 = interface_trig_min[17].SINCAL_ORE_SP.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_BILL_SP_15 = interface_trig_min[18].SINCAL_ORE_SP.ToDouble();
            if (MC_SINCAL_INTERFACE_VA_ != null)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_C_A = MC_SINCAL_INTERFACE_VA_.SINCAL_C_A;
                MC_SINCAL_RESULT_1MIN_.SINCAL_R_A = MC_SINCAL_INTERFACE_VA_.SINCAL_R_A;
                MC_SINCAL_RESULT_1MIN_.SINCAL_R_C = MC_SINCAL_INTERFACE_VA_.SINCAL_R_C;
                MC_SINCAL_RESULT_1MIN_.SINCAL_C_DC = MC_SINCAL_INTERFACE_VA_.SINCAL_C_DC;
            }

            ////////////////////////////////////////////////
            MC_SINCAL_RESULT_1MIN_.SINCAL_BLEND_ORE_BILL_PV = interface_trig_min[0].SINCAL_ORE_PV.ToDouble();
            if (qita.Count == 1)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_01 = qita[0].SINCAL_ORE_PV.ToDouble();
            }
            else if (qita.Count == 2)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_01 = qita[0].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_02 = qita[1].SINCAL_ORE_PV.ToDouble();
            }
            else if (qita.Count == 3)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_01 = qita[0].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_02 = qita[1].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_03 = qita[2].SINCAL_ORE_PV.ToDouble();
            }
            else if (qita.Count == 4)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_01 = qita[0].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_02 = qita[1].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_03 = qita[2].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_04 = qita[3].SINCAL_ORE_PV.ToDouble();
            }
            else if (qita.Count == 5)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_01 = qita[0].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_02 = qita[1].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_03 = qita[2].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_04 = qita[3].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_05 = qita[4].SINCAL_ORE_PV.ToDouble();
            }
            else if (qita.Count == 6)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_01 = qita[0].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_02 = qita[1].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_03 = qita[2].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_04 = qita[3].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_05 = qita[4].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_06 = qita[5].SINCAL_ORE_PV.ToDouble();
            }
            else if (qita.Count == 7)
            {
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_01 = qita[0].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_02 = qita[1].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_03 = qita[2].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_04 = qita[3].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_05 = qita[4].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_06 = qita[5].SINCAL_ORE_PV.ToDouble();
                MC_SINCAL_RESULT_1MIN_.SINCAL_BILL_ORE_PV_07 = qita[6].SINCAL_ORE_PV.ToDouble();
            }

            MC_SINCAL_RESULT_1MIN_.SINCAL_BRUN_PV_08 = interface_trig_min[7].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_STONE_PV_09 = interface_trig_min[9].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_STONE_PV_10 = interface_trig_min[11].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_ASH_DUST_PV_11 = interface_trig_min[13].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_PV_12 = interface_trig_min[15].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FLUX_PV_13 = interface_trig_min[16].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_BILL_PV_14 = interface_trig_min[17].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_BILL_PV_15 = interface_trig_min[18].SINCAL_ORE_PV.ToDouble();
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_SPARE1 = RL_SUM;
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_SPARE2 = FK_SUM;


            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_TFE = Math.Round(MC_SINCAL_RESULT_SP_.SP_TFE, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_FEO = Math.Round(MC_SINCAL_RESULT_SP_.SP_FeO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_CAO =  Math.Round(MC_SINCAL_RESULT_SP_.SP_CaO,3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_SIO2 = Math.Round(MC_SINCAL_RESULT_SP_.SP_SiO2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_AL2O3 = Math.Round(MC_SINCAL_RESULT_SP_.SP_Al2O3, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_MGO = Math.Round(MC_SINCAL_RESULT_SP_.SP_MgO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_S = Math.Round(MC_SINCAL_RESULT_SP_.SP_S, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_P = Math.Round(MC_SINCAL_RESULT_SP_.SP_P, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_MN = Math.Round(MC_SINCAL_RESULT_SP_.SP_Mn, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_R = Math.Round(MC_SINCAL_RESULT_SP_.SP_R, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_TIO2 = Math.Round(MC_SINCAL_RESULT_SP_.SP_TiO2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_K2O = Math.Round(MC_SINCAL_RESULT_SP_.SP_K2O, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_NA2O = Math.Round(MC_SINCAL_RESULT_SP_.SP_Na2O, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_PBO = Math.Round(MC_SINCAL_RESULT_SP_.SP_PbO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_ZNO = Math.Round(MC_SINCAL_RESULT_SP_.SP_ZnO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_TI = Math.Round(MC_SINCAL_RESULT_SP_.SP_F, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_AS = Math.Round(MC_SINCAL_RESULT_SP_.SP_As, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_CU = Math.Round(MC_SINCAL_RESULT_SP_.SP_Cu, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_PB = Math.Round(MC_SINCAL_RESULT_SP_.SP_Pb, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_ZN = Math.Round(MC_SINCAL_RESULT_SP_.SP_Zn, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_K = Math.Round(MC_SINCAL_RESULT_SP_.SP_K, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_NA = Math.Round(MC_SINCAL_RESULT_SP_.SP_Na, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_CR = Math.Round(MC_SINCAL_RESULT_SP_.SP_Cr, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_SP_NI = Math.Round(MC_SINCAL_RESULT_SP_.SP_Ni, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_SP_LOT = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_LOT, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_SP_H2O_1 = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_H2O_1, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_SP_H2O_2 = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_H2O_2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_SP_FEO = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_FEO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_SP_C = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_MIX_SP_C, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_NON_FUEL_SP_C = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_NON_FUEL_SP_C, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_SP_C = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_FUEL_SP_C, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_NON_FE_SP_SIO2 = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_NON_FE_SP_SIO2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_DRY_MIX_SP = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_DRY_MIX_SP, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_OUTPUT_SP = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_OUTPUT_SP, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_CON_SP = Math.Round(MC_SINCAL_RESULT_SP_.SINCAL_FUEL_CON_SP, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_TFE = Math.Round(MC_SINCAL_RESULT_PV_.PV_TFE, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_FEO = Math.Round(MC_SINCAL_RESULT_PV_.PV_FeO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_CAO = Math.Round(MC_SINCAL_RESULT_PV_.PV_CaO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_SIO2 = Math.Round(MC_SINCAL_RESULT_PV_.PV_SiO2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_AL2O3 = Math.Round(MC_SINCAL_RESULT_PV_.PV_Al2O3, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_MGO = Math.Round(MC_SINCAL_RESULT_PV_.PV_MgO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_S = Math.Round(MC_SINCAL_RESULT_PV_.PV_S, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_P = Math.Round(MC_SINCAL_RESULT_PV_.PV_P, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_MN = Math.Round(MC_SINCAL_RESULT_PV_.PV_Mn, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_R = Math.Round(MC_SINCAL_RESULT_PV_.PV_R, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_TIO2 = Math.Round(MC_SINCAL_RESULT_PV_.PV_TiO2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_K2O = Math.Round(MC_SINCAL_RESULT_PV_.PV_K2O, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_NA2O = Math.Round(MC_SINCAL_RESULT_PV_.PV_Na2O, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_PBO = Math.Round(MC_SINCAL_RESULT_PV_.PV_PbO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_ZNO = Math.Round(MC_SINCAL_RESULT_PV_.PV_ZnO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_TI = Math.Round(MC_SINCAL_RESULT_PV_.PV_F, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_AS = Math.Round(MC_SINCAL_RESULT_PV_.PV_As, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_CU = Math.Round(MC_SINCAL_RESULT_PV_.PV_Cu, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_PB = Math.Round(MC_SINCAL_RESULT_PV_.PV_Pb, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_ZN = Math.Round(MC_SINCAL_RESULT_PV_.PV_Zn, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_K = Math.Round(MC_SINCAL_RESULT_PV_.PV_K, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_NA = Math.Round(MC_SINCAL_RESULT_PV_.PV_Na, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_CR = Math.Round(MC_SINCAL_RESULT_PV_.PV_Cr, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_SIN_PV_NI = Math.Round(MC_SINCAL_RESULT_PV_.PV_Ni, 3);

            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_PV_LOT = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_LOT, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_PV_H2O_1 = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_H2O_1, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_PV_H2O_2 = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_H2O_2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_PV_FEO = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_FEO, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_MIX_PV_C = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_MIX_PV_C, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_NON_FUEL_PV_C = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_NON_FUEL_PV_C, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_PV_C = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_FUEL_PV_C, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_NON_FE_PV_SIO2 = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_NON_FE_PV_SIO2, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_DRY_MIX_PV = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_DRY_MIX_PV, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_OUTPUT_PV = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_OUTPUT_PV, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_FUEL_CON_PV = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_FUEL_CON_PV, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_B_B_RATIO_PV = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_B_B_RATIO_PV, 3);
            MC_SINCAL_RESULT_1MIN_.SINCAL_BL_RATIO_PV = Math.Round(MC_SINCAL_RESULT_PV_.SINCAL_BL_RATIO_PV, 3);
            if (Set_Admin_add(MC_SINCAL_RESULT_1MIN_))
            {
                log = "保存完成";
            }
            else
            {
                log = "保存失败";
            }
            Outputlog(log);
        }
        public bool Set_Admin_add(MC_SINCAL_RESULT_1MIN model)
        {
            bool listmodel = true;
        
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
    }
    /// <summary>
    /// 成分设定值变量
    /// </summary>
    public class sinter_ana
    {
        public double sumTFe = 0;
        public double sumFeO = 0;
        public double sumCaO = 0;
        public double sumSiO2 = 0;
        public double sumAl2O3 = 0;
        public double sumMgO = 0;
        public double sumS = 0;
        public double sumP = 0;
        public double sumC = 0;
        public double sumMn = 0;
        public double sumLOT = 0;//总烧损
        public double sumR = 0;
        public double sumH2O_1 = 0;//采用检化验检测水分计算的混合料初始含水量
        public double sumH2O_2 = 0;//采用人工设定水分计算的混合料初始含水量
        public double sumAsH = 0;
        public double sumVOL = 0;
        public double sumTiO2 = 0;
        public double sumK2O = 0;
        public double sumNa2O = 0;
        public double sumPbO = 0;
        public double sumZnO = 0;
        public double sumF = 0;
        public double sumAs = 0;
        public double sumCu = 0;
        public double sumPb = 0;
        public double sumZn = 0;
        public double sumK = 0;
        public double sumNa = 0;
        public double sumCr = 0;
        public double sumNi = 0;

        public double sumMix_Wet_SP = 0;     //设定总湿料量
        public double sumMix_Dry_SP = 0;     //设定总干料量
        public double sumRemnant_SP = 0;     //设定总残存量
        public double sumOther_SiO2_SP = 0;  //除混匀矿和直供料外，其他原料带入的SiO2量
        public double sumOther_C_SP = 0;     //非燃料总含碳量
        public double sumFuel_C_SP = 0;      //燃料带入的总固定碳量	
        public double sumOutput_SP = 0;      //设定下料量计算的理论产量(单位：t/h)
        public double sumFuel_SP = 0;        //燃料总设定干下料量
        public double FuelCon_SP = 0;        //设定下料量计算的理论燃耗
        public double m_SP = 2.9;            //C与FeO对应比例系数，需要现场数据分析进行修改
    }
    /// <summary>
    /// 成分当前值变量
    /// </summary>
    public class sinter_ana_pv
    {
       public double sumTFe = 0;
       public double sumFeO = 0;
       public double sumCaO = 0;
       public double sumSiO2 = 0;
       public double sumAl2O3 = 0;
       public double sumMgO = 0;
       public double sumS = 0;
       public double sumP = 0;
       public double sumC = 0;//混合料实际含碳
       public double sumMn = 0;
       public double sumLOT = 0;//总烧损
       public double sumR = 0;
       public double sumH2O_1 = 0;//采用检化验检测水分计算的混合料初始含水量
       public double sumH2O_2 = 0;//采用人工设定水分计算的混合料初始含水量
       public double  sumAsH = 0;
       public double  sumVOL = 0;
       public double  sumTiO2 = 0;
       public double  sumK2O = 0;
       public double  sumNa2O = 0;
       public double  sumPbO = 0;
       public double  sumZnO = 0;
       public double  sumF = 0;
       public double  sumAs = 0;
       public double  sumCu = 0;
       public double  sumPb = 0;
       public double  sumZn = 0;
       public double  sumK = 0;
       public double  sumNa = 0;
       public double  sumCr = 0;
       public double  sumNi = 0;

       public double  sumMix_Wet_PV = 0;      //实际总料量（湿）	
       public double  sumMix_Dry_PV = 0;      //实际总料量（干）
       public double  sumRemnant_PV = 0;       //实际总残存量
       public double  sumOther_SiO2_PV = 0;   //除混匀矿和直供料外，其他原料带入的SiO2量
       public double  sumOther_C_PV = 0;      //非燃料总含碳量
       public double  sumFuel_C_PV = 0;       //燃料带入的实际固定碳量
       public double  sumOutput_PV = 0;       //实际下料量计算理论产量(单位：t/h)
       public double  sumFuel_PV = 0;         //燃料总实际干下料量
       public double  FuelCon_PV = 0;         //实际下料量计算的理论燃耗
       public double  m_PV = 2.9;             //C与FeO对应比例系数，需要现场数据分析进行修改
       public double  sumDryBill = 0;         //当前干配比和
       public double  FeO_sp = 8.5;           //FeO设定值用于理论产量计算
       public double  Burn_Back_Ratio_PV = 0; //实际下料计算烧返矿百分比（干） 
       public double BurnLoss_Ratio_PV = 0;  //实际下料计算综合烧损(%)
    }
}

