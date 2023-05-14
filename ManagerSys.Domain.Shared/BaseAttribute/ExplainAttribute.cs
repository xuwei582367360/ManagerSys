using ManagerSys.Domain.Shared.Enums;
using ManagerSys.Domain.Shared.Enums.Util;

namespace ManagerSys.Domain.Shared.BaseAttribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class ExplainAttribute : Attribute
    {
        /// <summary>
        /// 异常辅助类
        /// </summary>
        /// <param name="_explain">说明</param>
        /// <param name="optionName">功能项目名称</param>

        public ExplainAttribute(string _explain, OperateType optionName)
        {
            this.Explain = _explain;
            this.OptionName = EnumUtils.GetDescription((Enum)optionName);
        }
        /// <summary>
        /// 功能项目名称
        /// </summary>
        public string OptionName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }
    }

}
