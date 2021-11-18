using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;
using System;
using System.Data;
namespace BKYL.Jobs.Base.Plugin
{
    public class Job_FullCopy :BaseJob
    {
        public override void Dispose()
        {

        }


        public override void InitTask()
        {

        }

        public override void RunTask(DateTime currentTime)
        {
            DataTable dt = null;
            string log = null;
            try
            {
                int deletenum = 0;
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    string sql = null;
                    try
                    {
                        sql = tableConfig.SelectSql;
                        if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                        {
                            sql += " where " + tableConfig.S_Filter;
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

                bool issuccess = false;
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {
                        iDataBase.BeginTran();
                        string Deletesql = string.Format("Delete from {0}", tableConfig.T_TableName);
                        if (!string.IsNullOrEmpty(tableConfig.T_DeleteFilter))
                        {
                            Deletesql += " where " + tableConfig.T_DeleteFilter;
                        }
                        deletenum = iDataBase.ExecuteCommand(Deletesql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            issuccess = iDataBase.BulkInsert(tableConfig.T_TableName, Columns, Columns, dt,tableConfig.IsExistTri);
                        }
                        else
                        {
                            issuccess = true;
                            log += string.Format("源数据{0}表为空!\r\n", tableConfig.S_TableName);
                        }
                        iDataBase.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        Utils.GlobalObject.RichTextLog.AppendTextByAsync("", System.Drawing.Color.AliceBlue);
                        iDataBase.RollbackTran();
                        issuccess = false;
                        throw ex;
                    }
                    log += string.Format("{0}表删除成功 {1}条!\r\n", tableConfig.T_TableName, deletenum);
                    log += string.Format("{0}表插入成功 {1}条!\r\n", tableConfig.T_TableName, dt == null ? 0 : dt.Rows.Count);
                    UpdateFlag(iDataBase, issuccess, tableConfig.UpdateSql, ref log);
                }
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
