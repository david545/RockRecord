using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using RockRecord.Models;
using RockRecord.Models.ViewModel;
using RockRecord.Filters;
using RockRecord.Extension;

namespace RockRecord.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {

        [HttpGet]
        [AllowViewOrder]
        public ActionResult Detail(int id)
        {
            OrderHeader order=db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) RedirectToAction("OrderHistory", "Member");

            return View(order);
        }


        public ActionResult OrderStatusFilter(int status=0)
        {
            var selectList = new SelectList(db.OrderStatuses, "Id", "Name",status);
            ViewData["Query"] = "status"; //傳給partialview filter在下拉式選單選取後重新導向的選取值的參數名稱
            return PartialView("Filter",selectList);
        }

        public ActionResult OrderSortSelect(OrderSort sort = OrderSort.ByDateHightToLow)
        {
            var selectList = new SelectList(GetSortSelectList(), "Value", "Text", sort);
            return PartialView("SortSelect",selectList);
        }

        private IEnumerable<SelectListItem> GetSortSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem(){Text="訂單日期由高到低",Value=OrderSort.ByDateHightToLow.ToString()},
                new SelectListItem(){Text="訂單日期由低到高",Value=OrderSort.ByDateLowToHight.ToString()},
                new SelectListItem(){Text="訂單編號由高到低",Value=OrderSort.ByOrderIdHightToLow.ToString()},
                new SelectListItem(){Text="訂單日期由低到高",Value=OrderSort.ByOrderIdLowToHight.ToString()}
            };
        }
        
        [Administrator]
        public ActionResult OrderStatusSelect(int id)
        {
            System.Threading.Thread.Sleep(1000);
            OrderHeader order = db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return HttpNotFound();
            var seletList = new SelectList(db.OrderStatuses, "Id", "Name", order.OrderStatus.Id);
            return PartialView(seletList);
        }

        [HttpPost]
        [Administrator]
        public ActionResult UpdateOrderStauts(int id,int status)
        {
            System.Threading.Thread.Sleep(1000);
            OrderHeader order = db.Orders.FirstOrDefault(o => o.Id == id);
            OrderStatus orderStatus = db.OrderStatuses.FirstOrDefault(os => os.Id == status);
            if (order == null || orderStatus==null) return HttpNotFound();
            order.OrderStatus = orderStatus;
            db.SaveChanges();
            return Content(order.OrderStatus.Name);
        }

        [HttpPost]
        [Administrator]
        public ActionResult Delete(int id)
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                OrderHeader order = db.Orders.FirstOrDefault(o => o.Id == id);
                if (order == null) return HttpNotFound();
                db.Orders.Remove(order);
                db.SaveChanges();
                TempData["deletedName"] = order.Id;
                return new HttpStatusCodeResult(200, "刪除成功");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "刪除失敗");
            }
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            if (CartItems.Count <= 0)
                return RedirectToAction("Index","Cart");
            return View();
        }


        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            var order = new OrderHeader();
            order.OrderDate = DateTime.Now;
            if (TryUpdateModel(order))
            {
                var orderViewModel = new OrderConfirmViewModel
                {
                    Order = order,
                    CartItems = this.CartItems
                };
                return View("Confirm", orderViewModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(FormCollection form)
        {
            if (CartItems.Count <= 0)
                return RedirectToAction("Index", "Cart");
            var order = new OrderHeader();
            order.OrderDate = DateTime.Now;

            if (TryUpdateModel(order))
            {
                var orderViewModel = new OrderConfirmViewModel
                {
                    Order = order,
                    CartItems = this.CartItems
                };
                return View(orderViewModel);
            }
            return View("CheckOut");

        }

        [HttpPost]
        public ActionResult Finish(FormCollection form)
        {
            if (CartItems.Count <= 0)
                return RedirectToAction("Index", "Cart");
            var order = new OrderHeader();
            order.OrderDate = DateTime.Now;
            if (TryUpdateModel(order))
            {
                order.OrderDetails = GetOrderDetails();
                order.TotalPrice = CartItems.Sum(item => item.Price);
                order.Member = db.Members.First(m => m.Email == User.Identity.Name);
                order.OrderStatus = db.OrderStatuses.First(os => os.Id == 1);
                StockSellOut(order);
                db.Orders.Add(order);
                db.SaveChanges();
                CartItems.Clear();
                TempData["OrderId"] = order.Id;
                SendFinishedOrderMail(order);
                return RedirectToAction("Finish");
            }
            return View("CheckOut");

        }

        private void StockSellOut(OrderHeader order)
        {
            foreach (var od in order.OrderDetails)
            {
                od.Album.Stock -= od.Amount;
            }
        }

        [HttpGet]
        public ActionResult Finish()
        {
            if (TempData["OrderId"] != null)
            {
                ViewData["OrderId"] = TempData["OrderId"];
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        private bool SendFinishedOrderMail(OrderHeader order)
        {
            try
            {
                string mailBody = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/FinishedOrderMail.html"));
                mailBody = mailBody.Replace("{{OrderId}}", order.Id.ToString());
                mailBody = mailBody.Replace("{{Name}}", db.Members.First(m => m.Email == User.Identity.Name).Name);
                mailBody = mailBody.Replace("{{OrderDate}}", order.OrderDate.ToShortDateString());
                mailBody = mailBody.Replace("{{ContactName}}", order.ContactName);
                mailBody = mailBody.Replace("{{ContactAddress}}", order.Zipcode +
                    order.City + order.State + order.ContactAddress);
                mailBody = mailBody.Replace("{{ContactPhone}}", order.ContactPhone);

                var smtpSever = new SmtpClient("smtp.gmail.com");
                smtpSever.Port = 587;
                smtpSever.Credentials = new System.Net.NetworkCredential("exile1030@gmail.com", "exile0204");
                smtpSever.EnableSsl = true;
                var mailMsg = new MailMessage
                {
                    From = new MailAddress("exile1030@gmail.com"),
                    Subject = "(RockRecord)您訂購了我們的產品",
                    Body = mailBody,
                    IsBodyHtml = true
                };
                mailMsg.To.Add(new MailAddress(User.Identity.Name));

                smtpSever.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

          
        }

        private List<OrderDetail> GetOrderDetails()
        {
            return CartItems.ConvertAll(item =>
            {
                return new OrderDetail
                {
                    Album=db.Albums.Find(item.Album.Id),
                    Price=item.Price,
                    Amount=item.Amount
                };
            });
        }

    }
}
