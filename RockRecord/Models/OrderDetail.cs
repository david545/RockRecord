using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockRecord.Models
{
    public class OrderDetail:IProduct
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DisplayName("價格")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        [DisplayName("數量")]
        public int Amount { get; set; }

        [Required]
        public virtual Album Album { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }


        
    }
}