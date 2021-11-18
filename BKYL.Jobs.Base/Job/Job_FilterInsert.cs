using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;
using System;
using System.Data;

namespace BKYL.Jobs.Base.Plugin
{
    public class Job_FilterInsert : BaseJob
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
                    try
                    {
                        string sql = string.Format("select max({0}) from {1} alias", tableConfig.S_TableSequential, tableConfig.S_TableName);
                        if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                        {
                            sql += " where " + tableConfig.S_Filter;
                        }
                        S_Max = iDataBase.GetScalar(sql);
                    }
                    catch (Exception ee)
                    {
                        throw ee;
                    }
                }
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {
                        T_Max = iDataBase.GetScalar(string.Format("select max({0}) from {1} ", tableConfig.T_TableSequential, tableConfig.T_TableName));
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

                #region 插入数据
                bool issuccess = false;
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {
                        iDataBase.BeginTran();
                        issuccess = iDataBase.BulkInsert(tableConfig.T_TableName, Columns, Columns, dt, tableConfig.IsExistTri);
                        iDataBase.CommitTran();
                        log += string.Format("{0}表插入成功 {1}条!", tableConfig.T_TableName, dt.Rows.Count);
                        UpdateFlag(iDataBase, issuccess, tableConfig.UpdateSql, ref log);
                    }
                    catch (Exception ex)
                    {
                        iDataBase.RollbackTran();
                        issuccess = false;
                        throw ex;
                    }
                }
                #endregion

                Outputlog(log);
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
    }
}

