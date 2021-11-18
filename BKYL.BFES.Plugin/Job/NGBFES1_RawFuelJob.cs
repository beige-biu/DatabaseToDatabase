using BFES.DataAccess;
using BFES.Models;
using BFES.Utils;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    public class NGBFES1_RawFuelJob : BaseJob
    {
        #region 初始化数据采集表
        //原燃料检化验主表
        List<TX_IO_LTXC_SINTER_MAIN> Tx_SinterMainList = null;
        //原燃料检化验明细表
        List<TX_IO_LTXC_SINTER_SUB> Tx_SinterSubList = null;
        #endregion
        #region 初始化入库表
        List<Models.NG.L3ANALYSI> L3analysiList = null;
        List<Models.NG.L3ANALYSI> L3analysiUpdateList = null;
        Models.NG.L3ANALYSI L3analysi = null;
        #endregion
        public override void InitTask()
        {

        }

        /// <summary>
        /// 更新原燃料检化验信息
        /// </summary>
        /// <param name="iDataBase"></param>
        public void update_L3analysiTest_Data(IDataBase iDataBase, List<Models.NG.L3ANALYSI> L3analysiModelList)
        {
            try
            {
                iDataBase.AddDisableUpdateColumns(new string[] { "SMP_NO", "SMP_TYPE" });
                iDataBase.BeginTran();
                iDataBase.UpdateRange<Models.NG.L3ANALYSI>(L3analysiModelList);
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
        /// 新增原燃料检化验信息
        /// </summary>
        /// <param name="iDataBase"></param>
        public void insert_L3analysiTest_Data(IDataBase iDataBase, List<Models.NG.L3ANALYSI> L3analysiModelList)
        {
            try
            {
                iDataBase.BeginTran();
                iDataBase.InsertRange<Models.NG.L3ANALYSI>(L3analysiModelList);
                iDataBase.CommitTran();
            }
            catch (Exception)
            {
                iDataBase.RollbackTran();
                throw;
            }
        }
        /// <summary>
        /// 检索采集原燃料检化验信息
        /// </summary>
        /// <param name="iDataBase"></param>
        public void retrieve_L3analysiTest_Data(IDataBase iDataBase)
        {
            //根据“L3ANALYSI”的最大序列号，通过比对查找“TX_IO_LTXC_SINTER_MAIN”表的最新信息
            string queryTxmainSql = @"select * from TX_IO_LTXC_SINTER_MAIN a where SERIAL_NO in( select max(SERIAL_NO) from TX_IO_LTXC_SINTER_MAIN GROUP BY SMP_TYPE,SMP_NO) 
  and  exists (select codel3 from L3MATERIAL l where l.codel3=a.MTRL_NO)  and SERIAL_NO > (select max(SERIAL_NO) from L3ANALYSI)";
            Tx_SinterMainList = iDataBase.GetList<TX_IO_LTXC_SINTER_MAIN>(queryTxmainSql);

            L3analysiList = new List<Models.NG.L3ANALYSI>();
            L3analysiUpdateList = new List<Models.NG.L3ANALYSI>();
            foreach (TX_IO_LTXC_SINTER_MAIN tx_SinterMain in Tx_SinterMainList)
            {
                L3analysi = new Models.NG.L3ANALYSI();
                L3analysi.SMP_NO = tx_SinterMain.SMP_NO;
                L3analysi.SMP_TYPE = tx_SinterMain.SMP_TYPE;
                L3analysi.SERIAL_NO = tx_SinterMain.SERIAL_NO;
                //获取取样时间
                L3analysi.TIMESTAMP = DateTime.ParseExact((tx_SinterMain.GET_SMP_DAY + tx_SinterMain.GET_SMP_TIME), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                //根据“TX_IO_LTXC_SINTER_MAIN”表的主键，查找“TX_IO_LTXC_SINTER_SUB”的信息并检索采集
                string queryTxsubSql = string.Format("select * from TX_IO_LTXC_SINTER_SUB where SMP_NO ='{0}' and SMP_TYPE ='{1}' and SERIAL_NO = '{2}'", tx_SinterMain.SMP_NO, tx_SinterMain.SMP_TYPE, tx_SinterMain.SERIAL_NO);
                Tx_SinterSubList = iDataBase.GetList<TX_IO_LTXC_SINTER_SUB>(queryTxsubSql);

                //根据“TX_IO_LTXC_SINTER_MAIN”表的<物料代码>,查找“L3MATERIAL”表的‘一级’‘三级’编码并采集
                L3analysi.M_CODE3 = tx_SinterMain.MTRL_NO;
                string queryL3materialSql = string.Format("select CODE  from L3MATERIAL where CODEL3 = '{0}'", tx_SinterMain.MTRL_NO);
                L3analysi.M_CODE = iDataBase.GetInt(queryL3materialSql);
                #region 检索采集"TX_IO_LTXC_SINTER_SUB"表中数据
                foreach (TX_IO_LTXC_SINTER_SUB tx_SinterSub in Tx_SinterSubList)
                {
                    if (!(string.IsNullOrWhiteSpace(tx_SinterSub.TEST_RST)))
                    {
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "C")
                        {
                            L3analysi.M_C_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "FE" || tx_SinterSub.SMP_ITEM.ToUpper() == "TFE")
                        {
                            L3analysi.M_TFE_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "FE2O3")
                        {
                            L3analysi.M_FE2O3_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "FEO")
                        {
                            L3analysi.M_FEO_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "SIO2")
                        {
                            L3analysi.M_SIO2_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "AL2O3" || tx_SinterSub.SMP_ITEM.ToUpper() == "AlO3")
                        {
                            L3analysi.M_AL2O3_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "CAO")
                        {
                            L3analysi.M_CAO_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "MGO")
                        {
                            L3analysi.M_MGO_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "NA2O")
                        {
                            L3analysi.M_NA2O_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "K2O")
                        {
                            L3analysi.M_K2O_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "MN")
                        {
                            L3analysi.M_MN_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "S")
                        {
                            L3analysi.M_S_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "P")
                        {
                            L3analysi.M_P_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "TIO2")
                        {
                            L3analysi.M_TIO2_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ') == "水份" || tx_SinterSub.SMP_ITEM.Trim(' ') == "水分")
                        {
                            L3analysi.M_MOISTURE = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "ZN")
                        {
                            L3analysi.M_ZN_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ') == "灰份")
                        {
                            L3analysi.M_COKEASH = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "CAF2")
                        {
                            L3analysi.M_CAF2_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ') == "挥发份")
                        {
                            L3analysi.M_VOLA_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "H")
                        {
                            L3analysi.M_H_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "MNO")
                        {
                            L3analysi.M_MNO_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "MNO2")
                        {
                            L3analysi.M_MNO2_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "FES")
                        {
                            L3analysi.M_FES_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "FES2")
                        {
                            L3analysi.M_FES2_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "V2O5")
                        {
                            L3analysi.M_V2O5_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "CUO")
                        {
                            L3analysi.M_CUO_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "CU2O")
                        {
                            L3analysi.M_CU2O_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "CO2")
                        {
                            L3analysi.M_CO2_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "SO2")
                        {
                            L3analysi.M_SO2_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "P2O5")
                        {
                            L3analysi.M_P2O5_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "PB")
                        {
                            L3analysi.M_PB_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "R2")
                        {
                            L3analysi.M_R2 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "R3")
                        {
                            L3analysi.M_R3 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "球径MM")
                        {
                            L3analysi.M_DIAMETER_PELLET = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "抗压N/球" || tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "抗压KN/球" || tx_SinterSub.SMP_ITEM.Trim(' ') == "抗压强度")
                        {
                            L3analysi.M_KANGYA_PELLET = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "-6.3MM" || tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度-6.3MM")
                        {
                            L3analysi.M_LOW_6POINT3 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "-10MM" || tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度-10MM")
                        {
                            L3analysi.M_SIZEUNDER10 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "+6.3MM")
                        {
                            L3analysi.M_UP_6POINT3 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度+10MM")
                        {
                            L3analysi.M_SIZEUP10 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ') == "烧损")
                        {
                            L3analysi.M_SHAOSUN = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ') == "杂石")
                        {
                            L3analysi.M_ZASHI = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ') == "膨胀率")
                        {
                            L3analysi.M_PENGZHANGRATIO = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "RDL+3.15")
                        {
                            L3analysi.M_RDL3POINT15 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "RDI+3.15")
                        {
                            L3analysi.M_RDI3POINT15 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "AS")
                        {
                            L3analysi.M_AS_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "CR")
                        {
                            L3analysi.M_CR_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "CU")
                        {
                            L3analysi.M_CU_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "SN")
                        {
                            L3analysi.M_SN_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "H2O")
                        {
                            L3analysi.M_H2O_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "RI")
                        {
                            L3analysi.M_RI = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "SiO2+Al2O3")
                        {
                            L3analysi.M_SIAL_CONT = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "-5MM")
                        {
                            L3analysi.M_SIZEUNDER5 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "-8MM")
                        {
                            L3analysi.M_SIZEUNDER8 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "8MM")
                        {
                            L3analysi.M_SIZEUP8 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.ToUpper() == "△PMAX")
                        {
                            L3analysi.M_PMAX = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "-20MM")
                        {
                            L3analysi.M_SIZEUNDER20 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "-30MM")
                        {
                            L3analysi.M_SIZEUNDER30 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "+40MM")
                        {
                            L3analysi.M_SIZEUP40 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度10-50MM")
                        {
                            L3analysi.M_SIZEUNDER10_50 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "20-80MM")
                        {
                            L3analysi.M_SIZEUNDER20_80 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度<6MM" || tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度-6MM")
                        {
                            L3analysi.M_SIZEUNDER6 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ') == "粒度6-50mm")
                        {
                            L3analysi.M_SIZEUNDER6_50 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "20~40MM")
                        {
                            L3analysi.M_SIZEUNDER20_40 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if ( tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "20~60MM")
                        {
                            L3analysi.M_SIZEUNDER20_60 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if ( tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "30~70MM")
                        {
                            L3analysi.M_SIZEUNDER30_70 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度50-60MM" || tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度(50~60)MM")
                        {
                            L3analysi.M_SIZEUNDER50_60 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度(60~90)MM")
                        {
                            L3analysi.M_SIZEUNDER60_90 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                        if (tx_SinterSub.SMP_ITEM.Trim(' ').ToUpper() == "粒度(90~100)MM")
                        {
                            L3analysi.M_SIZEUNDER90_100 = Convert.ToDouble(tx_SinterSub.TEST_RST);
                        }
                    }
                }
                #endregion
                string queryContainsSql = string.Format("select count(*) from L3ANALYSI where SMP_NO = '{0}' and SMP_TYPE = '{1}' ", tx_SinterMain.SMP_NO, tx_SinterMain.SMP_TYPE);
                if (iDataBase.GetInt(queryContainsSql) > 0)
                {
                    L3analysiUpdateList.Add(L3analysi);
                }
                else
                {
                    L3analysiList.Add(L3analysi);
                }
            }
            if (IsWhatExtensions.ISNotNULL_List(L3analysiList))
            {
                //插入数据
                insert_L3analysiTest_Data(iDataBase, L3analysiList);
            }
            if (IsWhatExtensions.ISNotNULL_List(L3analysiUpdateList))
            {
                //修改数据
                update_L3analysiTest_Data(iDataBase, L3analysiUpdateList);
            }
        }
        public override void RunTask(DateTime currentTime)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    //检索采集信息
                    retrieve_L3analysiTest_Data(iDataBase);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (IsWhatExtensions.ISNotNULL_List(Tx_SinterMainList)) Tx_SinterMainList.Clear();
                    if (IsWhatExtensions.ISNotNULL_List(Tx_SinterSubList)) Tx_SinterSubList.Clear();
                    if (IsWhatExtensions.ISNotNULL_List(L3analysiList)) L3analysiList.Clear();
                    if (IsWhatExtensions.ISNotNULL_List(L3analysiUpdateList)) L3analysiUpdateList.Clear();
                }
            }
        }


        /// <summary>
        /// 任务运行异常
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="exception"></param>
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
