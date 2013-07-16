using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using RockRecord.Models;

namespace RockRecord.Controllers
{
    public class BaseController : Controller
    {
        protected RockRecordContext db = new RockRecordContext();
        public List<CartItem> CartItems
        {
            get
            {
                if (Session["CartItems"] == null)
                {
                    Session["CartItems"] = new List<CartItem>();
                }
                return Session["CartItems"] as List<CartItem>;
            }
            set
            {
                CartItems = value;
            }
        }

        protected bool isPicture(string fileName)
        {
            string extensionName = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var reg = new Regex("^(gif|png|jpg|bmp)$", RegexOptions.IgnoreCase);
            return reg.IsMatch(extensionName);
        }

    }
}
