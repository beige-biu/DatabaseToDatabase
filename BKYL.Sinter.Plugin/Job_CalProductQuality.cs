using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace BKYL.Sinter.Plugin
{
    class Job_CalProductQuality : BaseJob
    {
        double R_A = 0;
        int DateChange = 5;//日统计数据计算时，删除5天内内的数据再计算插入，暂定5天
        double FOreWeight = 0;//成品矿重量，计算成品率使用
        double TOreWeight = 0;//总下料重量，计算成品率使用
        double ProductMiniteTime = 0; //流程总时间:成品矿到下料口倒推的时间
        List<M_SINTER_ANA> SinterAnaList = new List<M_SINTER_ANA>();
        List<M_FPQCAL_RESULT> ReusltList = null;
        M_FPQCAL_PAR ParModel = null;
        DataTable GraModel = null;
        string log = "";
      
        public override void InitTask()
        {
            //#region LT_MATERIAL_ANALYSI中只有一个字段存源数据
            //dicOre.Add("FeO", "M_FEO_CONT");
            //dicOre.Add("CaO", "M_CAO_CONT");
            //#endregion
        }
        public override void RunTask(DateTime currentTime)
        {

            int Sign = 0;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(T_DBSource))
            {
                Sign = idatabase.GetInt("select spare10 from M_SINTER_ANA where timestamp = (select max(timestamp) from M_SINTER_ANA)");
            }
            if (Sign == 0)
            { return; }
            #region 字段名
            string[] ColumnArry = { "TIMESTAMP",
                                    "FPQ_Q_RATE_DAY",
                                    "FPQ_Q_RATE_MON",
                                    "FPQ_FQ_RATE_DAY",
                                    "FPQ_FQ_RATE_MON",
                                    "FPQ_TF_S_R_DAY",
                                    "FPQ_TF_S_R_MON",
                                    "FPQ_BA_S_R_DAY",
                                    "FPQ_BA_S_R_MON",
                                    "FPQ_FEO_S_R_DAY",
                                    "FPQ_FEO_S_R_MON",
                                    "FPQ_MG_S_R_MON",
                                    "FPQ_RDI_S_R_DAY",
                                    "FPQ_RDI_S_R_MON",
                                    "FPQ_RI_S_R_DAY",
                                    "FPQ_RI_S_R_MON",
                                    "FPQ_DRUM_S_R_DAY",
                                    "FPQ_DRUM_S_R_MON",
                                    "FPQ_FP_S_R_DAY",
                                    "FPQ_FP_S_R_MON",
                                    "FPQ_FP_ACCU_W_DAY",
                                    "FPQ_TOTAL_ACCU_W_DAY",
                                    "FPQ_FP_ACCU_W_MON",
                                    "FPQ_TOTAL_ACCU_W_MON",
                                    "FPQ_GRA_LES_5_MON",
                                    "FPQ_GRA_LES_5_DAY",
                                    "FPQ_GRA_5_10_MON",
                                    "FPQ_GRA_5_10_DAY",
                                    "FPQ_GRA_10_16_MON",
                                    "FPQ_GRA_10_16_DAY",
                                    "FPQ_GRA_16_25_MON",
                                    "FPQ_GRA_16_25_DAY",
                                    "FPQ_GRA_25_40_MON",
                                    "FPQ_GRA_25_40_DAY",
                                    "FPQ_GRA_B_40_MON",
                                    "FPQ_GRA_B_40_DAY",
                                    "FPQ_GRA_AVG_MON",
                                    "FPQ_GRA_AVG_DAY",
                                    "FPQ_EXAMI_DAY",
                                    "FPQ_EXAMI_MON",
                                    "FPQ_S_Q_RATE_DAY",
                                    "FPQ_S_Q_RATE_MON",
                                    "FPQ_S_FQ_RATE_DAY",
                                    "FPQ_S_FQ_RATE_MON",
                                    "FPQ_S_TF_S_R_DAY",
                                    "FPQ_S_TF_S_R_MON",
                                    "FPQ_S_BA_S_R_DAY",
                                    "FPQ_S_BA_S_R_MON",
                                    "FPQ_S_FEO_S_R_DAY",
                                    "FPQ_S_FEO_S_R_MON",
                                    "FPQ_S_MG_S_R_DAY",
                                    "FPQ_S_MG_S_R_MON",
                                    "FPQ_S_RDI_S_R_DAY",
                                    "FPQ_S_RDI_S_R_MON",
                                    "FPQ_S_RI_S_R_DAY",
                                    "FPQ_S_RI_S_R_MON",
                                    "FPQ_S_DRUM_S_R_DAY",
                                    "FPQ_S_DRUM_S_R_MON",
                                    "FPQ_FP_MAT_TE",
                                    "FPQ_SPARE2",
                                    "FPQ_SPARE3",
                                    "FPQ_SPARE4",
                                    "FPQ_SPARE5",
                                    "FPQ_SPARE6",
                                    "FPQ_SPARE7",
                                    "FPQ_SPARE8",
                                    "FPQ_SPARE9",
                                    "FPQ_SPARE10",
                                    "FPQ_MG_S_R_DAY"};
            string[] ValueArry = {"TIMESTAMP",
                                    "FPQ_Q_RATE_DAY",
                                    "FPQ_Q_RATE_MON",
                                    "FPQ_FQ_RATE_DAY",
                                    "FPQ_FQ_RATE_MON",
                                    "FPQ_TF_S_R_DAY",
                                    "FPQ_TF_S_R_MON",
                                    "FPQ_BA_S_R_DAY",
                                    "FPQ_BA_S_R_MON",
                                    "FPQ_FEO_S_R_DAY",
                                    "FPQ_FEO_S_R_MON",
                                    "FPQ_MG_S_R_MON",
                                    "FPQ_RDI_S_R_DAY",
                                    "FPQ_RDI_S_R_MON",
                                    "FPQ_RI_S_R_DAY",
                                    "FPQ_RI_S_R_MON",
                                    "FPQ_DRUM_S_R_DAY",
                                    "FPQ_DRUM_S_R_MON",
                                    "FPQ_FP_S_R_DAY",
                                    "FPQ_FP_S_R_MON",
                                    "FPQ_FP_ACCU_W_DAY",
                                    "FPQ_TOTAL_ACCU_W_DAY",
                                    "FPQ_FP_ACCU_W_MON",
                                    "FPQ_TOTAL_ACCU_W_MON",
                                    "FPQ_GRA_LES_5_MON",
                                    "FPQ_GRA_LES_5_DAY",
                                    "FPQ_GRA_5_10_MON",
                                    "FPQ_GRA_5_10_DAY",
                                    "FPQ_GRA_10_16_MON",
                                    "FPQ_GRA_10_16_DAY",
                                    "FPQ_GRA_16_25_MON",
                                    "FPQ_GRA_16_25_DAY",
                                    "FPQ_GRA_25_40_MON",
                                    "FPQ_GRA_25_40_DAY",
                                    "FPQ_GRA_B_40_MON",
                                    "FPQ_GRA_B_40_DAY",
                                    "FPQ_GRA_AVG_MON",
                                    "FPQ_GRA_AVG_DAY",
                                    "FPQ_EXAMI_DAY",
                                    "FPQ_EXAMI_MON",
                                    "FPQ_S_Q_RATE_DAY",
                                    "FPQ_S_Q_RATE_MON",
                                    "FPQ_S_FQ_RATE_DAY",
                                    "FPQ_S_FQ_RATE_MON",
                                    "FPQ_S_TF_S_R_DAY",
                                    "FPQ_S_TF_S_R_MON",
                                    "FPQ_S_BA_S_R_DAY",
                                    "FPQ_S_BA_S_R_MON",
                                    "FPQ_S_FEO_S_R_DAY",
                                    "FPQ_S_FEO_S_R_MON",
                                    "FPQ_S_MG_S_R_DAY",
                                    "FPQ_S_MG_S_R_MON",
                                    "FPQ_S_RDI_S_R_DAY",
                                    "FPQ_S_RDI_S_R_MON",
                                    "FPQ_S_RI_S_R_DAY",
                                    "FPQ_S_RI_S_R_MON",
                                    "FPQ_S_DRUM_S_R_DAY",
                                    "FPQ_S_DRUM_S_R_MON",
                                    "FPQ_FP_MAT_TE",
                                    "FPQ_SPARE2",
                                    "FPQ_SPARE3",
                                    "FPQ_SPARE4",
                                    "FPQ_SPARE5",
                                    "FPQ_SPARE6",
                                    "FPQ_SPARE7",
                                    "FPQ_SPARE8",
                                    "FPQ_SPARE9",
                                    "FPQ_SPARE10",
                                    "FPQ_MG_S_R_DAY"};
            #endregion        
            DateTime endTime = currentTime;
            DateTime startTime = currentTime.ToString("yyyy-MM-dd 20:00:00").ToDate();
            ReusltList = new List<M_FPQCAL_RESULT>();

            CalcularData(startTime, endTime);

            int delete = 0;
            if (ReusltList != null && ReusltList.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {
                        idatabase.BeginTran();
                        //string sql = string.Format("delete from M_FPQCAL_RESULT where timestamp >={0}", DataBaseFactory.ConvertTimeString(startTime.AddDays(-DateChange), S_DBSource.DBType));
                        delete = idatabase.ExecuteCommand(string.Format("delete from M_FPQCAL_RESULT where timestamp >={0}", DataBaseFactory.ConvertTimeString(startTime.AddDays(-DateChange).ToString("yyyy-MM-dd 00:00:00"), T_DBSource.DBType)));
                        log = string.Format("M_FPQCAL_RESULT 成功删除{0}条记录！", delete);
                        delete = idatabase.ExecuteCommand(string.Format("delete from M_FPQCAL_RESULT_HIST where timestamp >={0}", DataBaseFactory.ConvertTimeString(startTime.AddDays(-DateChange).ToString("yyyy-MM-dd 00:00:00"), T_DBSource.DBType)));
                        log = string.Format("M_FPQCAL_RESULT_HIST 成功删除{0}条记录！", delete);
                        Outputlog(log);
                        idatabase.BulkInsert<M_FPQCAL_RESULT>("M_FPQCAL_RESULT", ColumnArry, ValueArry, ReusltList);
                        idatabase.CommitTran();
                        log = string.Format("M_FPQCAL_RESULT 成功插入{0}条记录！", ReusltList.Count);
                        Outputlog(log);
                    }
                    catch (Exception)
                    {
                        idatabase.RollbackTran();
                        throw;
                    }
                    finally
                    {
                        ReusltList.Clear();
                        ReusltList = null;
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
        //更新数据库标志位
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
        private void GetSINTER_ANA(DateTime startTime, DateTime endTime)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {

                //SinterAnaList = iDataBase.GetList<M_SINTER_ANA>(string.Format("select * from M_SINTER_ANA where sampltime >= {0} and  sampltime <{1}",
                //    DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType)));

                //GraModel = iDataBase.GetDataTable(string.Format("select round(avg(GRIT_SIN_1),3) as GRIT_SIN_1," +
                //   "round(avg(GRIT_SIN_2),3) as GRIT_SIN_2," +
                //   "round(avg(GRIT_SIN_3),3) as GRIT_SIN_3," +
                //   "round(avg(GRIT_SIN_4),3) as GRIT_SIN_4," +
                //   "round(avg(GRIT_SIN_5),3) as GRIT_SIN_5," +
                //   "round(avg(GRIT_SIN_6),3) as GRIT_SIN_6 " +
                //   "from M_SINTER_ANA where sampltime >= {0} and  sampltime <{1}",
                //   DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType)));


                SinterAnaList = iDataBase.GetList<M_SINTER_ANA>(string.Format("select * from M_SINTER_ANA where REOPTTIME >= {0} and  REOPTTIME <{1}",
                    DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType)));

                GraModel = iDataBase.GetDataTable(string.Format("select round(avg(GRIT_SIN_1),3) as GRIT_SIN_1," +
                   "round(avg(GRIT_SIN_2),3) as GRIT_SIN_2," +
                   "round(avg(GRIT_SIN_3),3) as GRIT_SIN_3," +
                   "round(avg(GRIT_SIN_4),3) as GRIT_SIN_4," +
                   "round(avg(GRIT_SIN_5),3) as GRIT_SIN_5," +
                   "round(avg(GRIT_SIN_6),3) as GRIT_SIN_6 " +
                   "from M_SINTER_ANA where REOPTTIME >= {0} and  REOPTTIME <{1}",
                   DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType)));


            }
        }
        private void GetPAR()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                ParModel = iDataBase.GetSingle<M_FPQCAL_PAR>(string.Format("select * from M_FPQCAL_PAR where timestamp  = (select max(timestamp) from M_FPQCAL_PAR)"));
            }
        }
        private void GetSINCAL_INTERFACE_VA()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(T_DBSource))
            {
                //MC_SINCAL_INTERFACE_VA表 SINCAL_R_A字段改为M_FPQCAL_PAR表PAR_DS_RANGE
                //R_A = idatabase.GetDouble("select SINCAL_R_A from MC_SINCAL_INTERFACE_VA where timestamp = (select max(timestamp) from MC_SINCAL_INTERFACE_VA)");
                R_A = idatabase.GetDouble("select PAR_DS_RANGE from M_FPQCAL_PAR where timestamp = (select max(timestamp) from M_FPQCAL_PAR)");
            }
        }
        private void GetWeight(DateTime startTime, DateTime endTime)
        {

            using (IDataBase idatabase = DalFactory.GreateIDataBase(T_DBSource))
            {
                ProductMiniteTime = idatabase.GetDouble(string.Format("select round(avg(MICAL_SAM_MAT_TIME),3) from MC_MICAL_RESULT where " +
                    "timestamp >= {0} and  timestamp <{1}",
                    DataBaseFactory.ConvertTimeString(startTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, T_DBSource.DBType)));

                FOreWeight = idatabase.GetDouble(string.Format("select sum(nvl(FP_BELT_FT_AVG,0)) from T_BURN_COLE_PLC_1MIN where " +
                    "timestamp >= {0} and  timestamp <{1}",
                    DataBaseFactory.ConvertTimeString(startTime.AddMinutes(-ProductMiniteTime), T_DBSource.DBType),
                    DataBaseFactory.ConvertTimeString(endTime.AddMinutes(-ProductMiniteTime), T_DBSource.DBType)));

                TOreWeight = idatabase.GetDouble(string.Format("select sum(nvl(M_TOTAL_SP_W,0)) from T_MATCH_PLC_1MIN where " +
                    "timestamp >= {0} and  timestamp <{1}",
                    DataBaseFactory.ConvertTimeString(startTime.AddMinutes(-ProductMiniteTime), T_DBSource.DBType),
                    DataBaseFactory.ConvertTimeString(endTime.AddMinutes(-ProductMiniteTime), T_DBSource.DBType)));
            }
        }
        private void CalProductQuality(int Flag, List<M_SINTER_ANA> SinterAnaList, ref M_FPQCAL_RESULT FPQCAL_RESULT)
        {
            if (SinterAnaList == null || SinterAnaList.Count <= 0)
            {
                return;
            }
            //计算日统计数据
            if (Flag == 1)
            {
                //计算合格率
                FPQCAL_RESULT.FPQ_S_Q_RATE_DAY = SinterAnaList.Count(m =>
                m.C_R >= (R_A - ParModel.PAR_Q_B_RANGE) && m.C_R <= (R_A + ParModel.PAR_Q_B_RANGE) &&
                m.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_Q_FE_RANGE) && m.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_Q_FE_RANGE) &&
                m.EQUITY >= ParModel.PAR_Q_D_MIN &&
                m.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_Q_TFE_RANGE) && m.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_Q_TFE_RANGE));

                int hgl = 0;
                foreach (var itemhgl in SinterAnaList)
                {
                    bool flag = true;
                    if (itemhgl.C_R != 0)
                    {
                        if (itemhgl.C_R >= (R_A - ParModel.PAR_Q_B_RANGE) && itemhgl.C_R <= (R_A + ParModel.PAR_Q_B_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (itemhgl.C_FEO != 0)
                    {
                        if (itemhgl.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_Q_FE_RANGE) && itemhgl.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_Q_FE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.EQUITY != 0)
                    {
                        if (itemhgl.EQUITY >= ParModel.PAR_Q_D_MIN)
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.C_TFE != 0)
                    {
                        if (itemhgl.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_Q_TFE_RANGE) && itemhgl.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_Q_TFE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (flag)
                    { hgl++; }
                }
                FPQCAL_RESULT.FPQ_S_Q_RATE_DAY = hgl;

                FPQCAL_RESULT.FPQ_EXAMI_DAY = SinterAnaList.Count;
                
                if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                {
                    //youxiaoCount = 0;
                    FPQCAL_RESULT.FPQ_Q_RATE_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_Q_RATE_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                }


                //计算一级品率
                FPQCAL_RESULT.FPQ_S_FQ_RATE_DAY = SinterAnaList.Count(m =>
                m.C_R >= (R_A - ParModel.PAR_FQ_B_RANGE) && m.C_R <= (R_A + ParModel.PAR_FQ_B_RANGE) &&
                m.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_FQ_FE_RANGE) && m.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_FQ_FE_RANGE) &&
                m.EQUITY >= ParModel.PAR_FQ_D_MIN &&
                m.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_FQ_TFE_RANGE) && m.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_FQ_TFE_RANGE));

                hgl = 0;
                foreach (var itemhgl in SinterAnaList)
                {
                    bool flag = true;
                    if (itemhgl.C_R != 0)
                    {
                        if (itemhgl.C_R >= (R_A - ParModel.PAR_FQ_B_RANGE) && itemhgl.C_R <= (R_A + ParModel.PAR_FQ_B_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (itemhgl.C_FEO != 0)
                    {
                        if (itemhgl.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_FQ_FE_RANGE) && itemhgl.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_FQ_FE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.EQUITY != 0)
                    {
                        if (itemhgl.EQUITY >= ParModel.PAR_FQ_D_MIN)
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.C_TFE != 0)
                    {
                        if (itemhgl.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_FQ_TFE_RANGE) && itemhgl.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_FQ_TFE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (flag)
                    { hgl++; }
                }
                FPQCAL_RESULT.FPQ_S_FQ_RATE_DAY = hgl;

                if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                {
                    FPQCAL_RESULT.FPQ_FQ_RATE_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_FQ_RATE_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                }

                //计算品位稳定率
                FPQCAL_RESULT.FPQ_S_TF_S_R_DAY = SinterAnaList.Count(m => m.C_TFE >= (ParModel.PAR_TATFE_CENTER - ParModel.PAR_TATFE_RANGE) &&
                m.C_TFE <= (ParModel.PAR_TATFE_CENTER + ParModel.PAR_TATFE_RANGE));

                int youxiaoCount = 0;
                youxiaoCount = SinterAnaList.Count(m => m.C_TFE > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_TF_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_TF_S_R_DAY / youxiaoCount * 100).ToDouble(), 3);
                }
                //if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                //{
                //    FPQCAL_RESULT.FPQ_TF_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_TF_S_R_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                //}

                //计算碱度稳定率
                FPQCAL_RESULT.FPQ_S_BA_S_R_DAY = SinterAnaList.Count(m => m.C_R >= (R_A - ParModel.PAR_R_S_RANGE) && m.C_R <= (R_A + ParModel.PAR_R_S_RANGE));

                youxiaoCount = SinterAnaList.Count(m => m.C_R > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_BA_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_BA_S_R_DAY / youxiaoCount * 100).ToDouble(), 3);
                }
                //if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                //{
                //    FPQCAL_RESULT.FPQ_BA_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_BA_S_R_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                //}

                //计算FeO稳定率
                FPQCAL_RESULT.FPQ_S_FEO_S_R_DAY = SinterAnaList.Count(m => m.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_FEO_S_RANGE) &&
                m.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_FEO_S_RANGE));

                youxiaoCount = SinterAnaList.Count(m => m.C_FEO > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_FEO_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_FEO_S_R_DAY / youxiaoCount * 100).ToDouble(), 3);
                }
                //if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                //{
                //    FPQCAL_RESULT.FPQ_FEO_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_FEO_S_R_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                //}

                //计算MgO稳定率
                FPQCAL_RESULT.FPQ_S_MG_S_R_DAY = SinterAnaList.Count(m => m.C_MGO >= (ParModel.PAR_MGO_CENTER - ParModel.PAR_FEO_S_RANGE) &&
                m.C_MGO <= (ParModel.PAR_MGO_CENTER + ParModel.PAR_FEO_S_RANGE));

                youxiaoCount = SinterAnaList.Count(m => m.C_MGO > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_MG_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_MG_S_R_DAY / youxiaoCount * 100).ToDouble(), 3);
                }
                //if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                //{
                //    FPQCAL_RESULT.FPQ_MG_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_MG_S_R_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                //}

                //计算低温还原粉化合格率
                FPQCAL_RESULT.FPQ_S_RDI_S_R_DAY = SinterAnaList.Count(m => m.RDI2 >= ParModel.PAR_Q_RDI_MIN);

                youxiaoCount = SinterAnaList.Count(m => m.RDI2 > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_RDI_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_RDI_S_R_DAY / youxiaoCount * 100).ToDouble(), 3);
                }
                //if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                //{
                //    FPQCAL_RESULT.FPQ_RDI_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_RDI_S_R_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                //}

                //计算还原度合格率
                FPQCAL_RESULT.FPQ_S_RI_S_R_DAY = SinterAnaList.Count(m => m.RI >= ParModel.PAR_Q_RI_MIN);

                youxiaoCount = SinterAnaList.Count(m => m.RI > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_RI_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_RI_S_R_DAY / youxiaoCount * 100).ToDouble(), 3);
                }
                //if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                //{
                //    FPQCAL_RESULT.FPQ_RI_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_RI_S_R_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                //}

                //计算转鼓强度合格率
                FPQCAL_RESULT.FPQ_S_DRUM_S_R_DAY = SinterAnaList.Count(m => m.EQUITY >= ParModel.PAR_Q_D_MIN);

                youxiaoCount = SinterAnaList.Count(m => m.EQUITY > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_DRUM_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_DRUM_S_R_DAY / youxiaoCount * 100).ToDouble(), 3);
                }
                //if (FPQCAL_RESULT.FPQ_EXAMI_DAY > 0)
                //{
                //    FPQCAL_RESULT.FPQ_DRUM_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_S_DRUM_S_R_DAY / FPQCAL_RESULT.FPQ_EXAMI_DAY * 100).ToDouble(), 3);
                //}

                //计算成品率
                FPQCAL_RESULT.FPQ_FP_ACCU_W_DAY = FOreWeight;
                FPQCAL_RESULT.FPQ_TOTAL_ACCU_W_DAY = TOreWeight;

                if (FPQCAL_RESULT.FPQ_TOTAL_ACCU_W_DAY > 0)
                {
                    FPQCAL_RESULT.FPQ_FP_S_R_DAY = Math.Round((FPQCAL_RESULT.FPQ_FP_ACCU_W_DAY / FPQCAL_RESULT.FPQ_TOTAL_ACCU_W_DAY * 100).ToDouble(), 3);
                }

                //计算平均粒度
                if (GraModel != null)
                {
                    int i = 0;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_LES_5_DAY = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_LES_5_DAY = null;
                    }
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_5_10_DAY = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_5_10_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_10_16_DAY = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_10_16_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_16_25_DAY = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_16_25_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_25_40_DAY = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_25_40_DAY = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_B_40_DAY = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_B_40_DAY = null;
                    }
                    FPQCAL_RESULT.FPQ_GRA_AVG_DAY = Math.Round((
                        (5 * FPQCAL_RESULT.FPQ_GRA_LES_5_DAY + 
                        7.5 * FPQCAL_RESULT.FPQ_GRA_5_10_DAY + 
                        13 * FPQCAL_RESULT.FPQ_GRA_10_16_DAY + 
                        20.5 * FPQCAL_RESULT.FPQ_GRA_16_25_DAY + 
                        32.5 * FPQCAL_RESULT.FPQ_GRA_25_40_DAY + 
                        40 * FPQCAL_RESULT.FPQ_GRA_B_40_DAY) / 100).ToDouble(), 3);
                }
            }
            //计算月统计数据
            else if (Flag == 2)
            {
                //计算合格率
                FPQCAL_RESULT.FPQ_S_Q_RATE_MON = SinterAnaList.Count(m => m.C_R >= (R_A - ParModel.PAR_Q_B_RANGE) && m.C_R <= (R_A + ParModel.PAR_Q_B_RANGE) &&
                m.EQUITY >= ParModel.PAR_Q_D_MIN && m.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_Q_TFE_RANGE) && m.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_Q_TFE_RANGE));

                int hgl = 0;
                foreach (var itemhgl in SinterAnaList)
                {
                    bool flag = true;
                    if (itemhgl.C_R != 0)
                    {
                        if (itemhgl.C_R >= (R_A - ParModel.PAR_Q_B_RANGE) && itemhgl.C_R <= (R_A + ParModel.PAR_Q_B_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (itemhgl.C_FEO != 0)
                    {
                        if (itemhgl.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_Q_FE_RANGE) && itemhgl.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_Q_FE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.EQUITY != 0)
                    {
                        if (itemhgl.EQUITY >= ParModel.PAR_Q_D_MIN)
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.C_TFE != 0)
                    {
                        if (itemhgl.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_Q_TFE_RANGE) && itemhgl.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_Q_TFE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (flag)
                    { hgl++; }
                }
                FPQCAL_RESULT.FPQ_S_Q_RATE_MON = hgl;
                FPQCAL_RESULT.FPQ_EXAMI_MON = SinterAnaList.Count;


                if (FPQCAL_RESULT.FPQ_EXAMI_MON > 0)
                {
                    FPQCAL_RESULT.FPQ_Q_RATE_MON = Math.Round((FPQCAL_RESULT.FPQ_S_Q_RATE_MON / FPQCAL_RESULT.FPQ_EXAMI_MON * 100).ToDouble(), 3);
                }

                //计算一级品率
                FPQCAL_RESULT.FPQ_S_FQ_RATE_MON = SinterAnaList.Count(m => m.C_R >= (R_A - ParModel.PAR_FQ_B_RANGE) && m.C_R <= (R_A + ParModel.PAR_FQ_B_RANGE) &&
                m.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_FQ_FE_RANGE) && m.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_FQ_FE_RANGE) &&
                m.EQUITY >= ParModel.PAR_FQ_D_MIN && m.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_FQ_TFE_RANGE) && m.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_FQ_TFE_RANGE));

                hgl = 0;
                foreach (var itemhgl in SinterAnaList)
                {
                    bool flag = true;
                    if (itemhgl.C_R != 0)
                    {
                        if (itemhgl.C_R >= (R_A - ParModel.PAR_FQ_B_RANGE) && itemhgl.C_R <= (R_A + ParModel.PAR_FQ_B_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (itemhgl.C_FEO != 0)
                    {
                        if (itemhgl.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_FQ_FE_RANGE) && itemhgl.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_FQ_FE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.EQUITY != 0)
                    {
                        if (itemhgl.EQUITY >= ParModel.PAR_FQ_D_MIN)
                        {
                        }
                        else
                        { flag = false; }
                    }
                    if (itemhgl.C_TFE != 0)
                    {
                        if (itemhgl.C_TFE >= (ParModel.PAR_TFE_CENTER - ParModel.PAR_FQ_TFE_RANGE) && itemhgl.C_TFE <= (ParModel.PAR_TFE_CENTER + ParModel.PAR_FQ_TFE_RANGE))
                        {
                        }
                        else
                        { flag = false; }
                    }

                    if (flag)
                    { hgl++; }
                }
                FPQCAL_RESULT.FPQ_S_FQ_RATE_MON = hgl;

                if (FPQCAL_RESULT.FPQ_EXAMI_MON > 0)
                {
                    // FPQCAL_RESULT.FPQ_FQ_RATE_MON = Math.Round((FPQCAL_RESULT.FPQ_S_Q_RATE_DAY / FPQCAL_RESULT.FPQ_EXAMI_MON * 100,3); FPQ_S_FQ_RATE_ MON
                    FPQCAL_RESULT.FPQ_FQ_RATE_MON = Math.Round((FPQCAL_RESULT.FPQ_S_FQ_RATE_MON / FPQCAL_RESULT.FPQ_EXAMI_MON * 100).ToDouble(), 3);
                }

                //计算品位稳定率
                FPQCAL_RESULT.FPQ_S_TF_S_R_MON = SinterAnaList.Count(m => m.C_TFE >= (ParModel.PAR_TATFE_CENTER - ParModel.PAR_TATFE_RANGE) &&
                m.C_TFE <= (ParModel.PAR_TATFE_CENTER + ParModel.PAR_TATFE_RANGE));

                int youxiaoCount = SinterAnaList.Count(m => m.C_TFE > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_TF_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_S_TF_S_R_MON / youxiaoCount * 100).ToDouble(), 3);
                }

                //计算碱度稳定率
                FPQCAL_RESULT.FPQ_S_BA_S_R_MON = SinterAnaList.Count(m => m.C_R >= (R_A - ParModel.PAR_R_S_RANGE) && m.C_R <= (R_A + ParModel.PAR_R_S_RANGE));
                youxiaoCount = SinterAnaList.Count(m => m.C_R > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_BA_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_S_BA_S_R_MON / youxiaoCount * 100).ToDouble(), 3);
                }

                //计算FeO稳定率
                FPQCAL_RESULT.FPQ_S_FEO_S_R_MON = SinterAnaList.Count(m => m.C_FEO >= (ParModel.PAR_FE_CENTER - ParModel.PAR_FEO_S_RANGE) &&
                m.C_FEO <= (ParModel.PAR_FE_CENTER + ParModel.PAR_FEO_S_RANGE));

                youxiaoCount = SinterAnaList.Count(m => m.C_FEO > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_FEO_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_S_FEO_S_R_MON / youxiaoCount * 100).ToDouble(), 3);
                }

                //计算MgO稳定率
                FPQCAL_RESULT.FPQ_S_MG_S_R_MON = SinterAnaList.Count(m => m.C_MGO >= (ParModel.PAR_MGO_CENTER - ParModel.PAR_FEO_S_RANGE) &&
                m.C_MGO <= (ParModel.PAR_MGO_CENTER + ParModel.PAR_FEO_S_RANGE));

                youxiaoCount = SinterAnaList.Count(m => m.C_MGO > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_MG_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_S_MG_S_R_MON / youxiaoCount * 100).ToDouble(), 3);
                }

                //计算低温还原粉化合格率
                FPQCAL_RESULT.FPQ_S_RDI_S_R_MON = SinterAnaList.Count(m => m.RDI2 >= ParModel.PAR_Q_RDI_MIN);

                youxiaoCount = SinterAnaList.Count(m => m.RDI2 > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_RDI_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_S_RDI_S_R_MON / youxiaoCount * 100).ToDouble(), 3);
                }

                //计算还原度合格率
                FPQCAL_RESULT.FPQ_S_RI_S_R_MON = SinterAnaList.Count(m => m.RI >= ParModel.PAR_Q_RI_MIN);

                youxiaoCount = SinterAnaList.Count(m => m.RI > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_RI_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_S_RI_S_R_MON / youxiaoCount * 100).ToDouble(), 3);
                }

                //计算转鼓强度合格率
                FPQCAL_RESULT.FPQ_S_DRUM_S_R_MON = SinterAnaList.Count(m => m.EQUITY >= ParModel.PAR_Q_D_MIN);

                youxiaoCount = SinterAnaList.Count(m => m.EQUITY > 0);
                if (youxiaoCount > 0)
                {
                    FPQCAL_RESULT.FPQ_DRUM_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_S_DRUM_S_R_MON / youxiaoCount * 100).ToDouble(), 3);
                }

                //计算成品率
                FPQCAL_RESULT.FPQ_FP_ACCU_W_MON = FOreWeight;
                FPQCAL_RESULT.FPQ_TOTAL_ACCU_W_MON = TOreWeight;

                if (FPQCAL_RESULT.FPQ_TOTAL_ACCU_W_MON > 0)
                {
                    FPQCAL_RESULT.FPQ_FP_S_R_MON = Math.Round((FPQCAL_RESULT.FPQ_FP_ACCU_W_MON / FPQCAL_RESULT.FPQ_TOTAL_ACCU_W_MON * 100).ToDouble(), 3);
                }

                if (GraModel != null)
                {
                    int i = 0;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_LES_5_MON = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_LES_5_MON = null;
                    }
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_5_10_MON = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_5_10_MON = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_10_16_MON = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_10_16_MON = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_16_25_MON = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_16_25_MON = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_25_40_MON = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_25_40_MON = null;
                    }
                    i++;
                    if (!Convert.IsDBNull(GraModel.Rows[0][i]))
                    {
                        FPQCAL_RESULT.FPQ_GRA_B_40_MON = Convert.ToDouble(GraModel.Rows[0][i]);
                    }
                    else
                    {
                        FPQCAL_RESULT.FPQ_GRA_B_40_MON = null;
                    }
                    //FPQCAL_RESULT.FPQ_GRA_LES_5_MON = Convert.ToDouble(GraModel.Rows[0][i]); i++;
                    //FPQCAL_RESULT.FPQ_GRA_5_10_MON = Convert.ToDouble(GraModel.Rows[0][i]); i++;
                    //FPQCAL_RESULT.FPQ_GRA_10_16_MON = Convert.ToDouble(GraModel.Rows[0][i]); i++;
                    //FPQCAL_RESULT.FPQ_GRA_16_25_MON = Convert.ToDouble(GraModel.Rows[0][i]); i++;
                    //FPQCAL_RESULT.FPQ_GRA_25_40_MON = Convert.ToDouble(GraModel.Rows[0][i]); i++;
                    //FPQCAL_RESULT.FPQ_GRA_B_40_MON = Convert.ToDouble(GraModel.Rows[0][i]);
                    FPQCAL_RESULT.FPQ_GRA_AVG_MON = Math.Round((
                        (5 * FPQCAL_RESULT.FPQ_GRA_LES_5_MON +
                        7.5 * FPQCAL_RESULT.FPQ_GRA_5_10_MON + 
                        13 * FPQCAL_RESULT.FPQ_GRA_10_16_MON + 
                        20.5 * FPQCAL_RESULT.FPQ_GRA_16_25_MON + 
                        32.5 * FPQCAL_RESULT.FPQ_GRA_25_40_MON + 
                        40 * FPQCAL_RESULT.FPQ_GRA_B_40_MON) / 100).ToDouble(), 3);
                }
            }
        }
        private void CalcularData(DateTime startTime, DateTime endTime)
        {
            try
            {


                M_FPQCAL_RESULT FPQCAL_RESULT = null;

                UpdateM_SINTER_ANA();

                GetSINCAL_INTERFACE_VA();
                GetPAR();

                for (int i = 1; i <= DateChange; i++)
                {
                    FPQCAL_RESULT = new M_FPQCAL_RESULT();
                    //计算日统计历史数据
                    GetSINTER_ANA(startTime.AddDays(-i - 1), startTime.AddDays(-i));
                    GetWeight(startTime.AddDays(-i - 1), startTime.AddDays(-i));
                    CalProductQuality(1, SinterAnaList, ref FPQCAL_RESULT);
                    //计算月统计历史数据
                    //DateTime startTime1 = currentTime.ToString("yyyy-MM-01 00:00:00").ToDate().AddHours(-4); //原计划按上月最后一天20点至本月最后一天20点作为统计周期，后经过与秦雪刚商议，改为按当前时间取20:00，前推30天作为月统计周期
                    GetSINTER_ANA(startTime.AddDays(-i - 30), startTime.AddDays(-i));
                    GetWeight(startTime.AddDays(-i - 30), startTime.AddDays(-i));
                    CalProductQuality(2, SinterAnaList, ref FPQCAL_RESULT);

                    if (SinterAnaList != null && SinterAnaList.Count > 0)
                    {
                        FPQCAL_RESULT.FPQ_FP_MAT_TE = ProductMiniteTime;
                        FPQCAL_RESULT.TIMESTAMP = startTime.AddDays(-i).ToString("yyyy-MM-dd 00:00:00").ToDate();
                        ReusltList.Add(FPQCAL_RESULT);
                    }
                }
                FPQCAL_RESULT = new M_FPQCAL_RESULT();
                if (endTime.Hour >= 20)
                {
                    //计算日统计实时数据
                    GetSINTER_ANA(startTime, endTime);
                    GetWeight(startTime, endTime);
                    CalProductQuality(1, SinterAnaList, ref FPQCAL_RESULT);
                    //计算月统计实时数据
                    GetSINTER_ANA(startTime.AddDays(-30), endTime);
                    GetWeight(startTime.AddDays(-30), endTime);
                    CalProductQuality(2, SinterAnaList, ref FPQCAL_RESULT);

                    if (SinterAnaList != null && SinterAnaList.Count > 0)
                    {
                        FPQCAL_RESULT.FPQ_FP_MAT_TE = ProductMiniteTime;
                        FPQCAL_RESULT.TIMESTAMP = endTime.ToString("yyyy-MM-dd 00:00:00").ToDate();
                        ReusltList.Add(FPQCAL_RESULT);
                    }
                }
                else
                {
                    //计算日统计实时数据
                    GetSINTER_ANA(startTime.AddDays(-1), endTime);
                    GetWeight(startTime.AddDays(-1), endTime);
                    CalProductQuality(1, SinterAnaList, ref FPQCAL_RESULT);
                    //计算月统计实时数据
                    GetSINTER_ANA(startTime.AddDays(-30), endTime);
                    GetWeight(startTime.AddDays(-30), endTime);
                    CalProductQuality(2, SinterAnaList, ref FPQCAL_RESULT);

                    if (SinterAnaList != null && SinterAnaList.Count > 0)
                    {
                        FPQCAL_RESULT.FPQ_FP_MAT_TE = ProductMiniteTime;
                        FPQCAL_RESULT.TIMESTAMP = endTime.ToString("yyyy-MM-dd 00:00:00").ToDate();
                        ReusltList.Add(FPQCAL_RESULT);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
