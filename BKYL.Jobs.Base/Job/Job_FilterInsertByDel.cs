using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;

namespace BKYL.Jobs.Base.Plugin
{
    public class Job_FilterInsertByDel : BaseJob
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
                #region 判断是否支持时间字段查询
                object S_Max;
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        string sql = string.Format("select max({0}) from {1} alias", tableConfig.S_TableSequential, tableConfig.S_TableName);
                        if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                        {
                            sql += " where " + tableConfig.S_Filter;                        }
                        S_Max = iDataBase.GetScalar(sql);
                    }
                    catch (Exception ee)
                    {
                        throw ee;
                    }
                }

                if (S_Max.GetType() == typeof(decimal))
                {
                    throw new Exception("配置类型2时间时序表时间字段必须为时间字段！");
                }

                #endregion

                int deletenum = 0;
                DateTime minTime = currentTime.AddSeconds(0 - tableConfig.DelectCycle);
                #region 查询数据
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    string sql = null;
                    try
                    {
                        sql = string.Format(tableConfig.SelectSql + " where {0}>={1}", tableConfig.S_TableSequential
                , DataBaseFactory.ConvertTimeString(minTime.AddSeconds(1), S_DBSource.DBType));
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
                        string Deletesql = string.Format("Delete from {0} where {1}>={2}", tableConfig.T_TableName, tableConfig.T_TableSequential
                    , DataBaseFactory.ConvertTimeString(minTime.AddSeconds(1), T_DBSource.DBType));
                        if (!string.IsNullOrEmpty(tableConfig.T_DeleteFilter))
                        {
                            Deletesql += " and " + tableConfig.T_DeleteFilter;
                        }
                        iDataBase.BeginTran();
                        deletenum = iDataBase.ExecuteCommand(Deletesql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            issuccess = iDataBase.BulkInsert(tableConfig.T_TableName, Columns, Columns, dt, tableConfig.IsExistTri);
                        }
                        else
                        {
                            issuccess = true;
                            log += string.Format("源数据{0}表为空!\r\n", tableConfig.S_TableName);
                        }
                        iDataBase.CommitTran();
                        log += string.Format("{0}表删除成功 {1}条!\r\n", tableConfig.T_TableName, deletenum);
                        log += string.Format("{0}表插入成功 {1}条!", tableConfig.T_TableName, dt == null ? 0 : dt.Rows.Count);
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
