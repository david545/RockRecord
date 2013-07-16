using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;

namespace RockRecord.Filters
{
    //檢查看看該使用者是否有權限拜訪該頁面
    public abstract class AllowEditAttribute:AuthorizeAttribute
    {
        private string compareEmail = "";
        protected RockRecordContext db = new RockRecordContext();

        protected abstract string GetCompareEmail(int id);

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext);

            if (filterContext.RouteData.Values["id"] != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                int id = Int32.Parse(filterContext.RouteData.Values["id"].ToString());
                
                string compareEmail = GetCompareEmail(id);
                if (!String.IsNullOrEmpty(compareEmail))
                {
                    if (compareEmail != filterContext.HttpContext.User.Identity.Name)
                    {
                        UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                        filterContext.Result = new RedirectResult(urlHelper.Action("NotAuthorization", "Error"));
                    }
                }
            }
        }
    }
}