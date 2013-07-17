using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockRecord.Models.ViewModel
{
    public class NavigaterViewModel
    {
        public Member Member { get; set; }
        public List<Genre> AlbumCategories { get; set; }
    }
}