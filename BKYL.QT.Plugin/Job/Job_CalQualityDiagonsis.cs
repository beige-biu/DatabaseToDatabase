using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Model;
using BKYL.Jobs.Base.Utils;
using BKYL.QT.Plugin.Interface;
using BKYL.QT.Plugin.Models;
using Hprose.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace BKYL.QT.Plugin.Job
{
    /// <summary>
    /// 路绳鑫：质量诊断-质量诊断基础数据计算
    /// </summary>
    public class Job_CalQualityDiagonsis : BaseJob
    {
        #region 采集数据
        List<L2_PELLET_ANA> pelletList = null;
        List<PAR_QUALITY_DIAGN> rap_QualityList = null;
        List<TC_QUALITY_DIAGN_A> tc_QualityList = null;
        List<R_ROASTER_PLC_01M> roasterModelList = null;
        R_ROASTER_PLC_01M roasterModel = null;
        TD_AUT_PELLE pelleModel = null;
        TC_QUALITY_DIAGN_B TrainingData = null;
        TC_QUALITY_DIAGN_B TestData = null;
        TC_RESULT tcResult = null;
        TC_PCA_RESULT tcPcaResult = null;
        TC_QUALITY_REAT tcReat = null;
        #endregion

        #region 属性
        double feoMax = 0;
        double csMax = 0;
        string RpcIp;
        string RpcPort;
        HproseHttpClient client;
        RPCReaderInterface myInterface;
        List<string> updateStrings;
        #endregion

        #region 入库数据
        TC_QUALITY_DIAGN_A TC_QUALITY_DIAGN_A = null;
        #endregion 

        #region 方法
        /// <summary>
        /// 返回该时间段的最新数据
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public R_ROASTER_PLC_01M GetRoasterModel(IDataBase iDataBase, DateTime startTime, DateTime endTime)
        {
            try
            {
                R_ROASTER_PLC_01M roasterModel = null;
                string selectRoasterSql = string.Format("select * from R_ROASTER_PLC_01M where timestamp = (select max(timestamp) from R_ROASTER_PLC_01M  where  timestamp>={0}  and timestamp <{1})", DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle), DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle));
                roasterModel = iDataBase.GetSingle<R_ROASTER_PLC_01M>(selectRoasterSql);
                return roasterModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 返回最新10分钟的所有数据
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public List<R_ROASTER_PLC_01M> GetRoasterModelList(IDataBase iDataBase)
        {
            try
            {
                DateTime maxTime = DateTime.MinValue;
                List<R_ROASTER_PLC_01M> roasterModelList = null;
                string selectTimeSql = string.Format("select max(timestamp) from R_ROASTER_PLC_01M");
                maxTime = iDataBase.GetDateTime(selectTimeSql);

                if (maxTime != DateTime.MinValue)
                {
                    string selectRoasterSql = string.Format("select * from R_ROASTER_PLC_01M where timestamp>={0}  and timestamp <{1}", DataBaseFactory.ConvertTimeString(maxTime.AddMinutes(-10), DataBaseType.Oracle), DataBaseFactory.ConvertTimeString(maxTime, DataBaseType.Oracle));
                    roasterModelList = iDataBase.GetList<R_ROASTER_PLC_01M>(selectRoasterSql);
                }
                return roasterModelList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TC_QUALITY_DIAGN_A GetTcQualityDiagn(IDataBase iDataBase, R_ROASTER_PLC_01M roasterModel, L2_PELLET_ANA pelletModel)
        {
            DateTime startTime;
            DateTime endTime;
            bool isBad = false;
            TC_QUALITY_DIAGN_A qualityDiagn = new TC_QUALITY_DIAGN_A();

            if (roasterModel != null)
            {
                foreach (PropertyInfo info in typeof(R_ROASTER_PLC_01M).GetProperties())
                {
                    double value = 0;
                    if (info.GetType() != typeof(DateTime))
                    {
                        value = info.GetValue(roasterModel, null).ToDouble();
                    }
                    if (value == -9999)
                    {
                        isBad = true;
                        return null;
                    }
                }
            }

            foreach (PAR_QUALITY_DIAGN qualityModel in rap_QualityList)
            {
                if (roasterModel == null)
                {
                    startTime = ((DateTime)pelletModel.M_SAMPLE_TIME).AddMinutes(-(double)qualityModel.M_START_TIME);
                    endTime = ((DateTime)pelletModel.M_SAMPLE_TIME).AddMinutes(-(double)qualityModel.M_END_TIME);
                    roasterModel = GetRoasterModel(iDataBase, startTime, endTime);
                }
                if (roasterModel != null)
                {
                    if (!isBad)
                    {
                        switch (qualityModel.M_ID)
                        {
                            case 1:
                               // qualityDiagn.TC_GRIT = pelleModel != null ? pelleModel.M_GRITCP_AVG : 0;
                                break;
                            case 2:
                                qualityDiagn.TC_FQ_W = roasterModel.A_FQ_W;
                                break;
                            case 3:
                                qualityDiagn.TC_TE_15 = roasterModel.A_TE_15;
                                break;
                            case 4:
                                qualityDiagn.TC_PH_BL_EP = roasterModel.A_PH_BL_EP;
                                break;
                            case 5:
                                qualityDiagn.TC_T_H_BCD = (roasterModel.A_T_H_B + roasterModel.A_T_H_C + roasterModel.A_T_H_D) / 3;
                                break;
                            case 6:
                               qualityDiagn.TC_T_H_BCD_PD = Math.Abs(roasterModel.A_T_H_B - (roasterModel.A_T_H_B + roasterModel.A_T_H_C + roasterModel.A_T_H_D) / 3) + Math.Abs(roasterModel.A_T_H_C - (roasterModel.A_T_H_B + roasterModel.A_T_H_C + roasterModel.A_T_H_D) / 3) + Math.Abs(roasterModel.A_T_H_D - (roasterModel.A_T_H_B + roasterModel.A_T_H_C + roasterModel.A_T_H_D) / 3);
                                break;
                            case 7:
                                qualityDiagn.TC_FT_DD_TP = roasterModel.A_FT_DD_TP;
                                break;
                            case 8:
                                qualityDiagn.TC_FT_ML_TE = roasterModel.A_FT_ML_TE;
                                break;
                            case 9:
                                qualityDiagn.TC_FT_RH_TE = roasterModel.A_FT_RH_TE;
                                break;
                            case 10:
                                qualityDiagn.TC_BURNERQ4_TE = (roasterModel.A_BURNER07_TE + roasterModel.A_BURNER08_TE) / 2;
                                break;
                            case 11:
                                qualityDiagn.TC_BURNERQ5_TE = (roasterModel.A_BURNER09_TE + roasterModel.A_BURNER10_TE) / 2;
                                break;
                            case 12:
                                qualityDiagn.TC_BURNERQ6_TE = (roasterModel.A_BURNER11_TE + roasterModel.A_BURNER12_TE + roasterModel.A_BURNER13_TE + roasterModel.A_BURNER14_TE + roasterModel.A_BURNER15_TE + roasterModel.A_BURNER16_TE) / 6;
                                break;
                            case 13:
                                qualityDiagn.TC_FT_ML_ST = roasterModel.A_FT_ML_ST;
                                break;
                            case 14:
                                qualityDiagn.TC_FT_RH_ST = roasterModel.A_FT_RH_ST;
                                break;
                            case 15:
                                qualityDiagn.TC_FT_CL_ST = roasterModel.A_FT_CL_ST;
                                break;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            return qualityDiagn;
        }
        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <param name="qualityDiagnList"></param>
        /// <returns></returns>
        public TC_QUALITY_DIAGN_B GetAverage(List<TC_QUALITY_DIAGN_A> qualityDiagnList)
        {
            TC_QUALITY_DIAGN_B qualityDiagn = null;
            if (qualityDiagnList.Count > 0)
            {
                qualityDiagn = new TC_QUALITY_DIAGN_B();
               // qualityDiagn.TC_GRIT_Y = Math.Round(tc_QualityList.Average(tc => tc.TC_GRIT_Y).ToDouble(), 2);
                qualityDiagn.TC_FQ_W = Math.Round(tc_QualityList.Average(tc => tc.TC_FQ_W).ToDouble(), 2);
                qualityDiagn.TC_TE_15= Math.Round(tc_QualityList.Average(tc => tc.TC_TE_15).ToDouble(), 2);
                qualityDiagn.TC_PH_BL_EP = Math.Round(tc_QualityList.Average(tc => tc.TC_PH_BL_EP).ToDouble(), 2);
                qualityDiagn.TC_T_H_BCD = Math.Round(tc_QualityList.Average(tc => tc.TC_T_H_BCD).ToDouble(), 2);
                qualityDiagn.TC_T_H_BCD_PD = Math.Round(tc_QualityList.Average(tc => tc.TC_T_H_BCD_PD).ToDouble(), 2);
                qualityDiagn.TC_FT_DD_TP = Math.Round(tc_QualityList.Average(tc => tc.TC_FT_DD_TP).ToDouble(), 2);
                qualityDiagn.TC_FT_ML_TE = Math.Round(tc_QualityList.Average(tc => tc.TC_FT_ML_TE).ToDouble(), 2);
                qualityDiagn.TC_FT_RH_TE = Math.Round(tc_QualityList.Average(tc => tc.TC_FT_RH_TE).ToDouble(), 2);
                qualityDiagn.TC_BURNERQ4_TE = Math.Round(tc_QualityList.Average(tc => tc.TC_BURNERQ4_TE).ToDouble(), 2);
                qualityDiagn.TC_BURNERQ5_TE = Math.Round(tc_QualityList.Average(tc => tc.TC_BURNERQ5_TE).ToDouble(), 2);
                qualityDiagn.TC_BURNERQ6_TE = Math.Round(tc_QualityList.Average(tc => tc.TC_BURNERQ6_TE).ToDouble(), 2);
                qualityDiagn.TC_FT_ML_ST = Math.Round(tc_QualityList.Average(tc => tc.TC_FT_ML_ST).ToDouble(), 2);
                qualityDiagn.TC_FT_RH_ST = Math.Round(tc_QualityList.Average(tc => tc.TC_FT_RH_ST).ToDouble(), 2);
                qualityDiagn.TC_FT_CL_ST = Math.Round(tc_QualityList.Average(tc => tc.TC_FT_CL_ST).ToDouble(), 2);
            }
            return qualityDiagn;
        }
        #endregion

        #region 重写
        public override void InitTask()
        {
            try
            {
                if (tableConfig.ColumnConfigList != null && tableConfig.ColumnConfigList.Count > 0)
                {
                    ColumnConfig column = tableConfig.ColumnConfigList[0];

                    RpcIp = column.S_DBField;
                    RpcPort = column.T_DBField;

                    client = new HproseHttpClient($"http://{RpcIp}:{RpcPort}/");
                    client.Timeout = 50000;
                    myInterface = client.UseService<RPCReaderInterface>();
                }
                else
                {
                    RunTaskException(DateTime.Now, new ContextMarshalException("配置文件缺少远程服务器信息,请检查配置信息。"));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override void RunTask(DateTime currentTime)
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    updateStrings = new List<string>();
                    //获取“最大亚铁含量”及“最大转鼓指数”
                    string selectFeoMaxSql = "select VALUE from CONFIG_PROGRAM_DETAIL where MODELID = 'ROASTINGEVALUATEMODELSET' and NAME = 'M_FEO_MAX'";
                    string selectCsMaxSql = "select VALUE from CONFIG_PROGRAM_DETAIL where MODELID = 'ROASTINGEVALUATEMODELSET' and NAME = 'M_CS_MAX'";
                    feoMax = iDataBase.GetString(selectFeoMaxSql).ToDouble();
                    csMax = iDataBase.GetString(selectCsMaxSql).ToDouble();
                    //加载配置表
                    string selectQualitySql = "select * from PAR_QUALITY_DIAGN";
                    rap_QualityList = iDataBase.GetList<PAR_QUALITY_DIAGN>(selectQualitySql);
                    //加载成品粒度表 
                    string selectPelleSql = "select * from TD_AUT_PELLE where timestamp = (select max(timestamp) from TD_AUT_PELLE) ";
                    pelleModel = iDataBase.GetSingle<TD_AUT_PELLE>(selectPelleSql);

                    //第一步
                    //最新10分钟R_ROASTER_PLC_01M数据
                    roasterModelList = GetRoasterModelList(iDataBase);
                    if (roasterModelList != null && roasterModelList.Count > 0)
                    {
                        tc_QualityList = new List<TC_QUALITY_DIAGN_A>();

                        foreach (R_ROASTER_PLC_01M roasterModel in roasterModelList)
                        {
                            tc_QualityList.Add(GetTcQualityDiagn(iDataBase, roasterModel, null));
                        }
                        TestData = GetAverage(tc_QualityList);
                    }
                    string testString = Newtonsoft.Json.JsonConvert.SerializeObject(TestData);
                    TC_QUALITY_DIAGN_B b = Newtonsoft.Json.JsonConvert.DeserializeObject<TC_QUALITY_DIAGN_B>(testString);
                    //第二步
                    string selectPelletSql = "select * from L2_PELLET_ANA where M_FLAG = 0";
                    pelletList = iDataBase.GetList<L2_PELLET_ANA>(selectPelletSql);

                    foreach (L2_PELLET_ANA pelletModel in pelletList)
                    {
                        TC_QUALITY_DIAGN_A = GetTcQualityDiagn(iDataBase, null, pelletModel);

                        if (TC_QUALITY_DIAGN_A != null)
                        {
                            TC_QUALITY_DIAGN_A.TIMESTAMP = pelletModel.TIMESTAMP;
                            if (pelletModel.M_FEO < feoMax && pelletModel.M_CS < csMax)
                            {
                                TrainingData = null;
                                TrainingData = new TC_QUALITY_DIAGN_B()
                                {
                                    //TC_GRIT_Y = TC_QUALITY_DIAGN_A.TC_GRIT,
                                    TC_FQ_W = TC_QUALITY_DIAGN_A.TC_FQ_W,
                                    TC_TE_15 = TC_QUALITY_DIAGN_A.TC_TE_15,
                                    TC_PH_BL_EP = TC_QUALITY_DIAGN_A.TC_PH_BL_EP,
                                    TC_T_H_BCD = TC_QUALITY_DIAGN_A.TC_T_H_BCD,
                                    TC_T_H_BCD_PD = TC_QUALITY_DIAGN_A.TC_T_H_BCD_PD,
                                    TC_FT_DD_TP = TC_QUALITY_DIAGN_A.TC_FT_DD_TP,
                                    TC_FT_ML_TE = TC_QUALITY_DIAGN_A.TC_FT_ML_TE,
                                    TC_FT_RH_TE = TC_QUALITY_DIAGN_A.TC_FT_RH_TE,
                                    TC_BURNERQ4_TE = TC_QUALITY_DIAGN_A.TC_BURNERQ4_TE,
                                    TC_BURNERQ5_TE = TC_QUALITY_DIAGN_A.TC_BURNERQ5_TE,
                                    TC_BURNERQ6_TE = TC_QUALITY_DIAGN_A.TC_BURNERQ6_TE,
                                    TC_FT_ML_ST = TC_QUALITY_DIAGN_A.TC_FT_ML_ST,
                                    TC_FT_RH_ST = TC_QUALITY_DIAGN_A.TC_FT_RH_ST,
                                    TC_FT_CL_ST = TC_QUALITY_DIAGN_A.TC_FT_CL_ST
                                };
                            }
                            if (TrainingData != null)
                            {
                                string strInsert = string.Format("insert into TC_QUALITY_DIAGN_B(TIMESTAMP,TC_FQ_W,TC_TE_15,TC_PH_BL_EP,TC_T_H_BCD,TC_FT_DD_TP,TC_FT_ML_TE,TC_FT_RH_TE,TC_BURNERQ4_TE,TC_BURNERQ5_TE,TC_BURNERQ6_TE,TC_FT_ML_ST,TC_FT_RH_ST,TC_FT_CL_ST,TC_T_H_BCD_PD) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14})", DataBaseFactory.ConvertTimeString(TC_QUALITY_DIAGN_A.TIMESTAMP, DataBaseType.Oracle),  TC_QUALITY_DIAGN_A.TC_FQ_W, TC_QUALITY_DIAGN_A.TC_TE_15, TC_QUALITY_DIAGN_A.TC_PH_BL_EP, TC_QUALITY_DIAGN_A.TC_T_H_BCD, TC_QUALITY_DIAGN_A.TC_FT_DD_TP, TC_QUALITY_DIAGN_A.TC_FT_ML_TE, TC_QUALITY_DIAGN_A.TC_FT_RH_TE, TC_QUALITY_DIAGN_A.TC_BURNERQ4_TE, TC_QUALITY_DIAGN_A.TC_BURNERQ5_TE, TC_QUALITY_DIAGN_A.TC_BURNERQ6_TE, TC_QUALITY_DIAGN_A.TC_FT_ML_ST, TC_QUALITY_DIAGN_A.TC_FT_RH_ST, TC_QUALITY_DIAGN_A.TC_FT_CL_ST, TC_QUALITY_DIAGN_A.TC_T_H_BCD_PD);
                                try
                                {
                                    iDataBase.BeginTran();
                                    iDataBase.ExecuteCommand(strInsert);
                                    iDataBase.Insert<TC_QUALITY_DIAGN_A>(TC_QUALITY_DIAGN_A);
                                    iDataBase.CommitTran();
                                }
                                catch (Exception)
                                {
                                    iDataBase.RollbackTran();
                                }
                            }
                            else
                            {
                                try
                                {
                                    iDataBase.Insert<TC_QUALITY_DIAGN_A>(TC_QUALITY_DIAGN_A);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                        iDataBase.ExecuteCommand(string.Format("update L2_PELLET_ANA set M_FLAG = 2 where timestamp = {0}", DataBaseFactory.ConvertTimeString(pelletModel.TIMESTAMP, DataBaseType.Oracle)));

                        Outputlog(string.Format("update L2_PELLET_ANA set M_FLAG = 2 where timestamp = {0}", DataBaseFactory.ConvertTimeString(pelletModel.TIMESTAMP, DataBaseType.Oracle)));

                        updateStrings.Add(string.Format("update L2_PELLET_ANA set M_FLAG = 1 where timestamp = {0}", DataBaseFactory.ConvertTimeString(pelletModel.TIMESTAMP, DataBaseType.Oracle)));
                    }
                    try
                    {
                        string trainingString = "NODATA";
                        if (TrainingData != null)
                        {
                            trainingString = Newtonsoft.Json.JsonConvert.SerializeObject(TrainingData);
                        }
                        Outputlog(trainingString);
                        Outputlog(testString);
                        string resultenString = myInterface.PCACAL(trainingString, testString);
                        Outputlog(resultenString);
                        tcResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TC_RESULT>(resultenString);
                        if (tcResult != null)
                        {
                            tcPcaResult = new TC_PCA_RESULT();
                            tcPcaResult.TIMESTAMP = currentTime;
                            tcPcaResult.TC_SPE_CT_VALUE = tcResult.QUCL;
                            tcPcaResult.TC_SPE = tcResult.Q;
                            tcPcaResult.TC_T2 = tcResult.T;
                            tcPcaResult.TC_T2_CT_VALUE = tcResult.T2UCL;
                            tcReat = new TC_QUALITY_REAT();
                            tcReat.TIMESTAMP = currentTime;
                            tcReat.TC_FQ_W_CR = tcResult.TC_FQ_W;
                            tcReat.TC_TE_15_CR = tcResult.TC_TE_15;
                            tcReat.TC_PH_BL_EP_CR = tcResult.TC_PH_BL_EP;
                            tcReat.TC_T_H_BCD_CR = tcResult.TC_T_H_BCD;
                            tcReat.TC_FT_DD_TP_CR = tcResult.TC_FT_DD_TP;
                            tcReat.TC_FT_ML_TE_CR = tcResult.TC_FT_ML_TE;
                            tcReat.TC_FT_RH_TE_CR = tcResult.TC_FT_RH_TE;
                            tcReat.TC_BURNERQ4_TE_CR = tcResult.TC_BURNERQ4_TE;
                            tcReat.TC_BURNERQ5_TE_CR = tcResult.TC_BURNERQ5_TE;
                            tcReat.TC_BURNERQ6_TE_CR = tcResult.TC_BURNERQ6_TE;
                            tcReat.TC_FT_ML_ST_CR = tcResult.TC_FT_ML_ST;
                            tcReat.TC_FT_RH_ST_CR = tcResult.TC_FT_RH_ST;
                            tcReat.TC_FT_CL_ST_CR = tcResult.TC_FT_CL_ST;
                        } 
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    if (tcResult != null && tcReat != null)
                    {
                        try
                        {
                            iDataBase.BeginTran();
                            iDataBase.Insert<TC_PCA_RESULT>(tcPcaResult);
                            iDataBase.Insert<TC_QUALITY_REAT>(tcReat);
                            foreach (string sql in updateStrings)
                            {
                                iDataBase.ExecuteCommand(sql);
                            }
                            iDataBase.CommitTran();
                        }
                        catch (Exception)
                        {
                            iDataBase.RollbackTran();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pelletList?.Clear();
                pelletList = null;
                rap_QualityList?.Clear();
                rap_QualityList = null;
                updateStrings?.Clear();
                updateStrings = null;
                tcPcaResult = null;
                tcReat = null;
                tcResult = null;
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        #endregion
    }
}
