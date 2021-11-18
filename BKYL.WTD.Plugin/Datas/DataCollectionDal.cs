using BFES.DataAccess;

using BKYL.Jobs.Base.Utils;
using BKYL.WTD.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace BKYL.WTD.Plugin
{
    public static class DataCollection
    {
        /// <summary>
        /// 获得指定sql语句的数据
        /// </summary>
        /// <param name="sql">T-sql语句</param>
        /// <returns></returns>
        public static DataTable Getdate_datatable(int conn_id, string table_name, string sql)
        {
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                {
                    DataTable dt = iDataBase.GetDataTable(sql);
                    dt.TableName = table_name;
                    return dt;
                }
            }
        }

        /// <summary>
        /// 获取对应的数据库链接
        /// </summary>
        /// <param name="conn_id"></param>
        /// <returns></returns>
        public static string Get_Connect_String(ref DataBaseType databasetype, int conn_id = 1)
        {
            string[] str = null;
            string conn_str = "";//IP,Dbname,user,pwd

            try
            {

                if (conn_id.Equals(1))
                {
                    str = Globals.Parameters.DB_Connect1.Split('$');
                    conn_str = string.Format(Get_Conn(ref databasetype, str[4]),
                        str[0], str[1], str[2], str[3]);
                }
                if (conn_id.Equals(2))
                {
                    str = Globals.Parameters.DB_Connect2.Split('$');
                    conn_str = string.Format(Get_Conn(ref databasetype, str[4]),
                        str[0], str[1], str[2], str[3]);
                }
                if (conn_id.Equals(3))
                {
                    str = Globals.Parameters.DB_Connect3.Split('$');
                    conn_str = string.Format(Get_Conn(ref databasetype, str[4]),
                       str[0], str[1], str[2], str[3]);
                }
                str = null;
            }
            catch (Exception ex)
            {

                Log.WriteLine(ex.Message, typeof(DataCollection).Name);
            }
            return conn_str;
        }

        public static string Get_Conn(ref DataBaseType databasetype, string str_dbtype)
        {
            string conn = "";
            if (str_dbtype.ToUpper().Equals("SQLSERVER"))
            {
                databasetype = DataBaseType.SqlServer;
                conn = "server ={0}; Database ={1}; Integrated Security = False; Pooling = True; user ={2}; pwd ={3}";
            }
            if (str_dbtype.ToUpper().Equals("ORACLE"))
            {
                databasetype = DataBaseType.Oracle;
                conn = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(CONNECT_DATA =(SERVICE_NAME = {1})));User Id={2};Password={3}";
            }
            return conn;
        }

        /// <summary>
        /// 获取泛型list
        /// </summary>
        /// <param name="table_name"></param>
        /// <param name="time_name"></param>
        /// <returns></returns>
        public static List<T> Get_Data_List<T>(int conn_id, string table_name, string string_sql = null, string where = "")
        {
            List<T> list = null;
            try
            {
                string sql = "";
                if (string_sql != null)
                {
                    sql = string_sql;
                }
                else
                {
                    sql = string.Format(" SELECT * FROM {0} T {1} "
                  , table_name, where);
                }
                DataBaseType databasetype = new DataBaseType();
                using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
                {
                    list = iDataBase.GetList<T>(sql);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(DataCollection).Name);

            }
            return list;
        }

        /// <summary>
        /// 查询指定数据表，最大时间
        /// </summary>
        /// <param name="tbname"></param>
        /// <returns></returns>
        public static string GetMaxTime(int conn_id, string tbname, string where = "")
        {
            string strSQL = string.Format("SELECT MAX(TIMESTAMP) TIMESTAMP FROM {0} WHERE {1}", tbname, where);
            DataTable dt = Getdate_datatable(conn_id, tbname, strSQL);
            return dt.Rows[0]["TIMESTAMP"].ToString();
        }

        /// <summary>
        /// 查询所有指定表数据
        /// </summary>
        /// <param name="databasetype">数据库类型</param>
        /// <param name="conn_id">链接编号</param>
        /// <param name="table_name">数据表名称</param>
        /// <returns></returns>
        public static DataSet Select_DataTable(int conn_id, DataTable table_name, string start_time)
        {
            DataSet ds = new DataSet();
            try
            {
                DataBaseType databasetype = new DataBaseType();
                Get_Connect_String(ref databasetype, conn_id);
                DataTable dt; string name = ""; string sql = null;
                for (int i = 0; i < table_name.Rows.Count; i++)
                {
                    name = table_name.Rows[i]["name"].ToString();
                    if (!string.IsNullOrEmpty(start_time))
                    {
                        sql = string.Format("SELECT * FROM {0} WHERE TIMESTAMP = {1}", name,
                            DataBaseFactory.ConvertTimeString(start_time, databasetype));
                    }

                    else if (name.ToUpper().Contains("CONFIG") || name.ToUpper().Contains("PLC"))
                    {
                        sql = string.Format("SELECT * FROM {0}", name);
                    }

                    dt = Getdate_datatable(conn_id, name, sql);
                    dt.TableName = name;
                    //如果表不为空，才添加到数据了列表中
                    if (BFES.Utils.IsWhatExtensions.ISNotNULL_DataTable(dt))
                    {
                        ds.Tables.Add(dt);
                    }
                    sql = null; name = null;

                }
                table_name = null;
                dt = null;
                start_time = null;
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(DataCollection).Name);
            }
            return ds;
        }
        /// <summary>
        /// 将数据添加到指定数据库指定表中
        /// </summary>
        /// <param name="databasetype"></param>
        /// <param name="conn_id"></param>
        /// <param name="list"></param>
        /// <param name="list_v"></param>
        /// <returns></returns>
        public static bool Insert_DataTable(int conn_id, List<string> list, List<WTD_DETAIL_PLC_V> list_v)
        {
            bool flag = false;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.BeginTran();
                    foreach (string item in list)
                    {
                        iDataBase.ExecuteCommand(item);
                    }
                    if (BFES.Utils.IsWhatExtensions.ISNotNULL_List(list_v))
                    {
                        string sql_delete = " DELETE WTD_DETAIL_PLC_V";
                        if (iDataBase.ExecuteCommand(sql_delete) >= 0)
                        {
                            iDataBase.InsertRange<WTD_DETAIL_PLC_V>(list_v);
                        }
                        sql_delete = null;
                    }
                    iDataBase.CommitTran();
                    flag = true;
                    list = null;
                    list_v = null;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }

            }
            return flag;
        }


        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="databasetype"></param>
        /// <param name="conn_id"></param>
        /// <param name="list"></param>
        /// <param name="list_v"></param>
        /// <returns></returns>
        public static bool Insert_Config(int conn_id, DataSet ds_config)
        {
            bool flag = false;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.BeginTran();
                    foreach (DataTable item in ds_config.Tables)
                    {
                        string sql_delete = string.Format(" DELETE {0} ", item.TableName);
                        if (iDataBase.ExecuteCommand(sql_delete) >= 0)
                        {
                            switch (item.TableName)
                            {
                                case "WTD_BF_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_BF_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_COM_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_COM_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_FT_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_FT_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_GROUP_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_GROUP_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_INPUT_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_INPUT_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_MONITOR_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_MONITOR_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_SEG_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_SEG_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_TAP_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_TAP_CONFIG>.ConvertToList(item));
                                    break;
                                case "WTD_TAP_STATISTICS_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<WTD_TAP_STATISTICS_CONFIG>.ConvertToList(item));
                                    break;
                                case "FS_GROUP_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<FS_GROUP_CONFIG>.ConvertToList(item));
                                    break;
                                case "FS_MONITOR_CONFIG":
                                    iDataBase.InsertRange(Jobs.Base.Utils.ConvertHelper<FS_MONITOR_CONFIG>.ConvertToList(item));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    flag = true;
                    iDataBase.CommitTran();
                    ds_config = null;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Jobs.Base.Utils.Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }

            }
            return flag;
        }



        /// <summary>
        /// 获取所有PLC表名称
        /// </summary>
        /// <param name="databasetype"></param>
        /// <param name="conn_id"></param>
        /// <returns></returns>
        public static DataTable Get_Plc_Name(int conn_id, string flag, string where)
        {
            string sql = "";
            if (flag.ToUpper().Equals("CONFIG") && string.IsNullOrEmpty(where))
            {
                sql = Globals.sql_all_config;
            }

            else if (flag.ToUpper().Equals("CONFIG") && !string.IsNullOrEmpty(where))
            {
                sql = Globals.sql_refeash_config;
            }
            else
            {
                sql = Globals.sql_plc;
            }
            return Getdate_datatable(conn_id, "name", string.Format(sql, where));
        }



        /// <summary>
        /// 获取添加sql语句
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<string> Get_Insert_Sql(int conn_id, DataSet ds)
        {
            List<string> list = new List<string>();
            try
            {
                DataBaseType databasetype = new DataBaseType();
                DataCollection.Get_Connect_String(ref databasetype, 2);
                string str = " INSERT INTO {0} VALUES({1})"; string sql = ""; string values = "";
                foreach (DataTable item in ds.Tables)
                {
                    DateTime time; values = "";
                    for (int i = 0; i < item.Columns.Count; i++)
                    {
                        string v = item.Rows[0][i].ToString();
                        //判断是否为时间类型，如果是 添加to_date
                        if (DateTime.TryParse(v, out time) && item.Columns[i].ColumnName.ToUpper().Equals("TIMESTAMP"))
                        {
                            values += DataBaseFactory.ConvertTimeString(time, databasetype);
                        }
                        else
                        {
                            values += string.Format(v);
                        }
                        values += ","; v = null;
                    }
                    values = values.Substring(0, values.Length - 1);
                    sql = string.Format(str, item.TableName, values);
                    list.Add(sql);
                    sql = string.Empty; values = string.Empty;
                }
                sql = null; values = null;
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(DataCollection).Name);
            }
            return list;


        }

        /// <summary>
        /// 同步Sys _WTD_TAP_HF_PLC 表,_WTDTAP_HF_01H表
        /// </summary>
        /// <param name="conn_id"></param>
        /// <param name="cc"></param>
        /// <param name="tabname"></param>
        /// <returns></returns>
        public static bool Insert_BF_WTD_TAP_HF_PLC(int conn_id, BF1_WTD_TAP_HF_PLC cc, string tabname)
        {
            bool flag = false;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.IsEnableAttributeMapping = true;
                    iDataBase.ClearMappingTables();
                    iDataBase.AddMappingTable(new BFES.DataAccess.Common.KeyValue()
                    {
                        Key = "BF1_WTD_TAP_HF_PLC",
                        Value = tabname
                    });
                    iDataBase.BeginTran();
                    iDataBase.Insert<BF1_WTD_TAP_HF_PLC>(cc);
                    iDataBase.CommitTran();
                    flag = true;
                    cc = null;
                    tabname = null;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }
                finally
                {
                    iDataBase.DisableInsertColumns = null;
                    iDataBase.ClearMappingTables();
                }
            }

            return flag;
        }

        /// <summary>
        /// 同步Sys _WTD_HFAVG_PLC 表
        /// </summary>
        /// <param name="conn_id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int INSERT_BF_WTD_HFAVG_PLC(int conn_id, string sql)
        {
            int flag = 0;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.BeginTran();
                    flag = iDataBase.ExecuteCommand(sql);
                    iDataBase.CommitTran();
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }
            }
            return flag;
        }

        /// <summary>
        /// 同步Sys _WTD_HF_01D 表
        /// </summary>
        /// <param name="conn_id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool Insert_WTD_HF_01D(int conn_id, List<string> list)
        {
            bool flag = false;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.BeginTran();
                    foreach (string item in list)
                    {
                        iDataBase.ExecuteCommand(item);
                    }

                    iDataBase.CommitTran();
                    flag = true;
                    list = null;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }

            }
            return flag;
        }

        /// <summary>
        /// 同步Sys _WTD_HFDAY_01D 表
        /// </summary>
        /// <param name="conn_id"></param>
        /// <param name="cc"></param>
        /// <param name="tabname"></param>
        /// <returns></returns>
        public static bool Insert_BF1_WTD_HFDAY_01D(int conn_id, List<BF1_WTD_HFDAY_01D> cc, string tabname)
        {
            bool flag = false;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.IsEnableAttributeMapping = true;
                    iDataBase.ClearMappingTables();
                    iDataBase.AddMappingTable(new BFES.DataAccess.Common.KeyValue()
                    {
                        Key = "BF1_WTD_HFDAY_01D",
                        Value = tabname
                    });
                    iDataBase.BeginTran();
                    iDataBase.InsertRange(cc);
                    iDataBase.CommitTran();
                    flag = true;
                    cc = null;
                    tabname = null;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }
                finally
                {
                    iDataBase.DisableInsertColumns = null;
                    iDataBase.ClearMappingTables();
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加报警信息
        /// </summary>
        /// <param name="databasetype"></param>
        /// <param name="conn_id"></param>
        /// <param name="ds_config"></param>
        /// <returns></returns>
        public static bool Insert_ALARM(int conn_id, DataSet ds_config)
        {
            bool flag = false; DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    foreach (DataTable item in ds_config.Tables)
                    {
                        iDataBase.InsertRange<WTD_ALARM>(Jobs.Base.Utils.ConvertHelper<WTD_ALARM>.ConvertToList(item));
                    }
                }
                catch (Exception ex)
                {

                    Jobs.Base.Utils.Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }

            }

            return flag;
        }

        /// <summary>
        /// 将连接日志写入到日志表中
        /// </summary>
        /// <param name="conn_id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool Insert_STATUSLOG(int conn_id, List<WTD_BF_STATUSLOG> list)
        {
            bool flag = false;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.BeginTran();
                    iDataBase.InsertRange(list);
                    iDataBase.CommitTran();
                    flag = true;
                    list = null;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }
            }
            return flag;


        }


        /// <summary>
        /// 获取指定表的列名。
        /// </summary>
        /// <param name="conn_id"></param>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public static DataSet Get_Interval_AVG(int conn_id)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt_colname = null;
                string table_colname = "";
                foreach (WTD_GROUP_CONFIG item in Globals.Wtd_Group_Config)
                {   
                    //表名称
                    string table_name = string.Format("WTD_HF{0}_01H", item.GROUPNO);

                    //获取指定表的列名
                    string sql = string.Format(@"SELECT COLUMN_NAME
                              FROM USER_TAB_COLUMNS
                             WHERE TABLE_NAME = '{0}'
                               AND COLUMN_NAME NOT LIKE('%TIMESTAMP%')
                               AND COLUMN_NAME NOT LIKE('%AVG%') ", table_name);                  
                    dt_colname = Getdate_datatable(conn_id, table_name, sql);
     
                    //拼接列名
                    if (BFES.Utils.IsWhatExtensions.ISNotNULL_DataTable(dt_colname))
                    {
                        foreach (DataRow row in dt_colname.Rows)
                        {           
                            table_colname += string.Format(" ROUND(NVL(AVG(DECODE(SIGN({0}), 1, {0}, null)),0),2){0},", row[0].ToString());
                        }
                    }
                    //全部列名，去掉最后的逗号
                    if (!string.IsNullOrEmpty(table_colname))
                    {
                        table_colname = table_colname.Remove(table_colname.Length - 1, 1);
                    }

                    //获取指定时间的均值 MIN(TIMESTAMP),
                   sql = string.Format(@"SELECT {0} FROM {1} T
                     WHERE T.TIMESTAMP > (SELECT MAX(TIMESTAMP) - {2}/24 FROM {1})", table_colname, table_name, Globals.Parameters.AVG_INTERVAL_HOUR);
                  
                    ds.Tables.Add(Getdate_datatable(conn_id, table_name, sql));
                    //释放已用过资源
                    table_colname = "";
                    dt_colname.Clear();
                }
                dt_colname = null;
                return ds;
 
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(DataCollection).Name);
            }
            return null;
        }


        /// <summary>
        /// 将连接日志写入到日志表中
        /// </summary>
        /// <param name="conn_id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool Insert_WTD_AVG_ALARM(int conn_id, List<WTD_AVG_ALARM> list)
        {
            bool flag = false;
            DataBaseType databasetype = new DataBaseType();
            using (IDataBase iDataBase = BFES.DataAccess.DataBaseFactory.GreateDataBaseFactory(Get_Connect_String(ref databasetype, conn_id), databasetype))
            {
                try
                {
                    iDataBase.BeginTran();
                    iDataBase.InsertRange(list);
                    iDataBase.CommitTran();
                    flag = true;
                    list = null;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    Log.WriteLine(ex.Message, typeof(DataCollection).Name);
                }
            }
            return flag;


        }
    }
}

