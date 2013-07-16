using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
using RockRecord.Models.ViewModel;
using PagedList;

namespace RockRecord.Controllers
{
    public enum OrderModel
    {
        ByTitle,
        ByTitleDsc,
        ByDate,
        ByDateDsc,
        ByPopularl,
        ByCustomerReview
    }
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            var categories = db.Genres.ToList();
            if (categories.Count == 0)
            {
                db.Genres.Add(new Genre() { Id = 1, Name = "Punk" });
                db.Genres.Add(new Genre() { Id = 2, Name = "Metal" });
                db.Genres.Add(new Genre() { Id = 3, Name = "Bitpop" });
                db.Genres.Add(new Genre() { Id = 4, Name = "Alternative/Indie Rock" });
                db.SaveChanges();
                categories = db.Genres.ToList();
            }

            return View(categories);
        }


    }
}
