﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domain.Shared.Consts
{
    public static class Extensions
    {
        /// <summary>
        /// 转换成完整的物理路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToPhysicalPath(this string path)
        {
            return $"{Directory.GetCurrentDirectory()}{path}".Replace('\\', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// 格式化为yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string StringFormat(this DateTime? obj)
        {
            if (!obj.HasValue)
            {
                return string.Empty;
            }
            return StringFormat(obj.Value);
        }

        /// <summary>
        /// 格式化为yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string StringFormat(this DateTime obj)
        {
            return obj.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 格式化为yyyy-MM-dd
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DateFormat(this DateTime? obj)
        {
            if (!obj.HasValue)
            {
                return string.Empty;
            }
            return DateFormat(obj.Value);
        }

        /// <summary>
        /// 格式化为yyyy-MM-dd
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DateFormat(this DateTime obj)
        {
            return obj.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 毫秒时间戳
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime obj)
        {
            TimeSpan ts = obj - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }

}
