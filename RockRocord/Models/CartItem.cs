using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockRecord.Models
{
    public class CartItem :IProduct
    {

        public int Id { get; set; }

        [DisplayName("唱片")]
        public Album Album { get; set;}

        [DisplayName("數量")]
        [Range(1,999,ErrorMessage="購買數量不得超過999個")]
        public int Amount { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("價格")]
        public int Price
        {
            get
            {
                if (Album != null)
                {
                    return Album.Price * Amount;
                }
                else
                {
                    return 0;
                }
                
            }
            set
            {
            }
        }


    
    }
}