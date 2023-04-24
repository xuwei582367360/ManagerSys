using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ManagerSys.Domian.CDZLS
{
    [Table("MRM_T_MS_AREAS")]
    public class AreaEntity : Entity<long>
    {
        /// <summary>
        /// 营业区域编码
        /// </summary>
        [Column("ORGANIZATION_CODE")]
        public string OrgCode { get; set; }

        /// <summary>
        /// 供水片区编码
        /// </summary>
        [Column("CODE")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("CREATOR_ID")]
        public long CreatorId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Column("CREATOR_NAME")]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CREATION_TIME")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否有效 1：有效
        /// </summary>
        [Column("IS_VALID")]
        public int IsValid { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [Column("MODIFY_ID")]
        public long? modifyId { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("MODIFY_NAME")]
        public string modifyName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("MODIFY_TIME")]
        public DateTime? modifyTime { get; set; }

        /// <summary>
        /// 经纬度
        /// 格式:经度,维度
        /// </summary>
        [Column("LAT_LON")]
        public string lat_lon { get; set; }

        /// <summary>
        /// 是否同步数据
        /// 0、是 1、不是 默认 1
        /// </summary>
        [Column("IS_SYNC")]
        public int IsSync { get; set; } = 1;
    }
}
