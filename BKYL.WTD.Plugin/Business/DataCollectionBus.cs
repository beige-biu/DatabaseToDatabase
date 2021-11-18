using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BFES.Utils;
using BKYL.WTD.Plugin.Models;
using BFES.DataAccess;

namespace BKYL.WTD.Plugin
{

    /// <summary>
    /// 数据采集
    /// </summary>
    public static class DataCollectionBus
    {
        /// <summary>
        /// 数据采集 PLC实时数据表
        /// </summary>
        public static void Collection_PLC(string flag = "PLC")
        {

            //获取所有plc表名称，查询所有表数据返回 dataset
            DataSet ds = DataCollection.Select_DataTable(1,
                DataCollection.Get_Plc_Name(1, flag, flag),
                Globals.DB_Max_DateTime_PLC.ToString("yyyy-MM-dd HH:mm:ss"));
            if (IsWhatExtensions.ISNotNULL_DataSet(ds))
            {
                //获取所有（有数据）PLC表的sql语句的list
                List<string> list = DataCollection.Get_Insert_Sql(2, ds);
                List<WTD_DETAIL_PLC_V> list_wtd_detail_plc_v = DataCollection.Get_Data_List<WTD_DETAIL_PLC_V>(1, "WTD_DETAIL_PLC_V");

                DataCollection.Insert_DataTable(2, list, list_wtd_detail_plc_v);

                //获取所有热流强度PLC表
                Globals.ds_HeatFlow_PLC = new DataSet();
                foreach (DataTable item in ds.Tables)
                {
                    if (item.TableName.Contains("WTD_HF"))
                    {
                        Globals.ds_HeatFlow_PLC.Tables.Add(item.Copy());
                    }
                }
            }
        }
        /// <summary>
        /// 数据采集1小时均值表
        /// </summary>
        /// <param name="flag"></param>
        public static void Collection_01H(DateTime Curr_DateTime, string flag = "01H")
        {
            //获取所有plc表名称，查询所有表数据返回 dataset
            DataSet ds = DataCollection.Select_DataTable(1,
                DataCollection.Get_Plc_Name(1, flag, flag)
                , Curr_DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            if (IsWhatExtensions.ISNotNULL_DataSet(ds))
            {

                //获取所有（有数据）PLC表的sql语句的list
                List<string> list = DataCollection.Get_Insert_Sql(2, ds);
                DataCollection.Insert_DataTable(2, list, null);
                //获取所有热流强度PLC表
                Globals.ds_HeatFlow_01H = new DataSet();
                foreach (DataTable item in ds.Tables)
                {
                    if (item.TableName.Contains("WTD_HF"))
                    {
                        Globals.ds_HeatFlow_01H.Tables.Add(item.Copy());
                    }
                }
            }

        }

        /// <summary>
        /// 数据采集配置表
        /// </summary>
        /// <param name="flag"></param>
        public static void Collection_CONFIG(string flag = "CONFIG")
        {   ///获取 LAST_USER_UPDATE

            //AND LAST_USER_UPDATE IS NOT NULL
            DataTable dt = null;
            if (!IsWhatExtensions.ISNotNULL_List(Globals.List_Config_Update_Datetime))
            {
                dt = DataCollection.Get_Plc_Name(1, flag, "");
                Globals.List_Config_Update_Datetime = Jobs.Base.Utils.ConvertHelper<LIST_CONFIG_UPDATE_DATETIME>.ConvertToList(dt);
            }
            else
            {
                dt = DataCollection.Get_Plc_Name(1, flag, " AND LAST_USER_UPDATE IS NOT NULL ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string name = dt.Rows[i]["NAME"].ToString();
                    DateTime curr_time = Convert.ToDateTime(dt.Rows[i]["LAST_USER_UPDATE"].ToString());
                    //如果最新修改时间小于目标数据库配置表修改时间，删除不操作。
                    if (curr_time <= Globals.List_Config_Update_Datetime.Find(m => m.NAME == name).LAST_USER_UPDATE)
                    {
                        dt.Rows.RemoveAt(0);
                    }
                    else//如果大于，有新配置，写到目标数据库，并记录最大时间。
                    {
                        Globals.List_Config_Update_Datetime.Find(m => m.NAME == name).LAST_USER_UPDATE = curr_time;
                    }
                }
            }
            //获取所有plc表名称，查询所有表数据返回 dataset
            DataSet ds = DataCollection.Select_DataTable(1, dt, null);
            if (IsWhatExtensions.ISNotNULL_DataSet(ds))
            {

                DataCollection.Insert_Config(2, ds);
            }
            if (IsWhatExtensions.ISNotNULL_DataTable(dt)) dt = null;
            if (IsWhatExtensions.ISNotNULL_DataSet(ds)) ds = null;
        }
        /// <summary>
        /// 数据采集1小时均值表
        /// </summary>
        /// <param name="flag"></param>
        public static void Collection_ALARM(string flag = "WTD_ALARM")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("name"));
            DataRow dr = dt.NewRow();
            dr["name"] = "WTD_ALARM";
            dt.Rows.Add(dr);
            //获取所有plc表名称，查询所有表数据返回 dataset
            DataSet ds = DataCollection.Select_DataTable(1, dt
                , Globals.DB_Max_DateTime_PLC.ToString("yyyy-MM-dd HH:mm:ss"));
            if (IsWhatExtensions.ISNotNULL_DataSet(ds))
            {
                //获取所有（有数据）PLC表的sql语句的list
                List<string> list = DataCollection.Get_Insert_Sql(2, ds);
                DataCollection.Insert_ALARM(2, ds);
                ds = null;
                dt = null;
            }
        }


