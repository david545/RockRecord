using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models.ViewModel;

namespace RockRecord.Controllers.Shared
{
    public class SharedController :BaseController
    {
        //
        // GET: /Shared/

        public ActionResult Navigater()
        {
            var navigaterViewModel = new NavigaterViewModel();
            navigaterViewModel.AlbumCategories = db.Genres.ToList();
            if (User.Identity.IsAuthenticated)
            {
                navigaterViewModel.Member = db.Members.FirstOrDefault(m => m.Email == User.Identity.Name);
                return PartialView(navigaterViewModel);
            }
            else
            {
                return PartialView(navigaterViewModel);
            }
        }

    }
}
