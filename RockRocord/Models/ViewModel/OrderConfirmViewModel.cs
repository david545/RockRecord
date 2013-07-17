using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockRecord.Models.ViewModel
{
    public class OrderConfirmViewModel
    {
        public OrderHeader Order { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}