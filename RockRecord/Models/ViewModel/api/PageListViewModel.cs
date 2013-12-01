using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace RockRecord.Models.ViewModel.api
{
    public class PageListViewModel<T>
    {
        public IPagedList<T> Items { get; set; }

        public int Total { get; set; }

        public int Size { get; set; }
    }
}