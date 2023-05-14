using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domain.Shared.Enums.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            string name = Enum.GetName(type, @enum);
            if (string.IsNullOrWhiteSpace(name)) return null;
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null) return name;
            return attribute?.Description;
        }
    }
}
