using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Utils
{
    /// <summary>
    /// ** 描述：公用转换函数
    /// </summary>
    public static class ConvertExtensions
    {
        #region 强转成int 如果失败返回 0
        /// <summary>
        /// 强转成int 如果失败返回 0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        #endregion
        #region 强转成int 如果失败返回 errorValue
        /// <summary>
        /// 强转成int 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion
        #region 强转成double 如果失败返回 0
        /// <summary>
        /// 强转成Double 如果失败返回 0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static double ToDouble(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        #endregion
        #region 强转成double 如果失败返回 errorValue
        /// <summary>
        /// 强转成double 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ToDouble(this object thisValue, double errorValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion

        #region float 如果失败返回 0
        /// <summary>
        /// 强转成float 如果失败返回 0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static float ToFloat(this object thisValue)
        {
            float reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && float.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        #endregion
        #region 强转成Float 如果失败返回 errorValue
        /// <summary>
        /// 强转成Float 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static float ToFloat(this object thisValue, float errorValue)
        {
            float reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && float.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion

        #region 强转成Decimal 如果失败返回 0
        /// <summary>
        /// 强转成Decimal 如果失败返回 0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this object thisValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        #endregion
        #region 强转成Decimal 如果失败返回 errorValue
        /// <summary>
        /// 强转成Decimal 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this object thisValue, decimal errorValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion
        #region 强转成DateTime 如果失败返回 DateTime.MinValue
        /// <summary>
        /// 强转成DateTime 如果失败返回 DateTime.MinValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        #endregion
        #region 强转成DateTime 如果失败返回 errorValue
        /// <summary>
        /// 强转成DateTime 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion
        #region 强转成Bool 如果失败返回 false
        /// <summary>
        /// 强转成Bool 如果失败返回 false
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        #endregion


        public static T ToEnum<T>(this object thisValue)
        {
            return (T)Enum.Parse(typeof(T), thisValue.ToString(), true);
        }

        /// <summary>
        /// 返回时间字符串格式yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static String ToDateString(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return reval.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回时间字符串格式
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="format">format</param>
        /// <returns></returns>
        public static String ToDateString(this object thisValue, string format)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval.ToString(format);
            }
            return reval.ToString(format);
        }
    }
}
