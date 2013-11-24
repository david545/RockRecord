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
    public class GenreController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>    
        public List<Genre> Get()
        {
            var genres = db.Genres.ToList();

            return genres;
        }

    }
}
