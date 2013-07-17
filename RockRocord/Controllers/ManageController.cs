using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using RockRecord.Models;
using RockRecord.Filters;
using RockRecord.Extension;

namespace RockRecord.Controllers
{
    [Administrator]
    public class ManageController :BaseController
    {
        //
        // GET: /Manage/

        public ActionResult Index()
        {
            return RedirectToAction("Album");
        }

        public ActionResult Album(int genre = 0,
                                   int p = 1,
                                   AlbumSort sort=AlbumSort.ByDateHightToLow,
                                   string keyWord = "")
        {
            IEnumerable<Album> albums = null;

            albums = db.Albums.Where(album => genre == 0 || album.Genre.Id == genre)
                              .Where(album => String.IsNullOrEmpty(keyWord) ||
                                              album.Name.IndexOf(keyWord) > -1 ||
                                              album.Artist.Name.IndexOf(keyWord) > -1);

            return View(albums.Sort(sort).ToPagedList(p, 20));
        }


        public ActionResult Artist(int p = 1, string keyWord = "")
        {
            var artists = db.Artists.Where(artist => String.IsNullOrEmpty(keyWord) ||
                                                     artist.Name.IndexOf(keyWord) > -1);
            return View(artists.OrderBy(artist => artist.Name).ToPagedList(p, 20));
        }

        public ActionResult Genre(int p=1,string keyWord="")
        {
            var genres = db.Genres.Where(genre => String.IsNullOrEmpty(keyWord) ||
                                                   genre.Name.IndexOf(keyWord)>-1);
            return View(genres.OrderBy(genre=>genre.Name).ToPagedList(p,20));
        }


        public ActionResult Order(int p = 1,
                                  int status = 0,
                                  string keyWord="",
                                  OrderSort sort=OrderSort.ByDateHightToLow)
        {
            var orders = db.Orders.ToList()
                                   .Where(order =>
                                       (status == 0 || order.OrderStatus.Id == status) &&
                                       (String.IsNullOrEmpty(keyWord) || order.Id.ToString() == keyWord))
                                  .Sort(sort);
            ViewData["Model"] = "editor";
            return View(orders.ToPagedList(p, 20));
        }

    }
}
