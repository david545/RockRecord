using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.Security;
using RockRecord.Models;
using AllMusic;
using WebHelper;
namespace RockRecord.Controllers
{
    //純粹測試的控制器
    public class BackgroundController : BaseController
    {
        private string pwSalt = "asliudfgisugakljsvblkfasjvbikwrgv";
        //
        // GET: /Background/

        public ActionResult AddOrderStatus()
        {
            db.Orders.ToList().ForEach(o => o.OrderStatus = db.OrderStatuses.First());
            db.SaveChanges();
            return Content("OK");
        }


        public ActionResult Delete()
        {
            db.Albums.Remove(db.Albums.Find(64));
            db.Albums.Remove(db.Albums.Find(65));
            db.SaveChanges();
            return Content("Delete");
        }

        public ActionResult CreatePrice()
        {
            Random random = new Random();
          var albums = from a in db.Artists
                       select a.Albums;

            
            foreach (var group in albums)
            {
                foreach (var a in group)
                {
                    a.Price = random.Next(300, 550);
                }
            }
            db.SaveChanges();

            return Content("SHIT");
        }
   
        public ActionResult CreateUser()
        {

            for (int i = 0; i <= 20; i++)
            {
                var user = new Member
                {
                    Name="user"+i,
                    Email="email"+i+"@gmail.com",
                    Password= FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + "user"+i, "SHA1"),
                    ConfirmPassword=FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + "user"+i, "SHA1"),
                    RegisterDate=DateTime.Now,
                };
                var reviews = new List<Review>();
                reviews.Add(new Review
                {
                    Album=db.Albums.Find(178),
                    Comment="經典中的經典",
                    Rating=5,
                    ReviewDate=DateTime.Now
                });
                user.Reviews = reviews;
                db.Members.Add(user);
                db.SaveChanges();
                System.IO.Directory.CreateDirectory(@"D:\Project\MvcRockShop\User\" + user.Id);
            }
            return Content("QQ");

        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = db.Genres;
            var selectList = new SelectList(categories, "Id", "Name",3);
            ViewData["selectList"] = selectList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Artist artist,int category)
        {
            ArtistPage artistPage = SearchEngine.SearchArtist(artist.Name);
            DomToArtist(artistPage, artist,category);
            db.Artists.Add(artist);
            db.SaveChanges();
            SaveArtistPhoto(artist, artistPage);
            SaveCover(artistPage.AlbumPages, artist.Albums.ToList());
            return Content("新增成功");
        }



        private void DomToArtist(ArtistPage artistPage,Artist artist,int categoryId)
        {
            artist.Albums = DomsToAlbums(artistPage.AlbumPages, categoryId);
        }

        private void DomToArtists(List<ArtistPage> artistPages, List<Artist> artists,int categoryId)
        {
            for (int i = 0; i < artistPages.Count; i++)
            {
                artists[i].Albums = DomsToAlbums(artistPages[i].AlbumPages, categoryId);
            }
        }

        private Album DomToAlbum(AlbumPage albumPage, Genre category)
        {
            return new Album
            {
                PublicDate = albumPage.ReleaseDate,
                Genre = category,
                Name = albumPage.Title,
                Songs=DomsToSongs(albumPage.SongRows)
            };

        }

        private List<Album> DomsToAlbums(List<AlbumPage> albumPages,int categoryId)
        {
            var category = db.Genres.Find(categoryId);
            var albums = new List<Album>();
            albumPages.ForEach(ap => albums.Add(DomToAlbum(ap, category)));
            return albums;
        }

        private void SaveArtistPhoto(Artist artist, ArtistPage artistPage)
        {
            for (int i = 0; i < artistPage.ArtistPhotos.Count; i++)
            {
                Image img=Image.FromStream(HtmlRequestHelper.GetStream(artistPage.ArtistPhotos[i],artistPage.ArtistPhotos[i]));
                img.Save(@"D:\Project\MvcRockShop\Artist\"+artist.Id+"_"+i+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
            }
        }

        private void SaveCover(List<AlbumPage> albumPages, List<Album> albums)
        {
            for (int i = 0; i < albumPages.Count; i++)
            {
                if (!String.IsNullOrEmpty(albumPages[i].CoverImg))
                {
                    Image img = Image.FromStream(HtmlRequestHelper.GetStream(albumPages[i].CoverImg, albumPages[i].CoverImg));
                    img.Save(@"D:\Project\MvcRockShop\Album\" + albums[i].Id + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    img.Dispose();
                }
            }

        }

        private Song DomToSong(SongRow songRow)
        {
            return new Song
            {
                Name=songRow.Title,
                Length=songRow.Length
            };
        }

        private List<Song> DomsToSongs(List<SongRow> songRows)
        {
            var songs=new List<Song>();
            songRows.ForEach(sr => songs.Add(DomToSong(sr)));
            return songs;
        }

       


    }
}
