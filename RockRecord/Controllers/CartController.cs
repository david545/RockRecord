using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using RockRecord.Models;

namespace RockRecord.Controllers
{
    public class CartController : BaseController
    {


        public ActionResult Index()
        {
            return View(CartItems);
        }

        [HttpPost]
        public ActionResult AddToCart(int albumId)
        {
            CartItem inCartItem=CartItems.FirstOrDefault(item => item.Album.Id == albumId);
            if (inCartItem != null)
            {
                inCartItem.Amount = ++inCartItem.Amount;
            }
            else
            {
                var item = new CartItem
                {
                    Id=CartItems.Count()>0?CartItems.Last().Id+1:1,
                    Album = db.Albums.Find(albumId),
                    Amount = 1
                };
                CartItems.Add(item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateAmount(List<CartItem> updateCartItems)
        {
            Thread.Sleep(1000);
            if (ModelState.IsValid)
            {

                foreach (var item in CartItems)
                {
                    var updateCartItem = updateCartItems.FirstOrDefault(newItem => newItem.Id == item.Id);
                    if (updateCartItem != null)
                    {
                        //若要購買的數量大於存貨，則將購買數量減少至存貨數量
                        if (updateCartItem.Amount > item.Album.Stock)
                            item.Amount=item.Album.Stock;
                        else
                            item.Amount = updateCartItem.Amount;
                    }
                }
                return Json(
                    new { 
                            NewCartItems =CartItems.Select(item => 
                                new { 
                                        Id = item.Id, Amount = item.Amount, 
                                        Price = item.Price.ToString("C") 
                                     }),
                            Total=CartItems.Sum(i=>i.Price).ToString("C")
                         });
            }
            else
            {
                
                return Json(new { Error="更新失敗"});
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Thread.Sleep(1000);
            CartItem inCartItem = CartItems.FirstOrDefault(item => item.Id == id);
            if (inCartItem != null)
            {
                CartItems.Remove(inCartItem);
                return new HttpStatusCodeResult(200, "資料已刪除");
            }
            else
            {
                return HttpNotFound();
            }

        }


        public ActionResult NotItems()
        {
            return PartialView();
        }

    }
}
