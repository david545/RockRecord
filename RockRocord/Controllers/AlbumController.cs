using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using RockRecord.Models;
using RockRecord.Models.ViewModel;
using RockRecord.Filters;
using RockRecord.Helper;
using PagedList;
using RockRecord.Extension;

namespace RockRecord.Controllers
{
    public class AlbumController:BaseController
    {

        public ActionResult Detail(int id, int p = 1)
        {
            
            var albumDetailModel = new AlbumDetailViewModel();
            Album album = db.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return HttpNotFound();
            albumDetailModel.Album = album;
            albumDetailModel.RecommandationItemCount = GetRecommandationAlbums(id).Count();
            albumDetailModel.isAdminustrator = User.Identity.IsAuthenticated &&
                                               db.Members.First(m => m.Email == User.Identity.Name).Role == Role.Administrator;
            albumDetailModel.LoginUserReview = User.Identity.IsAuthenticated ?
                                   GetUserOnAlbumReview(id, User.Identity.Name) :
                                   null;

            return View(albumDetailModel);

        }

        private Review GetUserOnAlbumReview(int albumId, string email)
        {
            return db.Members.First(m => m.Email == email)
                     .Reviews.FirstOrDefault(r => r.Album.Id == albumId);
        }

        public ActionResult RecommandationItems(int albumId)
        {
            var hasBoughtalbums = GetRecommandationAlbums(albumId);
            return PartialView("ItemSlider", hasBoughtalbums.ToList());
        }

        public ActionResult MonthTopSellAlbums()
        {
            var topSellAlbums = GetMonthTopSellAlbums();
            return PartialView("ItemSlider", topSellAlbums.ToList());
        }

        public ActionResult NewArrivalAlbums()
        {
            var newArrivalAlbums = db.Albums.OrderByDescending(album => album.PublicDate)
                                            .Take(15);
            return PartialView("ItemSlider", newArrivalAlbums.ToList());
        }

        private IEnumerable<Album> GetRecommandationAlbums(int albumId)
        {
            var hasBoughtUsers =db.Members
                                  .Where(user=>user.Orders.Any(order => order.OrderDetails.Any(od => od.Album.Id == albumId)));


            var hasBoughtItems = from od in db.OrderDetails
                                 where hasBoughtUsers.Any(user => user.Id == od.OrderHeader.Member.Id) && od.Album.Id != albumId
                                 group od by od.Album into albumGroup
                                 orderby albumGroup.Count() descending, albumGroup.Key.Reviews.Count descending
                                 select albumGroup.Key;

            return hasBoughtItems.Take(30);
        }

        public IEnumerable<Album> GetMonthTopSellAlbums()
        {
            var startDate = DateTime.Today.AddDays(-30);
            var endDate=DateTime.Now;
            var monthOrders = db.Orders.Where(order=>order.OrderDate >= startDate && order.OrderDate <= endDate)
                                       .ToList();

            var topSellItems = new List<OrderDetail>();
            foreach (var order in monthOrders)
            {
                topSellItems.AddRange(order.OrderDetails);
            }

            var topSellAlbums = from od in topSellItems
                                group od by od.Album into albumGroup
                                orderby albumGroup.Count() descending, albumGroup.Key.Reviews.Count descending
                                select albumGroup.Key;

            return topSellAlbums.Take(15);
   
                     
        }

        //唱片排序的DropDowmSelect
        public ActionResult AlbumSortSelect(AlbumSort sort=AlbumSort.ByDateHightToLow)
        {
            var selectList=new SelectList(GetSortSelectList(), "Value", "Text", sort);
            return PartialView("SortSelect",selectList);
        }

        //唱片曲風篩選的DropDowmSelect
        public ActionResult AlbumGenreFilter(int genre = 0)
        {
            var selectList = new SelectList(db.Genres, "Id", "Name",genre);
            ViewData["Query"] = "genre"; //傳給partialview filter在下拉式選單選取後重新導向的選取值的參數名稱
            return PartialView("Filter",selectList);
        }


