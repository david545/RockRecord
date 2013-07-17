using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
using RockRecord.Models.ViewModel;
using RockRecord.Filters;
using PagedList;

namespace RockRecord.Controllers
{
    public class ReviewController : BaseController
    {




        [Authorize]
        public ActionResult Create(int id)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
                return HttpNotFound();
            
            ViewData["AlbumTitle"] = album.Name;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(int albumId,FormCollection form)
        {
            var review = new Review();
            review.Member = db.Members.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            review.Album = db.Albums.Find(albumId);
            review.ReviewDate = DateTime.Now;
            if (TryUpdateModel<Review>(review))
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Detail", "Album", new { id = albumId });
            }
            ViewData["AlbumTitle"] = review.Album.Name;
            return View();
        }

        [HttpGet]
        [Authorize]
        [AllowEditReviewAttribute]
        public ActionResult Edit(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null) return HttpNotFound();

            return View(review);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                Review oldReview = db.Reviews.Find(review.Id);
                oldReview.Rating = review.Rating;
                oldReview.Comment = review.Comment;
                oldReview.ReviewDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Detail", "Album", new { id = oldReview.Album.Id });
            }
            return View(review);
        }

        [HttpGet]
        [Authorize]
        [AllowEditReviewAttribute]
        public ActionResult Delete(int id)
        {
            var review = db.Reviews.Find(id);
            if (review == null)
                return HttpNotFound();
            return View(review);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id,FormCollection form)
        {
            System.Threading.Thread.Sleep(1000);
            Review review=db.Reviews.FirstOrDefault(r=> r.Id == id);
            if (review==null) return HttpNotFound();
            try
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "評論已成功移除");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "資料刪除失敗");
            }
           
        }

        public ActionResult ReviewList(int id,int p=1)
        {
            Album album = db.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return HttpNotFound();
            return PartialView(album.Reviews.ToPagedList(p, 10));
        }

    }
}
