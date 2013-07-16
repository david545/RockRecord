using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockRecord.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }

        [DisplayName("訂單狀態")]
        [Required(ErrorMessage="請輸入訂單狀態")]
        public string Name { get; set; }

        public virtual ICollection<OrderHeader> Orders { get; set; }
    }
}