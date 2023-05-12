using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ServerNodes")]
    public class ServerNodeEntity : BaseEntity<Guid>
    {
        [Key]
        [Column("NodeId")]
        public Guid NodeId { get; set; }
        /// <summary>
        /// 节点标识
        /// </summary>

        [MaxLength(100)]
        [Column("NodeName")]
        public string NodeName { get; set; }

        /// <summary>
        /// 节点类型 master/worker
        /// </summary>
        [Required]
        [Column("NodeType")]
        [MaxLength(20)]
        public string NodeType { get; set; }

        /// <summary>
        /// 所在机器
        /// </summary>
        [Column("MachineName")]
        [MaxLength(100)]
        public string MachineName { get; set; }

        /// <summary>
        /// 访问协议，http/https
        /// </summary>
        [Required]
        [Column("AccessProtocol")]
        [MaxLength(20)]
        public string AccessProtocol { get; set; }

        /// <summary>
        /// 节点主机(IP+端口)
        /// </summary>
        [Required]
        [Column("Host")]
        [MaxLength(100)]
        public string Host { get; set; }

        /// <summary>
        /// 访问秘钥，每次节点激活时会更新，用来验证访问权限
        /// </summary>
        [Column("AccessSecret")]
        [MaxLength(50)]
        public string AccessSecret { get; set; }

        /// <summary>
        /// 节点状态，0-下线，1-空闲，2-运行
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        [Column("Priority")]
        public int Priority { get; set; }

        /// <summary>
        /// 当节点类型为worker时，Quartz最大并发执行数量
        /// </summary>
        [Column("MaxConcurrency")]
        public int MaxConcurrency { get; set; }
    }
}
