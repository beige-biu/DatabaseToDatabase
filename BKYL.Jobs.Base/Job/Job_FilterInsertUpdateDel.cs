using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;
using BKYL.Jobs.Base.Model;

namespace BKYL.Jobs.Base.Plugin
{
    public class Job_FilterInsertUpdateDel : BaseJob
    {
        public override void Dispose()
        {
        }
        public override void InitTask()
        {
        }       
        public override void RunTask(DateTime currentTime)
        {
            int deleteNum = 0;
            int upNum = 0;
            DataTable dtS = null;
            DataTable dtT = null;
            object S_Max;
            string sql = null;
            string tempDele = "";
            string deleteSql = "";
            string upSql = "";
            string log = "";
            bool issuccess = false;
            bool mulPriKey = false;
            DateTime minTime = currentTime.AddSeconds(0 - tableConfig.DelectCycle);
            try
            {
                #region 判断是否支持时间字段查询               
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        sql = string.Format("select max({0}) from {1} alias", tableConfig.S_TableSequential, tableConfig.S_TableName);
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
                if (S_Max.GetType() == typeof(decimal))
                {
                    throw new Exception("配置类型2时间时序表时间字段必须为时间字段！");
                }

                #endregion               
                #region 查询数据
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        sql = string.Format(tableConfig.SelectSql + " where {0}>={1}", tableConfig.S_TableSequential
                , DataBaseFactory.ConvertTimeString(minTime.AddSeconds(1), S_DBSource.DBType));
                        if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                        {
                            sql += " and " + tableConfig.S_Filter;
                        }
                        dtS = iDataBase.GetDataTable(sql);
                        //转换中文乱码
                        dtS = RemoveDump(dtS);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != null && ex.Message.Contains("*&*&"))
                        {
                            throw ex;
                        }
                        else
                        {
                            throw new Exception(sql + "语句错误！" + ex.Message + ex.StackTrace);
                        }
                    }
                }
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {                        
                        sql = GetSelectSql_T(tableConfig);
                        sql = string.Format(sql + " where {0}>={1}", tableConfig.T_TableSequential
               , DataBaseFactory.ConvertTimeString(minTime.AddSeconds(1), T_DBSource.DBType));
                        if (!string.IsNullOrEmpty(tableConfig.T_DeleteFilter))
                        {
                            sql += " and " + tableConfig.T_DeleteFilter;
                        }
                        dtT = iDataBase.GetDataTable(sql);
                        dtT = RemoveDump(dtT);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != null && ex.Message.Contains("*&*&"))
                        {
                            throw ex;
                        }
                        else
                        {
                            throw new Exception(sql + "语句错误！" + ex.Message + ex.StackTrace);
                        }
                    }
                }
                #endregion
                #region 判断多主键
                string[] strs = tableConfig.S_TablePrimaryKey.Split(',');
                if (strs.Length > 1)
                {
                    mulPriKey = true;
                }
                #endregion
                #region 比较datatable      
                DataTable dtUp = dtS.Clone();     //更新集合表         
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {
                        for (int i = dtS.Rows.Count - 1; i >= 0; i--)
                        {
                            for (int j = dtT.Rows.Count - 1; j >= 0; j--)
                            {
                                if (JudgeMulKey(dtS.Rows[i], dtT.Rows[j], strs, mulPriKey))
                                {
                                    if (JudgeCol(dtS.Rows[i], dtT.Rows[j]))            //主键相等再看各个字段是否相等 默认相等
                                    {
                                        dtT.Rows.RemoveAt(j);           //俩个表中删除了从而获得插入个数和删除个数
                                        dtS.Rows.RemoveAt(i);
                                        break;
                                    }
                                    else
                                    {
                                        dtUp.Rows.Add(dtS.Rows[i].ItemArray);
                                        dtS.Rows.RemoveAt(i);
                                        dtT.Rows.RemoveAt(j);
                                        break;
                                    }
                                }
                            }
                        }
                        //删除daT   
                        if (dtT != null && dtT.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtT.Rows)
                            {
                                GenDelSql(item, mulPriKey, strs, ref tempDele);
                            }
                            tempDele = tempDele.Remove(tempDele.Length - 2, 2);
                            deleteSql = string.Format("Delete from {0} where ", tableConfig.T_TableName);
                            deleteSql += tempDele;
                            if (!string.IsNullOrEmpty(tableConfig.T_DeleteFilter))
                            {
                                deleteSql += " and " + tableConfig.T_DeleteFilter;
                            }
                        }
                        //更新语句
                        if (dtUp != null && dtUp.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtUp.Rows)
                            {
                                string fieldStr = "";
                                string priStr = "";
                                if (Columns != null && Columns.Length > 0)
                                {
                                    List<string> temstrs = Columns.ToList();
                                    for (int i = 0; i < strs.Length; i++)
                                    {
                                        temstrs.Remove(strs[i]);
                                    }
                                    foreach (string col in temstrs)
                                    {
                                        if (item[col] is DateTime)
                                        {
                                            fieldStr += col + "=" + DataBaseFactory.ConvertTimeString((DateTime)item[col], T_DBSource.DBType);      //拼接字段和字段值
                                            fieldStr += ",";
                                        }
                                        else
                                        {
                                            fieldStr += col + "=" + "'" + item[col] + "'";      //拼接字段和字段值
                                            fieldStr += ",";
                                        }
                                    }
                                }
                                string upSingle = string.Format("Update {0} set ", tableConfig.T_TableName);
                                fieldStr = fieldStr.Remove(fieldStr.Length - 1);
                                GenUpSql(item, mulPriKey, strs, ref priStr);
                                upSingle += (fieldStr + priStr);
                                if (!string.IsNullOrEmpty(tableConfig.T_DeleteFilter))
                                {
                                    upSingle += " and " + tableConfig.T_DeleteFilter;
                                }
                                upSingle += ";";
                                upSql += upSingle;
                            }
                            upSql = "begin " + upSql + "end;";
                        }
                        //语句执行区
                        iDataBase.BeginTran();
                        if (dtT != null && dtT.Rows.Count > 0)
                        {
                            deleteNum = iDataBase.ExecuteCommand(deleteSql);
                        }
                        if (dtUp != null && dtUp.Rows.Count > 0)
                        {
                            upNum = iDataBase.ExecuteCommand(upSql);
                        }
                        //插入OK                       
                        if (dtS != null && dtS.Rows.Count > 0)
                        {
                            issuccess = iDataBase.BulkInsert(tableConfig.T_TableName, Columns, Columns, dtS, tableConfig.IsExistTri);
                        }
                        else
                        {
                            issuccess = true;
                            log += string.Format("源数据{0}表无新数据!\r\n", tableConfig.S_TableName);
                        }
                        iDataBase.CommitTran();
                        log += string.Format("{0}表插入成功 {1}条!", tableConfig.T_TableName, dtS == null ? 0 : dtS.Rows.Count);
                        log += string.Format("{0}表删除成功 {1}条!", tableConfig.T_TableName, deleteNum);
                        log += string.Format("{0}表更新成功 {1}条!", tableConfig.T_TableName, dtUp.Rows.Count);
                        UpdateFlag(iDataBase, issuccess, tableConfig.UpdateSql, ref log);
                        Outputlog(log);
                    }
                    catch (Exception ex)
                    {
                        iDataBase.RollbackTran();
                        issuccess = false;
                        throw ex;
                    }
                    finally
                    {
                        if (dtUp != null)
                        {
                            dtUp.Clear();
                            dtUp.Dispose();
                            dtUp = null;
                        }
                    }
                }
                #endregion   
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                if (dtS != null)
                {
                    dtS.Clear();
                    dtS.Dispose();
                    dtS = null;
                }
                if (dtT != null)
                {
                    dtT.Clear();
                    dtT.Dispose();
                    dtT = null;
                }
                sql = null;
                tempDele = null;
                deleteSql = null;
                upSql = null;
                log = null;
                S_Max = null;
            }
        }
        /// <summary>
        /// 拼接查询语句
        /// </summary>
        /// <param name="tableConfig"></param>
        /// <returns></returns>
        private string GetSelectSql_T(TableConfig tableConfig)
        {
            string T_DBField = "";
            string selectSql = "SELECT {0} FROM {1} alias ";
            string Fields = "";
            foreach (ColumnConfig item in tableConfig.ColumnConfigList)
            {
                T_DBField = item.T_DBField;
                Fields += T_DBField + " as " + item.T_DBField + " ,";
            }
            if (Fields.Length > 0)
                Fields = Fields.Remove(Fields.Length - 1, 1);
            return string.Format(selectSql, Fields, tableConfig.T_TableName);
        }
        /// <summary>
        /// 主键一一对比
        /// </summary>
        /// <param name="dtS"></param>
        /// <param name="dtT"></param>
        /// <param name="strs"></param>
        /// <param name="isMulKey"></param>
        /// <returns></returns>
        private bool JudgeMulKey(DataRow dtS, DataRow dtT, string[] strs, bool isMulKey)
        {
            bool isOk = false;
            if (isMulKey)
            {
                for (int k = 0; k < strs.Length; k++)
                {
                    if (dtS[strs[k]].ToString() == dtT[strs[k]].ToString())
                    {
                        isOk = true;
                    }
                    else
                    {
                        isOk = false;
                        return isOk;
                    }
                }
            }
            else
            {
                if (dtS[tableConfig.S_TablePrimaryKey].ToString() == dtT[tableConfig.T_TablePrimaryKey].ToString())
                {
                    isOk = true;
                    return isOk;
                }
            }
            return isOk;
        }
        /// <summary>
        /// 删除SQL 判断多主键 是否时间格式
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isMulKey"></param>
        /// <param name="strs"></param>
        /// <param name="tempDele"></param>
        private void GenDelSql(DataRow item, bool isMulKey, string[] strs, ref string tempDele)
        {
            if (isMulKey)
            {
                string temp = "( ";
                for (int i = 0; i < strs.Length; i++)
                {
                    if (item[strs[i]] is DateTime)
                    {
                        temp += strs[i] + " = " + DataBaseFactory.ConvertTimeString(Convert.ToDateTime(item[strs[i]]), T_DBSource.DBType);
                        temp += " and ";
                    }
                    else
                    {
                        temp += strs[i] + " = " + "'" + item[strs[i]] + "'";
                        temp += " and ";
                    }
                }
                temp = temp.Remove(temp.Length - 3, 3);
                temp += ") or";
                tempDele += temp;
            }
            else
            {
                if (item[tableConfig.T_TablePrimaryKey] is DateTime)
                {
                    tempDele += "(" + tableConfig.T_TablePrimaryKey + " = " + DataBaseFactory.ConvertTimeString((DateTime)item[tableConfig.T_TablePrimaryKey], T_DBSource.DBType) + ") or";
                }
                else
                {
                    tempDele += "(" + tableConfig.T_TablePrimaryKey + " = " + "'" + item[tableConfig.T_TablePrimaryKey] + "'" + ") or";
                }
            }
        }
        /// <summary>
        /// 更新SQL 判断多主键 是否时间格式
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isMulKey"></param>
        /// <param name="strs"></param>
        /// <param name="tempDele"></param>
        private void GenUpSql(DataRow item, bool isMulKey, string[] strs, ref string priStr)
        {
            if (isMulKey)
            {
                string str = "where ";
                for (int i = 0; i < strs.Length; i++)
                {
                    if (item[strs[i]] is DateTime)
                    {
                        str += strs[i] + " = " + DataBaseFactory.ConvertTimeString(Convert.ToDateTime(item[strs[i]]), T_DBSource.DBType);
                        str += " and ";
                    }
                    else
                    {
                        str += strs[i] + " = " + "'" + item[strs[i]] + "'";
                        str += " and ";
                    }
                }
                str = str.Remove(str.Length - 4, 4);
                priStr = str;
            }
            else
            {
                if (item[tableConfig.T_TablePrimaryKey] is DateTime)
                {
                    priStr = " where " + tableConfig.T_TablePrimaryKey + "=" + DataBaseFactory.ConvertTimeString((DateTime)item[tableConfig.T_TablePrimaryKey], T_DBSource.DBType);
                }
                else
                {
                    priStr = " where " + tableConfig.T_TablePrimaryKey + " = " + "'" + item[tableConfig.T_TablePrimaryKey] + "'";
                }
            }
        }        
        private bool JudgeCol(DataRow dtrS, DataRow dtrT)
        {
            bool isOk = false;
            int len = dtrS.ItemArray.Length;
            for (int i = 0; i < len; i++)
            {
                if (dtrS[i].ToString() == dtrT[i].ToString())
                {
                    isOk = true;
                }
                else
                {
                    isOk = false;
                    return isOk;
                }
            }
            return isOk;
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("任务进程(" + currentTime.ToString("yyyy-MM-dd :HH:mm:ss") + ")：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