        /// <summary>
        /// 高炉连接状态判断
        /// </summary>
        public static string  Checking_Connect_Status()
        {
            List<VM_STATUSINFO> list_info = null; List<WTD_BF_STATUSLOG> list_log = null;
            try
            {

                string sql = string.Format(" SELECT ID,BFESNO,STATUS,TIMESTAMP,RANGETIME FROM VM_STATUSINFO where id={0}", Globals.Parameters.BF_Id);
                list_info = DataCollection.Get_Data_List<VM_STATUSINFO>(3, "VM_STATUSINFO", sql, null);
                if (!IsWhatExtensions.ISNotNULL_List(Globals.List_StatusInfo))
                {
                    Globals.List_StatusInfo = list_info;
                }
                if (IsWhatExtensions.ISNotNULL_List(list_info))
                {
                    list_log = new List<WTD_BF_STATUSLOG>();
                    foreach (VM_STATUSINFO item in list_info)
                    {
                        //如果延迟时间（系统时间-采集时间）大于指定时间（RANGETIME）,
                        if (Globals.List_StatusInfo.Find(m => m.ID == item.ID).NOT_CONNECT_FLAG == false && item.STATUS > item.RANGETIME)
                        {
                            //断开表示为true
                            Globals.List_StatusInfo.Find(m => m.ID == item.ID).NOT_CONNECT_FLAG = true;
                            list_log.Add(new WTD_BF_STATUSLOG { ID = item.ID, BFESNO = item.BFESNO, TIMESTAMP = item.TIMESTAMP, CONTENT = "数据采集异常停止" });
                        }

                        //如果已经断开，之后恢复
                        else if (Globals.List_StatusInfo.Find(m => m.ID == item.ID).NOT_CONNECT_FLAG == true && item.STATUS <= item.RANGETIME)
                        {
                            //断开表示为true
                            Globals.List_StatusInfo.Find(m => m.ID == item.ID).NOT_CONNECT_FLAG = false;
                            list_log.Add(new WTD_BF_STATUSLOG { ID = item.ID, BFESNO = item.BFESNO, TIMESTAMP = item.TIMESTAMP, CONTENT = "数据采集恢复正常" });
                        }
                    }
                    if (IsWhatExtensions.ISNotNULL_List(list_log))
                    {
                        DataCollection.Insert_STATUSLOG(3, list_log);
         
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                if (IsWhatExtensions.ISNotNULL_List(list_log)) list_log = null;
                if (IsWhatExtensions.ISNotNULL_List(list_info)) list_info = null;
            }

        }
    }
}
