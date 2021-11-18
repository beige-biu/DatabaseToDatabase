using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin
{
    /// <summary>
    /// 信息管理 生产参数评价 赵飞
    /// </summary>
    public class Console_Related : BaseJob
    {
        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    //PPE_SPARE10
                    DataTable M_PEE = iDataBase.GetDataTable("select PPE_SPARE10,TIMESTAMP from M_PPE where PPE_SPARE10='1'");
                    if (M_PEE.Rows.Count > 0)
                    {
                        foreach (DataRow item in M_PEE.Rows)
                        {
                            DateTime TimeStamp = item["TIMESTAMP"].ToDate();
                            //FeO
                            DataTable table1 = GetSourceByConfigParams(TimeStamp,1);
                            List<double> D1 = StrValues(table1);
                            List<M_CONFIG_RELATED> strField1 = StrRELATEDB(1);
                            //转鼓
                            DataTable table2= GetSourceByConfigParams(TimeStamp, 2);
                            List<double> D2 = StrValues(table2);
                            List<M_CONFIG_RELATED> strField2 = StrRELATEDB(2);
                            string updateSql ="update M_PPE set PPE_SPARE10=0,";
                            for (int i = 0; i < D1.Count; i++)
                            {
                                updateSql += strField1[i].FEILDNAME + "=" + nanInfinity(D1[i])+",";
                            }
                            for (int i = 0; i < D2.Count; i++)
                            {
                                updateSql += strField2[i].FEILDNAME + "="+ nanInfinity(D2[i])+",";
                            }
                            updateSql = updateSql.Substring(0, updateSql.Length - 1);
                            updateSql += "where 1=1 and TIMESTAMP="+DataBaseFactory.ConvertTimeString(TimeStamp,T_DBSource.DBType);
                            iDataBase.ExecuteCommand(updateSql);
                        }                  
                    }
                }
                log = "信息管理 生产参数评价执行成功";
                Outputlog(log);

                #region

                #endregion
            }
            catch (Exception ex)
            {
                Log.WriteLine("异常简述" + ex.Message + "\r\n",typeof(Console_Related).Name);
                Log.WriteLine("异常堆栈" + ex.StackTrace + "\r\n", typeof(Console_Related).Name);
                log = string.Format("发生异常，异常信息{0}",ex.Message);
                Outputlog(log);
            }
            finally
            {
                log = null;
            }
        }
        #region
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
        #endregion
        #region
        private List<double> StrValues(DataTable datasource)
        {
            try
            {
                List<double[]> list1 = new List<double[]>();
                for (int i = 0; i < datasource.Columns.Count; i++)
                {
                    double[] D = new double[datasource.Rows.Count];
                    for (int j = 0; j < datasource.Rows.Count; j++)
                    {
                        // D[i] = Convert.ToDouble(datasource.Rows[j][datasource.Columns[i]].ToString());
                        string n = datasource.Rows[j][datasource.Columns[i].ColumnName].ToString();
                        //if (string.IsNullOrEmpty(n)) { n = "0"; }
                        D[j] = Convert.ToDouble(n);
                    }
                    list1.Add(D);
                }
                double[] RELEVANCE_SZ_2 = new double[list1.Count];
                List<double> doubleY = new List<double>();
                doubleY.Clear();
                for (int i = 0; i < list1.Count; i++)
                {                   
                        RELEVANCE_SZ_2[i] = Correlation.Spearman(list1[i], list1[0]);                  
                }
                //如果查询出来数据，就把数据添加到集合中
                if (datasource.Rows.Count > 0)
                {
                    for (int i = 1; i < list1.Count; i++)
                    {
                        doubleY.Add(RELEVANCE_SZ_2[i]);
                    }
                    return doubleY;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine("异常简述" + ex.Message + "\r\n");
                Log.WriteLine("异常堆栈" + ex.StackTrace + "\r\n");
                throw;
            }

        }
        #endregion
        #region
        private List<M_CONFIG_RELATED> StrRELATEDB(int config_id)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                List<M_CONFIG_RELATED> list = iDataBase.GetList<M_CONFIG_RELATED>("select * from M_CONFIG_RELATED").Where(m=>m.ID==config_id).ToList();
                return list;
            }            
        }
        #endregion
        public void RefreshConfig()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    Outputlog("Job_CalFormula：开始更新参数...");
                    Outputlog("Job_CalFormula：更新参数完成！");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public override void InitTask()
        {
            RefreshConfig();
            //从数据库中读取时间范围的信息，天数
        }
        /// <summary>
        /// 通过M_CONFIG_RELATED表参数获取数据源
        /// </summary>
        /// <returns></returns>
        private DataTable GetSourceByConfigParams(DateTime TIMESTAMP,int config_id)
        {
            List<M_CONFIG_RELATED> list = StrRELATEDB(config_id);
            string fieldName = "nvl("+list.First().RELATEDA+",0),";
            for (int i = 0; i < list.Count; i++)
            {
                fieldName += list[i].RELATEDB + ",";
            }
            fieldName = fieldName.Substring(0, fieldName.Length - 1);
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                string sql = "select " + fieldName + " from M_PPE where rownum between 1 and 50 order by timestamp desc";
                return iDataBase.GetDataTable(sql);
            }
        }
        public double nanInfinity(double num)
        {
            if (double.IsNaN(num))
            {
                num = 0;
            }
            if (double.IsInfinity(num))
            {
                num = 0;
            }
            return num;
        }
    }
}
