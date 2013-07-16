using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
using RockRecord.Models.ViewModel;
using PagedList;
using System.Text.RegularExpressions;
namespace RockRecord.Controllers
{
    public class SearchController :BaseController
    {

        [HttpGet]
        public ActionResult Index(string keyWord,int p=1)
        {
           var albums= db.Albums.Where(album => String.IsNullOrEmpty(keyWord) ||
                                    album.Name.IndexOf(keyWord) > -1 ||
                                    album.Artist.Name.IndexOf(keyWord) > -1)
                         .OrderByDescending(album => album.Artist.Name);
           ViewData["ResultCount"] = albums.Count();
            return View(albums.ToPagedList(p,20));
        }

        [HttpPost]
        public ActionResult Index(string searchType, string keyWord)
        {
            return RedirectToAction("Index", new { keyWord = keyWord.Trim(),searchType=searchType });
        }


    }
}
