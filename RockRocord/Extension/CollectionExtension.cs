using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
namespace RockRecord.Extension
{
    public enum OrderSort
    {
        ByOrderIdHightToLow,
        ByOrderIdLowToHight,
        ByDateHightToLow,
        ByDateLowToHight
    }

    public enum AlbumSort
    {
        ByTitleHightToLow,
        ByTitleLowToHight,
        ByDateHightToLow,
        ByDateLowToHight,
        ByPopularHightToLow,
        ByPopularLowToHight
    }

    //擴充泛型集合的方法
    public static class CollectionExtension
    {
        public static IOrderedEnumerable<Album> Sort(this IEnumerable<Album> albums,AlbumSort sort)
        {
            switch (sort)
            {
                case AlbumSort.ByTitleHightToLow:
                    return albums.OrderByDescending(album => album.Name);
                case AlbumSort.ByTitleLowToHight:
                    return albums.OrderBy(album => album.Name);
                case AlbumSort.ByDateHightToLow:
                    return albums.OrderByDescending(album => album.PublicDate);
                case AlbumSort.ByDateLowToHight:
                    return albums.OrderBy(album => album.PublicDate);
                case AlbumSort.ByPopularHightToLow:
                    return albums.OrderByDescending(album => album.Popular);
                case AlbumSort.ByPopularLowToHight:
                    return albums.OrderBy(album => album.Popular);
                default:
                    return albums.OrderBy(album => album.Name);
            }
        }

        public static IOrderedEnumerable<OrderHeader> Sort(this IEnumerable<OrderHeader> orders, OrderSort sort)
        {
            switch (sort)
            {
                case OrderSort.ByDateHightToLow:
                    return orders.OrderByDescending(order => order.OrderDate);
                case OrderSort.ByDateLowToHight:
                    return orders.OrderBy(order => order.OrderDate);
                case OrderSort.ByOrderIdHightToLow:
                    return orders.OrderByDescending(order => order.Id);
                case OrderSort.ByOrderIdLowToHight:
                    return orders.OrderBy(order => order.Id);
                default:
                    return orders.OrderByDescending(order => order.OrderDate);
            }
        }

        public static int AtWhere<T>(this IEnumerable<T> items,T findItem)where T:class
        {
            int i = 0;
            foreach (var item in items)
            {
                if (item == findItem)
                    return i;
                i++;
            }
            return -1;
        }


    }
}