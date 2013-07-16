using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace RockRecord.Models.ViewModel
{
    public class AlbumDetailViewModel
    {
        public Album Album { get; set; }
        public bool isAdminustrator { get; set; }
        public Review LoginUserReview{get;set;}
        public int RecommandationItemCount { get; set; }
    }
}