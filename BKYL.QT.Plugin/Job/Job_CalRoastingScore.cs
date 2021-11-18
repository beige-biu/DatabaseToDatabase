using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.QT.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Job
{
    /// <summary>
    /// 路绳鑫：焙烧评价-焙烧参数
    /// </summary>
    public class Job_CalRoastingScore : BaseJob
    {
        #region 采集数据
        R_ROASTER_PLC_01M poaster_01m = null;
        TD_AUT_PELLE p_aut_pelle = null;
        R_MATERIAL_PLC_01M material_01m = null;
        R_PELLET_PLC_01M pellet_01m = null;
        TC_POPCAL_RESULT_CONSUME resultConsume = null;
        List<PAR_ROASTER> roasterList = null;
        List<L2_PELLET_ANA> pelletList = null;
        List<TC_ROASTER_BAS_10M> roasterBasList = null;
        #endregion
        #region 入库数据
        TC_ROASTER_BAS_10M poaster_10m = null;
        TC_ROASTER_WAVE_10M roasterWave = null;
        #endregion
        #region 字典
        Dictionary<int, string> idNameDic = null;
        Dictionary<int, string> idFieldDic = null;
        #endregion
        public override void InitTask()
        {

        }
        public override void RunTask(DateTime currentTime)
        {
            try
            {
                idNameDic = new Dictionary<int, string>()
        { {1,"GRIT" }, { 2,"FQ"}, { 3, "15" }, { 4, "PH_TP" }, { 5, "BT_TP" }, { 6, "UC" }, { 7, "BL" }, { 8, "EP" }, { 9, "BCD" }, { 10, "PD" }, { 11, "TP" }, { 12, "R" }, { 13, "DD" }, { 14, "ML" }, { 15, "RH" }, { 16, "PI" }, { 17, "Q3" }, { 18, "Q4" }, { 19, "Q5" }, { 20, "Q6" }, { 21, "RQ1" }, { 22, "RQ4" }, { 23, "RQ5" }, { 24, "MLST" }, { 25, "RHST" }, { 26, "CLST" }, { 27, "BTE" }, { 28, "WR" }, { 29, "WATER" }, { 30, "AMIX" } , { 31, "08" }, { 32, "16" }, { 33, "21" }};
                idFieldDic = new Dictionary<int, string>()
        {  {1,"TC_GRIT" }, { 2,"TC_FQ_W"}, { 3, "TC_TE_15" }, { 4, "TC_PH_TP" }, { 5, "TC_FT_BT_TP" }, { 6, "TC_UC_BL_W" }, { 7, "TC_PH_BL_W" }, { 8, "TC_PH_BL_EP" }, { 9, "TC_T_H_BCD" }, { 10, "TC_T_H_BCD_PD" }, { 11, "TC_FT_DD_TP" }, { 12, "TC_PH_R_TE" }, { 13, "TC_FT_DD_TE" }, { 14, "TC_FT_ML_TE" }, { 15, "TC_FT_RH_TE" }, { 16, "TC_PH_PI_TP" }, { 17, "TC_BURNERQ3_TE" }, { 18, "TC_BURNERQ4_TE" }, { 19, "TC_BURNERQ5_TE" }, { 20, "TC_BURNERQ6_TE" }, { 21, "TC_ROASTERQ1_TE" }, { 22, "TC_ROASTERQ4_TE" }, { 23, "TC_ROASTERQ5_TE" }, { 24, "TC_FT_ML_ST" }, { 25, "TC_FT_RH_ST" }, { 26, "TC_FT_CL_ST" }, { 27, "TC_BTE" }, { 28, "TC_WR" }, { 29, "TC_DRY_A_WATER" }, { 30, "TC_A_MIX" } , { 31, "TC_BELLOWS08_TP" }, { 32, "TC_BELLOWS16_TP" }, { 33, "TC_BELLOWS21_TP" }};

                poaster_10m = new TC_ROASTER_BAS_10M();
                roasterWave = new TC_ROASTER_WAVE_10M();
                poaster_10m.TIMESTAMP = currentTime;
                roasterWave.TIMESTAMP = currentTime;
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    string select_aut_pelle_sql=string.Format("select * from {0} where  timestamp = (select max(timestamp) from {0})", "TD_AUT_PELLE");
                    p_aut_pelle= iDataBase.GetSingle<TD_AUT_PELLE>(select_aut_pelle_sql);

                    string selectPoasterSql = string.Format("select * from {0} where timestamp = (select max(timestamp) from {0})", "R_ROASTER_PLC_01M");
                    poaster_01m = iDataBase.GetSingle<R_ROASTER_PLC_01M>(selectPoasterSql);

                    string selectConsume = string.Format("select * from {0} where timestamp = (select max(timestamp) from {0} where DT_CODE = 101) and DT_CODE = 101", "TC_POPCAL_RESULT_CONSUME");
                    resultConsume = iDataBase.GetSingle<TC_POPCAL_RESULT_CONSUME>(selectConsume);

                    string selectMaterialSql = string.Format("select * from {0} where timestamp = (select max(timestamp) from {0})", typeof(R_MATERIAL_PLC_01M).Name);
                    material_01m = iDataBase.GetSingle<R_MATERIAL_PLC_01M>(selectMaterialSql);

                    string selectPelletSql = string.Format("select * from {0} where timestamp = (select max(timestamp) from {0})", typeof(R_PELLET_PLC_01M).Name);
                    pellet_01m = iDataBase.GetSingle<R_PELLET_PLC_01M>(selectPelletSql);

                    string selectRoasterSql = string.Format("select * from {0}", typeof(PAR_ROASTER).Name);
                    roasterList = iDataBase.GetList<PAR_ROASTER>(selectRoasterSql);

                    foreach (PAR_ROASTER roaster in roasterList)
                    {
                        switch (roaster.M_ID)
                        {
                            case 1:
                                poaster_10m.TC_GRIT = p_aut_pelle==null ? 0 : p_aut_pelle.M_GRITCP_AVG;
                                break;
                            case 2:
                                poaster_10m.TC_FQ_W = poaster_01m == null ? 0 : poaster_01m.A_FQ_W;
                                break;
                            case 3:
                                poaster_10m.TC_TE_15 = poaster_01m == null ? 0 : poaster_01m.A_TE_15;
                                break;
                            case 4:
                                poaster_10m.TC_PH_TP = poaster_01m == null ? 0 : poaster_01m.A_PH_TP;
                                break;
                            case 5:
                                poaster_10m.TC_FT_BT_TP = poaster_01m == null ? 0 : poaster_01m.A_FT_BT_TE;
                                break;
                            case 6:
                                poaster_10m.TC_UC_BL_W = poaster_01m == null ? 0 : poaster_01m.A_UC_BL_W;
                                break;
                            case 7:
                                poaster_10m.TC_PH_BL_W = poaster_01m == null ? 0 : poaster_01m.A_PH_BL_W;
                                break;
                            case 8:
                                poaster_10m.TC_PH_BL_EP = poaster_01m == null ? 0 : poaster_01m.A_PH_BL_EP;
                                break;
                            case 9:
                                poaster_10m.TC_T_H_BCD = poaster_01m == null ? 0 : (poaster_01m.A_T_H_B + poaster_01m.A_T_H_C + poaster_01m.A_T_H_D) / 3;
                                break;
                            case 10:
                                poaster_10m.TC_T_H_BCD_PD = poaster_01m == null ? 0 : Math.Abs(poaster_01m.A_T_H_B - (poaster_01m.A_T_H_B + poaster_01m.A_T_H_C + poaster_01m.A_T_H_D) / 3) + Math.Abs(poaster_01m.A_T_H_C - (poaster_01m.A_T_H_B + poaster_01m.A_T_H_C + poaster_01m.A_T_H_D) / 3) + Math.Abs(poaster_01m.A_T_H_D - (poaster_01m.A_T_H_B + poaster_01m.A_T_H_C + poaster_01m.A_T_H_D) / 3);
                                break;
                            case 11:
                                poaster_10m.TC_FT_DD_TP = poaster_01m == null ? 0 : poaster_01m.A_FT_DD_TP;
                                break;
                            case 12:
                                poaster_10m.TC_PH_R_TE = poaster_01m == null ? 0 : poaster_01m.A_PH_R_TE;
                                break;
                            case 13:
                                poaster_10m.TC_FT_DD_TE = poaster_01m == null ? 0 : poaster_01m.A_FT_DD_TE;
                                break;
                            case 14:
                                poaster_10m.TC_FT_ML_TE = poaster_01m == null ? 0 : poaster_01m.A_FT_ML_TE;
                                break;
                            case 15:
                                poaster_10m.TC_FT_RH_TE = poaster_01m == null ? 0 : poaster_01m.A_FT_RH_TE;
                                break;
                            case 16:
                                poaster_10m.TC_PH_PI_TP = poaster_01m == null ? 0 : poaster_01m.A_PH_PI_TP;
                                break;
                            case 17:
                                poaster_10m.TC_BURNERQ3_TE = poaster_01m == null ? 0 : (poaster_01m.A_BURNER05_TE + poaster_01m.A_BURNER06_TE) / 2;
                                break;
                            case 18:
                                poaster_10m.TC_BURNERQ4_TE = poaster_01m == null ? 0 : (poaster_01m.A_BURNER07_TE + poaster_01m.A_BURNER08_TE) / 2;
                                break;
                            case 19:
                                poaster_10m.TC_BURNERQ5_TE = poaster_01m == null ? 0 : (poaster_01m.A_BURNER09_TE + poaster_01m.A_BURNER10_TE) / 2;
                                break;
                            case 20:
                                poaster_10m.TC_BURNERQ6_TE = poaster_01m == null ? 0 : (poaster_01m.A_BURNER11_TE + poaster_01m.A_BURNER12_TE + poaster_01m.A_BURNER13_TE + poaster_01m.A_BURNER14_TE + poaster_01m.A_BURNER15_TE + poaster_01m.A_BURNER16_TE) / 6;
                                break;
                            case 21:
                                poaster_10m.TC_ROASTERQ1_TE = poaster_01m == null ? 0 : (poaster_01m.A_ROASTER101_TE + poaster_01m.A_ROASTER102_TE) / 2;
                                break;
                            case 22:
                                poaster_10m.TC_ROASTERQ4_TE = poaster_01m == null ? 0 : (poaster_01m.A_ROASTER401_TE + poaster_01m.A_ROASTER402_TE) / 2;
                                break;
                            case 23:
                                poaster_10m.TC_ROASTERQ5_TE = poaster_01m == null ? 0 : (poaster_01m.A_ROASTER401_TE + poaster_01m.A_ROASTER402_TE) / 2;
                                break;
                            case 24:
                                poaster_10m.TC_FT_ML_ST = poaster_01m == null ? 0 : poaster_01m.A_FT_RH_ST;
                                break;
                            case 25:
                                poaster_10m.TC_FT_RH_ST = poaster_01m == null ? 0 : poaster_01m.A_FT_RH_ST;
                                break;
                            case 26:
                                poaster_10m.TC_FT_CL_ST = poaster_01m == null ? 0 : poaster_01m.A_FT_CL_ST;
                                break;
                            case 27:
                                poaster_10m.TC_BTE = resultConsume == null ? 0 : resultConsume.DT_RATIO;
                                break;
                            case 28:
                                poaster_10m.TC_WR = poaster_01m == null ? 0 : poaster_01m.A_WR;
                                break;
                            case 29:
                                poaster_10m.TC_DRY_A_WATER = material_01m == null ? 0 : material_01m.A_DRY_A_WATER;
                                break;
                            case 30:
                                poaster_10m.TC_A_MIX = pellet_01m == null ? 0 : pellet_01m.A_A_MIX;
                                break;
                            case 31:
                                poaster_10m.TC_BELLOWS08_TP = poaster_01m == null ? 0 : poaster_01m.A_BELLOWS08_TP;
                                break;
                            case 32:
                                poaster_10m.TC_BELLOWS16_TP = poaster_01m == null ? 0 : poaster_01m.A_BELLOWS16_TP;
                                break;
                            case 33:
                                poaster_10m.TC_BELLOWS21_TP = poaster_01m == null ? 0 : poaster_01m.A_BELLOWS21_TP;
                                break;
                        }

                        double poasterValue = CustomModelInfo.GetModelItemValue<TC_ROASTER_BAS_10M>(poaster_10m, idFieldDic[roaster.M_ID]).ToDouble();

                        if (roaster.M_IL_LOWER <= poasterValue && poasterValue <= roaster.M_IL_UP)
                        {
                            CustomModelInfo.SetModelItemValue<TC_ROASTER_BAS_10M>(poaster_10m, string.Format("TC_FEO_{0}", idNameDic[roaster.M_ID]), roaster.M_FEO_BRA);
                            CustomModelInfo.SetModelItemValue<TC_ROASTER_BAS_10M>(poaster_10m, string.Format("TC_CS_{0}", idNameDic[roaster.M_ID]), roaster.M_CS_BRA);
                            CustomModelInfo.SetModelItemValue<TC_ROASTER_BAS_10M>(poaster_10m, string.Format("TC_DRIX_{0}", idNameDic[roaster.M_ID]), roaster.M_DRIX_BRA);
                            CustomModelInfo.SetModelItemValue<TC_ROASTER_BAS_10M>(poaster_10m, string.Format("TC_B_S_{0}", idNameDic[roaster.M_ID]), roaster.M_B_S_BRA);
                            CustomModelInfo.SetModelItemValue<TC_ROASTER_BAS_10M>(poaster_10m, string.Format("TC_OTHER_{0}", idNameDic[roaster.M_ID]), roaster.M_OTHER_BRA);
                        }
                        else
                        {
                            CustomModelInfo.SetModelItemValue<TC_ROASTER_WAVE_10M>(roasterWave, idFieldDic[roaster.M_ID], poasterValue);
                        }
                    }
                    try
                    {
                        iDataBase.BeginTran();
                        iDataBase.Insert<TC_ROASTER_BAS_10M>(poaster_10m);
                        iDataBase.Insert<TC_ROASTER_WAVE_10M>(roasterWave);
                        iDataBase.CommitTran();
                    }
                    catch (Exception)
                    {
                        iDataBase.RollbackTran();
                    }
                    finally
                    {
                        roasterList?.Clear();
                        roasterList = null;
                        idNameDic?.Clear();
                        idNameDic = null;
                        idFieldDic?.Clear();
                        idFieldDic = null;
                    }

                    //焙烧参数与质量关联
                    try
                    {
                        string updatePelletSql = "";
                        string selectSql1 = string.Format("select * from {0} where M_CAL_FLAG=0", typeof(L2_PELLET_ANA).Name);
                        pelletList = iDataBase.GetList<L2_PELLET_ANA>(selectSql1);
                        if (pelletList != null)
                        {
                            iDataBase.BeginTran();
                            foreach (L2_PELLET_ANA pelletModel in pelletList)
                            {
                                DateTime startTime = pelletModel.M_SAMPLE_TIME.Value.AddMinutes(-60);
                                DateTime endTime = pelletModel.M_SAMPLE_TIME.Value;

                                string selectSql2 = string.Format("select * from  {0} where  timestamp>{1}  and timestamp <={2} order by timestamp ", "TC_ROASTER_BAS_10M", DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle), DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle));
                                roasterBasList = iDataBase.GetList<TC_ROASTER_BAS_10M>(selectSql2);
                                if (roasterBasList != null)
                                {
                                    string updateRoasterSql = "";
                                    foreach (TC_ROASTER_BAS_10M roasterModel in roasterBasList)
                                    {
                                        roasterModel.TC_TFE = pelletModel.M_TFE;
                                        roasterModel.TC_FEO = pelletModel.M_FEO;
                                        roasterModel.TC_CS = pelletModel.M_CS;
                                        roasterModel.TC_DRIX = pelletModel.M_DRIX;
                                        updateRoasterSql = string.Format("update TC_ROASTER_BAS_10M set TC_TFE={0},TC_FEO={1},TC_CS = {2},TC_DRIX={3} where TIMESTAMP = {4}", pelletModel.M_TFE, pelletModel.M_FEO, pelletModel.M_CS, pelletModel.M_DRIX, DataBaseFactory.ConvertTimeString(roasterModel.TIMESTAMP, DataBaseType.Oracle));
                                        iDataBase.ExecuteCommand(updateRoasterSql);
                                    }
                                    updatePelletSql = string.Format("update L2_PELLET_ANA set M_CAL_FLAG = 1 where TIMESTAMP = {0}", DataBaseFactory.ConvertTimeString(pelletModel.TIMESTAMP, DataBaseType.Oracle));
                                    iDataBase.ExecuteCommand(updatePelletSql);
                                }
                            }
                            iDataBase.CommitTran();
                        }
                    }
                    catch (Exception)
                    {
                        iDataBase.RollbackTran();
                    }
                    finally
                    {
                        roasterBasList?.Clear();
                        roasterBasList = null;
                        pelletList?.Clear();
                        pelletList = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取时间Error:" + ex.Message + ex.StackTrace);
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
