using OTC.CommonHelper.FormatUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTC.CommonHelper.StringUtil
{
    /// <summary>
    /// 系统扩展 - 类型转换
    /// </summary>
    public static partial class Extensions
    {
        #region Type Check
        public static bool IsDecimal(this object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                decimal newData = Convert.ToDecimal(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsInt32(this object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                int newData = Convert.ToInt32(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTime(this object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                DateTime newData = Convert.ToDateTime(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsBoolean(this object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                bool newData = Convert.ToBoolean(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsFloat(this string data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                float newData = Convert.ToSingle(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDouble(this object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                double newData = Convert.ToDouble(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsByte(this object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                byte newData = Convert.ToByte(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsChar(this object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                char newData = Convert.ToChar(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsString(this object data)
        {
            if (data == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Type Convert
        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        public static string TrimString(this object input)
        {
            return input == null ? string.Empty : input.ToString().Trim();
        }
        public static decimal? ToDecimal(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                decimal newData = Convert.ToDecimal(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static int? ToInt32(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                int newData = Convert.ToInt32(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static Int64? ToInt64(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                Int64 newData = Convert.ToInt64(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? ToDateTime(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                DateTime newData = Convert.ToDateTime(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static bool? ToBoolean(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                bool newData = Convert.ToBoolean(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static float? ToFloat(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                float newData = Convert.ToSingle(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static double? ToDouble(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                double newData = Convert.ToDouble(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static byte? ToByte(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                byte newData = Convert.ToByte(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static char? ToChar(this object data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                char newData = Convert.ToChar(data);
                return newData;
            }
            catch
            {
                return null;
            }
        }

        public static string ToString(this object data)
        {
            try
            {
                if (data == null)
                {
                    return "";
                }
                return data.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static decimal ToDecimal(this object data, decimal defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                decimal newData = Convert.ToDecimal(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ToInt32(this object data, int defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                int newData = Convert.ToInt32(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Int64 ToInt64(this object data, int defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                Int64 newData = Convert.ToInt64(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ToDateTime(this object data, DateTime defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                //string sss = DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss.fff");
                DateTime newData = Convert.ToDateTime(data);//new System.Globalization.CultureInfo("zh-CN")
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ToBoolean(this object data, bool defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                bool newData = Convert.ToBoolean(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static float ToFloat(this object data, float defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                float newData = Convert.ToSingle(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double ToDouble(this object data, double defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                double newData = Convert.ToDouble(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static byte ToByte(this object data, byte defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                byte newData = Convert.ToByte(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static char ToChar(this object data, char defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                char newData = Convert.ToChar(data);
                return newData;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string ToString(this object data, string defaultValue)
        {
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                if (data == DBNull.Value)
                {
                    return defaultValue;
                }
                return data.ToString();
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="input">以逗号分隔的元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<T> ToList<T>(this string input)
        {
            return TypeParse.ToList<T>(input);
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="input">输入值</param>
        public static T To<T>(this object input)
        {
            return TypeParse.To<T>(input);
        }
        #endregion

        #region Type Format
        /// <summary>
        /// 格式化数字
        /// </summary>
        /// <param name="data">数字</param>
        /// <param name="type">格式类型：0-向下取 1-向上取 2-四舍五入</param>
        /// <param name="precision">保留小数位数</param>
        /// <returns></returns>
        public static decimal FormatDecimal(this object data, int type, int precision, decimal defaultValue)
        {
            decimal ret = defaultValue;
            try
            {
                if (data == null)
                {
                    return defaultValue;
                }
                decimal newData = data.ToDecimal(defaultValue);
                decimal p = Math.Pow(10, precision).ToDecimal(0);
                switch (type)
                {
                    case 0:
                        ret = Math.Floor(newData * p) / p;
                        break;
                    case 1:
                        ret = Math.Ceiling(newData * p) / p;
                        break;
                    case 2:
                        ret = Math.Round(newData, precision, MidpointRounding.AwayFromZero);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                return defaultValue;
            }
            return ret;
        }
        #endregion Type
    }
    public static class TypeParse
    {
        /// <summary>
        /// 泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="input">以逗号分隔的元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<T> ToList<T>(string input)
        {
            var result = new List<T>();
            if (string.IsNullOrWhiteSpace(input))
                return result;
            var array = input.Split(',');
            result.AddRange(from each in array where !string.IsNullOrWhiteSpace(each) select To<T>(each));
            return result;
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="input">输入值</param>
        public static T To<T>(object input)
        {
            if (input == null)
                return default(T);
            if (input is string && string.IsNullOrWhiteSpace(input.ToString()))
                return default(T);
            Type type = TypeHelper.GetType<T>();
            var typeName = type.Name.ToLower();
            try
            {
                if (typeName == "string")
                    return (T)(object)input.ToString();
                if (typeName == "guid")
                    return (T)(object)new Guid(input.ToString());
                if (type.IsEnum)
                    return EnumHelper.Parse<T>(input);
                if (input is IConvertible)
                    return (T)System.Convert.ChangeType(input, type);
                return (T)input;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
