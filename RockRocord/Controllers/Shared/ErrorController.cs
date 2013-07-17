using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockRecord.Controllers.Shared
{
    public class ErrorController : Controller
    {
        public ActionResult NotAuthorization()
        {
            return View();
        }

    }
}
