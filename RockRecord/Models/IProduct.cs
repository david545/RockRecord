using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockRecord.Models
{
    public interface IProduct
    {
        int Id { get; set; }
        Album Album { get; set; }

        [DisplayName("數量")]
        int Amount { get; set; }

        [DisplayName("價格")]
        [DataType(DataType.Currency)]
        int Price { get; set; }
    }
}