using BFES.DataAccess;
using BFES.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    public class JGBFES7_DEALTAPTIME : BaseJob
    {
        public override void InitTask()
        {

        }
        public override void RunTask(DateTime currentTime)
        {
            string strT = currentTime.ToString("yyyy-MM-dd");
            DealHearhtBalance(strT);
            string strTimeYes = currentTime.AddDays(-1).ToString("yyyy-MM-dd");
            DealHearhtBalance(strTimeYes);
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        public void DealHearhtBalance(string strTime)
        {

            string sql = "";
            int business = 26;
            using (IDataBase idataBase = DalFactory.GreateIDataBase(base.S_DBSource))
            {
                sql = string.Format("select \"MeasureBeginDate\", \"Tc\",\"CtsjS\",\"CtsjZ\",\"Lltl\",\"Hl\",\"TswdQ\",\"TswdH\" ,\"Ctc\" from sa.\"BB_CtCz\"@REPORT t where \"BusinessGroup\"={0}  and  \"MeasureBeginDate\" =to_date('{1}','yyyy-mm-dd') order by \"Tc\" asc", business, strTime);
                DataTable dt = idataBase.GetDataTable(sql);
                sql = string.Format("select * from HEARTH_BALANCE_ASSITENT where MeasureBeginDate=to_date('{0}','yyyy-mm-dd' )", strTime);
                List<HEARTH_BALANCE_ASSITENT> modellistOld = null;
                modellistOld = idataBase.GetList<HEARTH_BALANCE_ASSITENT>(sql);
                List<HEARTH_BALANCE_ASSITENT> modellistNew = new List<HEARTH_BALANCE_ASSITENT>();
                HEARTH_BALANCE_ASSITENT model = null;
                HEARTH_BALANCE_ASSITENT temp = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow item = dt.Rows[i];
                    int tc = 0;
                    tc = item["Tc"].ToInt();                  
                    if (modellistOld.Find(match => match.TC == tc) != null)
                    {
                        continue;
                    }
                    model = new HEARTH_BALANCE_ASSITENT();
                    model.CTC = item["Ctc"].ToString();
                    model.BUSINESSGROUP = business;
                    model.TC = item["Tc"].ToInt();
                    model.LUTL = DealValue(item["Lltl"].ToString()).ToDouble();
                    if (model.LUTL == 0)
                    {
                        continue;
                    }
                    model.SJTL = DealValue(item["Hl"].ToString()).ToDouble();
                    if (model.SJTL == 0)
                    {
                        continue;
                    }
                    model.CTSJ = string.Format("{0} {1}", strTime, DealTime(item["CtsjS"].ToString())).ToDate();
                    model.DKSJ = string.Format("{0} {1}", strTime, DealTime(item["CtsjZ"].ToString())).ToDate();
                    if (model.DKSJ == DateTime.MinValue)
                    {
                        continue;
                    }

                    //查找上次出铁信息
                    if (temp == null)
                    {
                        temp = modellistNew.Find(match => match.TC == model.TC - 1);
                    }
                    //如果modellistNew还没有添加记录，从HEARTH_BALANCE_ASSITENT中匹配
                    if (temp == null)
                    {
                        temp = modellistOld.Find(match => match.TC == model.TC - 1);
                    }
                    //查找上上次出铁信息
                    if (temp == null)
                    {
                        temp = modellistNew.Find(match => match.TC == model.TC - 2);
                    }
                    //如果modellistNew还没有添加记录，从HEARTH_BALANCE_ASSITENT中匹配
                    if (temp == null)
                    {
                        temp = modellistOld.Find(match => match.TC == model.TC - 2);
                    }
                    if (temp != null)
                    {
                        //如果上次堵口时间大于本次堵口时间，本次堵口时间加1
                        if (temp.DKSJ > model.DKSJ)
                        {
                            model.DKSJ = model.DKSJ.ToDate().AddDays(1);
                        }
                        //如果上次出铁时间大于本次出铁时间，本次出铁时间加1
                        if (temp.CTSJ > model.CTSJ)
                        {
                            model.CTSJ = model.CTSJ.ToDate().AddDays(1);
                        }
                    }
                    model.TSWD1 = DealStop(item["TswdQ"].ToString()).ToDouble();
                    model.TSWD2 = DealStop(item["TswdH"].ToString()).ToDouble();
                    model.MEASUREBEGINDATE = item["MeasureBeginDate"].ToDate();
                    modellistNew.Add(model);
                }
                if (modellistNew.Count > 0)
                {
                    try
                    {
                        idataBase.InsertRange<HEARTH_BALANCE_ASSITENT>(modellistNew);
                    }
                    catch (Exception e)
                    {
                        Log.WriteLine(e.Message);
                    }

                }
            }





        }

        #region 处理输入异常
        //处理句号 值类型
        private static string DealStop(string Value)
        {
            return Value.Contains('。') ? Value.Replace('。', '.') : Value;
        }
        //处理反斜杠 值类型
        private static string DealBackslash(string Value)
        {
            List<string> stringValue = Value.Contains('/') ? Value.Split('/').ToList<string>() : new List<string>() { Value };
            double va = 0;
            foreach (string item in stringValue)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    va += item.ToDouble();
                }
            }
            return va.ToString();
        }
        //处理冒号 时间字段
        private string DealColon(string Value)
        {
            return Value.Contains('：') ? Value.Replace('：', ':') : Value;
        }
        //处理英文分号 时间字段
        private string DealSemicolonEn(string Value)
        {
            return Value.Contains(';') ? Value.Replace(';', ':') : Value;
        }
        //处理汉语分号 时间字段
        private string DealSemicolonCh(string Value)
        {
            return Value.Contains('；') ? Value.Replace('；', ':') : Value;
        }
        private string DealTime(string Value)
        {
            return DealSemicolonEn(DealSemicolonCh(DealColon(Value)));
        }
        private string DealValue(string Value)
        {
            return DealBackslash(DealStop(Value));
        }

        #endregion
    }

    public class HEARTH_BALANCE_ASSITENT
    {

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BUSINESSGROUP { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TC { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CTC { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? CTSJ { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? DKSJ { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? LUTL { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SJTL { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TKSD { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TSWD1 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TSWD2 { get; set; }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MEASUREBEGINDATE { get; set; }
    }
}


