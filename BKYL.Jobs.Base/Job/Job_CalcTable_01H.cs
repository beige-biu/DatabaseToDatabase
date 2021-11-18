using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BKYL.Jobs.Base.Job
{
    public class Job_CalcTable_01H : BaseJob
    {
        public override void InitTask()
        {
            //throw new NotImplementedException();
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                #region 1.查询数据 
                //1.情况一 正常情况 到点查询 例如三点 进行查询 两点到三点数据 平均值 where 字段 != -9999 或者其他异常数据 然后插入01H表
                //2.情况二 数据不正常状态 
                //2.1 补数据   根据目标库的最大时间进行循环查询插入 查询到最大时间减一小时 的数据的平均值 然后入库
                //2.2 没有数据 查询不到 不插入

                //1.查询目标表的最大时间
                //2.与当前时间做循环 每次增加一小时
                //3.查询数据 
                //4.插入数据

                //时间去除分钟和秒数
                currentTime = currentTime.ToString("yyyy-MM-dd HH:00:00").ToDate();
                object T_Max;
                DateTime T_Max_Date = new DateTime();
                try
                {
                    using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                    {
                        try
                        {
                            T_Max = iDataBase.GetScalar(string.Format("select max({0}) from {1}", tableConfig.T_TableSequential, tableConfig.T_TableName));
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                object S_Max;
                DateTime S_Max_Date = new DateTime();
                try
                {
                    using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                    {
                        try
                        {
                            string sql = string.Format("select max({0}) from {1} where 1=1 ", tableConfig.S_TableSequential, tableConfig.S_TableName);
                            if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                            {
                                sql += " and " + tableConfig.S_Filter;
                            }
                            S_Max = iDataBase.GetScalar(sql);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                #region  类型判断
                if (S_Max.GetType() == typeof(DBNull))
                    return;
                else if (S_Max.GetType() != typeof(DateTime))
                {
                    throw new Exception("目标-时间时序表时间字段 不是时间类型!");
                }
                else
                {
                    S_Max_Date = S_Max.ToDate().ToString("yyyy-MM-dd HH:00:00").ToDate();
                }

                //如果目标表查询不到最大时间，那么去查询原表的最小时间进行补数 如果原表依然查询不到时间那么直接返回
                if (T_Max.GetType() == typeof(DBNull))
                {
                    using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                    {
                        try
                        {
                            string sql = string.Format("select min({0}) from {1} where 1=1 ", tableConfig.S_TableSequential, tableConfig.S_TableName);
                            if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                            {
                                sql += " and " + tableConfig.S_Filter;
                            }
                            T_Max = iDataBase.GetScalar(sql);

                            if (T_Max.GetType() == typeof(DBNull))
                                return;
                            else if (T_Max.GetType() != typeof(DateTime))
                            {
                                throw new Exception("目标-时间时序表时间字段 不是时间类型!");
                            }

                            DateTime tempMax = T_Max.ToDate().ToString("yyyy-MM-dd HH:00:00").ToDate();

                            int tempYS = (24 - tempMax.Hour) % tableConfig.HourCycle;
                            T_Max = tempMax.AddHours(tempYS);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                if (T_Max.GetType() == typeof(DBNull))
                    return;
                else if (T_Max.GetType() != typeof(DateTime))
                {
                    throw new Exception("目标-时间时序表时间字段 不是时间类型!");
                }
                else
                {
                    //增加目标表最大的时间一小时进行查询
                    T_Max_Date = T_Max.ToDate().AddHours(tableConfig.HourCycle);
                    //格式化时间
                    T_Max_Date = T_Max_Date.ToString("yyyy-MM-dd HH:00:00").ToDate();
                }
                #endregion
                string Selsql = "select {0} from {1} ailas ";
                string Fields = "";
                //做平均值的列不包含时间列
                for (int i = 0; i < Columns.Length; i++)
                {
                    Fields += Columns[i] + ",";
                }
                Fields = Fields.Substring(0, Fields.Length - 1);
                //拼接字段表名及排除异常的数据的条件
                Selsql = string.Format(Selsql, Fields, tableConfig.S_TableName);
                DataTable Data = new DataTable();
                int insertnum = 0;
                //如果缺失数据进行补数
                while (true)
                {
                    if (T_Max_Date > S_Max_Date)
                    {
                        break;
                    }
                    else
                    { 
                        if (insertnum == 0)
                        {
                            Outputlog(string.Format("任务进程：{0}->{1} 启动补数job，时间可能会较长。", tableConfig.FolderName, tableConfig.FileName));
                        }
                        else if (insertnum % 30 == 0)
                        {
                            Outputlog(string.Format("任务进程：{0}->{1} 补数次数{2}，时间可能会较长。", tableConfig.FolderName, tableConfig.FileName, insertnum));
                        }
                    }
                    //时间区间的拼接
                    string timeSection = string.Format(" where {0}>{1} and {0}<={2} "
                         , tableConfig.S_TableSequential
                         , DataBaseFactory.ConvertTimeString(T_Max_Date.AddHours(0 - tableConfig.HourCycle), S_DBSource.DBType)
                         , DataBaseFactory.ConvertTimeString(T_Max_Date, S_DBSource.DBType));
                    //查询数据
                    Data = SelectData(Selsql + timeSection);

                    if (Data != null && Data.Rows.Count > 0)
                    {
                        if (tableConfig.S_Insert01HType.Trim().ToUpper() == "STD")
                        {
                            Data = Data_STD(Data);
                        }
                        else if (tableConfig.S_Insert01HType.Trim().ToUpper() == "AVG")
                        {
                            Data = Data_Avg(Data);
                        }
                        else
                        {
                            throw new Exception(string.Format("不支持的计算方式{0}", tableConfig.S_Insert01HType));
                        }

                        if (Data != null && Data.Rows.Count > 0)
                        {
                            //插入数据
                            InsertData(Data, T_Max_Date);
                            //Outputlog(string.Format("时间戳：{0},计算{1}平均值 ,插入成功！", T_Max_Date, tableConfig.HourCycle));
                            insertnum++;
                        }
                    }
                    Data.Dispose();
                    Data = null;

                    T_Max_Date = T_Max_Date.AddHours(tableConfig.HourCycle);
                }
                if (insertnum > 0)
                {
                    log += string.Format("{0}表插入成功 {1}条!", tableConfig.T_TableName, insertnum);
                    //界面输出
                    Outputlog(log);
                }
                #endregion
            }
            catch (Exception ee)
            {

                throw ee;
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="selectDataSql"></param>
        /// <returns></returns>
        private DataTable SelectData(string selectDataSql)
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    string sql = selectDataSql;
                    if (!string.IsNullOrEmpty(tableConfig.S_Filter))
                    {
                        sql += " and " + tableConfig.S_Filter;
                    }
                    return iDataBase.GetDataTable(sql);
                }
            }
            catch (Exception ee)
            {
                throw new Exception("查询数据Error:" + ee.Message + ee.StackTrace);
            }

        }
        /// <summary>
        /// 数据做平均值
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private DataTable Data_Avg(DataTable data)
        {
            string filterString = null;
            try
            {
                DataTable returndt = new DataTable();
                object[] obj = new object[Columns.Length];
                DataColumn[] datacolums = new DataColumn[Columns.Length];
                for (int i = 0; i < Columns.Length; i++)
                {
                    filterString = tableConfig.ColumnConfigList[i].FilterString;
                    obj[i] = data.Compute(tableConfig.S_Insert01HType + "(" + Columns[i] + ")", filterString);
                    datacolums[i] = new DataColumn(Columns[i], typeof(double));
                }
                returndt.Columns.AddRange(datacolums);
                returndt.Rows.Add(obj);

                return returndt;
            }
            catch (Exception ee)
            {
                throw new Exception(string.Format("数据平均值(过滤表达式：{0} )Error:{1} \n{2}", filterString, ee.Message, ee.StackTrace));
            }
        }

        private DataTable Data_STD(DataTable data)
        {
            try
            {
                DataTable returndt = new DataTable();
                object[] obj = new object[Columns.Length];
                List<Double> valList = new List<Double>();
                DataColumn[] datacolums = new DataColumn[Columns.Length];
                for (int i = 0; i < Columns.Length; i++)
                {
                    datacolums[i] = new DataColumn(Columns[i], typeof(double));
                    valList.Clear();
                    for (int j = 0; j < data.Rows.Count; j++)
                    {
                        valList.Add(data.Rows[j][Columns[i]].ToDouble());
                    }
                    obj[i] = CalStdev(valList);
                }
                returndt.Columns.AddRange(datacolums);
                returndt.Rows.Add(obj);
                valList.Clear();
                valList = null;
                return returndt;
            }
            catch (Exception ee)
            {
                throw new Exception(string.Format("数据STD值 Error:{0} \n{1}", ee.Message, ee.StackTrace));
            }
        }
        private Double CalStdev(List<Double> Data)
        {
            Double Temp = 0;
            Double Avg = Data.Average().ToDouble();
            for (int i = 0; i < Data.Count; i++)
            {
                Temp += Math.Pow((Avg - Data[i]), 2);
            }
            return Math.Pow(Temp / Data.Count, 0.5);
        }

        private void InsertData(DataTable dt, DateTime Inserttime)
        {
            string log = "";
            try
            {
                string InsertSql = "insert into {0}({1}) values({2})";
                string Fields = tableConfig.T_TableSequential + ",";
                string values = DataBaseFactory.ConvertTimeString(Inserttime, T_DBSource.DBType) + ",";

                for (int i = 0; i < Columns.Length; i++)
                {
                    Fields += Columns[i] + ",";
                    values += "'" + dt.Rows[0][Columns[i]] + "',";
                }
                Fields = Fields.Substring(0, Fields.Length - 1);
                values = values.Substring(0, values.Length - 1);
                InsertSql = string.Format(InsertSql, tableConfig.T_TableName, Fields, values);
                #region 插入数据
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    try
                    {
                        iDataBase.ExecuteCommand(InsertSql);
                        UpdateFlag(iDataBase, true, tableConfig.UpdateSql, ref log);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                #endregion
            }
            catch (Exception ee)
            {
                throw new Exception("查询插入Error:" + ee.Message + ee.StackTrace);
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);

        }
    }
}
