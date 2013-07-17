using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockRecord.Filters
{
    public class AllowViewOrderAttribute:AllowEditAttribute
    {
        protected override string GetCompareEmail(int id)
        {
            var order = db.Orders.Find(id);
            return order.Member.Email; 
        }
    }
}