        private IEnumerable<SelectListItem> GetSortSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem(){Text="專輯名稱低到高",Value=AlbumSort.ByTitleLowToHight.ToString()},
                new SelectListItem(){Text="專輯名稱由高到低",Value=AlbumSort.ByTitleHightToLow.ToString()},
                new SelectListItem(){Text="日期由舊到新",Value=AlbumSort.ByDateLowToHight.ToString()},
                new SelectListItem(){Text="日期由新到舊",Value=AlbumSort.ByDateHightToLow.ToString()},
                new SelectListItem(){Text="人氣由高到低",Value=AlbumSort.ByPopularHightToLow.ToString()},
                new SelectListItem(){Text="人氣由低到高",Value=AlbumSort.ByPopularLowToHight.ToString()}
            };
        }


        [HttpGet]
        [Administrator]
        public ActionResult Create()
        {
            var editorViewModel = new AlbumEditorViewModel
            {
                ArtistSelectList=new SelectList(db.Artists,"Id","Name"),
                AlbumCatetorySelectList=new SelectList(db.Genres,"Id","Name")
            };

            return View(editorViewModel);
        }

        [HttpPost]
        [Administrator]
        [ValidateInput(false)]
        public ActionResult Create(AlbumEditorViewModel editorViewModel, HttpPostedFileBase albumCover)
        {

            if (albumCover != null && !isPicture(albumCover.FileName))
            {
                ModelState.AddModelError("albumCover", "您上傳的檔案格式有誤，只能上傳圖檔");
            }

            if (ModelState.IsValid)
            {
                Album album = editorViewModel.Album;
                db.Albums.Add(album);
                db.SaveChanges();
                SaveAlbumCover(album.Id, albumCover);
                //用來讓view存取新增成功的唱片資訊
                TempData["createdId"] = album.Id;
                TempData["createdName"] =album.Name;
                return RedirectToAction("Album","Manage");
            }
           
            editorViewModel.ArtistSelectList = new SelectList(db.Artists, "Id", "Name");
            editorViewModel.AlbumCatetorySelectList = new SelectList(db.Genres, "Id", "Name");
            return View(editorViewModel);
        }

        [HttpGet]
        [Administrator]
        public ActionResult Edit(int id)
        {
           
            Album album = db.Albums.FirstOrDefault(alb => alb.Id == id);
          
            if (album == null) return HttpNotFound();
            var createViewModel = new AlbumEditorViewModel
            {
                Album=album,
                ArtistSelectList = new SelectList(db.Artists, "Id", "Name",album.Artist.Id),
                AlbumCatetorySelectList = new SelectList(db.Genres, "Id", "Name",album.Genre.Id)
            };
           
            return View(createViewModel);
        }

        [HttpPost]
        [Administrator]
        [ValidateInput(false)]
        public ActionResult Edit(AlbumEditorViewModel editorViewModel, HttpPostedFileBase albumCover)
        {
            if (albumCover!=null && !isPicture(albumCover.FileName))
            {
                ModelState.AddModelError("albumCover", "您上傳的檔案格式有誤，只能上傳圖檔");
            }
            Album newAlbum=editorViewModel.Album;
            Album oldAlbum = db.Albums.FirstOrDefault(alb => alb.Id == newAlbum.Id);
            
            if (oldAlbum !=null && ModelState.IsValid)
            {
                oldAlbum.Name = newAlbum.Name;
                oldAlbum.Description = newAlbum.Description;
                oldAlbum.PublicDate = newAlbum.PublicDate;
                oldAlbum.Price = newAlbum.Price;
                oldAlbum.Stock = newAlbum.Stock;
                oldAlbum.ArtistId = newAlbum.ArtistId;
                oldAlbum.GenreId = newAlbum.GenreId;
                UpdateSongs(oldAlbum, newAlbum.Songs);
           
                db.SaveChanges();
                SaveAlbumCover(oldAlbum.Id, albumCover);
                //用來讓view存取更新成功的唱片資訊
                TempData["success"] = true;
                return RedirectToAction("Edit", new { id = oldAlbum.Id });
            }

            return View(editorViewModel);
        }

        private bool SaveAlbumCover(int id,HttpPostedFileBase albumCover)
        {
            try
            {
                if (albumCover != null)
                {
                    Image photo = Image.FromStream(albumCover.InputStream);
                    photo.Save(@"D:\Project\MvcRockShop\Album\" + id + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }catch(Exception ex)
            {
                return false;
            }
            return true;
            
        }



        private void UpdateSongs(Album oldAlbum,IEnumerable<Song> newSongs)
        {
            foreach (var oldSong in oldAlbum.Songs)
            {
                var newSong = newSongs.FirstOrDefault(song => song.Id == oldSong.Id);
                if (newSong != null)
                {
                    oldSong.Name = newSong.Name;
                    oldSong.Length = newSong.Length;
                    oldSong.SongNumber = newSong.SongNumber;
                }
            }
            RemoveSongs(oldAlbum, newSongs);
            InsertNewSongs(oldAlbum, newSongs);
        }

        private void InsertNewSongs(Album oldAlbum,IEnumerable<Song> newSongs)
        {
            foreach (var newSong in newSongs)
            {
                if (newSong.Id<=0 || !oldAlbum.Songs.Any(oldSong => oldSong.Id == newSong.Id))
                {
                    oldAlbum.Songs.Add(newSong);
                }
            }
        }

        private void RemoveSongs(Album oldAlbum, IEnumerable<Song> newSongs)
        {
            var removeIds = new List<int>();
            foreach (var oldSong in oldAlbum.Songs)
            {
                var newSong = newSongs.FirstOrDefault(song => song.Id == oldSong.Id);
                if (newSong == null)
                {
                    var dbSong = db.Songs.FirstOrDefault(song => song.Id == oldSong.Id);
                    if (dbSong != null)
                    {
                        removeIds.Add(oldSong.Id);
                    }
                }
            }
            removeIds.ForEach(id => db.Songs.Remove(db.Songs.Find(id)));
        }


        [HttpPost]
        [Administrator]
        public ActionResult Delete(int id)
        {
            Album album=db.Albums.FirstOrDefault(alb => alb.Id == id);
            if (album != null)
            {
                try
                {
                    //在Detail頁面刪除完成後會導向下一張專輯的頁面,若無下一張,則回到分類頁面
                    string redirectUrl = "";
                    var albumsOfThisGenre = db.Genres.First(g => g.Id == album.Genre.Id).Albums.Sort(AlbumSort.ByDateHightToLow);
                    int where = albumsOfThisGenre.AtWhere(album);
                    Album nextAlbum = albumsOfThisGenre.ElementAtOrDefault(where + 1);
                    if (nextAlbum == null)
                    {
                        redirectUrl = Url.Action("Albums", "Genre", new { id = album.Genre.Id }, null);
                    }
                    else
                    {
                        redirectUrl = Url.Action("Detail", "Album", new { id = nextAlbum.Id }, null);
                    }
                    db.Albums.Remove(album);
                    db.SaveChanges();
                    //用來讓view存取新增成功的唱片資訊
                    TempData["deletedName"] = album.Name;
                    return Json(new { redirectUrl=redirectUrl });
                }
                catch(Exception ex)
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
