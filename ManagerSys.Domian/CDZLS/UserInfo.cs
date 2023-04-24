using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ManagerSys.Domian.CDZLS
{
    [Table("UserInfo")]
    public class UserInfo : Entity<long>
    {
        /// <summary>
        /// 营业区域编码
        /// </summary>
        [Column("Id")]
        public int Id { get; set; }

        /// <summary>
        /// 供水片区编码
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
    }
}