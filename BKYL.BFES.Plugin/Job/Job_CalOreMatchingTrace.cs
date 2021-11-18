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

namespace BKYL.BFES.Plugin
{
    public class Job_CalOreMatchingTrace : BaseJob
    {
        int iCalDays;       //计算天数
        int iTimes_ToBin;   //取样点到仓的时间(s)
        Dictionary<string, double> DicCang;//仓位阶段
        bool isFirstRun = true;
        public override void InitTask()
        {
            iCalDays = 1;
            iTimes_ToBin = 270;//皮带长431，速度1.6
            DicCang = new Dictionary<string, double>();
            DicCang.Add("4米", 4);
            DicCang.Add("5米", 5);
            DicCang.Add("6米", 6);
            DicCang.Add("7.1米", 7.1);
            DicCang.Add("8米", 8);
            DicCang.Add("9米", 9);
            DicCang.Add("10米", 10);
            DicCang.Add("11米", 11);
            
        }

        public override void RunTask(DateTime currentTime)
        {
           // currentTime = "2019/7/31 23:00:00".ToDate();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    DateTime dtLastTime = currentTime.AddDays(-iCalDays);
                    
                    if (isFirstRun)
                    {
                        string strSql = "select max(cs_time) from OREBLENDING_TRACE";
                        dtLastTime = idatabase.GetDateTime(strSql);
                        if (dtLastTime == DateTime.MinValue)
                        {
                            //数据库中没有数据
                            dtLastTime = currentTime.AddSeconds(-tableConfig.DelectCycle);
                        }
                        isFirstRun = false;
                    }
                   
                   
                    List<BATCHHIS_CS> L_BATCHHIS_CS = GetBATCHHIS_CSList(dtLastTime, currentTime);
                    List<OREBLENDING_TRACE> L_OREBLENDING_TRACE = new List<OREBLENDING_TRACE>();
                    List<double> L_dbl1 = new List<double>();
                    List<double> L_dbl2 = new List<double>();
                    List<double> L_dbl3 = new List<double>();
                    List<double> L_dbl4 = new List<double>();
                    List<double> L_dbl5 = new List<double>();
                    List<double> L_dbl6 = new List<double>();
                    List<double> L_dbl7= new List<double>();
                    List<double> L_dbl8 = new List<double>();
                    foreach (BATCHHIS_CS BatCs in L_BATCHHIS_CS)
                    {
                        BATCHHISCODE BatCode = GetBATCHHISCODE(BatCs.TIMESTAMP);
                        double dblSumWeight = 0;
                        if (BatCode.KCODE2 == 50)
                        {
                            //烧1仓
                            dblSumWeight += CalSinter1((double)BatCs.SORE2_CS);
                            L_dbl1.Add(dblSumWeight);
                        }
                        if (BatCode.KCODE3 == 50)
                        {
                            //烧2仓
                            dblSumWeight += CalSinter2_4_5((double)BatCs.SORE3_CS);
                            L_dbl2.Add(dblSumWeight);
                        }
                        if (BatCode.KCODE4 == 50)
                        {
                            //烧3仓
                            dblSumWeight += CalSinter3((double)BatCs.SORE4_CS);
                            L_dbl3.Add(dblSumWeight);
                        }
                        if (BatCode.KCODE5 == 50)
                        {
                            //烧4仓
                            dblSumWeight += CalSinter2_4_5((double)BatCs.SORE5_CS);
                            L_dbl4.Add(dblSumWeight);
                        }
                        if (BatCode.KCODE6 == 50)
                        {
                            //烧5仓
                            dblSumWeight += CalSinter2_4_5((double)BatCs.SORE6_CS);
                            L_dbl5.Add(dblSumWeight);
                        }
                        double dblSpeedSinter = CalSpeedSinter(BatCs.TIMESTAMP);
                        L_dbl6.Add(dblSpeedSinter);
                        double dblTimesToTop = iTimes_ToBin / 3600 + (dblSpeedSinter > 0 ? dblSumWeight / dblSpeedSinter : 0);
                        L_dbl7.Add(dblSumWeight);
                        DateTime time1 = BatCs.TIMESTAMP.AddHours(dblTimesToTop);
                        TimeSpan ts = GetTimespamFromMaterialTrack(time1);
                        L_dbl8.Add(ts.TotalSeconds);
                        if (ts.TotalSeconds > 0)
                        {
                            DateTime time2 = time1 + ts;
                            OREBLENDING_TRACE ot = new OREBLENDING_TRACE();
                            ot.TIMESTAMP = BatCs.TIMESTAMP;
                            ot.CS_TIME = BatCs.TIMESTAMP; //取样时间
                            ot.TAPPING_TIME = time2; //出铁时间=炉顶时间+冶炼周期
                            ot.TOPBIN_TIME = time1; //炉顶时间 = 取样时间+从取样点到炉顶的时间
                            ot.COHESIVE_TIME = time1.AddSeconds(0.5 * ts.TotalSeconds); //软熔带时间=炉顶时间+0.5冶炼周期
                            L_OREBLENDING_TRACE.Add(ot);
                        }
                    }
                    saveOREBLENDING_TRACEData(L_OREBLENDING_TRACE);
                }
                catch (Exception ex)
                {

                    throw;
                }
               
            }
        }
        public void saveOREBLENDING_TRACEData(List<OREBLENDING_TRACE> L_OREBLENDING_TRACE)
        {
            if (L_OREBLENDING_TRACE.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        L_OREBLENDING_TRACE = L_OREBLENDING_TRACE.OrderBy(m=>m.CS_TIME).ToList();
                        DateTime minTime = L_OREBLENDING_TRACE[0].CS_TIME;
                        DateTime maxTime = L_OREBLENDING_TRACE[L_OREBLENDING_TRACE.Count-1].CS_TIME;
                        string strSql = string.Format("delete from OREBLENDING_TRACE where CS_TIME >= {0} and CS_TIME <= {1}",
                            UtilsConvert.GetOraString(minTime), UtilsConvert.GetOraString(maxTime));
                        idatabase.BeginTran();
                        idatabase.ExecuteCommand(strSql);
                        idatabase.InsertRange(L_OREBLENDING_TRACE);
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

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        public List<BATCHHIS_CS> GetBATCHHIS_CSList(DateTime startTime, DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from BATCHHIS_CS where timestamp > {0} and timestamp <= {1} and "+
                    " mod(to_number( to_char(timestamp,'mi')),10) = 0 and to_number( to_char(timestamp,'ss')) = 0  order by  timestamp",
                    UtilsConvert.GetOraString(startTime), UtilsConvert.GetOraString(endTime));
                return idatabase.GetList<BATCHHIS_CS>(strSql);
            }
        }
        public BATCHHISCODE GetBATCHHISCODE(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from BATCHHISCODE where TIMESTAMP " +
                    "= (select max(timestamp) from  BATCHHISCODE where timestamp <= {0})",
                    UtilsConvert.GetOraString(EndTime));
                return idatabase.GetSingle<BATCHHISCODE>(strSql);
            }
        }
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
            //else
            //{
            //    Volume = Cangwei / 4.0 * 96;
            //}
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
            //else
            //{
            //    Volume = Cangwei / 4.0 * 53;
            //}
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
            //else
            //{
            //    Volume = Cangwei / 4.0 * 67;
            //}
            return Volume * 1.8;
        }


        public double CalSpeedSinter(DateTime startTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select max(timestamp) as maxtime,min(timestamp) as mintime,round(nvl(sum(I_SINTER_WEIGHT),0),2) as weight from MATERIAL_BALANCE_Bat  where TIMESTAMP >= {0} and TIMESTAMP <= {1}",
                    UtilsConvert.GetOraString(startTime), UtilsConvert.GetOraString(startTime.AddHours(12)));
                DataTable dt = idatabase.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    DateTime maxTime = dt.Rows[0]["maxtime"].ToDate();
                    DateTime minTime = dt.Rows[0]["mintime"].ToDate();
                    double sumWeight = dt.Rows[0]["weight"].ToDouble();
                    if (maxTime > minTime)
                    {
                        return sumWeight / (maxTime - minTime).TotalHours;
                    }
                    else
                    {
                        return 0;
                    }
                    
                }
                else
                {
                    return 0;
                }
            }
        }
        private TimeSpan GetTimespamFromMaterialTrack(DateTime time1)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select FABRICS_TIME,TUYERE_FABRICS_TIME from " +
                "MATERIAL_TRACK where FABRICS_TIME = (select min(FABRICS_TIME) from MATERIAL_TRACK " +
                "where TUYERE_FABRICS_TIME > {0} and TUYERE_FABRICS_TIME <= {1} and type = 0 )", 
                UtilsConvert.GetOraString(time1), UtilsConvert.GetOraString(time1.AddHours(0.5)));
                DataTable dt = idatabase.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {

                    DateTime fabricTime = dt.Rows[0]["FABRICS_TIME"].ToDate();
                    DateTime tuyFabricTime = dt.Rows[0]["TUYERE_FABRICS_TIME"].ToDate();
                   return fabricTime - tuyFabricTime;
                }
                else
                {
                    return new TimeSpan(0);
                }
            }

        }

    }
}
