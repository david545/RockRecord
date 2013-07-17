using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Drawing;
using System.IO;
using DrawExtension;
using RockRecord.Models;


namespace RockRecord.Helper
{
    /*擴充一些常用在這個軟體的一些Helper*/
    public static class RockRecordHelper
    {
        /*用來匯出網站裡一些圖片的Helper,只要傳入Id和Size就會將圖片調整至適當大小然後繪製HTML*/
        #region ImageHelper  GraphicsNotCover
        public static MvcHtmlString NotCover(this HtmlHelper helper,int size, object htmlAttribute = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string imgUrl = urlHelper.Action("GraphicsNotCover", "Image", new { size = size });
            return CreateImgTag(helper, imgUrl, size, size, new RouteValueDictionary(htmlAttribute));
        }

        public static MvcHtmlString AlbumCover(this HtmlHelper helper,int id,int size,object htmlAttribute=null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string imgUrl = urlHelper.Action("GraphicsAlbumCover", "Image", new { id = id,size=size });
            return CreateImgTag(helper, imgUrl, size, size, new RouteValueDictionary(htmlAttribute));
        }

        public static MvcHtmlString AlbumCoverLink(this HtmlHelper helper, int id, int size, object htmlAttribute = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var imgMvcString=helper.AlbumCover(id, size);
            var link = new TagBuilder("a");
            link.MergeAttribute("href", urlHelper.Action("Detail", "Album", new { id = id }));
            link.InnerHtml = imgMvcString.ToString();
            return MvcHtmlString.Create(link.ToString());

        }


        #region 刪除的功能
        /*
        public static MvcHtmlString ArtistPhotoSmall(this HtmlHelper helper, int id, int size, object htmlAttribute = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string imgUrl = urlHelper.Action("GraphicsArtistSmall", "Image", new { id = id, size = size });
            return CreateImgTag(helper, imgUrl, size, size, new RouteValueDictionary(htmlAttribute));

        }
        
        //藝術家照片不會強制將圖片縮成要求的大小，而是將整張圖的比例做調整
        public static MvcHtmlString ArtistPhoto(this HtmlHelper helper, int id, int size, object htmlAttribute = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string imgUrl = urlHelper.Action("GraphicsArtistLarge", "Image", new { id = id, size = size });
            int width = size;
            int height =size;

            //計算圖片最終大小，讓前端設定和圖片相等的大小
            if (File.Exists(@"D:\Project\MvcRockShop\Artist\" + id + "_0.jpg"))
            {
                var img = Image.FromFile(@"D:\Project\MvcRockShop\Artist\" + id + "_0.jpg")
                               .NarrowImage(size, size, NarrowType.WholeImage);
                width = img.Width;
                height = img.Height;
                img.Dispose();
            }
            return CreateImgTag(helper, imgUrl, width,height, new RouteValueDictionary(htmlAttribute));
        }
        */
        #endregion

        public static MvcHtmlString SongPhoto(this HtmlHelper helper, int size, object htmlAttribute = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string imgUrl = urlHelper.Action("GraphicsSong", "Image", new {size = size });
            return CreateImgTag(helper, imgUrl, size, size, new RouteValueDictionary(htmlAttribute));
        }

        public static MvcHtmlString UserPhoto(this HtmlHelper helper, int id, int size, object htmlAttribute = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string imgUrl = urlHelper.Action("GraphicsUserPhoto", "Image", new { id = id, size = size });
            var routeDictionnary = new RouteValueDictionary(htmlAttribute);
            routeDictionnary.Add("class", "user-photo");
            return CreateImgTag(helper, imgUrl, size, size, routeDictionnary);
        }
        
        private static MvcHtmlString CreateImgTag(this HtmlHelper helper,string imgUrl, int width,int height, IDictionary<string,object> htmlAttribute = null)
        {
            var imgTag = new TagBuilder("img");
            imgTag.MergeAttribute("src", imgUrl);
            imgTag.MergeAttribute("height", height + "px");
            imgTag.MergeAttribute("width", width + "px");
            imgTag.MergeAttributes(htmlAttribute);

            return MvcHtmlString.Create(imgTag.ToString(TagRenderMode.SelfClosing));
        }
        #endregion

        /*其它的Helper*/
   
        #region OtherHelper

        public static MvcHtmlString ImageLink(this HtmlHelper helper,
                                              string actionName,
                                              string controllerName,
                                              string imageUrl,
                                              object routeValue = null,
                                              object htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var link = new TagBuilder("a");
            var img = new TagBuilder("img");
            link.MergeAttribute("href", urlHelper.Action(actionName,controllerName, routeValue));
            img.MergeAttribute("src", imageUrl);
            img.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            link.InnerHtml = img.ToString();
            return MvcHtmlString.Create(link.ToString());
        }

        public static MvcHtmlString AlbumTitle(this HtmlHelper helper,Album album)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var container = new TagBuilder("div");
            var albumTitle = new TagBuilder("a");
            var artistTitle = new TagBuilder("a");
            albumTitle.MergeAttribute("href", urlHelper.Action("Detail", "Album", new { id = album.Id }));
            albumTitle.MergeAttribute("class", "list-item-AlbumTitle");
            albumTitle.SetInnerText(album.Name);
            artistTitle.MergeAttribute("href", urlHelper.Action("Albums", "Artist", new { id = album.Artist.Id }));
            artistTitle.SetInnerText(album.Artist.Name);

            container.InnerHtml = albumTitle.ToString() + " by " + artistTitle.ToString();
            return MvcHtmlString.Create(container.ToString());
        }

        //會根據除傳入的selectedRouteName和selctedRouteValue來決定該listItem是否要被設為選取狀態
        public static MvcHtmlString MenuItem(this HtmlHelper helper,
                                             string linkText,
                                             string actionName,
                                             string controllerName,
                                             string selectedRouteName="action",
                                             object selectedRouteValue=null,
                                             object routeValue=null,
                                             object htmlAttributes=null)
        {
            var li = new TagBuilder("li");
            if (selectedRouteName == "action")
            {
                if (helper.ViewContext.RequestContext.RouteData.Values[selectedRouteName].ToString() == actionName)
                {
                    li.AddCssClass("selected");
                }
            }
            else if(helper.ViewContext.RequestContext.RouteData.Values[selectedRouteName]!=null &&
                   (helper.ViewContext.RequestContext.RouteData.Values[selectedRouteName].ToString() == selectedRouteValue.ToString()))
            {
                li.AddCssClass("selected");
            }
            li.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            li.InnerHtml=helper.ActionLink(linkText, actionName, controllerName, routeValue, htmlAttributes).ToString();
            return MvcHtmlString.Create(li.ToString());

        }


        #endregion

    }
}