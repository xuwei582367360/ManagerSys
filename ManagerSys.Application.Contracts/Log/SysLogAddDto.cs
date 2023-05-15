namespace ManagerSys.Application.Contracts.Log
{
    public class SysLogAddDto
    {
        /// <summary>
        /// 日志类型 1、 操作日志  2、错误日志
        /// </summary>
        public int LogType { get; set; }

        /// <summary>
        /// 所属页面
        /// </summary>
        public string Page { get; set; }
        /// <summary>
        /// 操作类型   添加  修改 删除 查询
        /// </summary>
        public string OperateType { get; set; }  

        /// <summary>
        /// 操作内容描述
        /// </summary>
        public string OperateContent { get; set; }

        /// <summary>
        /// 操作人IP
        /// </summary>
        public string OperateUserIp { get; set; }

        /// <summary>
        /// 操作人MAC地址
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// 删除状态
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string error { get; set; }
    }

}
