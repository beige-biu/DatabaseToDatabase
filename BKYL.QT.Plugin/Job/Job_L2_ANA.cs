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
    public class Job_L2_ANA : BaseJob
    {
        #region 采集数据
        List<L3TOL2ANALYSISDATA> pelletList = null;
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

        public override void InitTask()
        {
        }
        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {





                }
                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {


                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {

            }
        }
    }
}
