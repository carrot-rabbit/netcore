using System;
using System.Collections.Generic;
using System.Text;

namespace OTC.CommonHelper.FormatUtil
{
    public static class TypeHelper
    { /// <summary>
      /// 获取类型
      /// </summary>
      /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return GetType(typeof(T));
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        public static Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line => Environment.NewLine;

    }
}
