using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ManagerSys.Domian.Order
{
    [Table("OrderInfo")]
    public class OrderInfo : Entity<long>
    {
        /// <summary>
        /// 营业区域编码
        /// </summary>
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
        /// <summary>
        /// 供水片区编码
        /// </summary>
        [Column("Address")]
        public string Address { get; set; }
        /// <summary>
        /// 供水片区编码
        /// </summary>
        [Column("Code")]
        public string Code { get; set; }
    }
}
