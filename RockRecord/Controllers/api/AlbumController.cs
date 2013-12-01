using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RockRecord.Models.ViewModel.api;
using PagedList;
using RockRecord.Models;

namespace RockRecord.Controllers.api
{
    public class AlbumController : BaseController
    {
        public PageListViewModel<Album> Get(int genreId, int page, int size)
        {
            System.Threading.Thread.Sleep(700);
            var albums = db.Albums.Where(a => a.GenreId == genreId).OrderByDescending(a => a.PublicDate).ToPagedList(page, size);

            return new PageListViewModel<Album> { Items = albums, Size = albums.PageSize, Total = albums.TotalItemCount };
        }

    }
}
