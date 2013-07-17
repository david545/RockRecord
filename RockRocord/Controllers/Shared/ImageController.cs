using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using DrawExtension;
namespace RockRecord.Controllers.Shared
{
    public class ImageController : Controller
    {
        private MemoryStream GraphicsPictureToStream(string filePath,int size,NarrowType narrowType)
        {
            var ms = new MemoryStream();
            Image.FromFile(filePath)
                 .NarrowImage(size, size, narrowType)
                 .SaveAndDispose(ms, System.Drawing.Imaging.ImageFormat.Jpeg, 100L);
            return ms;
                
        }

        public ActionResult GraphicsUserPhoto(int id, int size)
        {
            string filePath=@"D:\Project\MvcRockShop\User\"+id+@"\photo.jpg";
            if (!System.IO.File.Exists(filePath))
                filePath = @"D:\Project\MvcRockShop\User\no-userphoto.gif";
            return File(GraphicsPictureToStream(filePath, size,NarrowType.ReachedNarrowSize).ToArray(), "image/jpeg");
        }

        public ActionResult GraphicsAlbumCover(int id,int size)
        {
            string filePath = @"D:\Project\MvcRockShop\Album\" + id + ".jpg";
            if (!System.IO.File.Exists(filePath))
                filePath = @"D:\Project\MvcRockShop\Album\" + "no-image.jpg";
            return File(GraphicsPictureToStream(filePath,size,NarrowType.WholeImage).ToArray(),"image/jpeg");
        }

        public ActionResult GraphicsNotCover(int size)
        {
            string filePath = @"D:\Project\MvcRockShop\Album\" + "no-image.jpg";
            return File(GraphicsPictureToStream(filePath, size, NarrowType.WholeImage).ToArray(), "image/jpeg");
        }

        public ActionResult GraphicsArtistSmall(int id, int size)
        {
            string filePath = @"D:\Project\MvcRockShop\Artist\" + id + "_0.jpg";
            if (!System.IO.File.Exists(filePath))
                filePath = @"D:\Project\MvcRockShop\Album\" + "no-image.jpg";
            return File(GraphicsPictureToStream(filePath, size, NarrowType.ReachedNarrowSize).ToArray(), "image/jpeg");
        }

        public ActionResult GraphicsArtistLarge(int id, int size)
        {
            string filePath = @"D:\Project\MvcRockShop\Artist\" + id + "_0.jpg";
            if (!System.IO.File.Exists(filePath))
                filePath = @"D:\Project\MvcRockShop\Album\" + "no-image.jpg";
            return File(GraphicsPictureToStream(filePath, size,NarrowType.WholeImage).ToArray(), "image/jpeg");
        }

        public ActionResult GraphicsSong(int size)
        {
            string filePath = @"D:\Project\MvcRockShop\Album\" + "no-image.jpg";
            return File(GraphicsPictureToStream(filePath, size, NarrowType.WholeImage).ToArray(), "image/jpeg");
        }

    }
}
