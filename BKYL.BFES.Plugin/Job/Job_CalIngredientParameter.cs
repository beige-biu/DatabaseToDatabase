using BFES.DataAccess;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Job
{
    public class Job_CalIngredientParameter : BaseJob
    {

        #region 采集数据表
        List<L2_PELLET_ANA> pelletList = null;
        List<TC_ROASTER_BAS_10M> roasterList = null;
        #endregion
        #region 入库修改数据
        List<L2_PELLET_ANA> pelletUpdateList = new List<L2_PELLET_ANA>();
        List<TC_ROASTER_BAS_10M> roasterUpdateList = new List<TC_ROASTER_BAS_10M>();
        #endregion
        public override void InitTask()
        {           
        }

        public override void RunTask(DateTime currentTime)
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    string selectPelletSql = string.Format("select * from {0} where M_CAL_FLAG=0", typeof(L2_PELLET_ANA).Name);
                    pelletList = iDataBase.GetList<L2_PELLET_ANA>(selectPelletSql);
                    foreach (L2_PELLET_ANA pelletModel in pelletList)
                    {
                        DateTime startTime = pelletModel.M_SAMPLE_TIME.Value.AddMinutes(-60);
                        DateTime endTime = pelletModel.M_SAMPLE_TIME.Value;

                        string selectRoasterSql = string.Format("select * from  {0} where  timestamp>{1}  and timestamp <={2} order by timestamp ", "TC_ROASTER_BAS_10M", DataBaseFactory.ConvertTimeString(startTime), DataBaseFactory.ConvertTimeString(endTime));
                        roasterList = iDataBase.GetList<TC_ROASTER_BAS_10M>(selectRoasterSql);
                        foreach (TC_ROASTER_BAS_10M roasterModel in roasterList)
                        {
                            roasterModel.TC_TFE = pelletModel.M_TFE;
                            roasterModel.TC_FEO = pelletModel.M_FEO;
                            roasterModel.TC_CS = pelletModel.M_CS;
                            roasterModel.TC_DRIX = pelletModel.M_DRIX;
                            roasterUpdateList.Add(roasterModel);
                        }
                        pelletModel.M_CAL_FLAG = 1;
                        pelletUpdateList.Add(pelletModel);
                    }
                    if (pelletUpdateList.Count > 0)
                        iDataBase.UpdateRange<L2_PELLET_ANA>(pelletUpdateList);
                    if (roasterUpdateList.Count > 0)
                        iDataBase.UpdateRange<TC_ROASTER_BAS_10M>(roasterList);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {

        }
    }
}
