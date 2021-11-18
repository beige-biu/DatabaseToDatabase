using BFES.DataAccess;
using BFES.Models;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base.Utils;
using BKYL.Jobs.Base;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKYL.BFES.Plugin
{
    public class CalAnaData_AG : BaseJob
    {
        List<CONFIG_BFANADATA> ConfigListModel = null;
        string log = "";
        List<string> ColumnArry = new List<string>();
        List<string> ValueArry = new List<string>();
        Dictionary<string, string> dicName = new Dictionary<string, string>();
        public override void InitTask()
        {

        }

        public override void RunTask(DateTime currentTime)
        {
            if (dicName.Count > 0)
                dicName.Clear();
            if (ColumnArry.Count > 0)
                ColumnArry.Clear();
            if (ValueArry.Count > 0)
                ValueArry.Clear();
            GetCONFIG_BFANADATAList();
            if (ConfigListModel == null || ConfigListModel.Count == 0)
            {
                return;
            }
            ColumnArry = ConfigListModel.Select(k => k.TFIELDNAME).ToList();
            ValueArry = ConfigListModel.Select(k => k.TFIELDNAME).ToList();
            ColumnArry.Add("TIMESTAMP");
            ValueArry.Add("TIMESTAMP");
            foreach (var item in ConfigListModel)
            {
                dicName.Add(item.TFIELDNAME, item.TFIELDNAME);
            }
            List<Models.AG.BF_ANADATA_08H> ResultList = new List<Models.AG.BF_ANADATA_08H>();
            DateTime TodayTime = currentTime.ToString("yyyy-MM-dd").ToDate().AddDays(-0);
            int DealDayNumb = 1;
            List<CONFIG_BFANADATA> ListModel = ConfigListModel.FindAll(match => match.DEALTYPE == 0 || match.DEALTYPE == 1);

            #region 处理数据
            for (int i = 0; i <= DealDayNumb; i++)
            {
                for (int j = 0; j < 3; j++) //每8小时计算一次
                {
                    try
                    {
                        DateTime DataEndTime = TodayTime.AddDays(-i).AddHours(j * 8);
                        Models.AG.BF_ANADATA_08H bf_ana = null;
                        bf_ana = new Models.AG.BF_ANADATA_08H();
                        bf_ana = Cal_BFANADATA(ListModel, DataEndTime);
                        if (bf_ana != null)
                        {
                            ResultList.Add(bf_ana);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            #endregion
            #region 插入数据
            if (ResultList != null && ResultList.Count > 0)
            {
                try
                {
                    using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                    {
                        DateTime endTime = ResultList.Max(m => m.TIMESTAMP);
                        DateTime startTime = ResultList.Min(m => m.TIMESTAMP);
                        int delete = 0;
                        idatabase.BeginTran();
                        delete = idatabase.ExecuteCommand($"delete from BF_ANADATA_08H where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)}");
                        log = $"BF_ANADATA_08H成功删除{delete}条记录\r\n";
                        idatabase.BulkInsert<Models.AG.BF_ANADATA_08H>("BF_ANADATA_08H", ColumnArry.ToArray(), ValueArry.ToArray(), ResultList);
                        log += $"BF_ANADATA_08H成功插入{ResultList.Count}条记录\r\n";
                        idatabase.CommitTran();
                        Outputlog(log);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ConfigListModel.Clear(); ConfigListModel = null;
                    ResultList.Clear(); ResultList = null;
                }
            }
            #endregion
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        /// <summary>
        /// 拼接数据源SQL
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ShortDate">数据开始时间，当日短型日期</param>
        /// <returns></returns>
        private string GetSqlByModel(CONFIG_BFANADATA model, DateTime endTime)
        {
            //数据结束日期
            DateTime startTime = endTime.AddHours(-8);

            string SQL = "";

            SQL = $"select {model.FIELDNAME} from {model.TABLENAME} where 1=1 and  {model.TIMESQL} ";
            if (!string.IsNullOrEmpty(model.FILTERSQL) && !string.IsNullOrWhiteSpace(model.FILTERSQL))
            {
                SQL += $"and {model.FILTERSQL}";
            }
            SQL = SQL.Replace("<%STARTTIME%>", DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle));
            SQL = SQL.Replace("<%ENDTIME%>", DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle));

            return SQL;

        }
        private Double CalValue(List<double> Data, CalType calType)
        {
            if (Data.Count > 0)
            {
                switch (calType)
                {
                    case CalType.Avg:
                        return Data.Average();
                    case CalType.Sum:
                        return Data.Sum();
                    case CalType.Std:
                        return CalStdev(Data);
                    case CalType.Max:
                        return Data.Max();
                    case CalType.Min:
                        return Data.Min();
                    case CalType.Ran:
                        return Data.Max() - Data.Min();
                    default:
                        throw new Exception("未知类型的计算"); ;
                }
            }
            else
            {
                return 0.0;
            }

        }
        private List<Double> Convert(DataTable dt)
        {
            List<Double> list = new List<double>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item[0].ToDouble(0));
                }
            }
            return list;
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
        private void GetCONFIG_BFANADATAList()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                ConfigListModel = idatabase.GetList<CONFIG_BFANADATA>("select * from CONFIG_BFANADATA");
            }
        }
        private Models.AG.BF_ANADATA_08H Cal_BFANADATA(List<CONFIG_BFANADATA> ListModel, DateTime det)
        {
            DateTime InsertTime = det.ToDate();
            //List<double> datalist = null;
            DataTable dataTable = null;
            Models.AG.BF_ANADATA_08H ResultModel = null;
            ResultModel = new Models.AG.BF_ANADATA_08H() { TIMESTAMP = InsertTime };
            //处理DEALTYPE0与1类型
            foreach (CONFIG_BFANADATA model in ListModel)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
                {
                    dataTable = idatabase.GetDataTable(GetSqlByModel(model, det));
                    if (dataTable != null & dataTable.Rows.Count != 0)
                    {
                        Double Value = CalValue(Convert(dataTable), (CalType)model.CALTYPE);
                        //Writelog(string.Format("Name：{0},Value：{1}.", dicName[model.TFIELDNAME], Value));
                        CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, dicName[model.TFIELDNAME], Math.Round(Value.ToDouble(), 5));
                        //var val=  CustomModelInfo.GetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, dicName[model.TFIELDNAME]);//如果是反射取数
                    }
                    //如果没有数据取前边的数据
                    else if (model.DEALTYPE == 1)
                    {
                        ResultModel = ReadBF_ANADATA_08(model.TFIELDNAME, InsertTime);
                        //如果前边也没有从数据源取最新的
                        if (ResultModel == null)
                        {
                            string sql = $"select {model.FIELDNAME} from {model.TABLENAME} where  {model.TIMEFIELD}=(select max({model.TIMEFIELD}) from {model.TABLENAME} where {model.FILTERSQL} )";
                            DataTable tableTemp = idatabase.GetDataTable(sql);
                            if (tableTemp.Rows.Count > 0)
                            {
                                //Writelog(string.Format("Name：{0},Value：{1}.", dicName[model.TFIELDNAME], tableTemp.Rows[0][0].ToDouble()));
                                CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, dicName[model.TFIELDNAME], Math.Round(tableTemp.Rows[0][0].ToDouble(), 5));
                            }
                        }
                    }
                }
            }
            //处理自定义数据计算方式
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                //计算煤气阻力系数均值与标准差
                string sql = $"select a.A_BP,a.A_TPAVG,b.C_BVSG from AV05M a LEFT JOIN (select C_BVSG,timestamp from TC05M )b on a.timestamp =b.timestamp where  a.timestamp >={DataBaseFactory.ConvertTimeString(det.AddHours(-8), DataBaseType.Oracle)} and  a.timestamp <={DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)} and A_BP>0 and C_BVSG >0";
                DataTable tableTemp = idatabase.GetDataTable(sql);
                if (tableTemp != null && tableTemp.Rows.Count > 0)
                {
                    List<Double> list = new List<double>();
                    foreach (DataRow item in tableTemp.Rows)
                    {
                        list.Add((Math.Pow(item[0].ToDouble(0), 2) - Math.Pow(item[1].ToDouble(0), 2)) / Math.Pow(item[2].ToDouble(0), 1.7) * 100);
                    }
                    Double Value = CalValue(list, (CalType)1);
                    //Writelog(string.Format("标准差：Value：{0}.", Value));
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, "A_K", Math.Round(Value.ToDouble(), 5));
                    //modelList.Add(model);

                    Value = CalValue(list, (CalType)3);
                    //Writelog(string.Format("标准差：Value：{0}.", Value));
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, "A_K_STD", Math.Round(Value.ToDouble(), 5));
                    //modelList.Add(model);

                    list.Clear();
                    list = null;
                }
                //计算中心加焦比例
                sql = $"select a_circle1,a_circle1+a_circle2+a_circle3+a_circle4+a_circle5+a_circle6+a_circle7+a_circle8+a_circle9+a_circle10+a_circle11 from STUFF_MATRIX t where timestamp >={DataBaseFactory.ConvertTimeString(det.AddHours(-8), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(det, DataBaseType.Oracle)} and a_type=1 ";
                DataTable tableTemp1 = idatabase.GetDataTable(sql);
                if (tableTemp1 != null && tableTemp1.Rows.Count > 0)
                {
                    List<Double> list = new List<double>();

                    foreach (DataRow item in tableTemp1.Rows)
                    {
                        if (item[1].ToDouble(0) > 0)
                        {
                            list.Add((item[0].ToDouble(0) / item[1].ToDouble(0)).ToDouble() * 100);
                        }
                    }
                    Double Value = CalValue(list, (CalType)1);
                    //Writelog(string.Format("加焦比例：Value：{0}.", Value));
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, "A_CENTERCOKE", Math.Round(Value.ToDouble(), 5));

                    Value = CalValue(list, (CalType)3);
                    // Writelog(string.Format("加焦比例：Value：{0}.", Value));
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, "A_CENTERCOKE_STD", Math.Round(Value.ToDouble(), 5));

                    list.Clear();
                    list = null;
                }
            }
            return ResultModel;
        }
        private void GetCustomModel(DateTime dt)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                //string sql = $"select a.A_BP,a.A_TPAVG,b.C_BVSG from AV05M a LEFT JOIN (select C_BVSG,timestamp from TC05M )b on a.timestamp =b.timestamp where  a.timestamp <={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  a.timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and A_BP>0 and C_BVSG >0";
                //DataTable tableTemp = idatabase.GetDataTable(sql);

                //List<Double> list = new List<double>();
                //if (tableTemp != null && tableTemp.Rows.Count > 0)
                //{
                //    foreach (DataRow item in tableTemp.Rows)
                //    {                      
                //        list.Add((Math.Pow(item[0].ToDouble(0),2)-Math.Pow(item[1].ToDouble(0),2))/Math.Pow(item[2].ToDouble(0),1.7)*100);
                //    }
                //    Double Value = CalValue(list, (CalType)1);
                //    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(ResultModel, dicName[model.TFIELDNAME], Value.ToDouble());
                //}                
                ////读取铁水温度合格率数据
                //sql = $"select count(*) from TAPPING where  HMTP_TAPEND between 1490 and 1510 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                //hmtpnum = idatabase.GetInt(sql);
                //sql = $"select count(*) from TAPPING where  HMTP_TAPEND >0 and tapstart>{DataBaseFactory.ConvertTimeString(dt.AddDays(-1), DataBaseType.Oracle)} and  tapstart<={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)}";
                //hmtpsum = idatabase.GetInt(sql);
            }
        }
        private Models.AG.BF_ANADATA_08H GetCustomCal(DateTime dt)
        {
            DateTime InsertTime = dt.ToDate();
            //List<Models.AG.BF_ANADATA_08H> modelList = new List<Models.AG.BF_ANADATA_08H>();
            Models.AG.BF_ANADATA_08H model = null;
            model = new Models.AG.BF_ANADATA_08H();
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                //计算煤气阻力系数均值与标准差
                string sql = $"select a.A_BP,a.A_TPAVG,b.C_BVSG from AV05M a LEFT JOIN (select C_BVSG,timestamp from TC05M )b on a.timestamp =b.timestamp where  a.timestamp >={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  a.timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and A_BP>0 and C_BVSG >0";
                DataTable tableTemp = idatabase.GetDataTable(sql);
                if (tableTemp != null && tableTemp.Rows.Count > 0)
                {
                    List<Double> list = new List<double>();
                    foreach (DataRow item in tableTemp.Rows)
                    {
                        list.Add((Math.Pow(item[0].ToDouble(0), 2) - Math.Pow(item[1].ToDouble(0), 2)) / Math.Pow(item[2].ToDouble(0), 1.7) * 100);
                    }
                    Double Value = CalValue(list, (CalType)1);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_K", Value.ToDouble());
                    //modelList.Add(model);

                    Value = CalValue(list, (CalType)3);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_K_STD", Value.ToDouble());
                    //modelList.Add(model);

                    list.Clear();
                    list = null;
                }
                //计算中心加焦比例
                sql = $"select a_circle1,a_circle1+a_circle2+a_circle3+a_circle4+a_circle5+a_circle6+a_circle7+a_circle8+a_circle9+a_circle10+a_circle11 from STUFF_MATRIX t where timestamp >={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and a_type=1 ";
                DataTable tableTemp1 = idatabase.GetDataTable(sql);
                if (tableTemp1 != null && tableTemp1.Rows.Count > 0)
                {
                    List<Double> list = new List<double>();
                    foreach (DataRow item in tableTemp1.Rows)
                    {
                        list.Add((item[0].ToDouble(0) / item[1].ToDouble(0)).ToDouble() * 100);
                    }
                    Double Value = CalValue(list, (CalType)1);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_CENTERCOKE", Value.ToDouble());
                    //modelList.Add(model);

                    Value = CalValue(list, (CalType)3);
                    CustomModelInfo.SetModelItemValue<Models.AG.BF_ANADATA_08H>(model, "A_CENTERCOKE_STD", Value.ToDouble());
                    //modelList.Add(model);

                    list.Clear();
                    list = null;
                }
                //计算铁水温度均值
                //sql = $"select HMTP from LT_TAPPING where TAPSTART timestamp <={DataBaseFactory.ConvertTimeString(dt.AddHours(-8), DataBaseType.Oracle)} and  timestamp <={DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and HMTP>0";
                //DataTable tableTemp = idatabase.GetDataTable(sql);
            }
            return model;
        }
        private Models.AG.BF_ANADATA_08H ReadBF_ANADATA_08(string fieldname, DateTime dt)
        {
            Models.AG.BF_ANADATA_08H model = null;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                model = idatabase.GetSingle<Models.AG.BF_ANADATA_08H>($"select {fieldname} from BF_ANADATA_08H where timestamp = (select max(timestamp) from BF_ANADATA_08H where timestamp<{DataBaseFactory.ConvertTimeString(dt, DataBaseType.Oracle)} and {fieldname}>0)");
            }
            return model;
        }
    }
}
