using BFES.DataAccess;
using BFES.Models;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    class Job_SinterTrace : BaseJob
    {
     
        int iCalDays;       //计算天数
        int iTimes_ToBin;   //取样点到仓的时间(s)
        Dictionary<string, double> DicCang;//仓位阶段
        public override void InitTask()
        {
            iCalDays = 7;
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
        #region 继承
        public override void RunTask(DateTime currentTime)
        {
           // currentTime = "2019/7/31 23:00:00".ToDate();
            List<L3ANALYSI> L_L3ANALYSI = GetL3ANALYSI(currentTime.AddDays(-iCalDays), currentTime);
            L_L3ANALYSI = L_L3ANALYSI.OrderByDescending(m => m.MAT_SAMPLE_TIME).ToList();
            List<int> L_Index = new List<int>();
            DateTime DT_TEMP = DateTime.MinValue;
            for (int i = 0; i < L_L3ANALYSI.Count; i++)
            {
                if (L_L3ANALYSI[i].MAT_SAMPLE_TIME == DT_TEMP)
                {
                    L_Index.Add(i);
                }
                DT_TEMP = (DateTime)L_L3ANALYSI[i].MAT_SAMPLE_TIME;
            }

            for (int i = L_Index.Count - 1; i >= 0; i--)
            {
                L_L3ANALYSI.RemoveAt(L_Index[i]);
            }

            L_L3ANALYSI = L_L3ANALYSI.OrderBy(m => m.MAT_SAMPLE_TIME).ToList();


            List<SINTER_TRACE> L_SINTER_TRACE = new List<SINTER_TRACE>();
            foreach (L3ANALYSI L3Analysi in L_L3ANALYSI)
            {
                double dblSumWeight = 0;    //仓中烧结矿总量
                double dblSpeedSinter = 0;  //烧结矿排料速度
                double dblTimesToTop = 0;
                if (L3Analysi.MAT_SAMPLE_TIME == null)
                {
                    continue;
                }
                DateTime dt_BinTime = ((DateTime)L3Analysi.MAT_SAMPLE_TIME).AddSeconds(iTimes_ToBin);
                BATCHHISCODE BatCode = GetBATCHHISCODE(dt_BinTime);
                BATCHHIS_CS BatCs = GetBATCHHIS_CS(dt_BinTime);
                SINTER_TRACE sinterTrace = new SINTER_TRACE();
                if (BatCode.KCODE2 == 50)
                {
                    //烧1仓
                    dblSumWeight +=  CalSinter1((double)BatCs.SORE2_CS); 
                }
                if (BatCode.KCODE3 == 50)
                {
                    //烧2仓
                    dblSumWeight += CalSinter2_4_5((double)BatCs.SORE3_CS);
                }
                if (BatCode.KCODE4 == 50)
                {
                    //烧3仓
                    dblSumWeight += CalSinter3((double)BatCs.SORE4_CS);
                }
                if (BatCode.KCODE5 == 50)
                {
                    //烧4仓
                    dblSumWeight += CalSinter2_4_5((double)BatCs.SORE5_CS);
                }
                if (BatCode.KCODE6 == 50)
                {
                    //烧5仓
                    dblSumWeight += CalSinter2_4_5((double)BatCs.SORE6_CS);
                }
                dblSpeedSinter = CalSpeedSinter(dt_BinTime);
                dblTimesToTop = iTimes_ToBin/3600 + (dblSpeedSinter > 0 ? dblSumWeight / dblSpeedSinter : 0);

                sinterTrace.MATERIAL_NAME = "400烧结矿";
                sinterTrace.TIMESTAMP = (DateTime)L3Analysi.MAT_SAMPLE_TIME;
                sinterTrace.TIMES_TOTOP = dblTimesToTop;
                sinterTrace.TIME_TOBF = sinterTrace.TIMESTAMP.AddHours(sinterTrace.TIMES_TOTOP);
                sinterTrace.M_TFE_CONT = L3Analysi.M_TFE_CONT;
                sinterTrace.M_FEO_CONT = L3Analysi.M_FEO_CONT;
                sinterTrace.M_AL2O3_CONT = L3Analysi.M_AL2O3_CONT;
                sinterTrace.M_SIO2_CONT = L3Analysi.M_SIO2_CONT;
                sinterTrace.M_CAO_CONT = L3Analysi.M_CAO_CONT;
                sinterTrace.M_MGO_CONT = L3Analysi.M_MGO_CONT;
                sinterTrace.M_TI6Z3 = L3Analysi.M_TI6Z3;
                sinterTrace.M_RDI_3Z15 = L3Analysi.M_RDI_3Z15;
                sinterTrace.MR_R2 = L3Analysi.M_SIO2_CONT > 0? L3Analysi.M_CAO_CONT / L3Analysi.M_SIO2_CONT : 0;
                sinterTrace.M_SAMPLE_NO = L3Analysi.MAT_SAMPLE_NO;
                L_SINTER_TRACE.Add(sinterTrace);
            }
            SaveSINTER_TRACE(L_SINTER_TRACE, currentTime.AddDays(-iCalDays), currentTime);
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        #endregion 继承

        public List<L3ANALYSI> GetL3ANALYSI(DateTime StartTime,DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from L3ANALYSI where MAT_SAMPLE_TIME >= {0} and MAT_SAMPLE_TIME <= {1} and M_CODE = 50 and MAT_SAMPLE_NO like 'RSJ%' ",
                    UtilsConvert.GetOraString(StartTime), UtilsConvert.GetOraString(EndTime));
                return idatabase.GetList<L3ANALYSI>(strSql);
            }
        }
        public BATCHHISCODE GetBATCHHISCODE( DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from BATCHHISCODE where TIMESTAMP "+
                    "= (select max(timestamp) from  BATCHHISCODE where timestamp <= {0})",
                    UtilsConvert.GetOraString(EndTime));
                return idatabase.GetSingle<BATCHHISCODE>(strSql);
            }
        }
        public BATCHHIS_CS GetBATCHHIS_CS(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from BATCHHIS_CS where TIMESTAMP " +
                    "= (select max(timestamp) from  BATCHHIS_CS where timestamp <= {0})",
                    UtilsConvert.GetOraString(EndTime));
                return idatabase.GetSingle<BATCHHIS_CS>(strSql);
            }
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


        public double CalSpeedSinter(DateTime EndTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select round(nvl(avg(I_SINTER_WEIGHT),0),2) from MATERIAL_BALANCE_HOUR  where TIMESTAMP >= {0} and TIMESTAMP <= {1}",
                    UtilsConvert.GetOraString(EndTime.AddHours(-12)), UtilsConvert.GetOraString(EndTime));
                return idatabase.GetDouble(strSql);
            }
        }

        public void SaveSINTER_TRACE(List<SINTER_TRACE> L_SINTER_TRACE,DateTime startTime,DateTime endTime)
        {
            if (L_SINTER_TRACE.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        string deleteSQL = $"delete from SINTER_TRACE where timestamp >= {DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle)}" +
                                                 $"and timestamp <= {DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)}";
                        idatabase.BeginTran();
                        idatabase.ExecuteCommand(deleteSQL);
                        idatabase.InsertRange(L_SINTER_TRACE);
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
    }
}
