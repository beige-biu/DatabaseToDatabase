using BFES.DataAccess;
using BFES.Models;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BFES.Utils;
namespace BKYL.BFES.Plugin
{
    public class NGBFES1_TappingJob : BaseJob
    {
        #region 初始化数据采集表
        //出铁检化验明细表
        List<TPP_QMITEM> Tpp_QmitmList = null;
        //出铁检化验主表
        List<TPP_QMSAMP> Tpp_QmsampList = null;
        List<TPS_MEASUREMAIN> Tps_MeasuremainList = null;
        #endregion
        #region 初始化入库
        //铁水化验结果表
        List<TAPPING_IRON_LAB> Tapping_IronList_Add = null;
        List<TAPPING_IRON_LAB> Tapping_IronList_Update = null;
        List<TAPPING_IRON_LAB> Update_Tapping_IronList = null;
        List<TAPPING_IRON_LAB> Insert_Tapping_IronList = null;
        TAPPING_IRON_LAB ironModel = null;
        #endregion
        /// <summary>
        /// 初始化任务
        /// </summary>
        public override void InitTask()
        {
        }
        /// <summary>
        /// 运行此任务
        /// </summary>
        /// <param name="currentTime"></param>
        public override void RunTask(DateTime currentTime)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                //更新出铁检化验数据
                update_TappingTest_Data(iDataBase);
                //更新净重数据
                update_Tapwreal_TappingTest_Data(iDataBase);

            }
        }
        /// <summary>
        /// 添加出铁检化验数据，并将铁次号，罐号，净重，时间等插入到数据
        /// </summary>
        public void update_Tapwreal_TappingTest_Data(IDataBase iDataBase)
        {

            string queryTspmSql = string.Format("select *  from TPS_MEASUREMAIN where C_MATERIELID LIKE '%20002%' AND C_STOVELOT LIKE '1%' AND to_date(C_MEASUREBEGINDATE,'yyyy-mm-dd hh24:mi:ss') >=(select (max(SAMPTIME) - 1) from TAPPING_IRON_LAB)");
            try
            {
                Tps_MeasuremainList = iDataBase.GetList<TPS_MEASUREMAIN>(queryTspmSql);
                if (IsWhatExtensions.ISNotNULL_List(Tps_MeasuremainList) == false) return;
                Tapping_IronList_Add = new List<TAPPING_IRON_LAB>();
                Tapping_IronList_Update = new List<TAPPING_IRON_LAB>();
                foreach (TPS_MEASUREMAIN meas in Tps_MeasuremainList)
                {
                    ironModel = new TAPPING_IRON_LAB();
                    ironModel.SAMPTIME = DateTime.ParseExact(meas.C_MEASUREBEGINDATE, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                    ironModel.TAPNUMB = Convert.ToInt32(meas.C_STOVELOT);
                    ironModel.LADLE1_NR = meas.C_CONTAINERID;
                    ironModel.TAPWREAL = Convert.ToDouble(meas.N_NETWEIGHT);
                    string queryCount = string.Format("select count(*) from TAPPING_IRON_LAB where TAPNUMB='{0}' and LADLE1_NR = '{1}' ", ironModel.TAPNUMB, ironModel.LADLE1_NR);
                    int count = iDataBase.GetInt(queryCount);
                    if (count > 0)
                    {
                        Tapping_IronList_Update.Add(ironModel);
                    }
                    else
                    {
                        Tapping_IronList_Add.Add(ironModel);
                    }



                    if (ironModel != null) ironModel = null;
                }
                if (IsWhatExtensions.ISNotNULL_List(Tapping_IronList_Update))
                {
                    //修改表数据方法
                    UpdateTapwrealDate(Tapping_IronList_Update, iDataBase);
                }
                if (IsWhatExtensions.ISNotNULL_List(Tapping_IronList_Add))
                {
                    //新增表数据方法
                    InsertDate(Tapping_IronList_Add, iDataBase);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryTspmSql = null;
                if (IsWhatExtensions.ISNotNULL_List(Tps_MeasuremainList)) Tps_MeasuremainList.Clear();
                if (IsWhatExtensions.ISNotNULL_List(Tapping_IronList_Add)) Tapping_IronList_Add.Clear();
            }
        }
        public void UpdateTapwrealDate(List<TAPPING_IRON_LAB> listModel, IDataBase iDataBase)
        {
            try
            {
                iDataBase.AddDisableUpdateColumns(new string[] { "TAPWREAL", "LADLE1_NR" });
                iDataBase.BeginTran();
                foreach (TAPPING_IRON_LAB tapp_iron in listModel)
                {
                    string updateSql = string.Format("UPDATE TAPPING_IRON_LAB SET SAMPTIME=to_date('{0}','yyyy-mm-dd hh24:mi:ss'),TAPWREAL={1}  WHERE LADLE1_NR = '{2}' and TAPNUMB = {3}", tapp_iron.SAMPTIME, tapp_iron.TAPWREAL, tapp_iron.LADLE1_NR, tapp_iron.TAPNUMB);
                    iDataBase.ExecuteCommand(updateSql);
                }
                iDataBase.CommitTran();
            }
            catch (Exception)
            {
                iDataBase.RollbackTran();
                throw;
            }
            finally
            {
                iDataBase.DisableUpdateColumns = null;
            }
        }



        /// <summary>
        /// 更新出铁检化验数据
        /// </summary>
        public void update_TappingTest_Data(IDataBase iDataBase)
        {
            string queryTppqsmampSql = string.Format("select substr(C_SMP_NO,3,6) TAPNUMB, substr(C_SMP_NO,length(C_SMP_NO)-2,3) LADLE1_NR,t.* from TPP_QMSAMP t "
          + " where  (c_mtrl_no like '%020002%' and  substr(C_SMP_NO,0,3)='PI1') and "
          + "  C_SMPID >(select max(C_SMPID)  from TAPPING_IRON_LAB) ");

            try
            {
                Tpp_QmsampList = iDataBase.GetList<TPP_QMSAMP>(queryTppqsmampSql);
                if (IsWhatExtensions.ISNotNULL_List(Tpp_QmsampList) == false) return;
                Update_Tapping_IronList = new List<TAPPING_IRON_LAB>();
                Insert_Tapping_IronList = new List<TAPPING_IRON_LAB>();
                foreach (TPP_QMSAMP qmsamp in Tpp_QmsampList)
                {
                    ironModel = new TAPPING_IRON_LAB();
                    ironModel.C_SMPID = long.Parse(qmsamp.C_SMPID);
                    ironModel.TAPNUMB = Convert.ToInt32(qmsamp.TAPNUMB);
                    ironModel.LADLE1_NR = qmsamp.LADLE1_NR;
                    ironModel.UPDATETIME = qmsamp.C_TIMESTAMP;
                    string queryCount = string.Format("select count(*) from TAPPING_IRON_LAB where TAPNUMB='{0}' and LADLE1_NR = '{1}' ", ironModel.TAPNUMB, ironModel.LADLE1_NR);
                    int count = iDataBase.GetInt(queryCount);
                    //查询检化验明细表
                    string queryMXSql = string.Format("select * from TPP_QMITEM where C_SMPID = '{0}'", qmsamp.C_SMPID);
                    Tpp_QmitmList = iDataBase.GetList<TPP_QMITEM>(queryMXSql);
                    #region 迭代,获取模型对象列表
                    foreach (TPP_QMITEM qmitem in Tpp_QmitmList)
                    {
                        if ((!(string.IsNullOrWhiteSpace(qmitem.C_TEST_RST))))
                        {
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("SI"))
                            {
                                ironModel.HMSI = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("S"))
                            {
                                ironModel.HMS = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("MN"))
                            {
                                ironModel.HMMN = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("C"))
                            {
                                ironModel.HMC = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("AS"))
                            {
                                ironModel.HMAS = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("CR"))
                            {
                                ironModel.HMCR = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("TI"))
                            {
                                ironModel.HMTI = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("CU"))
                            {
                                ironModel.HMCU = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                            if (qmitem.C_ITEM_NAME.ToUpper().Equals("P"))
                            {
                                ironModel.HMP = Convert.ToDouble(qmitem.C_TEST_RST);
                            }
                        }
                    }
                    if (count > 0)
                    {
                        Update_Tapping_IronList.Add(ironModel);
                    }
                    else
                    {
                        Insert_Tapping_IronList.Add(ironModel);
                    }

                    if (ironModel != null) ironModel = null;
                }
                #endregion

                if (IsWhatExtensions.ISNotNULL_List(Update_Tapping_IronList))
                {
                    //修改表数据方法
                    UpdateDate(Update_Tapping_IronList, iDataBase);
                }
                if (IsWhatExtensions.ISNotNULL_List(Insert_Tapping_IronList))
                {
                    //新增表数据方法
                    InsertDate(Insert_Tapping_IronList, iDataBase);
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryTppqsmampSql = null;
                if (IsWhatExtensions.ISNotNULL_List(Tpp_QmsampList)) Tpp_QmsampList.Clear();
                if (IsWhatExtensions.ISNotNULL_List(Update_Tapping_IronList)) Update_Tapping_IronList.Clear();
                if (IsWhatExtensions.ISNotNULL_List(Insert_Tapping_IronList)) Insert_Tapping_IronList.Clear();
                if (IsWhatExtensions.ISNotNULL_List(Tpp_QmitmList)) Tpp_QmitmList.Clear();
            }
        }
        /// <summary>
        /// 批量更新出铁检化验成分
        /// </summary>
        /// <param name="listModel"></param>
        public void UpdateDate(List<TAPPING_IRON_LAB> listModel, IDataBase iDataBase)
        {
            try
            {
                iDataBase.AddDisableUpdateColumns(new string[] { "SAMPTIME", "TAPWREAL", "LADLE1_NR" });
                iDataBase.BeginTran();
                foreach (TAPPING_IRON_LAB tapp_iron in listModel)
                {
                    string updateSql = string.Format("UPDATE TAPPING_IRON_LAB SET HMSI={0}, HMS={1},HMP={2},HMMN={3},HMC={4}, HMAS={5},HMCR={6},HMTI ={7},HMCU ={8},C_SMPID={9} WHERE LADLE1_NR = '{10}' and TAPNUMB ={11}", tapp_iron.HMSI, tapp_iron.HMS, tapp_iron.HMP, tapp_iron.HMMN, tapp_iron.HMC, tapp_iron.HMAS, tapp_iron.HMCR, tapp_iron.HMTI, tapp_iron.HMCU, tapp_iron.C_SMPID, tapp_iron.LADLE1_NR, tapp_iron.TAPNUMB);
                    iDataBase.ExecuteCommand(updateSql);
                }
                iDataBase.CommitTran();
            }
            catch (Exception)
            {
                iDataBase.RollbackTran();
                throw;
            }
            finally
            {
                iDataBase.DisableUpdateColumns = null;
            }
        }

        /// <summary>
        /// 批量添加出铁数据
        /// </summary>
        /// <param name="listModel"></param>
        public void InsertDate(List<TAPPING_IRON_LAB> listModel, IDataBase iDataBase)
        {
            try
            {
                iDataBase.BeginTran();
                iDataBase.InsertRange<TAPPING_IRON_LAB>(listModel);
                iDataBase.CommitTran();
            }
            catch (Exception)
            {
                iDataBase.RollbackTran();
                throw;
            }
        }
        /// <summary>
        /// 任务运行异常
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="exception"></param>
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" +

tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }

    }
}
