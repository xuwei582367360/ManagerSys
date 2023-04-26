using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ScheduleHttpOptions")]
    public class ScheduleHttpOptionEntity : BaseEntity<Guid>
    {
        /// <summary>
        /// 任务id
        /// </summary>
        [Key]
        [Column("ScheduleId"), Required]
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [Column("RequestUrl"), MaxLength(500), Required]
        [MapperSetting(BindField = "HttpRequestUrl")]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [Column("Method"), MaxLength(10), Required]
        [MapperSetting(BindField = "HttpMethod")]
        public string Method { get; set; }

        /// <summary>
        /// 数据格式
        /// </summary>
        [Column("ContentType"), MaxLength(50), Required]
        [MapperSetting(BindField = "HttpContentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// 自定义请求头（json格式）
        /// </summary>
        [Column("Headers")]
        [MapperSetting(BindField = "HttpHeaders")]
        public string Headers { get; set; }

        /// <summary>
        /// 数据内容（json格式）
        /// </summary>
        [Column("Body")]
        [MapperSetting(BindField = "HttpBody")]
        public string Body { get; set; }
    }

    /// <summary>
    /// 映射配置标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MapperSettingAttribute : Attribute
    {
        /// <summary>
        /// 指定映射字段名称
        /// </summary>
        public string BindField;

        /// <summary>
        /// 映射时是否忽略该字段
        /// </summary>
        public bool Ignore;
    }
}
