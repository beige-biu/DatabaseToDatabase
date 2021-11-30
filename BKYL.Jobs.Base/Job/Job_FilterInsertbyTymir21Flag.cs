using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace BKYL.Jobs.Base.Plugin
{
    public class Job_FilterInsertbyTymir21Flag : BaseJob
    {

        public override void Dispose()
        {

        }

        public override void InitTask()
        {

        }

        public override void RunTask(DateTime currentTime)
        {

            string log = "";
            DataTable dt = null;
            try
            {
                object S_Max;
                object T_Max;
                decimal S_Max_Int = 0;
                decimal T_Max_Int = 0;
                DateTime S_Max_Date = new DateTime();
                DateTime T_Max_Date = new DateTime();
                bool IsDateTimeType = true;
                #region 查询最大时间
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    //获取当前时间
                    S_Max = DateTime.Now.ToString().ToDate();


                }
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {

                        //获取前6个月时间
                        T_Max = DateTime.Now.AddDays(-180).Date.ToString();


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                #endregion

                #region 类型判断
                Type S_Maxtype = S_Max.GetType();

                if (S_Maxtype == typeof(DateTime))
                {
                    S_Max_Date = S_Max.ToDate();
                    T_Max_Date = T_Max.ToDate();
                    if (S_Max_Date <= T_Max_Date)
                    {
                        return;
                    }
                }
                else if (S_Maxtype == typeof(System.Decimal) || S_Maxtype == typeof(System.Double) || S_Maxtype == typeof(System.Int32) || S_Maxtype == typeof(int) || S_Maxtype == typeof(System.Single) || S_Maxtype == typeof(float))
                {
                    S_Max_Int = S_Max.ToDecimal();
                    T_Max_Int = T_Max.ToDecimal();
                    if (S_Max_Int <= T_Max_Int)
                    {
                        return;
                    }
                    IsDateTimeType = false;
                }
                else if (S_Maxtype == typeof(System.String))
                {
                    throw new Exception("源-时间时序表时间字段是字符串类型，如果是时间请转换时间类型！！" + S_Maxtype.Name + " 值Value:" + S_Max);
                }
                else if (S_Maxtype == typeof(DBNull))
                {
                    return;
                }

                else
                {
                    throw new Exception("缺失最大值匹配的类型！！" + S_Maxtype.Name + " 值Value:" + S_Max);
                }
                #endregion

                #region 查询数据
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    string sql = null;
                    try
                    {
                        if (IsDateTimeType)
                        {
                            sql = string.Format(tableConfig.SelectSql + " where {0}>={1} and {0}<{2} ", tableConfig.S_TableSequential
                         , DataBaseFactory.ConvertTimeString(T_Max_Date.AddSeconds(1), S_DBSource.DBType), DataBaseFactory.ConvertTimeString(S_Max_Date.AddSeconds(1), S_DBSource.DBType));
                        }
                        else
                        {
                            sql = string.Format(tableConfig.SelectSql + " where {0}>{1} and {0}<={2} ", tableConfig.S_TableSequential
                        , T_Max_Int, S_Max_Int);
                        }
                        if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                        {
                            sql += " and " + tableConfig.S_Filter;
                        }
                        dt = iDataBase.GetDataTable(sql);
                        //转换中文乱码
                        dt = RemoveDump(dt);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != null && ex.Message.Contains("*&*&"))
                        {
                            throw ex;
                        }
                        else
                        {
                            throw new Exception(sql + "语句错误！");
                        }
                    }
                }

                #endregion

                //判断一下dt是否为空，如果不为空，则插入到目标表
                if (dt.Rows.Count < 1)
                {
                    dt.Clear();
                    dt.Dispose();
                    dt = null;


                }

                #region 插入数据
                else
                {

                    //获取连接串,3200高炉
                    //string constr = "User Id = ORAL2DB_BF; Password=BF_IRON2;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.8.215)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = oraltmes)))";
                    string constr = "User Id = ORALTL2_ST; Password=oraltl2_st;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 127.0.0.1)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = ORCL)))";
                    //获取连接串，l2bf_com
                    //string conl2bf_com = "User Id = l2bf_com; Password=l2;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.8.16)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = oraltl2)))";
                    string conl2bf_com = "User Id = ORALTL2_ST; Password=oraltl2_st;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 127.0.0.1)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = ORCL)))";
                    //循环获取dt[erp_code]和dt[erp_name]得值
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {

                        string erp_code = dt.Rows[i]["erp_code"].ToString();
                        string erp_name = dt.Rows[i]["erp_name"].ToString();
                        string sql = "insert into FL2_TB_BAS_MATERIAL(erp_code,erp_name,fac_code,fac_name) values('" + erp_code + "','" + erp_name + "','" + erp_code + "','" + erp_name + "'" + ")";
                        ExcuteSQL1(constr, sql);
                        string sql2 = "insert into T_BASE_MATERIAL_NEW(erp_code,erp_name,fac_code,fac_name) values('" + erp_code + "','" + erp_name + "','" + erp_code + "','" + erp_name + "'" + ")";
                        ExcuteSQL1(conl2bf_com, sql2);
                    }
                    log += string.Format("{0}表插入成功 {1}条!", tableConfig.T_TableName, dt.Rows.Count);


                    #endregion
                    //置标志位

                    using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                    {

                        string sql = null;

                        try
                        {
                            if (IsDateTimeType)
                            {
                                sql = string.Format("update {0} ", tableConfig.S_TableName);
                                sql += string.Format(tableConfig.UpdateSql + " set FROM_N_PROC_UNIT='1' where {0}>={1} and {0}<{2}  ", tableConfig.S_TableSequential
                             , DataBaseFactory.ConvertTimeString(T_Max_Date.AddSeconds(1), S_DBSource.DBType), DataBaseFactory.ConvertTimeString(S_Max_Date.AddSeconds(1), S_DBSource.DBType));
                            }
                            else
                            {
                                sql = string.Format("update {0} ", tableConfig.S_TableName);
                                sql = string.Format(tableConfig.UpdateSql + " set FROM_N_PROC_UNIT='1' where {0}>{1} and {0}<={2}  ", tableConfig.S_TableSequential
                            , T_Max_Int, S_Max_Int);
                            }
                            if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                            {
                                sql += " and " + tableConfig.S_Filter;
                            }

                            iDataBase.ExecuteCommand(sql);


                        }
                        catch (Exception ex)
                        {
                            if (ex.Message != null && ex.Message.Contains("*&*&"))
                            {
                                throw ex;
                            }
                            else
                            {
                                throw new Exception(sql + "语句错误！");
                            }
                        }
                    }

                    Outputlog(log);
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Clear();
                    dt.Dispose();
                    dt = null;
                }
                log = null;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("任务进程(" + currentTime.ToString("yyyy-MM-dd :HH:mm:ss") + ")：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }


        //添加的执行连接字符串的方法
        public static int ExcuteSQL1(string strUDL, string SQL)
        {
            //防止插入sql语句中文乱码
            System.Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            int i_ES = 0;
            OracleConnection SqlConnection = null;
            OracleCommand SqlServerCommand = null;
            try
            {
                SqlConnection = new OracleConnection(strUDL);
                SqlConnection.Open();
                SqlServerCommand = SqlConnection.CreateCommand();
                SqlServerCommand.CommandType = CommandType.Text;
                SqlServerCommand.CommandText = SQL;
                SqlServerCommand.ExecuteNonQuery();
            }
            catch (Exception ExFail)
            {
                i_ES = 1;
            }
            finally
            {
                if (SqlConnection.State.ToString() == "Open")
                {
                    if (i_ES != 1) { i_ES = 2; }
                    SqlConnection.Close();
                }
            }
            return i_ES;
        }


    }
}

