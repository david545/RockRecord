using RockRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RockRecord.Controllers.api
{
    public class BaseController : ApiController
    {
        protected RockRecordContext db = new RockRecordContext();
    }
}