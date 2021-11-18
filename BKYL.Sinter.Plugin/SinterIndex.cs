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
    /// <summary>
    /// 主窗体数据信息倒推   周云恒2019
    /// </summary>
    public class SinterIndex : BaseJob
    {

        public override void InitTask()
        {
            //加载配置

        }
        DataTable data = new DataTable();
        ///// <summary>
        ///// 14行数据的目标moudel
        ///// </summary>
        //BKYL.Sinter.Plugin.Models.MC_MICAL_RESULT modelTag = null;// new MC_MICAL_RESULT();
        /// <summary>
        /// 皮带的长度和运转时间信息model
        /// </summary>
        BKYL.Sinter.Plugin.Models.MC_MICAL_PAR modelPiDaiInfo = null;
        /// <summary>
        /// 皮带的启动停机时间model列表信息
        /// </summary>
        List<BKYL.Sinter.Plugin.Models.MC_STOP_START_Time> listStartStopTime = null;

        /// <summary>
        /// 设备的运行时间model列表信息
        /// </summary>
        List<BKYL.Sinter.Plugin.Models.MC_RUN_START_TIME> listRUNTIME = null;

        /// <summary>
        /// 获取某个设备的某个时间端的停机时间（单位是分钟）
        /// </summary>
        /// <param name="dTime1Max">大的时间</param>
        /// <param name="dTime2Min">小的时间</param>
        /// <param name="InstaCode">设备编码</param>
        /// <param name="iDataBase"></param>
        public double GetMC_STOP_START_Time(DateTime dTime1Max, DateTime dTime2Min, string InstaCode, IDataBase iDataBase)
        {
            if (listStartStopTime == null)
            {
                listStartStopTime = new List<MC_STOP_START_Time>();
            }
            else
            {
                listStartStopTime.Clear();
            }
            //string sqltime = string.Format("select timestamp,InstaCode,Stop_BeginTime,Stop_EndTime,INTERVAL_TIME from MC_STOP_START_Time ");
            //sqltime += string.Format(" where Stop_EndTime <= to_date('{0}', 'yyyy-MM-dd HH24:mi:ss')", dTime1.ToString());
            //sqltime += string.Format(" and Stop_EndTime >= to_date('{0}', 'yyyy-MM-dd HH24:mi:ss')", dTime2.ToString());

            listStartStopTime = iDataBase.Queryable<MC_STOP_START_Time>().Where(m => (m.STOP_ENDTIME <= dTime1Max && m.STOP_ENDTIME >= dTime2Min && m.INSTA_CODE == InstaCode)).ToList();
            double tingjiTime = 0;
            foreach (var item in listStartStopTime)
            {
                tingjiTime += item.INTERVAL_TIME;
            }
            return tingjiTime;
            //sqlCol = "timestamp,SINCAL_MIX_PV_FeO,SINCAL_SIN_PV_SIO2,SINCAL_SIN_PV_MGO,SINCAL_SIN_PV_AL2O3,SINCAL_SIN_PV_R,SINCAL_MIX_PV_C";
            //sqlstr = GetSql(sqlCol, "MC_SINCAL_result_1min", dTime);
            //data = iDataBase.GetDataTable(sqlstr);
        }

        /// <summary>
        /// 获取某个设备的某个时间端的停机时间（单位是分钟）
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="InstaCode"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public double GetMC_STOP_START_Time0(DateTime dTime, string InstaCode, IDataBase iDataBase)
        {
            if (listStartStopTime == null)
            {
                listStartStopTime = new List<MC_STOP_START_Time>();
            }
            else
            {
                listStartStopTime.Clear();
            }
            //string sqltime = string.Format("select timestamp,InstaCode,Stop_BeginTime,Stop_EndTime,INTERVAL_TIME from MC_STOP_START_Time ");
            //sqltime += string.Format(" where Stop_EndTime <= to_date('{0}', 'yyyy-MM-dd HH24:mi:ss')", dTime1.ToString());
            //sqltime += string.Format(" and Stop_EndTime >= to_date('{0}', 'yyyy-MM-dd HH24:mi:ss')", dTime2.ToString());
            listStartStopTime = iDataBase.Queryable<MC_STOP_START_Time>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.STOP_BEGINTIME && (m.STOP_ENDTIME == null ? true : dTime <= m.STOP_ENDTIME))).ToList();


            double tingjiTime = 0;
            foreach (var item in listStartStopTime)
            {
                if (item.STOP_ENDTIME == null)
                {
                    TimeSpan ts = dTime - item.STOP_BEGINTIME;
                    double diffMinutes = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + (double)ts.Seconds / 60;
                    tingjiTime += diffMinutes;
                }
                else
                {
                    tingjiTime += item.INTERVAL_TIME;
                }
            }
            return tingjiTime;
            //sqlCol = "timestamp,SINCAL_MIX_PV_FeO,SINCAL_SIN_PV_SIO2,SINCAL_SIN_PV_MGO,SINCAL_SIN_PV_AL2O3,SINCAL_SIN_PV_R,SINCAL_MIX_PV_C";
            //sqlstr = GetSql(sqlCol, "MC_SINCAL_result_1min", dTime);
            //data = iDataBase.GetDataTable(sqlstr);
        }
        /// <summary>
        /// 获取某个设备运行开始时间 的时间
        /// </summary>
        /// <param name="dTime">运行开始时间</param>
        /// <param name="InstaCode">设备编码</param>
        /// <param name="iDataBase"></param>
        public string GetMC_RUN_START_Time_Str0(DateTime dTime, string InstaCode, IDataBase iDataBase)
        {
            if (listRUNTIME == null)
            {
                listRUNTIME = new List<MC_RUN_START_TIME>();
            }
            else
            {
                listRUNTIME.Clear();
            }
            string strDTime = "";
            //listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME && (m.RUN_ENDTIME == null ? true : dTime <= m.RUN_ENDTIME))).ToList();

            listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode)).OrderBy(M => M.RUN_BEGINTIME, BFES.DataAccess.Common.OrderByType.Desc).ToList();
            listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => ((m.INSTA_CODE == InstaCode) && (dTime >= m.RUN_BEGINTIME) && (m.RUN_ENDTIME == null || dTime <= m.RUN_ENDTIME))).ToList();
            //listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME && (m.RUN_ENDTIME == null ? true : dTime <= m.RUN_ENDTIME))).ToList();
            if (listRUNTIME != null && listRUNTIME.Count > 0)
            {
                strDTime = dTime.ToString();
            }
            else
            {
                string strSQL = string.Format("select max(RUN_ENDTIME) from MC_RUN_START_TIME where RUN_ENDTIME <= to_date('{0}', 'yyyy-MM-dd HH24:mi:ss')", dTime.ToString());

                string Temp = iDataBase.GetString(strSQL);
                if (!string.IsNullOrEmpty(Temp))
                {
                    DateTime strtimestamp = Convert.ToDateTime(Temp);
                    strDTime = strtimestamp.ToString();
                }

                //listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && m.RUN_ENDTIME == strtimestamp)).ToList();

            }
            return strDTime;
        }
        /// <summary>
        /// 任意一个设备 有停机的标志
        /// </summary>
        bool isStopHost = false;
        /// <summary>
        /// 获取某个设备运行开始时间到运行时长所倒推的时间
        /// </summary>
        /// <param name="dTime">运行开始时间</param>
        /// <param name="InstaCode">设备编码</param>
        /// <param name="iDataBase"></param>
        /// <param name="shichang">时长</param>
        public string GetMC_RUN_START_Time_Str(DateTime dTime, string InstaCode, IDataBase iDataBase, double shichang, MC_MICAL_RESULT modelTag)
        {
            //if (listStartStopTime == null)
            //{
            //    listStartStopTime = new List<MC_STOP_START_Time>();
            //}
            //else
            //{
            //    listStartStopTime.Clear();
            //}
            bool isRunState = false;
            try
            {

                if (listRUNTIME == null)
                {
                    listRUNTIME = new List<MC_RUN_START_TIME>();
                }
                else
                {
                    listRUNTIME.Clear();
                }
                listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode)).OrderBy(M => M.RUN_BEGINTIME, BFES.DataAccess.Common.OrderByType.Desc).ToList();
                if (listRUNTIME != null && listRUNTIME.Count > 0)
                {
                    int num = 0;
                    foreach (var item in listRUNTIME)
                    {
                        num++;
                        if (item.RUN_BEGINTIME.Value > dTime)
                        {
                            continue;
                        }
                        if (num == 1)
                        {
                            if (item.RUN_ENDTIME == null || item.RUN_ENDTIME.ToString().Trim() == "")
                            {
                                TimeSpan ts = dTime - item.RUN_BEGINTIME.Value;
                                double diffMinutes = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + (double)ts.Seconds / 60;
                                if (diffMinutes >= shichang)
                                {
                                    isRunState = true;
                                    return dTime.AddMinutes(0 - shichang).ToString();
                                }
                                else
                                {
                                    shichang = shichang - diffMinutes;
                                    //dTime = item.RUN_BEGINTIME.Value;
                                }
                            }
                            else
                            {
                                DateTime dtENDTIME = item.RUN_ENDTIME.Value;
                                if (dTime > item.RUN_ENDTIME.Value)
                                {
                                    dtENDTIME = item.RUN_ENDTIME.Value;
                                }
                                else if (dTime >= item.RUN_BEGINTIME.Value && dTime <= item.RUN_ENDTIME.Value)
                                {
                                    dtENDTIME = dTime;
                                }
                                else if (dTime < item.RUN_BEGINTIME.Value)
                                {
                                    continue;
                                }
                                TimeSpan ts = dtENDTIME - item.RUN_BEGINTIME.Value;
                                double diffMinutes = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + (double)ts.Seconds / 60;
                                if (diffMinutes >= shichang)
                                {
                                    return dtENDTIME.AddMinutes(0 - shichang).ToString();
                                }
                                else
                                {
                                    shichang = shichang - diffMinutes;
                                    //dTime = item.RUN_BEGINTIME.Value;
                                }
                            }
                        }
                        else
                        {
                            if (item.RUN_ENDTIME == null || item.RUN_ENDTIME.ToString().Trim() == "")
                            {
                                item.RUN_ENDTIME = listRUNTIME[num - 1 - 1].RUN_BEGINTIME;
                            }
                            DateTime dtENDTIME = item.RUN_ENDTIME.Value;
                            if (dTime > item.RUN_ENDTIME.Value)
                            {
                                dtENDTIME = item.RUN_ENDTIME.Value;
                            }
                            else if (dTime >= item.RUN_BEGINTIME.Value && dTime <= item.RUN_ENDTIME.Value)
                            {
                                dtENDTIME = dTime;
                            }
                            else if (dTime < item.RUN_BEGINTIME.Value)
                            {
                                continue;
                            }
                            TimeSpan ts = dtENDTIME - item.RUN_BEGINTIME.Value;
                            double diffMinutes = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + (double)ts.Seconds / 60;
                            if (diffMinutes >= shichang)
                            {
                                return dtENDTIME.AddMinutes(0 - shichang).ToString();
                            }
                            else
                            {
                                shichang = shichang - diffMinutes;
                                //dTime = item.RUN_BEGINTIME.Value;
                            }
                        }
                    }
                    return "";

                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                if (isRunState == false)
                {
                    isStopHost = true;
                    modelTag.MICAL_M_STATE_FLAG = 2;
                }
            }
            return "";

            //listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME && m.RUN_ENDTIME == null)).OrderBy(M => M.RUN_BEGINTIME).ToList();
            //if (listRUNTIME == null || listRUNTIME.Count == 0)
            //{
            //    listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME && dTime <= m.RUN_ENDTIME)).OrderBy(M => M.RUN_BEGINTIME).ToList();
            //    if (listRUNTIME == null || listRUNTIME.Count == 0)
            //    {
            //        listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME && dTime <= m.RUN_ENDTIME)).OrderBy(M => M.RUN_BEGINTIME).ToList();
            //    }
            //}
            ////listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME && (m.RUN_ENDTIME == null ? true : dTime <= m.RUN_ENDTIME))).OrderBy(M => M.RUN_BEGINTIME).ToList();
            //listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME && (m.RUN_ENDTIME == null || dTime <= m.RUN_ENDTIME))).OrderBy(M => M.RUN_BEGINTIME).ToList();
            //listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME)).OrderBy(M => M.RUN_BEGINTIME).ToList();

            ////listRUNTIME = iDataBase.Queryable<MC_RUN_START_TIME>().Where(m => (m.INSTA_CODE == InstaCode && dTime >= m.RUN_BEGINTIME)).OrderBy(M => M.RUN_BEGINTIME).ToList();

        }
        /// <summary>
        /// 数据5分钟的平均值
        /// </summary>
        int MinuteAvg = 5;
        string sqlstr = "";
        string sqlCol = "";
        T_PLC_5S t_PLC_5S = new T_PLC_5S();
        DateTime LastDateTime2 = DateTime.MinValue;
        public override void RunTask(DateTime nowTime)
        {
            ////MC_MICAL_RESULT

            //BFES.Models.AV05M_LININGTC_ALARMINFO
            string log = "";
            try
            {
                //nowTime = DateTime.Parse("2019/4/22 15:57:00");

                ////源数据库 表
                //using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                //{
                //    // 数据库查询
                //}

                //sqlstr = string.Format("select * from MC_SINCAL_INTERFACE_TRIG");
                //sqlstr = string.Format("select * from MC_SINCAL_INTERFACE_VA");

                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {
                        InitModelPiDaiInfo(iDataBase);

                        double par_T1 = modelPiDaiInfo.PAR_T1.Value;//运转周期

                        string str_sql000 = string.Format("select max(timestamp) from MC_MICAL_RESULT where datanum=14 ");//获取最新的 
                        string maxTime = iDataBase.GetString(str_sql000);//
                        if (!string.IsNullOrEmpty(maxTime))
                        {
                            DateTime.TryParse(maxTime, out LastDateTime2);
                        }

                        //维护修改一下运转周期
                        if (LastDateTime2.AddMinutes(par_T1) < DateTime.Now)
                        {
                            LastDateTime2 = DateTime.Now;


                            //烧结机和环冷机同时停下（表T_PLC_5S，字段(T_SIN_SL_5S+T_RC_SL_5S) ＜ 1），为停机状态，程序停止计算
                            string str_sql = string.Format("select * from T_PLC_5S where TIMESTAMP =(select max(timestamp) from T_PLC_5S) ");//获取最新的 
                            t_PLC_5S = iDataBase.GetList<T_PLC_5S>(str_sql).FirstOrDefault();//

                            //str_sql = string.Format("select * from MC_MICAL_RESULT where DATANUM=14 and TIMESTAMP =(select max(timestamp) from MC_MICAL_RESULT) ");//获取最新的 
                            //MC_MICAL_RESULT ssss = iDataBase.GetList<MC_MICAL_RESULT>(str_sql).FirstOrDefault();//
                            //double ddddd= ssss.MICAL_BL_BUNK_TIME.Value;

                            MC_MICAL_RESULT modelTag14 = null;
                            MC_MICAL_RESULT modelTag13 = null;
                            MC_MICAL_RESULT modelTag12 = null;
                            MC_MICAL_RESULT modelTag11 = null;
                            MC_MICAL_RESULT modelTag10 = null;

                            MC_MICAL_RESULT modelTag9 = null;
                            MC_MICAL_RESULT modelTag8 = null;
                            MC_MICAL_RESULT modelTag7 = null;
                            MC_MICAL_RESULT modelTag6 = null;
                            MC_MICAL_RESULT modelTag5 = null;

                            MC_MICAL_RESULT modelTag4 = null;
                            MC_MICAL_RESULT modelTag3 = null;
                            MC_MICAL_RESULT modelTag2 = null;
                            MC_MICAL_RESULT modelTag1 = null;


                            //InitListStartStopTime(iDataBase);

                            //modelTag = new MC_MICAL_RESULT();
                            // 数据库查询
                            DaoTui14_Quyangdian(iDataBase, nowTime, out modelTag14);//倒推14数据
                            DaoTui13_Shaifen(iDataBase, nowTime, out modelTag13);//倒推13数据
                            DaoTui12_Bsgkj(iDataBase, nowTime, out modelTag12);
                            DaoTui11_Hlrk(iDataBase, nowTime, out modelTag11);
                            DaoTui10_Fengxiang19_21(iDataBase, nowTime, out modelTag10);
                            DaoTui9_Fengxiang16_18(iDataBase, nowTime, out modelTag9);
                            DaoTui8_Fengxiang13_15(iDataBase, nowTime, out modelTag8);
                            DaoTui7_Fengxiang10_12(iDataBase, nowTime, out modelTag7);
                            DaoTui6_Fengxiang7_9(iDataBase, nowTime, out modelTag6);
                            DaoTui5_Fengxiang4_6(iDataBase, nowTime, out modelTag5);
                            DaoTui4_Fengxiang1_3(iDataBase, nowTime, out modelTag4);
                            DaoTui3_Buliao(iDataBase, nowTime, out modelTag3);
                            DaoTui2_ErHun(iDataBase, nowTime, out modelTag2);
                            DaoTui1_YiHun(iDataBase, nowTime, out modelTag1);

                            try
                            {
                                //存库
                                iDataBase.BeginTran();
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag14);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag13);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag12);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag11);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag10);

                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag9);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag8);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag7);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag6);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag5);

                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag4);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag3);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag2);
                                iDataBase.Insert<MC_MICAL_RESULT>(modelTag1);
                                iDataBase.CommitTran();
                            }
                            catch (Exception ex)
                            {
                                iDataBase.RollbackTran();
                                log = ex.Message;
                                Outputlog(log);
                                Writelog(ex.Message + ex.StackTrace);
                            }


                        }



                    }
                    catch (Exception ex)
                    {
                        log = ex.Message;
                        Outputlog(log);
                        Writelog(ex.Message + ex.StackTrace);
                    }
                    finally
                    {
                        iDataBase.Dispose();
                    }

                }
            }
            catch (Exception ee)
            {
                Outputlog(ee.Message);
                Writelog(ee.Message + ee.StackTrace);
                throw ee;
            }
            finally
            {
                //释放资源
                // log = null;
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
        //public List<MC_SINCAL_INTERFACE_TRIG> Get_MC_SINCAL_INTERFACE_TRIG()
        //{
        //    using (var iDataBase = DataBaseFactory.NewConnection)
        //    {
        //        string strSQL = "select max(timestamp)  from MC_SINCAL_INTERFACE_TRIG";
        //        string Temp = iDataBase.GetString(strSQL);
        //        DateTime strtimestamp = Convert.ToDateTime(Temp);
        //        return iDataBase.Queryable<MC_SINCAL_INTERFACE_TRIG>().Where(m => m.TIMESTAMP == strtimestamp).OrderBy(M => M.ID).ToList();
        //    }
        //}

        /// <summary>
        /// 初始化 皮带的长度和运转时间信息model
        /// </summary>
        /// <returns></returns>
        private void InitModelPiDaiInfo(IDataBase iDataBase)
        {
            modelPiDaiInfo = new Models.MC_MICAL_PAR();
            modelPiDaiInfo = iDataBase.Queryable<MC_MICAL_PAR>().FirstOrDefault();
            //using (var iDataBase = DataBaseFactory.NewConnection)
            //{
            //    modelPiDaiInfo = iDataBase.Queryable<MC_MICAL_PAR>().FirstOrDefault();

            //}

            //string sql = "select * from MC_MICAL_PAR";
            //data = iDataBase.GetDataTable(sql);
            //if (data != null && data.Rows.Count > 0)
            //{
            //    var row = data.Rows[0];
            //    if (row != null)
            //    {
            //        //if (row["SINCAL_MIX_PV_C"] != null && row["SINCAL_MIX_PV_C"].ToString() != "")
            //        //{
            //        //    modelPiDaiInfo.PAR_LENGTH_1H_1 = double.Parse(row["SINCAL_MIX_PV_C"].ToString());
            //        //}
            //    }
            //}
        }
        /// <summary>
        /// 获取sql脚本
        /// </summary>
        /// <param name="sqlcol"></param>
        /// <param name="tabName"></param>
        /// <param name="dTime"></param>
        /// <returns></returns>
        private string GetSql(string sqlcol, string tabName, DateTime dTime)
        {

            //select "AVG"(BED_MATERAL_AO_FT) as  ,"AVG"(COLD_AO_FT),"AVG"(FP_BELT_FT_AVG) from T_BURN_COLE_PLC_1MIN where timestamp <=
            //(select max(timestamp) from T_BURN_COLE_PLC_1MIN where timestamp <= to_date('2019-01-28 04:00:01', 'yyyy-MM-dd HH24:mi:ss'))

            string sqltime = string.Format("select max(timestamp) from {0} where timestamp <= to_date('{1}', 'yyyy-MM-dd HH24:mi:ss')", tabName, dTime.ToString());

            return string.Format("select {0} from {1} where timestamp=({2})", sqlcol, tabName, sqltime);

        }
        /// <summary>
        /// 求平均的
        /// </summary>
        /// <param name="sqlcol"></param>
        /// <param name="tabName"></param>
        /// <param name="dTime">时间</param>
        /// <param name="minuteAvg">分钟的平均值</param>
        /// <returns></returns>
        private string GetSql_AVG(string sqlcol, string tabName, DateTime dTime, int minuteAvg)
        {
            //select AVG(BED_MATERAL_AO_FT) as BED_MATERAL_AO_FT,"AVG"(COLD_AO_FT),"AVG"(FP_BELT_FT_AVG)  from T_BURN_COLE_PLC_1MIN
            //where timestamp <= to_date('2019-01-28 04:00:01', 'yyyy-MM-dd HH24:mi:ss')
            //and timestamp>= (to_date('2019-01-28 04:00:01', 'yyyy-MM-dd HH24:mi:ss') - 5 / (24 * 60))

            string sqlStr = string.Format("select {0} from {1} ", sqlcol, tabName);
            sqlStr = string.Format("{0} where timestamp <= to_date('{1}', 'yyyy-MM-dd HH24:mi:ss') ", sqlStr, dTime);
            sqlStr = string.Format("{0} and timestamp>= (to_date('{1}', 'yyyy-MM-dd HH24:mi:ss') - {2} / (24 * 60)) ", sqlStr, dTime, minuteAvg);

            return sqlStr;

        }
        /// <summary>
        /// 配比的数据信息
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL0_PeiBi(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_MATCH_FeO 配料混合料FeO    采集值 MC_SINCAL_result_1min   SINCAL_MIX_PV_FeO
            //MICAL_MATCH_SiO2    配料混合料SiO2 采集值 MC_SINCAL_result_1min SINCAL_SIN_PV_SIO2
            //MICAL_MATCH_MGO 配料混合料MgO    采集值 MC_SINCAL_result_1min   SINCAL_SIN_PV_MGO
            //MICAL_MATCH_AL2O3   配料混合料Al2O3 采集值 MC_SINCAL_result_1min SINCAL_SIN_PV_AL2O3
            //MICAL_MATCH_R 配料混合料碱度               MC_SINCAL_result_1min SINCAL_SIN_PV_R
            //MICAL_MATCH_C	配料混合料含碳		MC_SINCAL_result_1min	SINCAL_MIX_PV_C



            modelTag.MICAL_MATCH_TIME = dTime;


            //sqlCol = "timestamp,SINCAL_MIX_PV_FeO,SINCAL_SIN_PV_SIO2,SINCAL_SIN_PV_MGO,SINCAL_SIN_PV_AL2O3,SINCAL_SIN_PV_R,SINCAL_MIX_PV_C";
            //sqlstr = GetSql(sqlCol, "MC_SINCAL_result_1min", dTime);
            sqlCol = "round(AVG(SINCAL_MIX_PV_FeO),5) as SINCAL_MIX_PV_FeO,round(AVG(SINCAL_SIN_PV_SIO2),5) as SINCAL_SIN_PV_SIO2,round(AVG(SINCAL_SIN_PV_MGO),5) as SINCAL_SIN_PV_MGO,round(AVG(SINCAL_SIN_PV_AL2O3),5) as SINCAL_SIN_PV_AL2O3,round(AVG(SINCAL_SIN_PV_R),5) as SINCAL_SIN_PV_R,round(AVG(SINCAL_MIX_PV_C),5) as SINCAL_MIX_PV_C";
            sqlstr = GetSql_AVG(sqlCol, "MC_SINCAL_result_1min", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["SINCAL_MIX_PV_FeO"] != null && row["SINCAL_MIX_PV_FeO"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_FEO = double.Parse(row["SINCAL_MIX_PV_FeO"].ToString());
                    }
                    if (row["SINCAL_SIN_PV_SIO2"] != null && row["SINCAL_SIN_PV_SIO2"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_SIO2 = double.Parse(row["SINCAL_SIN_PV_SIO2"].ToString());
                    }
                    if (row["SINCAL_SIN_PV_MGO"] != null && row["SINCAL_SIN_PV_MGO"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_MGO = double.Parse(row["SINCAL_SIN_PV_MGO"].ToString());
                    }
                    if (row["SINCAL_SIN_PV_AL2O3"] != null && row["SINCAL_SIN_PV_AL2O3"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_AL2O3 = double.Parse(row["SINCAL_SIN_PV_AL2O3"].ToString());
                    }
                    if (row["SINCAL_SIN_PV_R"] != null && row["SINCAL_SIN_PV_R"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_R = double.Parse(row["SINCAL_SIN_PV_R"].ToString());
                    }
                    if (row["SINCAL_MIX_PV_C"] != null && row["SINCAL_MIX_PV_C"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_C = double.Parse(row["SINCAL_MIX_PV_C"].ToString());
                    }

                    //MICAL_MATCH_C
                }
            }


            //data.Clear();
            //data.Dispose();
            ////MICAL_MATCH_C 配料混合料含碳               MC_SINCAL_INTERFACE_VA SINCAL_MIX_PV_C
            //sqlCol = "timestamp,SINCAL_MIX_PV_C";
            //sqlstr = GetSql(sqlCol, "MC_SINCAL_INTERFACE_VA", dTime);
            //data = iDataBase.GetDataTable(sqlstr);
            //if (data != null && data.Rows.Count > 0)
            //{
            //    var row = data.Rows[0];
            //    if (row != null)
            //    {
            //        if (row["SINCAL_MIX_PV_C"] != null && row["SINCAL_MIX_PV_C"].ToString() != "")
            //        {
            //            modelTag.MICAL_MATCH_C = double.Parse(row["SINCAL_MIX_PV_C"].ToString());
            //        }
            //    }
            //}

            data.Clear();
            data.Dispose();
            //MICAL_MATCH_TOTAL_SP 总料量SP   采集值     T_MATCH_PLC_1MIN    M_TOTAL_SP_W
            //MICAL_MATCH_TOTAL_PV 总料量PV   采集值     T_MATCH_PLC_1MIN    M_TOTAL_PV_W
            //sqlCol = "timestamp,M_TOTAL_SP_W,M_TOTAL_PV_W";
            //sqlstr = GetSql(sqlCol, "T_MATCH_PLC_1MIN", dTime);
            sqlCol = "round(AVG(M_TOTAL_SP_W),5) as M_TOTAL_SP_W,round(AVG(M_TOTAL_PV_W),5) as M_TOTAL_PV_W";
            sqlstr = GetSql_AVG(sqlCol, "T_MATCH_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["M_TOTAL_SP_W"] != null && row["M_TOTAL_SP_W"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_TOTAL_SP = double.Parse(row["M_TOTAL_SP_W"].ToString());
                    }
                    if (row["M_TOTAL_PV_W"] != null && row["M_TOTAL_PV_W"].ToString() != "")
                    {
                        modelTag.MICAL_MATCH_TOTAL_PV = double.Parse(row["M_TOTAL_PV_W"].ToString());
                    }
                }
            }

            MICAL0_PeiBi_Bak(dTime, iDataBase, modelTag);
        }
        /// <summary>
        /// 一混数据
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL1_YiHun(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_BLEND_BELT_W_1H1  1H - 1皮带秤值 采集值 T_BLEND_PLC_1MIN B_1M_PRE_BELT_W_1H_1
            //MICAL_BLEND_1M_RATE 一混转速    采集值 T_BLEND_PLC_1MIN    B_1M_MIXER_RATE
            //MICAL_BLEND_1M_TARGET_WATER 一混目标水分 采集值 T_BLEND_PLC_1MIN B_1M_TARGET_WATER
            //MICAL_BLEND_1M_FT_SP 一混设定加水量 采集值 T_BLEND_PLC_1MIN    B_1M_FT_SP
            //MICAL_BLEND_1M_FT_PV    一混实际加水量 采集值 T_BLEND_PLC_1MIN B_1M_FT_PV
            //MICAL_BLEND_1M_NEX_WAT_AVG 一混后检测水分 采集值 T_BLEND_PLC_1MIN    B_1M_NEX_WATER_AVG

            modelTag.MICAL_BLEND_1M_TIME = dTime;
            //sqlCol = "timestamp,B_1M_PRE_BELT_W_1H_1,B_1M_MIXER_RATE,B_1M_TARGET_WATER,B_1M_FT_SP,B_1M_FT_PV,B_1M_NEX_WATER_AVG";
            //sqlstr = GetSql(sqlCol, "T_BLEND_PLC_1MIN", dTime);
            sqlCol = "round(AVG(B_1M_PRE_BELT_W_1H_1),5) as B_1M_PRE_BELT_W_1H_1,round(AVG(B_1M_MIXER_RATE),5) as B_1M_MIXER_RATE,round(AVG(B_1M_TARGET_WATER),5) as B_1M_TARGET_WATER,round(AVG(B_1M_FT_SP),5) as B_1M_FT_SP,round(AVG(B_1M_FT_PV),5) as B_1M_FT_PV,round(AVG(B_1M_NEX_WATER_AVG),5) as B_1M_NEX_WATER_AVG";
            sqlstr = GetSql_AVG(sqlCol, "T_BLEND_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["B_1M_PRE_BELT_W_1H_1"] != null && row["B_1M_PRE_BELT_W_1H_1"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_BELT_W_1H1 = double.Parse(row["B_1M_PRE_BELT_W_1H_1"].ToString());
                    }
                    if (row["B_1M_MIXER_RATE"] != null && row["B_1M_MIXER_RATE"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_1M_RATE = double.Parse(row["B_1M_MIXER_RATE"].ToString());
                    }
                    if (row["B_1M_TARGET_WATER"] != null && row["B_1M_TARGET_WATER"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_1M_TARGET_WATER = double.Parse(row["B_1M_TARGET_WATER"].ToString());
                    }
                    if (row["B_1M_FT_SP"] != null && row["B_1M_FT_SP"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_1M_FT_SP = double.Parse(row["B_1M_FT_SP"].ToString());
                    }
                    if (row["B_1M_FT_PV"] != null && row["B_1M_FT_PV"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_1M_FT_PV = double.Parse(row["B_1M_FT_PV"].ToString());
                    }
                    if (row["B_1M_NEX_WATER_AVG"] != null && row["B_1M_NEX_WATER_AVG"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_1M_NEX_WAT_AVG = double.Parse(row["B_1M_NEX_WATER_AVG"].ToString());
                    }
                }
            }
        }
        /// <summary>
        /// 二混数据
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL2_ErHun(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_BLEND_BELT_W_2H1	2H-1皮带秤值	采集值	T_BLEND_PLC_1MIN	B_1M_NEX_BELT_W_2H_1
            //MICAL_BLEND_2M_RATE 二混转速    采集值 T_BLEND_PLC_1MIN    B_2M_MIXER_RATE
            //MICAL_BLEND_2M_TARGET_WATER 二混目标水分 采集值 T_BLEND_PLC_1MIN B_2M_TARGET_WATER
            //MICAL_BLEND_2M_FLOW_SP 二混设定加水量 采集值 T_BLEND_PLC_1MIN    B_2M_FLOW_SP
            //MICAL_BLEND_2M_FLOW_PV  二混实际加水量 采集值 T_BLEND_PLC_1MIN B_2M_FLOW_PV
            //MICAL_BLEND_2M_NEX_WAT_AVG 二混后检测水分 采集值 T_BLEND_PLC_1MIN    B_2M_NEX_WATER_AVG

            modelTag.MICAL_BLEND_2M_TIME = dTime;
            //sqlCol = "timestamp,B_1M_NEX_BELT_W_2H_1,B_2M_MIXER_RATE,B_2M_TARGET_WATER,B_2M_FLOW_SP,B_2M_FLOW_PV,B_2M_NEX_WATER_AVG";
            //sqlstr = GetSql(sqlCol, "T_BLEND_PLC_1MIN", dTime);
            sqlCol = "round(AVG(B_1M_NEX_BELT_W_2H_1),5) as B_1M_NEX_BELT_W_2H_1,round(AVG(B_2M_MIXER_RATE),5) as B_2M_MIXER_RATE,round(AVG(B_2M_TARGET_WATER),5) as B_2M_TARGET_WATER,round(AVG(B_2M_FLOW_SP),5) as B_2M_FLOW_SP,round(AVG(B_2M_FLOW_PV),5) as B_2M_FLOW_PV,round(AVG(B_2M_NEX_WATER_AVG),5) as B_2M_NEX_WATER_AVG";
            sqlstr = GetSql_AVG(sqlCol, "T_BLEND_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["B_1M_NEX_BELT_W_2H_1"] != null && row["B_1M_NEX_BELT_W_2H_1"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_BELT_W_2H1 = double.Parse(row["B_1M_NEX_BELT_W_2H_1"].ToString());
                    }
                    if (row["B_2M_MIXER_RATE"] != null && row["B_2M_MIXER_RATE"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_2M_RATE = double.Parse(row["B_2M_MIXER_RATE"].ToString());
                    }
                    if (row["B_2M_TARGET_WATER"] != null && row["B_2M_TARGET_WATER"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_2M_TARGET_WATER = double.Parse(row["B_2M_TARGET_WATER"].ToString());
                    }
                    if (row["B_2M_FLOW_SP"] != null && row["B_2M_FLOW_SP"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_2M_FLOW_SP = double.Parse(row["B_2M_FLOW_SP"].ToString());
                    }
                    if (row["B_2M_FLOW_PV"] != null && row["B_2M_FLOW_PV"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_2M_FLOW_PV = double.Parse(row["B_2M_FLOW_PV"].ToString());
                    }
                    if (row["B_2M_NEX_WATER_AVG"] != null && row["B_2M_NEX_WATER_AVG"].ToString() != "")
                    {
                        modelTag.MICAL_BLEND_2M_NEX_WAT_AVG = double.Parse(row["B_2M_NEX_WATER_AVG"].ToString());
                    }
                }
            }
        }
        private void MICAL3_BuLiao_Z2_1(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_CLOTH_STICK_PV_RATE   圆辊转速 采集值 T_CLOTH_PLC _1MIN   C_STICK_PV
            //MICAL_CLOTH_THICK_AVG   烧结料厚 采集值 T_CLOTH_PLC _1MIN   C_THICK_AVG
            //MICAL_CLOTH_BL_TAR_LEVEL    混合料槽料位 采集值 T_CLOTH_PLC _1MIN   C_BLEND_LEVEL
            //MICAL_CLOTH_2M_W_Z2_1   Z2 - 1皮带秤值 采集值 T_BLEND_PLC_1MIN B_2M_BELT_VALUE

            //modelTag.MICAL_CLOTH_DST_TIME = dTime;
            //sqlCol = "timestamp,C_STICK_PV,C_THICK_AVG,C_BLEND_LEVEL,B_2M_BELT_VALUE";
            //sqlstr = GetSql(sqlCol, "T_CLOTH_PLC", dTime);
            sqlCol = "round(AVG(B_2M_BELT_VALUE),5) as B_2M_BELT_VALUE";
            sqlstr = GetSql_AVG(sqlCol, "T_BLEND_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    //if (row["C_STICK_PV"] != null && row["C_STICK_PV"].ToString() != "")
                    //{
                    //    modelTag.MICAL_CLOTH_STICK_PV_RATE = double.Parse(row["C_STICK_PV"].ToString());
                    //}
                    //if (row["C_THICK_AVG"] != null && row["C_THICK_AVG"].ToString() != "")
                    //{
                    //    modelTag.MICAL_CLOTH_THICK_AVG = double.Parse(row["C_THICK_AVG"].ToString());
                    //}
                    //if (row["C_BLEND_LEVEL"] != null && row["C_BLEND_LEVEL"].ToString() != "")
                    //{
                    //    modelTag.MICAL_CLOTH_BL_TAR_LEVEL = double.Parse(row["C_BLEND_LEVEL"].ToString());
                    //}
                    if (row["B_2M_BELT_VALUE"] != null && row["B_2M_BELT_VALUE"].ToString() != "")
                    {
                        modelTag.MICAL_CLOTH_2M_W_Z2_1 = double.Parse(row["B_2M_BELT_VALUE"].ToString());
                    }

                }
            }
        }
        /// <summary>
        /// 布料
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL3_BuLiao(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_CLOTH_STICK_PV_RATE   圆辊转速 采集值 T_CLOTH_PLC _1MIN   C_STICK_PV
            //MICAL_CLOTH_THICK_AVG   烧结料厚 采集值 T_CLOTH_PLC _1MIN   C_THICK_AVG
            //MICAL_CLOTH_BL_TAR_LEVEL    混合料槽料位 采集值 T_CLOTH_PLC _1MIN   C_BLEND_LEVEL
            //MICAL_CLOTH_2M_W_Z2_1   Z2 - 1皮带秤值 采集值 T_BLEND_PLC_1MIN B_2M_BELT_VALUE

            modelTag.MICAL_CLOTH_DST_TIME = dTime;
            ////sqlCol = "timestamp,C_STICK_PV,C_THICK_AVG,C_BLEND_LEVEL,B_2M_BELT_VALUE";
            ////sqlstr = GetSql(sqlCol, "T_CLOTH_PLC", dTime);
            //sqlCol = "AVG(C_STICK_PV) as C_STICK_PV,AVG(C_THICK_AVG) as C_THICK_AVG,AVG(C_BLEND_LEVEL) as C_BLEND_LEVEL,AVG(B_2M_BELT_VALUE) as B_2M_BELT_VALUE";
            sqlCol = "round(AVG(C_STICK_PV),5) as C_STICK_PV,round(AVG(C_THICK_PV),5) as C_THICK_PV,round(AVG(C_BLEND_LEVEL),5) as C_BLEND_LEVEL";
            sqlstr = GetSql_AVG(sqlCol, "T_CLOTH_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["C_STICK_PV"] != null && row["C_STICK_PV"].ToString() != "")
                    {
                        modelTag.MICAL_CLOTH_STICK_PV_RATE = double.Parse(row["C_STICK_PV"].ToString());
                    }
                    if (row["C_THICK_PV"] != null && row["C_THICK_PV"].ToString() != "")
                    {
                        modelTag.MICAL_CLOTH_THICK_AVG = double.Parse(row["C_THICK_PV"].ToString());
                    }
                    if (row["C_BLEND_LEVEL"] != null && row["C_BLEND_LEVEL"].ToString() != "")
                    {
                        modelTag.MICAL_CLOTH_BL_TAR_LEVEL = double.Parse(row["C_BLEND_LEVEL"].ToString());
                    }
                    //if (row["B_2M_BELT_VALUE"] != null && row["B_2M_BELT_VALUE"].ToString() != "")
                    //{
                    //    modelTag.MICAL_CLOTH_2M_W_Z2_1 = double.Parse(row["B_2M_BELT_VALUE"].ToString());
                    //}

                }
            }
            //if (modelTag.MICAL_CLOTH_STICK_PV_RATE == null || modelTag.MICAL_CLOTH_THICK_AVG == null || modelTag.MICAL_CLOTH_BL_TAR_LEVEL == null)
            //{ }
            if (modelTag.MICAL_CLOTH_STICK_PV_RATE == null)
            {
                modelTag.MICAL_CLOTH_STICK_PV_RATE = 0;
            }
            if (modelTag.MICAL_CLOTH_THICK_AVG == null)
            {
                modelTag.MICAL_CLOTH_THICK_AVG = 0;
            }
            if (modelTag.MICAL_CLOTH_BL_TAR_LEVEL == null)
            {
                modelTag.MICAL_CLOTH_BL_TAR_LEVEL = 0;
            }

        }
        /// <summary>
        /// 点火
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL4_DianHuo(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_BU_C_IG_AIM_TE    点火温度 采集值 T_BURN_COLE_PLC_1MIN BC_AIM_TE
            //MICAL_BU_C_IG_GAS_PV 煤气流量    采集值 T_BURN_COLE_PLC_1MIN    BC_IG_GAS_PV
            //MICAL_BU_C_IG_AIR_PV    空气流量 采集值 T_BURN_COLE_PLC_1MIN BC_IG_AIR_PV
            //MICAL_BU_C_AIM_AFR 空燃比        采集值 T_BURN_COLE_PLC_1MIN    BC_AIM_AFR

            modelTag.MICAL_BU_C_IGNI_TIME = dTime;
            //sqlCol = "timestamp,BC_AIM_TE,BC_IG_GAS_PV,BC_IG_AIR_PV,BC_AIM_AFR";
            //sqlstr = GetSql(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime);
            //点火段温度2，采集位置：T_BURN_COLE_PLC_1MIN表，BC_IG_02_TE字段；
            //点火段温度3，采集位置：T_BURN_COLE_PLC_1MIN表，BC_IG_03_TE字段；
            sqlCol = "round(AVG(BC_AIM_TE),5) as BC_AIM_TE,round(AVG(BC_IG_GAS_PV),5) as BC_IG_GAS_PV,round(AVG(BC_IG_AIR_PV),5) as BC_IG_AIR_PV,round(AVG(BC_AIM_AFR),5) as BC_AIM_AFR,round(AVG(BC_IG_02_TE),4) as BC_IG_02_TE,round(AVG(BC_IG_03_TE),4) as BC_IG_03_TE";
            sqlstr = GetSql_AVG(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {

                    if (row["BC_IG_02_TE"] != null && row["BC_IG_02_TE"].ToString() != "" && row["BC_IG_03_TE"] != null && row["BC_IG_03_TE"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_IG_AIM_TE = (double.Parse(row["BC_IG_02_TE"].ToString()) + double.Parse(row["BC_IG_03_TE"].ToString())) / 2;
                    }
                    else if (row["BC_AIM_TE"] != null && row["BC_AIM_TE"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_IG_AIM_TE = double.Parse(row["BC_AIM_TE"].ToString());
                    }
                    if (row["BC_IG_GAS_PV"] != null && row["BC_IG_GAS_PV"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_IG_GAS_PV = double.Parse(row["BC_IG_GAS_PV"].ToString());
                    }
                    if (row["BC_IG_AIR_PV"] != null && row["BC_IG_AIR_PV"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_IG_AIR_PV = double.Parse(row["BC_IG_AIR_PV"].ToString());
                        if (row["BC_IG_GAS_PV"] != null && row["BC_IG_GAS_PV"].ToString() != "")
                        {
                            if (modelTag.MICAL_BU_C_IG_GAS_PV != null && modelTag.MICAL_BU_C_IG_GAS_PV.Value != 0)
                            {
                                try
                                {
                                    modelTag.MICAL_BU_C_AIM_AFR = Round3(modelTag.MICAL_BU_C_IG_AIR_PV / modelTag.MICAL_BU_C_IG_GAS_PV);
                                }
                                catch
                                { }
                            }
                        }
                    }
                    //if (row["BC_AIM_AFR"] != null && row["BC_AIM_AFR"].ToString() != "")
                    //{
                    //    modelTag.MICAL_BU_C_AIM_AFR = double.Parse(row["BC_AIM_AFR"].ToString());
                    //}

                }
            }
        }
        /// <summary>
        /// 保留3位小数的
        /// </summary>
        /// <param name="paraV"></param>
        /// <returns></returns>
        private double Round3(double? paraV)
        {
            double readV = (double)decimal.Round((decimal)paraV, 3);
            return readV;

        }
        /// <summary>
        /// 烧结
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        /// <param name="paraMinuteAvg">某段时间的平均（单位分钟）</param>
        private void MICAL5_ShaoJie(DateTime dTime, IDataBase iDataBase, int paraMinuteAvg, MC_MICAL_RESULT modelTag)
        {
            //修改此方法 全部取平均
            data.Clear();
            data.Dispose();

            //MICAL_BU_C_LOCAT_BTP    BTP位置 采集值 
            //MICAL_BU_C_BTP_TE BTP温度   采集值 
            //sqlCol = "timestamp,BTPCAL_OUT_8_X_AVG_BTP,BTPCAL_OUT_8_TE_AVG_BTP,BTPCAL_QE4_8_X_AVG_BRP,BTPCAL_QE4_8_TE_AVG_BRP";
            //sqlstr = GetSql(sqlCol, "MC_BTPCAL_result_1min", dTime);
            sqlCol = "round(AVG(BTPCAL_OUT_8_X_AVG_BTP),5) as BTPCAL_OUT_8_X_AVG_BTP,round(AVG(BTPCAL_OUT_8_TE_AVG_BTP),5) as BTPCAL_OUT_8_TE_AVG_BTP,round(AVG(BTPCAL_QE4_8_X_AVG_BRP),5) as BTPCAL_QE4_8_X_AVG_BRP,round(AVG(BTPCAL_QE4_8_TE_AVG_BRP),5) as BTPCAL_QE4_8_TE_AVG_BRP";
            if (modelPiDaiInfo != null && modelPiDaiInfo.PAR_AVG_T != null)
            {
                sqlstr = GetSql_AVG(sqlCol, "MC_BTPCAL_result_1min", dTime, (int)modelPiDaiInfo.PAR_AVG_T.Value);
            }
            else
            {
                sqlstr = GetSql_AVG(sqlCol, "MC_BTPCAL_result_1min", dTime, paraMinuteAvg);
            }
            //sqlstr = GetSql_AVG(sqlCol, "MC_BTPCAL_result_1min", dTime, paraMinuteAvg);//MinuteAvg

            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["BTPCAL_OUT_8_X_AVG_BTP"] != null && row["BTPCAL_OUT_8_X_AVG_BTP"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_LOCAT_BTP = double.Parse(row["BTPCAL_OUT_8_X_AVG_BTP"].ToString());
                    }
                    if (row["BTPCAL_OUT_8_TE_AVG_BTP"] != null && row["BTPCAL_OUT_8_TE_AVG_BTP"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_BTP_TE = double.Parse(row["BTPCAL_OUT_8_TE_AVG_BTP"].ToString());
                    }

                    //if (row["BTPCAL_QE4_8_X_AVG_BRP"] != null && row["BTPCAL_QE4_8_X_AVG_BRP"].ToString() != "")
                    //{
                    //    modelTag.MICAL_BU_C_LOCAT_BRP = double.Parse(row["BTPCAL_QE4_8_X_AVG_BRP"].ToString());
                    //}
                    //if (row["BTPCAL_QE4_8_TE_AVG_BRP"] != null && row["BTPCAL_QE4_8_TE_AVG_BRP"].ToString() != "")
                    //{
                    //    modelTag.MICAL_BU_C_BRP_TE = double.Parse(row["BTPCAL_QE4_8_TE_AVG_BRP"].ToString());
                    //}



                }
            }
            data.Clear();
            data.Dispose();

            //MICAL_BU_C_SIN_MS_SP    烧结机设定机速 采集值 T_BURN_COLE_PLC_1MIN BC_SIN_MS_SP
            //MICAL_BU_C_SIN_MS_PV 烧结机实际机速 采集值 T_BURN_COLE_PLC_1MIN    BC_SIN_MS_PV

            modelTag.MICAL_BU_C_SIN_TIME = dTime;
            //sqlCol = "timestamp,BC_SIN_MS_SP,BC_SIN_MS_PV";
            //sqlstr = GetSql(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime);
            sqlCol = "round(AVG(BC_SIN_MS_SP),5) as BC_SIN_MS_SP,round(AVG(BC_SIN_MS_PV),5) as BC_SIN_MS_PV";
            sqlstr = GetSql_AVG(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime, paraMinuteAvg);//MinuteAvg
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["BC_SIN_MS_SP"] != null && row["BC_SIN_MS_SP"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_SIN_MS_SP = double.Parse(row["BC_SIN_MS_SP"].ToString());
                    }
                    if (row["BC_SIN_MS_PV"] != null && row["BC_SIN_MS_PV"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_SIN_MS_PV = double.Parse(row["BC_SIN_MS_PV"].ToString());
                    }
                }
            }
            data.Clear();
            data.Dispose();

            //MICAL_MA_SB_FLUE_TE 主抽大烟道温度 （两个平均值）	计算值 T_MA_PGD_PLC_1MIN(PS_MA_SB_1_FLUE_TE+PS_MA_SB_2_FLUE_TE)/ 2
            //MICAL_MA_SB_FLUE_PT 主抽大烟道压力（两个平均值）	计算值 T_MA_PGD_PLC_1MIN(PS_MA_SB_1_FLUE_PT+PS_MA_SB_2_FLUE_PT)/ 2
            //MICAL_MA_SB_FLUE_FT 主抽大烟道流量（两个之和）	计算值 T_MA_PGD_PLC_1MIN   PS_MA_SB_1_FLUE_FT + PS_MA_SB_2_FLUE_FT
            //sqlCol = "timestamp,PS_MA_SB_1_FLUE_TE,PS_MA_SB_2_FLUE_TE,PS_MA_SB_1_FLUE_PT,PS_MA_SB_2_FLUE_PT,PS_MA_SB_1_FLUE_FT,PS_MA_SB_2_FLUE_FT";
            //sqlstr = GetSql(sqlCol, "T_MA_PGD_PLC_1MIN", dTime);
            sqlCol = "round(AVG(PS_MA_SB_1_FLUE_TE),5) as PS_MA_SB_1_FLUE_TE,round(AVG(PS_MA_SB_2_FLUE_TE),5) as PS_MA_SB_2_FLUE_TE";
            sqlCol += ",round(AVG(PS_MA_SB_1_FLUE_PT),5) as PS_MA_SB_1_FLUE_PT,round(AVG(PS_MA_SB_2_FLUE_PT),5) as PS_MA_SB_2_FLUE_PT";
            sqlCol += ",round(AVG(PS_MA_SB_1_FLUE_FT),5) as PS_MA_SB_1_FLUE_FT,round(AVG(PS_MA_SB_2_FLUE_FT),5) as PS_MA_SB_2_FLUE_FT";
            sqlstr = GetSql_AVG(sqlCol, "T_MA_PGD_PLC_1MIN", dTime, paraMinuteAvg);//MinuteAvg
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    double ddd1 = -99999;
                    double ddd2 = -99999;
                    bool flag1 = false;
                    bool flag2 = false;
                    if (row["PS_MA_SB_1_FLUE_TE"] != null && row["PS_MA_SB_1_FLUE_TE"].ToString() != "")
                    {
                        ddd1 = double.Parse(row["PS_MA_SB_1_FLUE_TE"].ToString());
                        flag1 = true;
                    }
                    if (row["PS_MA_SB_2_FLUE_TE"] != null && row["PS_MA_SB_2_FLUE_TE"].ToString() != "")
                    {
                        ddd2 = double.Parse(row["PS_MA_SB_2_FLUE_TE"].ToString());
                        flag2 = true;
                    }
                    if (flag1 && flag2)
                    {
                        modelTag.MICAL_MA_SB_FLUE_TE = (ddd1 + ddd2) / 2;
                    }
                    else if (flag1)
                    {
                        modelTag.MICAL_MA_SB_FLUE_TE = ddd1;
                    }
                    else if (flag2)
                    {
                        modelTag.MICAL_MA_SB_FLUE_TE = ddd2;
                    }


                    ddd1 = -99999;
                    ddd2 = -99999;
                    flag1 = false;
                    flag2 = false;
                    if (row["PS_MA_SB_1_FLUE_PT"] != null && row["PS_MA_SB_1_FLUE_PT"].ToString() != "")
                    {
                        ddd1 = double.Parse(row["PS_MA_SB_1_FLUE_PT"].ToString());
                        flag1 = true;
                    }
                    if (row["PS_MA_SB_2_FLUE_PT"] != null && row["PS_MA_SB_2_FLUE_PT"].ToString() != "")
                    {
                        ddd2 = double.Parse(row["PS_MA_SB_2_FLUE_PT"].ToString());
                        flag2 = true;
                    }
                    if (flag1 && flag2)
                    {
                        modelTag.MICAL_MA_SB_FLUE_PT = (ddd1 + ddd2) / 2;
                    }
                    else if (flag1)
                    {
                        modelTag.MICAL_MA_SB_FLUE_PT = ddd1;
                    }
                    else if (flag2)
                    {
                        modelTag.MICAL_MA_SB_FLUE_PT = ddd2;
                    }

                    ddd1 = -99999;
                    ddd2 = -99999;
                    flag1 = false;
                    flag2 = false;
                    if (row["PS_MA_SB_1_FLUE_FT"] != null && row["PS_MA_SB_1_FLUE_FT"].ToString() != "")
                    {
                        ddd1 = double.Parse(row["PS_MA_SB_1_FLUE_FT"].ToString());
                        flag1 = true;
                    }
                    if (row["PS_MA_SB_2_FLUE_FT"] != null && row["PS_MA_SB_2_FLUE_FT"].ToString() != "")
                    {
                        ddd2 = double.Parse(row["PS_MA_SB_2_FLUE_FT"].ToString());
                        flag2 = true;
                    }
                    if (flag1 && flag2)
                    {
                        modelTag.MICAL_MA_SB_FLUE_FT = (ddd1 + ddd2);
                    }
                    else if (flag1)
                    {
                        modelTag.MICAL_MA_SB_FLUE_FT = ddd1;
                    }
                    else if (flag2)
                    {
                        modelTag.MICAL_MA_SB_FLUE_FT = ddd2;
                    }
                }
            }
            data.Clear();
            data.Dispose();

            //MICAL_BREATH 透气性指数   采集值 M_PICAL_BREATH_RESULT_1MIN  PICAL_JPU
            //sqlCol = "timestamp,PICAL_JPU";
            //sqlstr = GetSql(sqlCol, "M_PICAL_BREATH_RESULT_1MIN", dTime);
            sqlCol = "round(AVG(PICAL_JPU),5) as PICAL_JPU";
            sqlstr = GetSql_AVG(sqlCol, "M_PICAL_BREATH_RESULT_1MIN", dTime, paraMinuteAvg);//MinuteAvg
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["PICAL_JPU"] != null && row["PICAL_JPU"].ToString() != "")
                    {
                        modelTag.MICAL_BREATH = double.Parse(row["PICAL_JPU"].ToString());
                    }
                }
            }
        }
        /// <summary>
        /// 烧结 BRP位置温度
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        /// <param name="paraMinuteAvg">某段时间的平均（单位分钟）</param>
        private void MICAL5_ShaoJie_BRP位置温度(DateTime dTime, IDataBase iDataBase, int paraMinuteAvg, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();

            //MICAL_BU_C_LOCAT_BTP    BTP位置 采集值 
            //MICAL_BU_C_BTP_TE BTP温度   采集值 
            //sqlCol = "timestamp,BTPCAL_OUT_8_X_AVG_BTP,BTPCAL_OUT_8_TE_AVG_BTP,BTPCAL_QE4_8_X_AVG_BRP,BTPCAL_QE4_8_TE_AVG_BRP";
            //sqlstr = GetSql(sqlCol, "MC_BTPCAL_result_1min", dTime);
            sqlCol = "round(AVG(BTPCAL_OUT_8_X_AVG_BTP),5) as BTPCAL_OUT_8_X_AVG_BTP,round(AVG(BTPCAL_OUT_8_TE_AVG_BTP),5) as BTPCAL_OUT_8_TE_AVG_BTP,round(AVG(BTPCAL_QE4_8_X_AVG_BRP),5) as BTPCAL_QE4_8_X_AVG_BRP,round(AVG(BTPCAL_QE4_8_TE_AVG_BRP),5) as BTPCAL_QE4_8_TE_AVG_BRP";
            sqlstr = GetSql_AVG(sqlCol, "MC_BTPCAL_result_1min", dTime, paraMinuteAvg);//MinuteAvg
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {

                    if (row["BTPCAL_QE4_8_X_AVG_BRP"] != null && row["BTPCAL_QE4_8_X_AVG_BRP"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_LOCAT_BRP = double.Parse(row["BTPCAL_QE4_8_X_AVG_BRP"].ToString());
                    }
                    if (row["BTPCAL_QE4_8_TE_AVG_BRP"] != null && row["BTPCAL_QE4_8_TE_AVG_BRP"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_BRP_TE = double.Parse(row["BTPCAL_QE4_8_TE_AVG_BRP"].ToString());
                    }



                }
            }

        }
        /// <summary>
        /// 环冷
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL6_HuanLeng(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_BU_C_RC_TIME 环冷记录时间  采集值
            //MICAL_BU_C_RC_B_IN_TE   入口温度 采集值 T_BURN_COLE_PLC_1MIN RC_IN_TE
            //MICAL_BU_C_RC_B_OUT_TE 出口温度    采集值 T_BURN_COLE_PLC_1MIN    RC_OUT_TE
            //MICAL_BU_C_RC_SPEED_SP  环冷机设定机速 采集值 T_BURN_COLE_PLC_1MIN RC_SPEED_SP
            //MICAL_BU_C_RC_SPEED_PV 环冷机实际机速 采集值 T_BURN_COLE_PLC_1MIN    RC_SPEED_PV

            modelTag.MICAL_BU_C_RC_TIME = dTime;
            //sqlCol = "timestamp,RC_IN_TE,RC_OUT_TE,RC_SPEED_SP,RC_SPEED_PV";
            //sqlstr = GetSql(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime);
            sqlCol = "round(AVG(RC_IN_TE),5) as RC_IN_TE,round(AVG(RC_OUT_TE),5) as RC_OUT_TE,round(AVG(RC_SPEED_SP),5) as RC_SPEED_SP,round(AVG(RC_SPEED_PV),5) as RC_SPEED_PV";
            sqlstr = GetSql_AVG(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["RC_IN_TE"] != null && row["RC_IN_TE"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_RC_B_IN_TE = double.Parse(row["RC_IN_TE"].ToString());
                    }
                    if (row["RC_OUT_TE"] != null && row["RC_OUT_TE"].ToString() != "")
                    {
                        //modelTag.MICAL_BU_C_RC_B_OUT_TE = double.Parse(row["RC_OUT_TE"].ToString());
                    }
                    if (row["RC_SPEED_SP"] != null && row["RC_SPEED_SP"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_RC_SPEED_SP = double.Parse(row["RC_SPEED_SP"].ToString());
                    }
                    if (row["RC_SPEED_PV"] != null && row["RC_SPEED_PV"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_RC_SPEED_PV = double.Parse(row["RC_SPEED_PV"].ToString());
                    }

                }
            }
        }
        /// <summary>
        ///  给出口温度赋值
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL6_HuanLeng_ChuKouTemp(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            ////MICAL_BU_C_RC_TIME 环冷记录时间  采集值
            ////MICAL_BU_C_RC_B_IN_TE   入口温度 采集值 T_BURN_COLE_PLC_1MIN RC_IN_TE
            ////MICAL_BU_C_RC_B_OUT_TE 出口温度    采集值 T_BURN_COLE_PLC_1MIN    RC_OUT_TE
            ////MICAL_BU_C_RC_SPEED_SP  环冷机设定机速 采集值 T_BURN_COLE_PLC_1MIN RC_SPEED_SP
            ////MICAL_BU_C_RC_SPEED_PV 环冷机实际机速 采集值 T_BURN_COLE_PLC_1MIN    RC_SPEED_PV

            //modelTag.MICAL_BU_C_RC_TIME = dTime;
            ////sqlCol = "timestamp,RC_IN_TE,RC_OUT_TE,RC_SPEED_SP,RC_SPEED_PV";
            ////sqlstr = GetSql(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime);
            sqlCol = "round(AVG(RC_IN_TE),5) as RC_IN_TE,round(AVG(RC_OUT_TE),5) as RC_OUT_TE,round(AVG(RC_SPEED_SP),5) as RC_SPEED_SP,round(AVG(RC_SPEED_PV),5) as RC_SPEED_PV";
            sqlstr = GetSql_AVG(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    //if (row["RC_IN_TE"] != null && row["RC_IN_TE"].ToString() != "")
                    //{
                    //    modelTag.MICAL_BU_C_RC_B_IN_TE = double.Parse(row["RC_IN_TE"].ToString());
                    //}
                    if (row["RC_OUT_TE"] != null && row["RC_OUT_TE"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_RC_B_OUT_TE = double.Parse(row["RC_OUT_TE"].ToString());
                    }
                    //if (row["RC_SPEED_SP"] != null && row["RC_SPEED_SP"].ToString() != "")
                    //{
                    //    modelTag.MICAL_BU_C_RC_SPEED_SP = double.Parse(row["RC_SPEED_SP"].ToString());
                    //}
                    //if (row["RC_SPEED_PV"] != null && row["RC_SPEED_PV"].ToString() != "")
                    //{
                    //    modelTag.MICAL_BU_C_RC_SPEED_PV = double.Parse(row["RC_SPEED_PV"].ToString());
                    //}

                }
            }
        }
        /// <summary>
        /// 筛分
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private void MICAL7_ShaiFen(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {
            data.Clear();
            data.Dispose();
            //MICAL_BU_C_SCREEN_TIME 筛分记录时间  采集值
            //MICAL_BU_C_BED_MATE_AO_FT   铺底料皮带秤值 采集值 T_BURN_COLE_PLC_1MIN BED_MATERAL_AO_FT
            //MICAL_BU_C_COLD_AO_FT 返矿皮带秤值  采集值 T_BURN_COLE_PLC_1MIN    COLD_AO_FT
            //MICAL_BU_C_FP_BELT_FT_AVG   成品皮带秤 采集值 T_BURN_COLE_PLC_1MIN FP_BELT_FT_AVG
            //MICAL_BU_C_FP_RATE 成品率计算   计算值？？？？？

            //MinuteAvg
            modelTag.MICAL_BU_C_SCREEN_TIME = dTime;
            sqlCol = "round(AVG(BED_MATERAL_AO_FT),5) as BED_MATERAL_AO_FT,round(AVG(COLD_AO_FT),5) as COLD_AO_FT,round(AVG(FP_BELT_FT_AVG),5) as FP_BELT_FT_AVG";
            sqlstr = GetSql_AVG(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime, MinuteAvg);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["BED_MATERAL_AO_FT"] != null && row["BED_MATERAL_AO_FT"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_BED_MATE_AO_FT = double.Parse(row["BED_MATERAL_AO_FT"].ToString());
                    }
                    if (row["COLD_AO_FT"] != null && row["COLD_AO_FT"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_COLD_AO_FT = double.Parse(row["COLD_AO_FT"].ToString());
                    }
                    if (row["FP_BELT_FT_AVG"] != null && row["FP_BELT_FT_AVG"].ToString() != "")
                    {
                        modelTag.MICAL_BU_C_FP_BELT_FT_AVG = double.Parse(row["FP_BELT_FT_AVG"].ToString());
                    }
                }
            }

        }
        //private void DaotuiModel_14(IDataBase iDataBase, DateTime dateTime)
        //{
        //    modelTag = new MC_MICAL_RESULT();
        //    modelTag.DATANUM = 14;
        //    modelTag.MICAL_BU_C_SAMP_TIME = dateTime;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        bool GetRunState(IDataBase iDataBase)
        {
            sqlstr = string.Format("select {0} from {1} where timestamp = (select max(timestamp) from {1})", "FP_BELT_FT_AVG", "T_BURN_COLE_PLC_1MIN");
            double fP_BELT_FT_AVG = iDataBase.GetDouble(sqlstr);
            if (fP_BELT_FT_AVG < 5)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 取样点倒推
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dTimeBegin">倒推开始时间</param>
        private void DaoTui14_Quyangdian(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 14;
            modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;

            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    if (!GetRunState(iDataBase))
                    {
                        modelTag.MICAL_M_STATE_FLAG = 4;
                        return;
                    }
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }
                #region 计算程序

                string strdTime_P8_End = GetMC_RUN_START_Time_Str(dTimeBegin, "Belt_P_8", iDataBase, 0, modelTag);
                if (string.IsNullOrEmpty(strdTime_P8_End))
                {
                    return;
                }
                //dTime_P8_End 成品皮带结束时间//dTime_p8_Begin 成品皮带开始时间
                DateTime dTime_P8_End = DateTime.Parse(strdTime_P8_End);

                //tP8= Lp8/Vp8/60，Tp8：P8成品皮带运行时间。单位min
                //tP8= (Lp8*取样点距成品皮带开始位置修正系数)/Vp8/60，Tp8：P8成品皮带运行时间。单位min
                double tP8 = (double)((modelPiDaiInfo.PAR_LENGTH_P_8 * modelPiDaiInfo.PAR_K) / modelPiDaiInfo.PAR_SPE_P_8 / 60);// Tp8：p8成品皮带运行时间。单位min
                string strdTime_p8_Begin = GetMC_RUN_START_Time_Str(dTime_P8_End, "Belt_P_8", iDataBase, tP8, modelTag);
                if (string.IsNullOrEmpty(strdTime_p8_Begin))
                {
                    return;
                }
                DateTime dTime_P8_Begin = DateTime.Parse(strdTime_p8_Begin);

                double t筛分 = modelPiDaiInfo.PAR_SCREEN_U_TIME.Value;
                string strdTime_筛分_Begin = GetMC_RUN_START_Time_Str(dTime_P8_Begin, "SCREEN_M", iDataBase, t筛分, modelTag);
                if (string.IsNullOrEmpty(strdTime_筛分_Begin))
                {
                    return;
                }
                DateTime dTime_筛分_Begin = DateTime.Parse(strdTime_筛分_Begin);

                //modelTag.MICAL_SAM_SCR_TIME = Utils.DateDiff(dTime_P8_Begin, dTime_筛分_Begin);
                modelTag.MICAL_SAM_SCR_TIME = Utils.DateDiff(dTimeBegin, dTime_筛分_Begin);
                MICAL7_ShaiFen(dTime_筛分_Begin, iDataBase, modelTag);
                if (modelTag.MICAL_MATCH_TOTAL_SP != null && modelTag.MICAL_MATCH_TOTAL_SP.Value != 0)
                {
                    modelTag.MICAL_BU_C_FP_RATE = modelTag.MICAL_BU_C_FP_BELT_FT_AVG / modelTag.MICAL_MATCH_TOTAL_SP;// 成品率计算   计算值？？？？？

                }

                //SF-1皮带 tsf1=LSF1/VSF1/60，tsf1：sf1出板式给矿机皮带运行时间；单位min
                double tSF_1 = (double)(modelPiDaiInfo.PAR_LENGTH_SF_1 / modelPiDaiInfo.PAR_SPE_SF_1 / 60);// Tp8：p8成品皮带运行时间。单位min
                string strdTime_SF_1_Begin = GetMC_RUN_START_Time_Str(dTime_筛分_Begin, "Belt_SF_1", iDataBase, tSF_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_SF_1_Begin))
                {
                    return;
                }
                DateTime dTime_SF_1_Begin = DateTime.Parse(strdTime_SF_1_Begin);


                double t板式 = modelPiDaiInfo.PAR_PF_U_TIME.Value;
                string strdTime_板式_Begin = GetMC_RUN_START_Time_Str(dTime_SF_1_Begin, "PF_M", iDataBase, t板式, modelTag);
                if (string.IsNullOrEmpty(strdTime_板式_Begin))
                {
                    return;
                }
                DateTime dTime_板式_Begin = DateTime.Parse(strdTime_板式_Begin);
                modelTag.MICAL_SCR_RC_TIME = Utils.DateDiff(dTime_筛分_Begin, dTime_板式_Begin);

                MICAL6_HuanLeng_ChuKouTemp(dTime_板式_Begin, iDataBase, modelTag);

                //环冷倒推时间
                //PAR_LENGTH_RC：环冷机周长，参数值，MC_MICAL_PAR表；
                //modelPiDaiInfo.PAR_LENGTH_RC
                bool flagHlj = false;
                double changdu = 0;
                int t_huanleng = 0;//环冷冷却时间t环冷 =n；单位min
                for (; t_huanleng < 600; t_huanleng++)
                {
                    changdu += MICAL6_HuanLeng_Sudu_ByDtime(dTime_板式_Begin.AddMinutes(0 - t_huanleng), iDataBase);
                    if (changdu >= modelPiDaiInfo.PAR_LENGTH_RC)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //环冷机累加不到，模糊计算
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "RC_SPEED_PV", dTime_板式_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t_huanleng = (int)(modelPiDaiInfo.PAR_LENGTH_RC / avgcol);
                        modelTag.MICAL_RC_STATE_FLAG = 0;
                    }
                    else
                    {
                        modelTag.MICAL_RC_STATE_FLAG = 0;
                        return;
                    }
                }
                else
                {
                    modelTag.MICAL_RC_STATE_FLAG = 1;
                }

                modelTag.MICAL_RC_U_TIME = t_huanleng;//环冷冷却时间t环冷 =n；单位min
                DateTime dTime_环冷机_Begin = dTime_板式_Begin.AddMinutes(0 - t_huanleng);
                MICAL6_HuanLeng(dTime_环冷机_Begin, iDataBase, modelTag);


                double t破碎 = (double)modelPiDaiInfo.PAR_BREAK_U_TIME;
                string strdTime_破碎_Begin = GetMC_RUN_START_Time_Str(dTime_环冷机_Begin, "BR_M", iDataBase, t破碎, modelTag);
                if (string.IsNullOrEmpty(strdTime_破碎_Begin))
                {
                    return;
                }
                DateTime dTime_破碎_Begin = DateTime.Parse(strdTime_破碎_Begin);

                ////dTime_破碎_Begin也就是烧结结束的时间
                //MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu2 = 0;
                int tb16_21 = 0;//烧结21#风箱到16#倒推时间tb16-21 =n；单位min
                double Lenght16_21 = (double)(modelPiDaiInfo.PAR_LENGTH_B21 + modelPiDaiInfo.PAR_LENGTH_B20 + modelPiDaiInfo.PAR_LENGTH_B19 + modelPiDaiInfo.PAR_LENGTH_B18 + modelPiDaiInfo.PAR_LENGTH_B17 + modelPiDaiInfo.PAR_LENGTH_B16);
                for (; tb16_21 < 600; tb16_21++)
                {
                    changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - tb16_21), iDataBase);
                    if (changdu2 >= Lenght16_21)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }

                if (flagHlj == false)
                {
                    //就不计算了  说明环冷机关闭的
                    //return;
                }
                double BRP_Locat_TimeValue = tb16_21;
                DateTime BRP_Locat_Time = dTime_破碎_Begin.AddMinutes(0 - BRP_Locat_TimeValue);
                //MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu烧结 = 0;
                int t烧结 = 0;
                //PAR_LENGTH_SIN：烧结机长度，参数值，MC_MICAL_PAR表；
                //MICAL_SIN_SPEED：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
                for (; t烧结 < 600; t烧结++)
                {
                    changdu烧结 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - t烧结), iDataBase);
                    if (changdu烧结 >= (double)(modelPiDaiInfo.PAR_LENGTH_SIN))//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //环冷机累加不到，模糊计算
                    //t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase));
                    modelTag.MICAL_SIN_STATE_FLAG = 0;
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / avgcol);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    modelTag.MICAL_SIN_STATE_FLAG = 1;
                }
                modelTag.MICAL_SIN_U_TIME = t烧结;//烧结工序所需时间
                MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase, t烧结, modelTag);//移到算出t烧结后了
                MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase, t烧结, modelTag);//移到算出t烧结后了

                DateTime dTime_烧结_Begin = dTime_破碎_Begin.AddMinutes(0 - t烧结);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo((DateTime)modelTag.MICAL_BU_C_SIN_TIME, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);

                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽= W混 /(L台* H* V烧 * ρ);
                //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                modelTag.MICAL_SAM_MAT_TIME = (int)Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算程序

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                try
                {
                    if (modelTag.MICAL_M_STATE_FLAG == 3)
                    {
                        //工序设备运行时间不够
                        //计算总时间
                        //MICAL_SAM_MAT_TIME= Lp8/Vp8/60+ t筛分+ Lsf1/Vsf1/60+ t板式+ L环冷/V环冷+ t破碎+L烧结/V烧结+t布料+t混料槽+ LS1/VS1/60+ LZ21/V Z21/60+t二混+ L2H1/V2H1/60+t一混+ L1H1/V1H1/60+t配料
                        modelTag.MICAL_SAM_MAT_TIME = (int)Js_MICAL_SAM_MAT_TIME(dTimeBegin, iDataBase);
                        if (modelTag.MICAL_SAM_MAT_TIME == -1)
                        {
                            modelTag.MICAL_SAM_MAT_TIME = 0;
                        }

                    }
                }
                catch
                { }
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }

        }

        /// <summary>
        /// 当初次运行时，没有基础数据时，算一个大概的运转倒推总时间
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        private double Js_MICAL_SAM_MAT_TIME(DateTime dTime, IDataBase iDataBase)
        {
            //工序设备运行时间不够
            //计算总时间
            //MICAL_SAM_MAT_TIME= Lp8/Vp8/60+ t筛分+ Lsf1/Vsf1/60+ t板式+ L环冷/V环冷+ t破碎+L烧结/V烧结+t布料+t混料槽+ LS1/VS1/60+ LZ21/V Z21/60+t二混+ L2H1/V2H1/60+t一混+ L1H1/V1H1/60+t配料
            //double ttttt= Lp8 / Vp8 / 60 + t筛分 + Lsf1 / Vsf1 / 60 + t板式 + L环冷 / V环冷 + t破碎 + L烧结 / V烧结 + t布料 + t混料槽 + LS1 / VS1 / 60 + LZ21 / V Z21 / 60 + t二混 + L2H1 / V2H1 / 60 + t一混 + L1H1 / V1H1 / 60 + t配料;

            double Lp8 = (double)(modelPiDaiInfo.PAR_LENGTH_P_8);//Lp8：成品皮带长度，参数值，MC_MICAL_PAR表；PAR_LENGTH_P_8字段
            double Vp8 = (double)(modelPiDaiInfo.PAR_SPE_P_8);//Vp8：成品皮带速度，参数值，MC_MICAL_PAR表；PAR_SPE_P_8字段
            double t筛分 = (double)(modelPiDaiInfo.PAR_SCREEN_U_TIME);//t筛分：筛分所需时间，参数值，MC_MICAL_PAR表；PAR_SCREEN_U_TIME字段；单位min
            double LSF1 = (double)(modelPiDaiInfo.PAR_LENGTH_SF_1);//LSF1：进筛分皮带长度，参数值，MC_MICAL_PAR表；PAR_LENGTH_SF_1字段
            double VSF1 = (double)(modelPiDaiInfo.PAR_SPE_SF_1);//VSF1：进筛分皮带速度，参数值，MC_MICAL_PAR表；PAR_SPE_SF_1字段
            double t板式 = (double)(modelPiDaiInfo.PAR_PF_U_TIME);//t板式：板式给矿机所需时间，参数值，MC_MICAL_PAR表；PAR_PF_U_TIME字段；单位min
            double L环冷 = (double)(modelPiDaiInfo.PAR_LENGTH_RC);//L环冷：环冷机周长，参数值，MC_MICAL_PAR表；PAR_LENGTH_RC
            double V环冷 = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "RC_SPEED_PV", dTime, iDataBase); //V环冷：环冷机机速，采集值，T_BURN_COLE_PLC_1MIN表，RC_SPEED_PV字段；
            double t破碎 = (double)(modelPiDaiInfo.PAR_BREAK_U_TIME);//t破碎：破碎工序所需时间，参数值，MC_MICAL_PAR表；PAR_BREAK_U_TIME字段；单位min
            double L烧结 = (double)(modelPiDaiInfo.PAR_LENGTH_SIN);//L烧结：烧结机长度，参数值，MC_MICAL_PAR表；PAR_LENGTH_SIN
            double V烧结 = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime, iDataBase);//V烧结：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
            double t布料 = (double)(modelPiDaiInfo.PAR_CLOTH_U_TIME);//t布料：圆辊到台车布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段；

            //    t混料槽：混合料入矿槽到出矿槽所需时间，单位：min；计算值
            //t混料槽 = W混 /（L台* H*V烧 * ρ）
            //式中：
            double W混 = AvgColumn_ByDtime("T_CLOTH_PLC_1MIN", "C_BLEND_LEVEL", dTime, iDataBase);//W混：混合料槽料位，单位：吨；采集值：T_CLOTH_PLC_1MIN表；C_BLEND_LEVEL字段;
            double L台 = (double)(modelPiDaiInfo.PAR_BL_DENSITY);//L台：台车宽度，单位：米；参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
            double H = AvgColumn_ByDtime("T_CLOTH_PLC_1MIN", "C_THICK_PV", dTime, iDataBase);//H：布料厚度，单位：mm；采集值：T_CLOTH_PLC_1MIN表；C_THICK_PV字段；；
            //V烧：烧结机机速，单位：m / min；采集值，T_BURN_COLE_PLC_1MIN表；BC_SIN_MS_PV字段；
            double ρ = (double)(modelPiDaiInfo.PAR_TROLLEY_L);//ρ：混合料密度，单位：kg / m3；参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；

            if ((L台 * H * V烧结 * ρ) == 0)
            {
                return -1;
            }
            double t混料槽 = W混 / (L台 * H * V烧结 * ρ);

            double Ls1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1);//Ls1：进梭式布料器S - 1皮带长度，单位：m；参数值，MC_MICAL_PAR表；PAR_LENGTH_S1字段
            double Vs1 = (double)(modelPiDaiInfo.PAR_SPE_S_1);//  Vs1：进梭式布料器S - 1皮带速度，参数值，MC_MICAL_PAR表；PAR_SPE_S_1字段
            double Lz21 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1);//    Lz21：出二混皮带长度，参数值，MC_MICAL_PAR表；PAR_LENGTH_Z2_1字段
            double Vz21 = (double)(modelPiDaiInfo.PAR_SPE_Z2_1);//    Vz21：出二混皮带速度，参数值，MC_MICAL_PAR表；PAR_SPE_Z2_1字段
            double t二混 = (double)(modelPiDaiInfo.PAR_2M_U_TIME);//    t二混：二次混合所需时间，参数值，MC_MICAL_PAR表；PAR_2M_U_TIME字段；
            double L2H1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1);//L2H1：进二混皮带长度，参数值，MC_MICAL_PAR表；PAR_LENGTH_2H_1字段
            double V2H1 = (double)(modelPiDaiInfo.PAR_SPE_2H_1);//V2H1：进二混皮带速度，参数值，MC_MICAL_PAR表；PAR_SPE_2H_1字段
            double t一混 = (double)(modelPiDaiInfo.PAR_1M_U_TIME);//t一混：一次混合所需时间，参数值，MC_MICAL_PAR表，PAR_1M_U_TIME字段；
            double L1H1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1);//L1H1：进一混皮带长度，参数值，MC_MICAL_PAR表；PAR_LENGTH_1H_1字段
            double V1H1 = (double)(modelPiDaiInfo.PAR_SPE_1H_1);//V1H1：进一混皮带速度，参数值，MC_MICAL_PAR表；PAR_SPE_1H_1字段
            double t配料 = (double)(modelPiDaiInfo.PAR_MAT_U_T);//t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；


            if ((Vp8 * VSF1 * V环冷 * V烧结 * Vs1 * Vz21 * V2H1 * V1H1) == 0)
            {
                return -1;
            }

            double MICAL_SAM_MAT_TIME = Lp8 / Vp8 / 60 + t筛分 + LSF1 / VSF1 / 60 + t板式 + L环冷 / V环冷 + t破碎 + L烧结 / V烧结 + t布料 + t混料槽 + Ls1 / Vs1 / 60 + Lz21 / Vz21 / 60 + t二混 + L2H1 / V2H1 / 60 + t一混 + L1H1 / V1H1 / 60 + t配料;

            return MICAL_SAM_MAT_TIME;
        }
        /// <summary>
        /// 筛分倒推
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui13_Shaifen(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 13;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;

            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算

                DateTime dTime_筛分_Begin = dTimeBegin;
                MICAL7_ShaiFen(dTime_筛分_Begin, iDataBase, modelTag);
                if (modelTag.MICAL_MATCH_TOTAL_SP != null && modelTag.MICAL_MATCH_TOTAL_SP.Value != 0)
                {
                    modelTag.MICAL_BU_C_FP_RATE = modelTag.MICAL_BU_C_FP_BELT_FT_AVG / modelTag.MICAL_MATCH_TOTAL_SP;// 成品率计算   计算值？？？？？
                }

                //SF-1皮带 tsf1=LSF1/VSF1/60，tsf1：sf1出板式给矿机皮带运行时间；单位min
                double tSF_1 = (double)(modelPiDaiInfo.PAR_LENGTH_SF_1 / modelPiDaiInfo.PAR_SPE_SF_1 / 60);// Tp8：p8成品皮带运行时间。单位min
                string strdTime_SF_1_Begin = GetMC_RUN_START_Time_Str(dTime_筛分_Begin, "Belt_SF_1", iDataBase, tSF_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_SF_1_Begin))
                {
                    return;
                }
                DateTime dTime_SF_1_Begin = DateTime.Parse(strdTime_SF_1_Begin);


                double t板式 = modelPiDaiInfo.PAR_PF_U_TIME.Value;
                string strdTime_板式_Begin = GetMC_RUN_START_Time_Str(dTime_SF_1_Begin, "PF_M", iDataBase, t板式, modelTag);
                if (string.IsNullOrEmpty(strdTime_板式_Begin))
                {
                    return;
                }
                DateTime dTime_板式_Begin = DateTime.Parse(strdTime_板式_Begin);
                modelTag.MICAL_SCR_RC_TIME = Utils.DateDiff(dTime_筛分_Begin, dTime_板式_Begin);
                MICAL6_HuanLeng_ChuKouTemp(dTime_板式_Begin, iDataBase, modelTag);

                //环冷倒推时间
                //PAR_LENGTH_RC：环冷机周长，参数值，MC_MICAL_PAR表；
                //modelPiDaiInfo.PAR_LENGTH_RC
                bool flagHlj = false;
                double changdu = 0;
                int t_huanleng = 0;//环冷冷却时间t环冷 =n；单位min
                for (; t_huanleng < 600; t_huanleng++)
                {
                    changdu += MICAL6_HuanLeng_Sudu_ByDtime(dTime_板式_Begin.AddMinutes(0 - t_huanleng), iDataBase);
                    if (changdu >= modelPiDaiInfo.PAR_LENGTH_RC)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //t_huanleng = (int)(modelPiDaiInfo.PAR_LENGTH_RC / AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "RC_SPEED_PV", dTime_板式_Begin, iDataBase));

                    modelTag.MICAL_RC_STATE_FLAG = 0;
                    //环冷机累加不到，模糊计算
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "RC_SPEED_PV", dTime_板式_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t_huanleng = (int)(modelPiDaiInfo.PAR_LENGTH_RC / avgcol);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    modelTag.MICAL_RC_STATE_FLAG = 1;
                }
                modelTag.MICAL_RC_U_TIME = t_huanleng;//环冷冷却时间t环冷 =n；单位min
                DateTime dTime_环冷机_Begin = dTime_板式_Begin.AddMinutes(0 - t_huanleng);
                MICAL6_HuanLeng(dTime_环冷机_Begin, iDataBase, modelTag);


                double t破碎 = (double)modelPiDaiInfo.PAR_BREAK_U_TIME;
                string strdTime_破碎_Begin = GetMC_RUN_START_Time_Str(dTime_环冷机_Begin, "BR_M", iDataBase, t破碎, modelTag);
                if (string.IsNullOrEmpty(strdTime_破碎_Begin))
                {
                    return;
                }
                DateTime dTime_破碎_Begin = DateTime.Parse(strdTime_破碎_Begin);

                ////dTime_破碎_Begin也就是烧结结束的时间
                //MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu2 = 0;
                int tb16_21 = 0;//烧结21#风箱到16#倒推时间tb16-21 =n；单位min
                double Lenght16_21 = (double)(modelPiDaiInfo.PAR_LENGTH_B21 + modelPiDaiInfo.PAR_LENGTH_B20 + modelPiDaiInfo.PAR_LENGTH_B19 + modelPiDaiInfo.PAR_LENGTH_B18 + modelPiDaiInfo.PAR_LENGTH_B17 + modelPiDaiInfo.PAR_LENGTH_B16);
                for (; tb16_21 < 600; tb16_21++)
                {
                    changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - tb16_21), iDataBase);
                    if (changdu2 >= Lenght16_21)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }

                if (flagHlj == false)
                {
                    //就不计算了  说明环冷机关闭的
                    //return;
                }
                double BRP_Locat_TimeValue = tb16_21;
                DateTime BRP_Locat_Time = dTime_破碎_Begin.AddMinutes(0 - BRP_Locat_TimeValue);
                //MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu烧结 = 0;
                int t烧结 = 0;
                //PAR_LENGTH_SIN：烧结机长度，参数值，MC_MICAL_PAR表；
                //MICAL_SIN_SPEED：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
                for (; t烧结 < 600; t烧结++)
                {
                    changdu烧结 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - t烧结), iDataBase);
                    if (changdu烧结 >= (double)(modelPiDaiInfo.PAR_LENGTH_SIN))//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //环冷机累加不到，模糊计算
                    //t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase));
                    modelTag.MICAL_SIN_STATE_FLAG = 0;
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / avgcol);
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    modelTag.MICAL_SIN_STATE_FLAG = 1;
                }
                modelTag.MICAL_SIN_U_TIME = t烧结;//烧结工序所需时间
                MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase, t烧结, modelTag);//移到算出t烧结后了
                MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase, t烧结, modelTag);//移到算出t烧结后了

                DateTime dTime_烧结_Begin = dTime_破碎_Begin.AddMinutes(0 - t烧结);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo((DateTime)modelTag.MICAL_BU_C_SIN_TIME, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);
                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 板式给矿机倒推
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui12_Bsgkj(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 12;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;


            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算

                DateTime dTime_板式_Begin = dTimeBegin;
                MICAL6_HuanLeng_ChuKouTemp(dTime_板式_Begin, iDataBase, modelTag);
                //modelTag.MICAL_BU_C_RC_TIME = null;

                //环冷倒推时间
                //PAR_LENGTH_RC：环冷机周长，参数值，MC_MICAL_PAR表；
                //modelPiDaiInfo.PAR_LENGTH_RC
                bool flagHlj = false;
                double changdu = 0;
                int t_huanleng = 0;//环冷冷却时间t环冷 =n；单位min
                for (; t_huanleng < 600; t_huanleng++)
                {
                    changdu += MICAL6_HuanLeng_Sudu_ByDtime(dTime_板式_Begin.AddMinutes(0 - t_huanleng), iDataBase);
                    if (changdu >= modelPiDaiInfo.PAR_LENGTH_RC)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //环冷机累加不到，模糊计算
                    //t_huanleng = (int)(modelPiDaiInfo.PAR_LENGTH_RC / AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "RC_SPEED_PV", dTime_板式_Begin, iDataBase));
                    modelTag.MICAL_RC_STATE_FLAG = 0;
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "RC_SPEED_PV", dTime_板式_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t_huanleng = (int)(modelPiDaiInfo.PAR_LENGTH_RC / avgcol);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    modelTag.MICAL_RC_STATE_FLAG = 1;
                }
                modelTag.MICAL_RC_U_TIME = t_huanleng;//环冷冷却时间t环冷 =n；单位min
                DateTime dTime_环冷机_Begin = dTime_板式_Begin.AddMinutes(0 - t_huanleng);
                modelTag.MICAL_BU_C_RC_TIME = dTime_环冷机_Begin;

                MICAL6_HuanLeng(dTime_环冷机_Begin, iDataBase, modelTag);


                double t破碎 = (double)modelPiDaiInfo.PAR_BREAK_U_TIME;
                string strdTime_破碎_Begin = GetMC_RUN_START_Time_Str(dTime_环冷机_Begin, "BR_M", iDataBase, t破碎, modelTag);
                if (string.IsNullOrEmpty(strdTime_破碎_Begin))
                {
                    return;
                }
                DateTime dTime_破碎_Begin = DateTime.Parse(strdTime_破碎_Begin);

                ////dTime_破碎_Begin也就是烧结结束的时间
                //MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase);

                flagHlj = false;
                double changdu2 = 0;
                int tb16_21 = 0;//烧结21#风箱到16#倒推时间tb16-21 =n；单位min
                double Lenght16_21 = (double)(modelPiDaiInfo.PAR_LENGTH_B21 + modelPiDaiInfo.PAR_LENGTH_B20 + modelPiDaiInfo.PAR_LENGTH_B19 + modelPiDaiInfo.PAR_LENGTH_B18 + modelPiDaiInfo.PAR_LENGTH_B17 + modelPiDaiInfo.PAR_LENGTH_B16);
                for (; tb16_21 < 600; tb16_21++)
                {
                    changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - tb16_21), iDataBase);
                    if (changdu2 >= Lenght16_21)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }

                if (flagHlj == false)
                {
                    //就不计算了  说明环冷机关闭的
                    //return;
                }
                double BRP_Locat_TimeValue = tb16_21;
                DateTime BRP_Locat_Time = dTime_破碎_Begin.AddMinutes(0 - BRP_Locat_TimeValue);
                //MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu烧结 = 0;
                int t烧结 = 0;
                //PAR_LENGTH_SIN：烧结机长度，参数值，MC_MICAL_PAR表；
                //MICAL_SIN_SPEED：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
                for (; t烧结 < 600; t烧结++)
                {
                    changdu烧结 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - t烧结), iDataBase);
                    if (changdu烧结 >= (double)(modelPiDaiInfo.PAR_LENGTH_SIN))//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //环冷机累加不到，模糊计算
                    //t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase));
                    modelTag.MICAL_SIN_STATE_FLAG = 0;
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / avgcol);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    modelTag.MICAL_SIN_STATE_FLAG = 1;
                }
                modelTag.MICAL_SIN_U_TIME = t烧结;//烧结工序所需时间
                MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase, t烧结, modelTag);//移到算出t烧结后了
                MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase, t烧结, modelTag);//移到算出t烧结后了

                DateTime dTime_烧结_Begin = dTime_破碎_Begin.AddMinutes(0 - t烧结);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo((DateTime)modelTag.MICAL_BU_C_SIN_TIME, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 换冷入口倒推
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui11_Hlrk(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 11;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;

            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算

                DateTime dTime_环冷机_Begin = dTimeBegin;
                MICAL6_HuanLeng(dTime_环冷机_Begin, iDataBase, modelTag);


                bool flagHlj = false;
                double t破碎 = (double)modelPiDaiInfo.PAR_BREAK_U_TIME;
                string strdTime_破碎_Begin = GetMC_RUN_START_Time_Str(dTime_环冷机_Begin, "BR_M", iDataBase, t破碎, modelTag);
                if (string.IsNullOrEmpty(strdTime_破碎_Begin))
                {
                    return;
                }
                DateTime dTime_破碎_Begin = DateTime.Parse(strdTime_破碎_Begin);

                ////dTime_破碎_Begin也就是烧结结束的时间
                //MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu2 = 0;
                int tb16_21 = 0;//烧结21#风箱到16#倒推时间tb16-21 =n；单位min
                double Lenght16_21 = (double)(modelPiDaiInfo.PAR_LENGTH_B21 + modelPiDaiInfo.PAR_LENGTH_B20 + modelPiDaiInfo.PAR_LENGTH_B19 + modelPiDaiInfo.PAR_LENGTH_B18 + modelPiDaiInfo.PAR_LENGTH_B17 + modelPiDaiInfo.PAR_LENGTH_B16);
                for (; tb16_21 < 600; tb16_21++)
                {
                    changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - tb16_21), iDataBase);
                    if (changdu2 >= Lenght16_21)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }

                if (flagHlj == false)
                {
                    //就不计算了  说明环冷机关闭的
                    //return;
                }
                double BRP_Locat_TimeValue = tb16_21;
                DateTime BRP_Locat_Time = dTime_破碎_Begin.AddMinutes(0 - BRP_Locat_TimeValue);
                //MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu烧结 = 0;
                int t烧结 = 0;
                //PAR_LENGTH_SIN：烧结机长度，参数值，MC_MICAL_PAR表；
                //MICAL_SIN_SPEED：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
                for (; t烧结 < 600; t烧结++)
                {
                    changdu烧结 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - t烧结), iDataBase);
                    if (changdu烧结 >= (double)(modelPiDaiInfo.PAR_LENGTH_SIN))//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //环冷机累加不到，模糊计算
                    //t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase));
                    modelTag.MICAL_SIN_STATE_FLAG = 0;
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / avgcol);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    modelTag.MICAL_SIN_STATE_FLAG = 1;
                }
                modelTag.MICAL_SIN_U_TIME = t烧结;//烧结工序所需时间
                MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase, t烧结, modelTag);//移到算出t烧结后了
                MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase, t烧结, modelTag);//移到算出t烧结后了

                DateTime dTime_烧结_Begin = dTime_破碎_Begin.AddMinutes(0 - t烧结);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo((DateTime)modelTag.MICAL_BU_C_SIN_TIME, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 19-21风箱
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui10_Fengxiang19_21(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 10;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;

            bool flagHlj = false;

            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算


                DateTime dTime_破碎_Begin = dTimeBegin;

                ////dTime_破碎_Begin也就是烧结结束的时间
                //MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu2 = 0;
                int tb16_21 = 0;//烧结21#风箱到16#倒推时间tb16-21 =n；单位min
                double Lenght16_21 = (double)(modelPiDaiInfo.PAR_LENGTH_B21 + modelPiDaiInfo.PAR_LENGTH_B20 + modelPiDaiInfo.PAR_LENGTH_B19 + modelPiDaiInfo.PAR_LENGTH_B18 + modelPiDaiInfo.PAR_LENGTH_B17 + modelPiDaiInfo.PAR_LENGTH_B16);
                for (; tb16_21 < 600; tb16_21++)
                {
                    changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - tb16_21), iDataBase);
                    if (changdu2 >= Lenght16_21)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }

                if (flagHlj == false)
                {
                    //就不计算了  说明环冷机关闭的
                    //return;
                }
                double BRP_Locat_TimeValue = tb16_21;
                DateTime BRP_Locat_Time = dTime_破碎_Begin.AddMinutes(0 - BRP_Locat_TimeValue);
                //MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase);//移到算出t烧结后了

                flagHlj = false;
                double changdu烧结 = 0;
                int t烧结 = 0;
                //PAR_LENGTH_SIN：烧结机长度，参数值，MC_MICAL_PAR表；
                //MICAL_SIN_SPEED：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
                for (; t烧结 < 600; t烧结++)
                {
                    changdu烧结 += MICAL5_ShaoJie_Sudu_ByDtime(dTime_破碎_Begin.AddMinutes(0 - t烧结), iDataBase);
                    if (changdu烧结 >= (double)(modelPiDaiInfo.PAR_LENGTH_SIN))//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }
                if (flagHlj == false)
                {
                    //环冷机累加不到，模糊计算
                    //t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase));
                    modelTag.MICAL_SIN_STATE_FLAG = 0;
                    double avgcol = AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime_破碎_Begin, iDataBase);
                    if (avgcol != 0)
                    {
                        t烧结 = (int)(modelPiDaiInfo.PAR_LENGTH_SIN / avgcol);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    modelTag.MICAL_SIN_STATE_FLAG = 1;
                }
                modelTag.MICAL_SIN_U_TIME = t烧结;//烧结工序所需时间
                MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase, t烧结, modelTag);//移到算出t烧结后了
                MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase, t烧结, modelTag);//移到算出t烧结后了

                DateTime dTime_烧结_Begin = dTime_破碎_Begin.AddMinutes(0 - t烧结);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo((DateTime)modelTag.MICAL_BU_C_SIN_TIME, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;


                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }





        }
        /// <summary>
        /// 16-18风箱
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui9_Fengxiang16_18(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 9;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;

            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }
                #region 计算


                MICAL5_ShaoJie(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                //判断BTP
                if (modelTag.MICAL_BU_C_LOCAT_BTP > 70)
                {
                    modelTag.MICAL_BU_C_LOCAT_BTP = null;
                    modelTag.MICAL_BU_C_BTP_TE = null;
                }


                double t1_18 = Get1_18_FengJi_Time(dTimeBegin, iDataBase, modelTag);

                bool flagHlj = false;

                //DateTime dTime_破碎_Begin = dTimeBegin;

                ////dTime_破碎_Begin也就是烧结结束的时间
                //MICAL5_ShaoJie(dTime_破碎_Begin, iDataBase);

                flagHlj = false;
                double changdu2 = 0;
                int tb16_18 = 0;//烧结21#风箱到16#倒推时间tb16-21 =n；单位min
                double Lenght16_18 = (double)(modelPiDaiInfo.PAR_LENGTH_B18 + modelPiDaiInfo.PAR_LENGTH_B17 + modelPiDaiInfo.PAR_LENGTH_B16);
                for (; tb16_18 < 600; tb16_18++)
                {
                    changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dTimeBegin.AddMinutes(0 - tb16_18), iDataBase);
                    if (changdu2 >= Lenght16_18)//大于 环冷机周长 时跳出
                    {
                        flagHlj = true;
                        break;
                    }
                }

                if (flagHlj == false)
                {
                    //就不计算了  说明环冷机关闭的
                    //return;
                }
                double BRP_Locat_TimeValue = tb16_18;
                DateTime BRP_Locat_Time = dTimeBegin.AddMinutes(0 - BRP_Locat_TimeValue);
                MICAL5_ShaoJie_BRP位置温度(BRP_Locat_Time, iDataBase, MinuteAvg, modelTag);

                modelTag.MICAL_SIN_1_18_TIME = t1_18;

                DateTime dTime_烧结_Begin = dTimeBegin.AddMinutes(0 - t1_18);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo(dTime_烧结_Begin, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;


                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算


                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }

            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 13-15风箱
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui8_Fengxiang13_15(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 8;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;


            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }
                #region 计算

                MICAL5_ShaoJie(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                //判断BTP
                if (modelTag.MICAL_BU_C_LOCAT_BTP > 58)
                {
                    modelTag.MICAL_BU_C_LOCAT_BTP = null;
                    modelTag.MICAL_BU_C_BTP_TE = null;
                }

                MICAL5_ShaoJie_BRP位置温度(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                if (modelTag.MICAL_BU_C_LOCAT_BRP > 58)
                {
                    modelTag.MICAL_BU_C_LOCAT_BRP = null;
                    modelTag.MICAL_BU_C_BRP_TE = null;
                }

                double t1_15 = Get1_15_FengJi_Time(dTimeBegin, iDataBase, modelTag);

                modelTag.MICAL_SIN_1_15_TIME = t1_15;

                DateTime dTime_烧结_Begin = dTimeBegin.AddMinutes(0 - t1_15);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo(dTime_烧结_Begin, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);
                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 10-12风箱
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui7_Fengxiang10_12(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 7;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;


            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算

                MICAL5_ShaoJie(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                //判断BTP
                if (modelTag.MICAL_BU_C_LOCAT_BTP > 46)
                {
                    modelTag.MICAL_BU_C_LOCAT_BTP = null;
                    modelTag.MICAL_BU_C_BTP_TE = null;
                }

                MICAL5_ShaoJie_BRP位置温度(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                if (modelTag.MICAL_BU_C_LOCAT_BRP > 46)
                {
                    modelTag.MICAL_BU_C_LOCAT_BRP = null;
                    modelTag.MICAL_BU_C_BRP_TE = null;
                }

                double t1_12 = Get1_12_FengJi_Time(dTimeBegin, iDataBase, modelTag);

                modelTag.MICAL_SIN_1_12_TIME = t1_12;

                DateTime dTime_烧结_Begin = dTimeBegin.AddMinutes(0 - t1_12);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo(dTime_烧结_Begin, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);
                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 7-9风箱
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui6_Fengxiang7_9(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 6;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;

            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算

                MICAL5_ShaoJie(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                //判断BTP
                if (modelTag.MICAL_BU_C_LOCAT_BTP > 34)
                {
                    modelTag.MICAL_BU_C_LOCAT_BTP = null;
                    modelTag.MICAL_BU_C_BTP_TE = null;
                }

                MICAL5_ShaoJie_BRP位置温度(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                if (modelTag.MICAL_BU_C_LOCAT_BRP > 34)
                {
                    modelTag.MICAL_BU_C_LOCAT_BRP = null;
                    modelTag.MICAL_BU_C_BRP_TE = null;
                }

                double t1_9 = Get1_9_FengJi_Time(dTimeBegin, iDataBase, modelTag);

                modelTag.MICAL_SIN_1_9_TIME = t1_9;

                DateTime dTime_烧结_Begin = dTimeBegin.AddMinutes(0 - t1_9);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo(dTime_烧结_Begin, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);
                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 4-6风箱
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui5_Fengxiang4_6(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 5;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;


            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算

                MICAL5_ShaoJie(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                //判断BTP
                if (modelTag.MICAL_BU_C_LOCAT_BTP > 22)
                {
                    modelTag.MICAL_BU_C_LOCAT_BTP = null;
                    modelTag.MICAL_BU_C_BTP_TE = null;
                }

                MICAL5_ShaoJie_BRP位置温度(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                if (modelTag.MICAL_BU_C_LOCAT_BRP > 22)
                {
                    modelTag.MICAL_BU_C_LOCAT_BRP = null;
                    modelTag.MICAL_BU_C_BRP_TE = null;
                }

                double t1_6 = Get1_6_FengJi_Time(dTimeBegin, iDataBase, modelTag);

                modelTag.MICAL_SIN_1_6_TIME = t1_6;

                DateTime dTime_烧结_Begin = dTimeBegin.AddMinutes(0 - t1_6);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo(dTime_烧结_Begin, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);
                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 1-3风箱
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui4_Fengxiang1_3(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 4;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;


            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }
                #region 计算

                MICAL5_ShaoJie(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                //判断BTP
                if (modelTag.MICAL_BU_C_LOCAT_BTP > 10)
                {
                    modelTag.MICAL_BU_C_LOCAT_BTP = null;
                    modelTag.MICAL_BU_C_BTP_TE = null;
                }

                MICAL5_ShaoJie_BRP位置温度(dTimeBegin, iDataBase, MinuteAvg, modelTag);
                if (modelTag.MICAL_BU_C_LOCAT_BRP > 10)
                {
                    modelTag.MICAL_BU_C_LOCAT_BRP = null;
                    modelTag.MICAL_BU_C_BRP_TE = null;
                }

                double t1_3 = Get1_3_FengJi_Time(dTimeBegin, iDataBase, modelTag);

                modelTag.MICAL_SIN_1_3_TIME = t1_3;

                DateTime dTime_烧结_Begin = dTimeBegin.AddMinutes(0 - t1_3);//烧结开始时间

                modelTag.MICAL_BU_C_SIN_TIME = dTime_烧结_Begin;

                MICAL4_DianHuo(dTime_烧结_Begin, iDataBase, modelTag);

                //t布料：布料所需时间，参数值，MC_MICAL_PAR表；PAR_CLOTH_U_TIME字段
                double t布料 = (double)modelPiDaiInfo.PAR_CLOTH_U_TIME;
                //string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "Belt_S_6", iDataBase, t布料);
                string strdTime_布料_Begin = GetMC_RUN_START_Time_Str((DateTime)modelTag.MICAL_BU_C_SIN_TIME, "C_STICK", iDataBase, t布料, modelTag);
                if (string.IsNullOrEmpty(strdTime_布料_Begin))
                {
                    return;
                }
                DateTime dTime_布料_Begin = DateTime.Parse(strdTime_布料_Begin);
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //t混料槽：目标料批在混料槽下料耗时(单位分钟)
                //DTime_混料槽：取样点做倒推时，目标料批在混料槽开始的时间
                //t混料槽=W混/（L台*H* V烧*ρ）
                //W混：混合料槽料位，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_BL_TAR_LEVEL
                //L台：台车宽度，参数值，MC_MICAL_PAR表；PAR_BL_DENSITY字段；
                //H：布料厚度，入库位置：MC_MICAL_RESULT表；字段MICAL_CLOTH_THICK_AVG
                //V烧：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表；字段BC_SIN_MS_PV  
                //ρ：混合料密度，参数值，MC_MICAL_PAR表；PAR_TROLLEY_L字段；
                double W混 = (double)modelTag.MICAL_CLOTH_BL_TAR_LEVEL;
                double L台 = (double)modelPiDaiInfo.PAR_BL_DENSITY;
                double H = (double)modelTag.MICAL_CLOTH_THICK_AVG;
                double V烧 = MICAL5_ShaoJie_AvgSudu_ByDtime(dTime_布料_Begin, iDataBase);
                double ρ = (double)modelPiDaiInfo.PAR_TROLLEY_L;
                double t混料槽 = 5;
                if ((L台 * H * V烧 * ρ) != 0)
                {
                    t混料槽 = W混 / (L台 * (H / 1000) * V烧 * (ρ / 1000));
                    //double t混料槽= W混 /(L台* (H/1000)* V烧 * (ρ/1000));
                }
                //double t混料槽 = W混 / (L台 * H * V烧 * ρ);
                modelTag.MICAL_BL_BUNK_TIME = t混料槽;
                DateTime dTime_混料槽_Begin = dTime_布料_Begin.AddMinutes(0 - t混料槽);

                modelTag.MICAL_CLOTH_DST_TIME = dTime_混料槽_Begin;

                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_混料槽_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                //modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_混料槽_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }




        }
        /// <summary>
        /// 布料倒推
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui3_Buliao(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 3;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;

            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }
                #region 计算


                //t布料：
                DateTime dTime_布料_Begin = dTimeBegin;
                MICAL3_BuLiao(dTime_布料_Begin, iDataBase, modelTag);


                //S-1皮带
                double tS_1 = (double)(modelPiDaiInfo.PAR_LENGTH_S1 / modelPiDaiInfo.PAR_SPE_S_1 / 60);//单位min
                string strdTime_S_1_Begin = GetMC_RUN_START_Time_Str(dTime_布料_Begin, "Belt_S_1", iDataBase, tS_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_S_1_Begin))
                {
                    return;
                }
                DateTime dTime_S_1_Begin = DateTime.Parse(strdTime_S_1_Begin);
                MICAL3_BuLiao_Z2_1(dTime_S_1_Begin, iDataBase, modelTag);


                //Z2-1皮带
                double tZ2_1 = (double)(modelPiDaiInfo.PAR_LENGTH_Z2_1 / modelPiDaiInfo.PAR_SPE_Z2_1 / 60);//单位min
                string strdTime_Z2_1_Begin = GetMC_RUN_START_Time_Str(dTime_S_1_Begin, "Belt_Z2_1", iDataBase, tZ2_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_Z2_1_Begin))
                {
                    return;
                }
                DateTime dTime_Z2_1_Begin = DateTime.Parse(strdTime_Z2_1_Begin);

                //二混：
                double t二混 = modelPiDaiInfo.PAR_2M_U_TIME.Value;
                string strdTime_二混_Begin = GetMC_RUN_START_Time_Str(dTime_Z2_1_Begin, "MIX_2", iDataBase, t二混, modelTag);
                if (string.IsNullOrEmpty(strdTime_二混_Begin))
                {
                    return;
                }
                DateTime dTime_二混_Begin = DateTime.Parse(strdTime_二混_Begin);

                modelTag.MICAL_CLO_2M_TIME = Utils.DateDiff(dTime_布料_Begin, dTime_二混_Begin);
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }




        }
        /// <summary>
        /// 二混倒推
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui2_ErHun(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 2;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;


            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算

                //二混：
                DateTime dTime_二混_Begin = dTimeBegin;
                MICAL2_ErHun(dTime_二混_Begin, iDataBase, modelTag);


                //2H-1皮带
                double t2H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_2H_1 / modelPiDaiInfo.PAR_SPE_2H_1 / 60);//单位min
                string strdTime_2H_1_Begin = GetMC_RUN_START_Time_Str(dTime_二混_Begin, "Belt_2H_1", iDataBase, t2H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_2H_1_Begin))
                {
                    return;
                }
                DateTime dTime_2H_1_Begin = DateTime.Parse(strdTime_2H_1_Begin);


                //一混：
                double t一混 = modelPiDaiInfo.PAR_1M_U_TIME.Value;
                string strdTime_一混_Begin = GetMC_RUN_START_Time_Str(dTime_2H_1_Begin, "MIX_1", iDataBase, t一混, modelTag);
                if (string.IsNullOrEmpty(strdTime_一混_Begin))
                {
                    return;
                }
                DateTime dTime_一混_Begin = DateTime.Parse(strdTime_一混_Begin);
                modelTag.MICAL_2M_1M_TIME = Utils.DateDiff(dTime_二混_Begin, dTime_一混_Begin);
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算


                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }



        }
        /// <summary>
        /// 一混倒推
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private void DaoTui1_YiHun(IDataBase iDataBase, DateTime dTimeBegin, out MC_MICAL_RESULT modelTag)
        {
            modelTag = new MC_MICAL_RESULT();
            modelTag.TIMESTAMP = dTimeBegin;
            modelTag.DATANUM = 1;
            //modelTag.MICAL_BU_C_SAMP_TIME = dTimeBegin;


            try
            {
                if (!t_PLC_5S.T_SIN_SL_5S && !t_PLC_5S.T_RC_SL_5S)//4：模型计算停止
                {
                    modelTag.MICAL_M_STATE_FLAG = 4;
                    return;
                }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 3;
                }

                #region 计算
                DateTime dTime_一混_Begin = dTimeBegin;
                MICAL1_YiHun(dTime_一混_Begin, iDataBase, modelTag);

                //1H-1皮带
                double t1H_1 = (double)(modelPiDaiInfo.PAR_LENGTH_1H_1 / modelPiDaiInfo.PAR_SPE_1H_1 / 60);//单位min
                string strdTime_1H_1_Begin = GetMC_RUN_START_Time_Str(dTime_一混_Begin, "Belt_1H_1", iDataBase, t1H_1, modelTag);
                if (string.IsNullOrEmpty(strdTime_1H_1_Begin))
                {
                    return;
                }
                DateTime dTime_1H_1_Begin = DateTime.Parse(strdTime_1H_1_Begin);


                //
                //t配料：配料所需时间，参数值，MC_MICAL_PAR表；PAR_MAT_U_T字段；
                double t配料 = (double)modelPiDaiInfo.PAR_MAT_U_T;
                string strdTime_配料_Begin = GetMC_RUN_START_Time_Str(dTime_1H_1_Begin, "Belt_P_7", iDataBase, t配料, modelTag);
                if (string.IsNullOrEmpty(strdTime_配料_Begin))
                {
                    return;
                }
                DateTime dTime_配料_Begin = DateTime.Parse(strdTime_配料_Begin);

                modelTag.MICAL_1M_MAT_TIME = Utils.DateDiff(dTime_一混_Begin, dTime_配料_Begin);

                //modelTag.MICAL_SAM_MAT_TIME = Utils.DateDiff(dTimeBegin, dTime_配料_Begin);
                MICAL0_PeiBi(dTime_配料_Begin, iDataBase, modelTag);

                #endregion 计算

                if (modelTag.MICAL_M_STATE_FLAG == 2)
                { }
                else
                {
                    modelTag.MICAL_M_STATE_FLAG = 1;
                }
            }
            catch (Exception ex)
            {
                modelTag.MICAL_M_STATE_FLAG = 2;
                Writelog(ex.Message + ex.StackTrace);
            }
            finally
            {
                //存库
                //iDataBase.Insert<MC_MICAL_RESULT>(modelTag);
            }




        }
        #region 倒推时引用的方法

        /// <summary>
        /// 环冷机器某个时间点的速度
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        private double MICAL6_HuanLeng_Sudu_ByDtime(DateTime dTime, IDataBase iDataBase)
        {
            double sudu = 0;
            data.Clear();
            data.Dispose();
            //MICAL_BU_C_RC_TIME 环冷记录时间  采集值
            //MICAL_BU_C_RC_B_IN_TE   入口温度 采集值 T_BURN_COLE_PLC_1MIN RC_IN_TE
            //MICAL_BU_C_RC_B_OUT_TE 出口温度    采集值 T_BURN_COLE_PLC_1MIN    RC_OUT_TE
            //MICAL_BU_C_RC_SPEED_SP  环冷机设定机速 采集值 T_BURN_COLE_PLC_1MIN RC_SPEED_SP
            //MICAL_BU_C_RC_SPEED_PV 环冷机实际机速 采集值 T_BURN_COLE_PLC_1MIN    RC_SPEED_PV

            //MICAL_SIN_SPEED：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段
            //T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
            sqlCol = "timestamp,RC_IN_TE,RC_OUT_TE,RC_SPEED_SP,RC_SPEED_PV,BC_SIN_MS_PV";
            sqlstr = GetSql(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["RC_SPEED_PV"] != null && row["RC_SPEED_PV"].ToString() != "")
                    {
                        sudu = double.Parse(row["RC_SPEED_PV"].ToString());
                    }

                }
            }
            return sudu;
        }



        private double MICAL5_ShaoJie_Sudu_ByDtime(DateTime dTime, IDataBase iDataBase)
        {
            double sudu = 0;
            data.Clear();
            data.Dispose();
            //MICAL_BU_C_RC_TIME 环冷记录时间  采集值
            //MICAL_BU_C_RC_B_IN_TE   入口温度 采集值 T_BURN_COLE_PLC_1MIN RC_IN_TE
            //MICAL_BU_C_RC_B_OUT_TE 出口温度    采集值 T_BURN_COLE_PLC_1MIN    RC_OUT_TE
            //MICAL_BU_C_RC_SPEED_SP  环冷机设定机速 采集值 T_BURN_COLE_PLC_1MIN RC_SPEED_SP
            //MICAL_BU_C_RC_SPEED_PV 环冷机实际机速 采集值 T_BURN_COLE_PLC_1MIN    RC_SPEED_PV

            //MICAL_SIN_SPEED：烧结机机速，采集值，T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段
            //T_BURN_COLE_PLC_1MIN表，BC_SIN_MS_PV字段；
            sqlCol = "timestamp,RC_IN_TE,RC_OUT_TE,RC_SPEED_SP,RC_SPEED_PV,BC_SIN_MS_PV";
            sqlstr = GetSql(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime);
            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["BC_SIN_MS_PV"] != null && row["BC_SIN_MS_PV"].ToString() != "")
                    {
                        sudu = double.Parse(row["BC_SIN_MS_PV"].ToString());
                    }

                }
            }
            return sudu;
        }

        private double MICAL5_ShaoJie_AvgSudu_ByDtime(DateTime dTime, IDataBase iDataBase)
        {
            return AvgColumn_ByDtime("T_BURN_COLE_PLC_1MIN", "BC_SIN_MS_PV", dTime, iDataBase);
            //double sudu = -1;
            //data.Clear();
            //data.Dispose();
            //sqlCol = "AVG(BC_SIN_MS_PV) as avg_BC_SIN_MS_PV";
            //sqlstr = GetSql_AVG(sqlCol, "T_BURN_COLE_PLC_1MIN", dTime, MinuteAvg);

            //data = iDataBase.GetDataTable(sqlstr);
            //if (data != null && data.Rows.Count > 0)
            //{
            //    var row = data.Rows[0];
            //    if (row != null)
            //    {
            //        if (row["avg_BC_SIN_MS_PV"] != null && row["avg_BC_SIN_MS_PV"].ToString() != "")
            //        {
            //            sudu = double.Parse(row["avg_BC_SIN_MS_PV"].ToString());
            //        }
            //    }
            //}
            //return sudu;
        }

        /// <summary>
        /// 调用此方法的地方要判断是否为零  待解决。。。！！！！！！
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="colName"></param>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        private double AvgColumn_ByDtime(string tabName, string colName, DateTime dTime, IDataBase iDataBase)
        {
            //double sudu = -1;
            double sudu = 0;
            data.Clear();
            data.Dispose();
            sqlCol = string.Format("round(AVG({0}),5) as avg_Col", colName);
            sqlstr = GetSql_AVG(sqlCol, tabName, dTime, MinuteAvg);

            data = iDataBase.GetDataTable(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                var row = data.Rows[0];
                if (row != null)
                {
                    if (row["avg_Col"] != null && row["avg_Col"].ToString() != "")
                    {
                        sudu = double.Parse(row["avg_Col"].ToString());
                    }
                }
            }

            if (sudu == 0)
            {
                string sqltime = string.Format("select max(timestamp) from {0}", tabName);
                string timeStr = iDataBase.GetString(sqltime);
                if (!string.IsNullOrEmpty(timeStr))
                {
                    DateTime maxTime = DateTime.Now;
                    if (DateTime.TryParse(timeStr, out maxTime))
                    {
                        sqlstr = GetSql_AVG(sqlCol, tabName, maxTime, MinuteAvg);
                        data = iDataBase.GetDataTable(sqlstr);
                        if (data != null && data.Rows.Count > 0)
                        {
                            var row = data.Rows[0];
                            if (row != null)
                            {
                                if (row["avg_Col"] != null && row["avg_Col"].ToString() != "")
                                {
                                    sudu = double.Parse(row["avg_Col"].ToString());
                                }
                            }
                        }
                    }
                }
            }

            return sudu;
        }


        /// <summary>
        /// 配比数据补充
        /// </summary>
        /// <param name="dTime"></param>
        public void MICAL0_PeiBi_Bak(DateTime dTime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {

            List<MC_SINCAL_INTERFACE_TRIG_MIN> mC_SINCAL_INTERFACE_TRIG_MINs = Get_MC_SINCAL_INTERFACE_TRIG_min(dTime, iDataBase);


            //混匀矿配比（干）
            var hunkuang = mC_SINCAL_INTERFACE_TRIG_MINs.FindAll(m => m.SINCAL_COOD <= 199);
            if (hunkuang.Count >= 1)
            {
                //modelTag.MICAL_MATCH_BLO_RATIO_DRY = hunkuang[0].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_BLO_RATIO_DRY = hunkuang[0].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_BLO_RATIO_WET = hunkuang[0].SINCAL_ORE_WET_BILL.ToDouble();
            }





            //直供料1配比（干） 1-4
            var zhigongliao = mC_SINCAL_INTERFACE_TRIG_MINs.FindAll(m => (m.SINCAL_COOD <= 299 && m.SINCAL_COOD > 199));
            if (zhigongliao.Count >= 1)
            {
                //modelTag.MICAL_MATCH_DRF_RATIO_1_DRY = hunkuang[0].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_1_DRY = zhigongliao[0].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_1_WET = zhigongliao[0].SINCAL_ORE_WET_BILL.ToDouble();
            }
            if (zhigongliao.Count >= 2)
            {
                //modelTag.MICAL_MATCH_DRF_RATIO_2_DRY = hunkuang[1].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_2_DRY = zhigongliao[1].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_2_WET = zhigongliao[1].SINCAL_ORE_WET_BILL.ToDouble();
            }
            if (zhigongliao.Count >= 3)
            {
                //modelTag.MICAL_MATCH_DRF_RATIO_3_DRY = hunkuang[2].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_3_DRY = zhigongliao[2].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_3_WET = zhigongliao[2].SINCAL_ORE_WET_BILL.ToDouble();
            }
            if (zhigongliao.Count >= 4)
            {
                //modelTag.MICAL_MATCH_DRF_RATIO_4_DRY = hunkuang[3].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_4_DRY = zhigongliao[3].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_DRF_RATIO_4_WET = zhigongliao[3].SINCAL_ORE_WET_BILL.ToDouble();
            }

            double RATIO_DRY = 0;
            double RATIO_WET = 0;
            //烧返矿配比（干）
            var shaofankuang = mC_SINCAL_INTERFACE_TRIG_MINs.FindAll(m => (m.SINCAL_COOD <= 699 && m.SINCAL_COOD > 600));
            for (int j = 0; j < shaofankuang.Count;)
            {
                var qita = shaofankuang.FindAll(m => m.SINCAL_COOD == shaofankuang[j].SINCAL_COOD && m.SINCAL_ID == shaofankuang[j].SINCAL_ID);
                if (qita.Count >= 1)
                {
                    RATIO_DRY += qita[0].SINCAL_ORE_DRY_BILL.ToDouble();
                    RATIO_WET += qita[0].SINCAL_ORE_WET_BILL.ToDouble();
                    ////modelTag.MICAL_MATCH_RE_RATIO_DRY = hunkuang[0].SINCAL_ORE_SP.ToDouble();
                    //modelTag.MICAL_MATCH_RE_RATIO_DRY += qita[0].SINCAL_ORE_DRY_BILL.ToDouble();
                    //modelTag.MICAL_MATCH_RE_RATIO_WET += qita[0].SINCAL_ORE_WET_BILL.ToDouble();
                }
                j += qita.Count;
            }
            modelTag.MICAL_MATCH_RE_RATIO_DRY = RATIO_DRY;
            modelTag.MICAL_MATCH_RE_RATIO_WET = RATIO_WET;

            //溶剂1配比（干） 1-3
            var rongjipeibi = mC_SINCAL_INTERFACE_TRIG_MINs.FindAll(m => (m.SINCAL_COOD <= 499 && m.SINCAL_COOD > 400));
            if (rongjipeibi.Count >= 1)
            {
                //modelTag.MICAL_MATCH_LIMS_RATIO_DRY = hunkuang[0].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_LIMS_RATIO_DRY = rongjipeibi[0].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_LIMS_RATIO_WET = rongjipeibi[0].SINCAL_ORE_WET_BILL.ToDouble();
            }
            if (rongjipeibi.Count >= 2)
            {
                //modelTag.MICAL_MATCH_SLIMS_RATIO_DRY = hunkuang[1].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_SLIMS_RATIO_DRY = rongjipeibi[1].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_SLIMS_RATIO_WET = rongjipeibi[1].SINCAL_ORE_WET_BILL.ToDouble();
            }
            if (rongjipeibi.Count >= 3)
            {
                //modelTag.MICAL_MATCH_QLIME_RATIO_DRY = hunkuang[2].SINCAL_ORE_SP.ToDouble();
                modelTag.MICAL_MATCH_QLIME_RATIO_DRY = rongjipeibi[2].SINCAL_ORE_DRY_BILL.ToDouble();
                modelTag.MICAL_MATCH_QLIME_RATIO_WET = rongjipeibi[2].SINCAL_ORE_WET_BILL.ToDouble();
            }


            double DUST_RATIO_DRY = 0;
            double DUST_RATIO_WET = 0;
            //除尘灰配比（干）MICAL_MATCH_DUST_RATIO_DRY  //求和
            var chuchenhuiPb = mC_SINCAL_INTERFACE_TRIG_MINs.FindAll(m => (m.SINCAL_COOD <= 599 && m.SINCAL_COOD > 500));
            for (int j = 0; j < chuchenhuiPb.Count;)
            {
                var qita = chuchenhuiPb.FindAll(m => m.SINCAL_COOD == chuchenhuiPb[j].SINCAL_COOD && m.SINCAL_ID == chuchenhuiPb[j].SINCAL_ID);
                if (qita.Count >= 1)
                {
                    DUST_RATIO_DRY += qita[0].SINCAL_ORE_DRY_BILL.ToDouble();
                    DUST_RATIO_WET += qita[0].SINCAL_ORE_WET_BILL.ToDouble();
                    ////modelTag.MICAL_MATCH_RE_RATIO_DRY = hunkuang[0].SINCAL_ORE_SP.ToDouble();
                    //modelTag.MICAL_MATCH_DUST_RATIO_DRY += qita[0].SINCAL_ORE_DRY_BILL.ToDouble();
                    //modelTag.MICAL_MATCH_DUST_RATIO_WET += qita[0].SINCAL_ORE_WET_BILL.ToDouble();
                }
                j += qita.Count;
            }
            modelTag.MICAL_MATCH_DUST_RATIO_DRY = DUST_RATIO_DRY;
            modelTag.MICAL_MATCH_DUST_RATIO_WET = DUST_RATIO_WET;


            double COKE_RATIO_DRY = 0;
            double COKE_RATIO_WET = 0;
            //MICAL_MATCH_COKE_RATIO_DRY	焦屑配比（干）  //求和
            var jiaoxiaoPb = mC_SINCAL_INTERFACE_TRIG_MINs.FindAll(m => (m.SINCAL_COOD <= 399 && m.SINCAL_COOD > 300));
            for (int j = 0; j < jiaoxiaoPb.Count;)
            {
                var qita = jiaoxiaoPb.FindAll(m => m.SINCAL_COOD == jiaoxiaoPb[j].SINCAL_COOD && m.SINCAL_ID == jiaoxiaoPb[j].SINCAL_ID);
                if (qita.Count >= 1)
                {
                    COKE_RATIO_DRY += qita[0].SINCAL_ORE_DRY_BILL.ToDouble();
                    COKE_RATIO_WET += qita[0].SINCAL_ORE_WET_BILL.ToDouble();
                    ////modelTag.MICAL_MATCH_RE_RATIO_DRY = hunkuang[0].SINCAL_ORE_SP.ToDouble();
                    //modelTag.MICAL_MATCH_COKE_RATIO_DRY += qita[0].SINCAL_ORE_DRY_BILL.ToDouble();
                    //modelTag.MICAL_MATCH_COKE_RATIO_WET += qita[0].SINCAL_ORE_WET_BILL.ToDouble();
                }
                j += qita.Count;
            }
            modelTag.MICAL_MATCH_COKE_RATIO_DRY = COKE_RATIO_DRY;
            modelTag.MICAL_MATCH_COKE_RATIO_WET = COKE_RATIO_WET;



            MD_PHY_PARTICLE_INFO_IN mD_PHY_PARTICLE_INFO_IN = null;
            string strSQL = string.Format("select max(timestamp) from MD_PHY_PARTICLE_INFO_IN WHERE L2_CODE>300 and L2_CODE<399 and timestamp <= to_date('{0}', 'yyyy-MM-dd HH24:mi:ss')", dTime.ToString());
            string Temp = iDataBase.GetString(strSQL);
            if (!string.IsNullOrEmpty(Temp))
            {
                DateTime strtimestamp = Convert.ToDateTime(Temp);
                mD_PHY_PARTICLE_INFO_IN = iDataBase.Queryable<MD_PHY_PARTICLE_INFO_IN>().FirstOrDefault(m => (m.TIMESTAMP == strtimestamp) && m.L2_CODE > 300 && m.L2_CODE < 399);

                if (mD_PHY_PARTICLE_INFO_IN != null && mD_PHY_PARTICLE_INFO_IN.GRIT_UP_3 != null)
                {
                    modelTag.MICAL_COKE_GRI = 100 - mD_PHY_PARTICLE_INFO_IN.GRIT_UP_3;
                }
            }


            T_MATCH_PLC_1MIN t_MATCH_PLC_1MIN = GetSum_T_MATCH_PLC_1MIN(dTime, iDataBase);

            if (t_MATCH_PLC_1MIN != null)
            {
                modelTag.MICAL_MATCH_BLO_RATIO_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_1 + t_MATCH_PLC_1MIN.M_ACTUAL_W_2 + t_MATCH_PLC_1MIN.M_ACTUAL_W_3), t_MATCH_PLC_1MIN);

                modelTag.MICAL_MATCH_DRF_RATIO_1_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_4), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_DRF_RATIO_2_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_5), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_DRF_RATIO_3_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_6), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_DRF_RATIO_4_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_7), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_RE_RATIO_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_8 + t_MATCH_PLC_1MIN.M_ACTUAL_W_9), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_LIMS_RATIO_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_10 + t_MATCH_PLC_1MIN.M_ACTUAL_W_11), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_SLIMS_RATIO_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_16), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_DUST_RATIO_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_12 + t_MATCH_PLC_1MIN.M_ACTUAL_W_13), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_QLIME_RATIO_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_17), t_MATCH_PLC_1MIN);
                modelTag.MICAL_MATCH_COKE_RATIO_WET = JsSuanPeiBi((t_MATCH_PLC_1MIN.M_ACTUAL_W_18 + t_MATCH_PLC_1MIN.M_ACTUAL_W_19), t_MATCH_PLC_1MIN);
            }

        }

        public List<MC_SINCAL_INTERFACE_TRIG_MIN> Get_MC_SINCAL_INTERFACE_TRIG_min(DateTime dTime, IDataBase iDataBase)
        {
            string strSQL = string.Format("select max(timestamp) from MC_SINCAL_INTERFACE_TRIG_MIN where timestamp <= to_date('{0}', 'yyyy-MM-dd HH24:mi:ss')", dTime.ToString());

            //return string.Format("select {0} from {1} where timestamp=({2})", sqlcol, tabName, sqltime);

            //string strSQL = "select max(timestamp)  from MC_SINCAL_INTERFACE_TRIG_MIN";
            string Temp = iDataBase.GetString(strSQL);
            if (!string.IsNullOrEmpty(Temp))
            {
                DateTime strtimestamp = Convert.ToDateTime(Temp);
                return iDataBase.Queryable<MC_SINCAL_INTERFACE_TRIG_MIN>().Where(m => (m.TIMESTAMP == strtimestamp)).OrderBy(M => M.BLANKING_ID).ToList();
            }
            else
            {
                return null;
            }
            //return iDataBase.Queryable<MC_SINCAL_INTERFACE_TRIG_MIN>().Where(m => (m.TIMESTAMP <= strtimestamp && m.TIMESTAMP >= strtimestamp.AddMinutes(0-MinuteAvg))).OrderBy(M => M.BLANKING_ID).ToList();

        }
        /// <summary>
        /// 料流追踪取配比的方法      
        /// </summary>
        /// <param name="dTime"></param>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public T_MATCH_PLC_1MIN GetSum_T_MATCH_PLC_1MIN(DateTime dTime, IDataBase iDataBase)
        {
            DateTime dateTime1 = dTime.AddMinutes(0 - MinuteAvg);
            DateTime dateTime2 = dTime;
            string strCol = "";//M_ACTUAL_W_1  round(sum(NVL(M_ACTUAL_W_1,0)),3)  NVL(M_ACTUAL_W_1,0)
            for (int num = 1; num <= 19; num++)
            {
                strCol += string.Format("round(sum(NVL(M_ACTUAL_W_{0},0)),3) as M_ACTUAL_W_{0},", num);
            }
            strCol = strCol.Trim(',');

            string strSQL = string.Format("select {0} from T_MATCH_PLC_1MIN where timestamp > to_date('{1}', 'yyyy-MM-dd HH24:mi:ss') and timestamp<=to_date('{2}', 'yyyy-MM-dd HH24:mi:ss')", strCol, dateTime1, dateTime2);

            return iDataBase.GetSingle<T_MATCH_PLC_1MIN>(strSQL);

        }
        /// <summary>
        /// 计算配比的值
        /// </summary>
        /// <param name="xll">元素的下料量</param>
        /// <param name="model"></param>
        /// <returns></returns>
        private double JsSuanPeiBi(double xll, T_MATCH_PLC_1MIN model)
        {
            double fm = model.M_ACTUAL_W_1 + model.M_ACTUAL_W_2 + model.M_ACTUAL_W_3 + model.M_ACTUAL_W_4 + model.M_ACTUAL_W_5 + model.M_ACTUAL_W_6 + model.M_ACTUAL_W_7 + model.M_ACTUAL_W_8 + model.M_ACTUAL_W_9 + model.M_ACTUAL_W_10 + model.M_ACTUAL_W_11 + model.M_ACTUAL_W_12 + model.M_ACTUAL_W_13 + model.M_ACTUAL_W_14 + model.M_ACTUAL_W_15 + model.M_ACTUAL_W_16 + model.M_ACTUAL_W_17 + model.M_ACTUAL_W_18 + model.M_ACTUAL_W_19;
            if (fm != 0)
            {
                double dou= (xll / fm) * 100;
                if (dou < 0)
                { dou = 0; }
                return dou;
            }
            else
            {
                return 0;
            }
        }
        #endregion 倒推时引用的方法
        /// <summary>
        /// 获取 烧结1#风箱到18#倒推时间  =n；单位min
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private int Get1_18_FengJi_Time(DateTime dtime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {

            bool flagHlj = false;
            double changdu2 = 0;
            int tb1_18 = 0;//烧结1#风箱到18#倒推时间  =n；单位min
            double Lenght1_18 = (double)(modelPiDaiInfo.PAR_LENGTH_B18 + modelPiDaiInfo.PAR_LENGTH_B17 + modelPiDaiInfo.PAR_LENGTH_B16 + modelPiDaiInfo.PAR_LENGTH_B15 + modelPiDaiInfo.PAR_LENGTH_B14 + modelPiDaiInfo.PAR_LENGTH_B13 + modelPiDaiInfo.PAR_LENGTH_B12 + modelPiDaiInfo.PAR_LENGTH_B11 + modelPiDaiInfo.PAR_LENGTH_B10 + modelPiDaiInfo.PAR_LENGTH_B09 + modelPiDaiInfo.PAR_LENGTH_B08 + modelPiDaiInfo.PAR_LENGTH_B07 + modelPiDaiInfo.PAR_LENGTH_B06 + modelPiDaiInfo.PAR_LENGTH_B05 + modelPiDaiInfo.PAR_LENGTH_B04 + modelPiDaiInfo.PAR_LENGTH_B03 + modelPiDaiInfo.PAR_LENGTH_B02 + modelPiDaiInfo.PAR_LENGTH_B01);
            for (; tb1_18 < 600; tb1_18++)
            {
                changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dtime.AddMinutes(0 - tb1_18), iDataBase);
                if (changdu2 >= Lenght1_18)//大于 风箱周长 时跳出
                {
                    flagHlj = true;
                    break;
                }
            }

            if (flagHlj == false)
            {
                //就不计算了  说明环冷机关闭的
            }

            modelTag.MICAL_SIN_1_18_TIME = tb1_18;
            return tb1_18;
        }
        /// <summary>
        /// 获取 烧结1#风箱到15#倒推时间  =n；单位min
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private int Get1_15_FengJi_Time(DateTime dtime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {

            bool flagHlj = false;
            double changdu2 = 0;
            int tb1_18 = 0;//烧结1#风箱到18#倒推时间  =n；单位min
            double Lenght1_18 = (double)(modelPiDaiInfo.PAR_LENGTH_B15 + modelPiDaiInfo.PAR_LENGTH_B14 + modelPiDaiInfo.PAR_LENGTH_B13 + modelPiDaiInfo.PAR_LENGTH_B12 + modelPiDaiInfo.PAR_LENGTH_B11 + modelPiDaiInfo.PAR_LENGTH_B10 + modelPiDaiInfo.PAR_LENGTH_B09 + modelPiDaiInfo.PAR_LENGTH_B08 + modelPiDaiInfo.PAR_LENGTH_B07 + modelPiDaiInfo.PAR_LENGTH_B06 + modelPiDaiInfo.PAR_LENGTH_B05 + modelPiDaiInfo.PAR_LENGTH_B04 + modelPiDaiInfo.PAR_LENGTH_B03 + modelPiDaiInfo.PAR_LENGTH_B02 + modelPiDaiInfo.PAR_LENGTH_B01);
            for (; tb1_18 < 600; tb1_18++)
            {
                changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dtime.AddMinutes(0 - tb1_18), iDataBase);
                if (changdu2 >= Lenght1_18)//大于 风箱周长 时跳出
                {
                    flagHlj = true;
                    break;
                }
            }

            if (flagHlj == false)
            {
                //就不计算了  说明环冷机关闭的
            }

            modelTag.MICAL_SIN_1_15_TIME = tb1_18;
            return tb1_18;
        }
        /// <summary>
        /// 获取 烧结1#风箱到12#倒推时间  =n；单位min
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private int Get1_12_FengJi_Time(DateTime dtime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {

            bool flagHlj = false;
            double changdu2 = 0;
            int tb1_18 = 0;//烧结1#风箱到12#倒推时间  =n；单位min
            double Lenght1_18 = (double)(modelPiDaiInfo.PAR_LENGTH_B12 + modelPiDaiInfo.PAR_LENGTH_B11 + modelPiDaiInfo.PAR_LENGTH_B10 + modelPiDaiInfo.PAR_LENGTH_B09 + modelPiDaiInfo.PAR_LENGTH_B08 + modelPiDaiInfo.PAR_LENGTH_B07 + modelPiDaiInfo.PAR_LENGTH_B06 + modelPiDaiInfo.PAR_LENGTH_B05 + modelPiDaiInfo.PAR_LENGTH_B04 + modelPiDaiInfo.PAR_LENGTH_B03 + modelPiDaiInfo.PAR_LENGTH_B02 + modelPiDaiInfo.PAR_LENGTH_B01);
            for (; tb1_18 < 600; tb1_18++)
            {
                changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dtime.AddMinutes(0 - tb1_18), iDataBase);
                if (changdu2 >= Lenght1_18)//大于 风箱周长 时跳出
                {
                    flagHlj = true;
                    break;
                }
            }

            if (flagHlj == false)
            {
                //就不计算了  说明环冷机关闭的
            }

            modelTag.MICAL_SIN_1_12_TIME = tb1_18;
            return tb1_18;
        }
        /// <summary>
        /// 获取 烧结1#风箱到9#倒推时间  =n；单位min
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private int Get1_9_FengJi_Time(DateTime dtime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {

            bool flagHlj = false;
            double changdu2 = 0;
            int tb1_18 = 0;//烧结1#风箱到9#倒推时间  =n；单位min
            double Lenght1_18 = (double)(modelPiDaiInfo.PAR_LENGTH_B09 + modelPiDaiInfo.PAR_LENGTH_B08 + modelPiDaiInfo.PAR_LENGTH_B07 + modelPiDaiInfo.PAR_LENGTH_B06 + modelPiDaiInfo.PAR_LENGTH_B05 + modelPiDaiInfo.PAR_LENGTH_B04 + modelPiDaiInfo.PAR_LENGTH_B03 + modelPiDaiInfo.PAR_LENGTH_B02 + modelPiDaiInfo.PAR_LENGTH_B01);
            for (; tb1_18 < 600; tb1_18++)
            {
                changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dtime.AddMinutes(0 - tb1_18), iDataBase);
                if (changdu2 >= Lenght1_18)//大于 风箱周长 时跳出
                {
                    flagHlj = true;
                    break;
                }
            }

            if (flagHlj == false)
            {
                //就不计算了  说明环冷机关闭的
            }

            modelTag.MICAL_SIN_1_9_TIME = tb1_18;
            return tb1_18;
        }
        /// <summary>
        /// 获取 烧结1#风箱到6#倒推时间  =n；单位min
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private int Get1_6_FengJi_Time(DateTime dtime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {

            bool flagHlj = false;
            double changdu2 = 0;
            int tb1_18 = 0;//烧结1#风箱到6#倒推时间  =n；单位min
            double Lenght1_18 = (double)(modelPiDaiInfo.PAR_LENGTH_B06 + modelPiDaiInfo.PAR_LENGTH_B05 + modelPiDaiInfo.PAR_LENGTH_B04 + modelPiDaiInfo.PAR_LENGTH_B03 + modelPiDaiInfo.PAR_LENGTH_B02 + modelPiDaiInfo.PAR_LENGTH_B01);
            for (; tb1_18 < 600; tb1_18++)
            {
                changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dtime.AddMinutes(0 - tb1_18), iDataBase);
                if (changdu2 >= Lenght1_18)//大于 风箱周长 时跳出
                {
                    flagHlj = true;
                    break;
                }
            }

            if (flagHlj == false)
            {
                //就不计算了  说明环冷机关闭的
            }

            modelTag.MICAL_SIN_1_6_TIME = tb1_18;
            return tb1_18;
        }
        /// <summary>
        /// 获取 烧结1#风箱到3#倒推时间  =n；单位min
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="dtime"></param>
        private int Get1_3_FengJi_Time(DateTime dtime, IDataBase iDataBase, MC_MICAL_RESULT modelTag)
        {

            bool flagHlj = false;
            double changdu2 = 0;
            int tb1_18 = 0;//烧结1#风箱到3#倒推时间  =n；单位min
            double Lenght1_18 = (double)(modelPiDaiInfo.PAR_LENGTH_B03 + modelPiDaiInfo.PAR_LENGTH_B02 + modelPiDaiInfo.PAR_LENGTH_B01);
            for (; tb1_18 < 600; tb1_18++)
            {
                changdu2 += MICAL5_ShaoJie_Sudu_ByDtime(dtime.AddMinutes(0 - tb1_18), iDataBase);
                if (changdu2 >= Lenght1_18)//大于 风箱周长 时跳出
                {
                    flagHlj = true;
                    break;
                }
            }

            if (flagHlj == false)
            {
                //就不计算了  说明环冷机关闭的
            }

            modelTag.MICAL_SIN_1_3_TIME = tb1_18;
            return tb1_18;
        }

    }
}
