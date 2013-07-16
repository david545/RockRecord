using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
using RockRecord.Filters;
using RockRecord.Extension;
using PagedList;
namespace RockRecord.Controllers
{
    public class GenreController : BaseController
    {
        //
        // GET: /Genre/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Albums(int id, int p = 1, AlbumSort sort = AlbumSort.ByDateHightToLow)
        {
            var category = db.Genres.Find(id);
            ViewData["Genre"] = category.Name;
            var albums = category.Albums.Sort(sort);

            if (albums != null)
                return View(albums.ToPagedList(pageNumber: p, pageSize: 10));
            else
                return HttpNotFound();
        }

        [Administrator]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Administrator]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                TempData["createdId"] = genre.Id;
                TempData["createdName"] = genre.Name;
                return RedirectToAction("Genre","Manage");
            }
            return View();
        }

 
        [Administrator]
        public ActionResult Edit(int id)
        {
            Genre genre = db.Genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return HttpNotFound();
            return View(genre);
        }

        [HttpPost]
        [Administrator]
        public ActionResult Edit(Genre newGenre)
        {
            Genre oldGenre = db.Genres.FirstOrDefault(g => g.Id == newGenre.Id);
            if (oldGenre == null) return HttpNotFound();
            oldGenre.Name = newGenre.Name;
            db.SaveChanges();
            TempData["success"] = true;
            return View();
        }

        [HttpPost]
        [Administrator]
        public ActionResult Delete(int id)
        {
            Genre genre = db.Genres.FirstOrDefault(g => g.Id == id);
            if (genre != null)
            {
                try
                {
                    db.Genres.Remove(genre);
                    db.SaveChanges();
                    //用來讓view存取新增成功的唱片資訊
                    TempData["deletedName"] = genre.Name;
                    return new HttpStatusCodeResult(200, "刪除成功");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(500, "刪除失敗");
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult GenreMenu()
        {
            var genre = db.Genres.ToList();
            return PartialView(genre);
        }
    }
}
