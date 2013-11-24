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
        public IPagedList<Album> Get(int genreId, int page, int size)
        {
            var albums = db.Albums.Where(a => a.GenreId == genreId).OrderByDescending(a => a.PublicDate).ToPagedList(page, size);

            return albums;
        }

    }
}
