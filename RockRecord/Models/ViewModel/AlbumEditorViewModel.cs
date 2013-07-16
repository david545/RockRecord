using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RockRecord.Models.ViewModel
{
    public class AlbumEditorViewModel
    {
        public SelectList AlbumCatetorySelectList { get; set; }
        public SelectList ArtistSelectList { get; set; }
        public Album Album { get; set; }

    }
}