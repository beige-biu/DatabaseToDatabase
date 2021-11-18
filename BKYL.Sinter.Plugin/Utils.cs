using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin
{
    public class Utils
    {
        /// <summary>
        /// 获取sql脚本
        /// </summary>
        /// <param name="sqlcol"></param>
        /// <param name="tabName"></param>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static string GetSql(string sqlcol, string tabName, DateTime dTime)
        {
            string sqltime = string.Format("select max(timestamp) from {0} where timestamp <= to_date('{1}', 'yyyy-MM-dd HH24:mi:ss')", tabName, dTime.ToString());
            return string.Format("select {0} from {1} where timestamp=({2})", sqlcol, tabName, sqltime);
        }

        /// <summary>
        /// 按实现倒叙 查询前多少行的数据
        /// </summary>
        /// <param name="sqlCol"></param>
        /// <param name="tabName"></param>
        /// <param name="rownum"></param>
        /// <returns></returns>
        public static string GetSql2(string sqlCol, string tabName, DateTime dTime, int rownum)
        {
            string sqlstr = string.Format("select {0} from (select {0} from {1} where timestamp <= to_date('{2}', 'yyyy-MM-dd HH24:mi:ss') order by TIMESTAMP desc) where rownum<={3}", sqlCol, tabName, dTime, rownum);
            //select * from (select * from T_BURN_COLE_PLC_1MIN order by TIMESTAMP desc) where rownum<=600
            return sqlstr;
        }

        /// <summary>
        /// 获取两个时间的差值 单位的分钟
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static double DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            double dateDiff = 0;
            TimeSpan ts = DateTime1.Subtract(DateTime2).Duration();
            
            dateDiff = ts.Days*24*60 + ts.Hours*60 + ts.Minutes + ts.Seconds/60f;
            return dateDiff;
            //return double.Parse(dateDiff.ToString("f3"));
        }


    }
}
