using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
using RockRecord.Filters;
using RockRecord.Helper;
using System.Drawing;
using DrawExtension;
using PagedList;
using RockRecord.Extension;

namespace RockRecord.Controllers
{
    public class ArtistController :BaseController
    {
    
        public ActionResult Albums(int id,int p = 1)
        {
            var artist = db.Artists.Find(id);
            ViewData["Artist"] = artist.Name;
            var albums = artist.Albums.Sort(AlbumSort.ByDateLowToHight);

            if (albums != null)
                return View(albums.ToPagedList(pageNumber: p, pageSize: 10));
            else
                return HttpNotFound();
        }

        [Administrator]
        public ActionResult Manage(int p=1,string keyWord="")
        {
            var artists=db.Artists.Where(artist=>String.IsNullOrEmpty(keyWord) ||
                                                 artist.Name.IndexOf(keyWord)>-1);
            return View(artists.OrderBy(artist=>artist.Name).ToPagedList(p,20));
        }

        [HttpGet]
        [Administrator]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Administrator]
        public ActionResult Create(Artist artist)
        {

            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                TempData["createdName"] = artist.Name;
                TempData["createdId"] = artist.Id;
                return RedirectToAction("Artist","Manage");
            }
            return View();
        }

        [HttpGet]
        [Administrator]
        public ActionResult Edit(int id)
        {
            var artist=db.Artists.FirstOrDefault(art => art.Id == id);
            if (artist == null)
                return HttpNotFound();
            return View(artist);
        }

        [HttpPost]
        [Administrator]
        public ActionResult Edit(Artist newArtist)
        {

            var oldArtist=db.Artists.FirstOrDefault(artist => artist.Id == newArtist.Id);
            if (oldArtist == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                oldArtist.Name = newArtist.Name;
                db.SaveChanges();
                TempData["success"] = true;
                return RedirectToAction("Edit", new { id = oldArtist.Id });
            }
            return View(newArtist);
        }

        [HttpPost]
        [Administrator]
        public ActionResult Delete(int id)
        {
            Artist artist = db.Artists.FirstOrDefault(art => art.Id == id);
            if (artist != null)
            {
                try
                {
                    db.Artists.Remove(artist);
                    db.SaveChanges();
                    //用來讓view存取新增成功的唱片資訊
                    TempData["deletedName"] = artist.Name;
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

  
    }
}
