using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
namespace RockRecord.Filters
{
    public class AdministratorAttribute:AuthorizeAttribute
    {
        RockRecordContext db = new RockRecordContext();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                !isAdiministrator(filterContext.HttpContext.User.Identity.Name))
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("NotAuthorization", "Error"));
            }
        }
        private bool isAdiministrator(string email)
        {
            return db.Members.First(member => member.Email == email).Role==Role.Administrator;
        }
    }
}