using System;
using System.Collections.Generic;
using System.Text;

namespace YuG.Framework.Core
{
    public static class ConvertHelper
    {
        /// <summary>
        /// object转int，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            int value = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out value))
            {
                return value;
            }
            return value;
        }
        /// <summary>
        /// object转int，转换失败返回提供的默认值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue">转换失败时返回的值</param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// object转dobule，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static double ObjToDobule(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }

        /// <summary>
        /// object转dobule，转换失败返回提供的默认值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ObjToDobule(this object thisValue, double errorValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// object转string(转化结果会调用.Trim())，如果为null返回 "" 字符串
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ObjToTrimString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }

        /// <summary>
        /// object转string(转化结果会调用.Trim())，如果为null返回提供的默认值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue">object为null时，默认值</param>
        /// <returns></returns>
        public static string ObjToTrimString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return errorValue;
        }

        /// <summary>
        /// object转decimal，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static decimal ObjToDecimal(this object thisValue)
        {
            decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }

        /// <summary>
        /// object转decimal，转换失败返回提供的默认值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue">转换失败时，返回的默认值</param>
        /// <returns></returns>
        public static decimal ObjToDecimal(this object thisValue, decimal errorValue)
        {
            decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// object转DateTime，转换失败返回DateTime.MinValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }

        /// <summary>
        /// object转DateTime，转换失败返回提供的默认值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// object转bool，转换失败返回false
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }


        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string DateToTimeStamp(this DateTime thisValue)
        {
            TimeSpan ts = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
