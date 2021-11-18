using BFES.DataAccess;
using BFES.Utils;
using BKYL.Jobs.Base;
using System;
using System.Linq;

namespace BKYL.WTD.Plugin
{
    public class WTDJob : BaseJob
    {
        string tname = string.Format("WTD_HF{0}", 1);
        string times1 = string.Empty;
        public override void InitTask()
        {
            ReadConfig.Get_All_Config();

            #region 测试使用

            //Globals.ds_HeatFlow_48H_Avg = DataCollection.Get_Interval_AVG(2);
            //times1 = DataCollection.GetMaxTime(1, tname + "_PLC", "1=1");
            //Globals.DB_Max_DateTime_PLC = times1.IsNullOrEmpty() ? DateTime.MinValue : Convert.ToDateTime(times1);

            ////分钟
            //DataCollectionBus.Collection_PLC();
            //DataStatistics.Data_Comparison();

            //DataCollectionBus.Collection_ALARM();
            //DataCollectionBus.Collection_CONFIG();
            //DataStatistics.To_TD_TAP_HF_PLC_OR_01H(Globals.ds_HeatFlow_PLC, "PLC");
            //Globals.DB_Max_DateTime_PLC2 = Globals.DB_Max_DateTime_PLC;


            //times1 = DataCollection.GetMaxTime(1, tname + "_01H", "1=1");
            //DateTime db_max_datetime_hour1 = times1.IsNullOrEmpty() ? DateTime.MinValue : Convert.ToDateTime(times1);
            //string times3 = DataCollection.GetMaxTime(2, tname + "_01H", "1=1");
            //DateTime db_max_datetime_hour2 = times3.IsNullOrEmpty() ? DateTime.MinValue : Convert.ToDateTime(times3);
            ////if (db_max_datetime_hour1 > db_max_datetime_hour2)
            ////{
            ////01H
            //DataCollectionBus.Collection_01H(db_max_datetime_hour1);
            //DataStatistics.To_TD_TAP_HF_PLC_OR_01H(Globals.ds_HeatFlow_01H, "01H");
            ////}

            ////01D
            //DataStatistics.TO_WTD_HF_01D(Globals.DB_Max_DateTime_PLC);
            //DataStatistics.TO_WTD_HFDAY_01D(Globals.DB_Max_DateTime_PLC); 
            #endregion
        }
        /// <summary>
        /// 需要执行的任务
        /// </summary>
        /// <param name="currentTime"></param>
        public override void RunTask(DateTime currentTime)
        {
           
            //每分钟统计，每10秒触发一次
            int time = currentTime.Hour * 3600 + currentTime.Minute * 60 + currentTime.Second;
            string times1 = null;
            tname = string.Format("WTD_HF{0}", Globals.Wtd_Group_Config.Min(m => m.GROUPNO));
            if (time % 60 == 0)
            {

                times1 = DataCollection.GetMaxTime(1, tname + "_PLC", "1=1");
                Globals.DB_Max_DateTime_PLC = times1.IsNullOrEmpty() ? DateTime.MinValue : Convert.ToDateTime(times1);
                //如果源数据库时间大于目标数据库时间。
                if (Globals.DB_Max_DateTime_PLC > Globals.DB_Max_DateTime_PLC2)
                {
                    //分钟
                    DataCollectionBus.Collection_PLC();
                    DataCollectionBus.Collection_ALARM();
                    DataCollectionBus.Collection_CONFIG();
                    DataStatistics.To_TD_TAP_HF_PLC_OR_01H(Globals.ds_HeatFlow_PLC, "PLC");
                    Globals.DB_Max_DateTime_PLC2 = Globals.DB_Max_DateTime_PLC;


                    //如果第一次为空，
                    if (Globals.ds_HeatFlow_48H_Avg.IsNullOrEmpty())
                    {
                        //计算时间范围内均值
                        Globals.ds_HeatFlow_48H_Avg = DataCollection.Get_Interval_AVG(2);
                    }
                    //进行数据比对，实时数值和范围均值比较
                    DataStatistics.Data_Comparison();
                }
            }
            //每小时统计
            if (currentTime.Minute == 1 && currentTime.Second == 0)
            {
                times1 = DataCollection.GetMaxTime(1, tname + "_01H", "1=1");
                DateTime db_max_datetime_hour1 = times1.IsNullOrEmpty() ? Convert.ToDateTime("2000-01-01 00:00:00") : Convert.ToDateTime(times1);
                string times3 = DataCollection.GetMaxTime(2, tname + "_01H", "1=1");
                DateTime db_max_datetime_hour2 = times3.IsNullOrEmpty() ? Convert.ToDateTime("2000-01-01 00:00:00") : Convert.ToDateTime(times3);
                if (db_max_datetime_hour1 > db_max_datetime_hour2)
                {
                    //01H
                    DataCollectionBus.Collection_01H(db_max_datetime_hour1);
                    DataStatistics.To_TD_TAP_HF_PLC_OR_01H(Globals.ds_HeatFlow_01H, "01H");
                    //在小时刷新，时间范围均值
                    Globals.ds_HeatFlow_48H_Avg = DataCollection.Get_Interval_AVG(2);
                }
            }
            //每天统计
            if (currentTime.Hour == 0 && currentTime.Minute == 2)
            {
                //01D
                DataStatistics.TO_WTD_HF_01D(Globals.DB_Max_DateTime_PLC);
                DataStatistics.TO_WTD_HFDAY_01D(Globals.DB_Max_DateTime_PLC);
            }
            //高炉连接状态监测，写入日志
            string error = DataCollectionBus.Checking_Connect_Status();
            if (!string.IsNullOrEmpty(error))
            {
                Outputlog(error);
            }

        }
    }
}
