using BFES.DataAccess;
using BFES.Utils;
using BKYL.WTD.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.WTD.Plugin
{
    class DataStatistics
    {
        /// <summary>
        /// 同步Sys TAP_HF_PLC 表,TAP_HF_01H表
        /// </summary>
        /// <param name="time"></param>
        public static void To_TD_TAP_HF_PLC_OR_01H(DataSet ds, string flag)
        {
            try
            {

                if (IsWhatExtensions.ISNotNULL_DataSet(ds) && IsWhatExtensions.ISNotNULL_List(Globals.Wtd_Tap_Statistics_Config))
                {
                    BF1_WTD_TAP_HF_PLC tap = new BF1_WTD_TAP_HF_PLC();
                    List<WTD_TAP_STATISTICS_CONFIG> monitor1; DataTable dt = new DataTable();
                    //for (int i = 1; i <= Globals.Wtd_Tap_Statistics_Config.GroupBy(m=>m.REGIONMARK).Count(); i++)
                    //{
                    foreach (WTD_TAP_STATISTICS_CONFIG item in Globals.Wtd_Tap_Statistics_Config.Where((x, i) => Globals.Wtd_Tap_Statistics_Config.FindIndex(n => n.REGIONMARK == x.REGIONMARK) == i).OrderBy(m => m.REGIONMARK).ToList())
                    {
                        monitor1 = Globals.Wtd_Tap_Statistics_Config.FindAll(m => m.REGIONMARK == item.REGIONMARK);
                        if (monitor1.Count > 0)
                        {
                            tap.TIMESTAMP = Convert.ToDateTime(ds.Tables[0].Rows[0][0].ToDate().ToString("yyyy-MM-dd HH:mm:ss"));
                            double avg = 0d;
                            double count = 0d; double sum = 0.00d;

                            foreach (WTD_TAP_STATISTICS_CONFIG monitor_config in monitor1)
                            {
                                dt = ds.Tables["WTD_HF" + monitor_config.GROUPNO + "_" + flag];
                                Double v = Convert.ToDouble(dt.Rows[0]["HF_" + monitor_config.GROUPNO + "_" + monitor_config.PIPENO]);
                                if (v > 0 && v != -9999)
                                {
                                    count += 1;
                                    sum += v;
                                }

                            }
                            if (count > 0 && sum > 0)
                            {
                                avg = Math.Round(sum / count, 3);
                            }
                            if (item.REGIONMARK == 1)
                            {
                                tap.TAP1 = avg;
                            }
                            if (item.REGIONMARK == 2)
                            {
                                tap.TAP2 = avg;
                            }
                            if (item.REGIONMARK == 3)
                            {
                                tap.TAP3 = avg;
                            }
                            if (item.REGIONMARK == 4)
                            {
                                tap.TAP4 = avg;
                            }

                        }
                    }

                    DataBaseType databasetype = new DataBaseType();
                    DataCollection.Get_Connect_String(ref databasetype, 3);
                    string insertavgsql = "insert into BF" + Globals.Parameters.BF_Id + "_WTD_HFAVG_" + flag + " values ({0},{1})";
                    string allavg = "";
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        string numb = ds.Tables[i].TableName.Split('_')[1].Replace("HF", "");
                        string name = ds.Tables[i].TableName;
                        allavg += Convert.ToDouble(ds.Tables[name].Rows[0]["HF" + numb + "_AVG"]).ToString() + ",";
                    }
                    insertavgsql = string.Format(insertavgsql, DataBaseFactory.ConvertTimeString(tap.TIMESTAMP, databasetype), allavg.TrimEnd(','));
                    //执行数据库操作
                    if (tap != null)
                    {
                        DataCollection.Insert_BF_WTD_TAP_HF_PLC(3, tap, "BF" + Globals.Parameters.BF_Id + "_WTD_TAP_HF_" + flag);
                    }

                    DataCollection.INSERT_BF_WTD_HFAVG_PLC(3, insertavgsql);

                    monitor1 = null;
                    tap = null;
                    dt = null;
                }

            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(DataCollection).Name);
            }
        }

        /// <summary>
        /// 同步Sys WTD_HF_01D 表
        /// </summary>
        /// <param name="time"></param>
        public static void TO_WTD_HF_01D(DateTime time)
        {
            try
            {
                string sql = "";
                string col_name = "";
                List<double> _avg = new List<double>();
                DataTable dtavg = new DataTable();
                List<string> insertsqls = new List<string>();

                foreach (WTD_GROUP_CONFIG item in Globals.Wtd_Group_Config)
                {
                    sql = "select max(timestamp), {0} from {1}";
                    col_name = "";

                    foreach (WTD_MONITOR_CONFIG monitor in Globals.Wtd_Monitor_Monfig.FindAll(m => m.GROUPNO == item.GROUPNO))
                    {
                        col_name += string.Format(" round(avg(decode(HF_{0}, 0, null, HF_{0})),3) HF_{0},", monitor.PIPENAME);
                    }
                    col_name += string.Format(" round(avg(decode(HF{0}_AVG, 0, null, HF{0}_AVG)),3) HF{0}AVG", item.GROUPNO);
                    DataBaseType databasetype = new DataBaseType();
                    DataCollection.Get_Connect_String(ref databasetype, 2);
                    string sql_one = string.Format(sql, col_name, string.Format("WTD_HF{0} where timestamp >= {1} and timestamp <{2} "
                        , item.GROUPNO, DataBaseFactory.ConvertTimeString(time.AddDays(-1).ToString("yyyy-MM-dd"), databasetype), DataBaseFactory.ConvertTimeString(time.ToString("yyyy-MM-dd"), databasetype)));
                    dtavg = DataCollection.Getdate_datatable(2, "", sql_one);
                    if (IsWhatExtensions.ISNotNULL_DataTable(dtavg))
                    {
                        string insertsql = "insert into BF" + Globals.Parameters.BF_Id + "_WTD_HF" + item.GROUPNO + "_01D values(" + DataBaseFactory.ConvertTimeString(dtavg.Rows[0][0].ToDate().ToString("yyyy-MM-dd"), databasetype) + ",{0})";
                        string values = "";
                        for (int i = 1; i < dtavg.Columns.Count; i++)
                        {
                            values += string.IsNullOrEmpty(dtavg.Rows[0][i].ToString()) ? "0.00" + "," : dtavg.Rows[0][i].ToString() + ",";
                        }
                        insertsqls.Add(string.Format(insertsql, values.TrimEnd(',')));
                    }
                }

                DataCollection.Insert_WTD_HF_01D(3, insertsqls);
                insertsqls = null;
                _avg = null;
                dtavg = null;

            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(DataCollection).Name);
            }
        }

        /// <summary>
        /// 同步Sys WTD_HFDAY_01D 表
        /// </summary>
        /// <param name="time"></param>
        public static void TO_WTD_HFDAY_01D(DateTime time)
        {
            try
            {
                string sql = "";
                string col_name = "";
                List<double> _avg = new List<double>();
                //List<BF_WTD_HFDAY> hfday = new List<BF_WTD_HFDAY>();
                List<WTD_MONITOR_CONFIG> list = null;
                BF1_WTD_HFDAY_01D statistics;
                List<BF1_WTD_HFDAY_01D> hfs = new List<BF1_WTD_HFDAY_01D>();
                DataTable dtmax;
                foreach (WTD_GROUP_CONFIG item in Globals.Wtd_Group_Config)
                {
                    statistics = new BF1_WTD_HFDAY_01D();
                    sql = "select MAX(timestamp), {0} from {1}";
                    col_name = "";
                    list = Globals.Wtd_Monitor_Monfig.FindAll(m => m.GROUPNO == item.GROUPNO);
                    foreach (WTD_MONITOR_CONFIG monitor in list)
                    {
                        col_name += string.Format("MAX(HF_{0}) HF_{0}", monitor.PIPENAME) + ",";
                    }
                    col_name += string.Format(" round(avg(decode(HF{0}_AVG, 0, null, HF{0}_AVG)),3) HF{0}_AVG", item.GROUPNO);
                    DataBaseType databasetype = new DataBaseType();
                    DataCollection.Get_Connect_String(ref databasetype, 2);

                    string sql_one = string.Format(sql, col_name, string.Format("WTD_HF{0} where timestamp >= {1} and timestamp <{2} "
                        , item.GROUPNO, DataBaseFactory.ConvertTimeString(time.AddDays(-1).ToString("yyyy-MM-dd"), databasetype), DataBaseFactory.ConvertTimeString(time.ToString("yyyy-MM-dd"), databasetype)));
                    dtmax = DataCollection.Getdate_datatable(2, "", sql_one);

                    if (IsWhatExtensions.ISNotNULL_DataTable(dtmax))
                    {
                        statistics.TIMESTAMP = Convert.ToDateTime(dtmax.Rows[0][0].ToDate().ToString("yyyy-MM-dd"));
                        statistics.GROUPNO = item.GROUPNO;
                        double HF_MAX_VALUE = -0.001;

                        double row_max = 0.00;
                        string row_maxname = "";
                        double v = 0.00d;
                        for (int i = 1; i < dtmax.Columns.Count - 1; i++)
                        {
                            v = string.IsNullOrEmpty(dtmax.Rows[0][i].ToString()) ? 0.00D : Convert.ToDouble(dtmax.Rows[0][i].ToString());
                            if (v > HF_MAX_VALUE)
                            {
                                row_max = v;
                                HF_MAX_VALUE = row_max;
                                row_maxname = dtmax.Columns[i].ColumnName.ToString();
                            }

                        }

                        statistics.HF_MAX_VALUE = row_max;
                        statistics.HF_MAX_NAME = Globals.Wtd_Monitor_Monfig.Find(m => "HF_" + m.PIPENAME == row_maxname).PIPEDESC;
                        string str_avg = dtmax.Rows[0][string.Format("HF{0}_AVG", item.GROUPNO)].ToString();
                        statistics.HF_AVG_VALUE = string.IsNullOrEmpty(str_avg) ? 0.00d : Convert.ToDouble(str_avg);
                        if (statistics != null)
                        {
                            hfs.Add(statistics);
                            statistics = null;
                        }
                    }

                    dtmax.Clear();

                }

                if (IsWhatExtensions.ISNotNULL_List(hfs))
                {
                    DataCollection.Insert_BF1_WTD_HFDAY_01D(3, hfs, "BF" + Globals.Parameters.BF_Id + "_WTD_HFDAY_01D");
                }

                dtmax = null;
                _avg = null;
                list = null;
                dtmax = null;
                hfs = null;
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(DataCollection).Name);
            }
        }

        /// <summary>
        /// 数据对比，实时数值和指定时间范围的均值比较。
        /// </summary>
        public static void Data_Comparison()
        {
            int avg = Globals.ds_HeatFlow_48H_Avg.Tables.Count;
            int plc = Globals.ds_HeatFlow_PLC.Tables.Count;

            List<WTD_AVG_ALARM> list_avg_alarm = new List<WTD_AVG_ALARM>();
            //按照每组水温差循环
            foreach (DataTable tab in Globals.ds_HeatFlow_48H_Avg.Tables)
            {

                double avg_val = 0.00d;//均值

                foreach (DataColumn item in tab.Columns)
                {
                    avg_val = Convert.ToDouble(tab.Rows[0][item.ColumnName]);

                    double real_value = 0.00d;//实时数值

                    if (avg_val > 0.00d)
                    {
                        DateTime timestamp = DateTime.MinValue;
                        int groupno = 0;
                        int pipeno = 0;
                        string pipedesc = "";

                        string alarm_desc = "";
                        string plc_table_name = tab.TableName.Replace("_01H", "_PLC");
                        real_value = Convert.ToDouble(Globals.ds_HeatFlow_PLC.Tables[plc_table_name].Rows[0][item.ColumnName]);
                        if (real_value > 0.00d)//如果当前值大于0
                        {
                            if (real_value < avg_val * (1 - Globals.Parameters.AVG_DIFF / 100))
                            {

                                timestamp = Convert.ToDateTime(Globals.ds_HeatFlow_PLC.Tables[plc_table_name].Rows[0]["TIMESTAMP"].ToString());
                                groupno = Convert.ToInt32(item.ColumnName.Split('_')[1].Replace("HF", ""));
                                pipeno = Convert.ToInt32(item.ColumnName.Split('_')[2].Replace("HF", ""));
                                pipedesc = Globals.Wtd_Monitor_Monfig.Find(m => m.GROUPNO == groupno && m.PIPENO == pipeno).PIPEDESC;
                                alarm_desc =string.Format( "热流强度低于{0}%", Globals.Parameters.AVG_DIFF);
                                list_avg_alarm.Add(new WTD_AVG_ALARM { TIMESTAMP = timestamp, GROUPNO = groupno, PIPENO = pipeno, PIPEDESC = pipedesc, ALARMDESC = alarm_desc, AVG_VAL = avg_val, CURRENT_VAL = real_value, VAL_FLAG=-1 });
                            }
                            else if (real_value > avg_val * (1 + Globals.Parameters.AVG_DIFF / 100))
                            {
                                timestamp = Convert.ToDateTime(Globals.ds_HeatFlow_PLC.Tables[plc_table_name].Rows[0]["TIMESTAMP"].ToString());
                                groupno = Convert.ToInt32(item.ColumnName.Split('_')[1].Replace("HF", ""));
                                pipeno = Convert.ToInt32(item.ColumnName.Split('_')[2].Replace("HF", ""));
                                pipedesc = Globals.Wtd_Monitor_Monfig.Find(m => m.GROUPNO == groupno && m.PIPENO == pipeno).PIPEDESC;
                                alarm_desc = string.Format("热流强度高于{0}%", Globals.Parameters.AVG_DIFF);
                                list_avg_alarm.Add(new WTD_AVG_ALARM { TIMESTAMP = timestamp, GROUPNO = groupno, PIPENO = pipeno, PIPEDESC = pipedesc, ALARMDESC = alarm_desc, AVG_VAL = avg_val, CURRENT_VAL = real_value, VAL_FLAG=1 });
                            }

                        }


                    }
                }

             
                
            }
            if (IsWhatExtensions.ISNotNULL_List(list_avg_alarm))
            {
                DataCollection.Insert_WTD_AVG_ALARM(2, list_avg_alarm);
                list_avg_alarm = null;
            }
        }

    }
}
