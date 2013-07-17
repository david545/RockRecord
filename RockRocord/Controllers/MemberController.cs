using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;
using System.Text.RegularExpressions;
using PagedList;
using RockRecord.Models;
using RockRecord.Filters;
using RockRecord.Helper;
using System.IO;
using System.Drawing;

namespace RockRecord.Controllers
{
    public class MemberController : BaseController
    {
      
        private string pwSalt = "asliudfgisugakljsvblkfasjvbikwrgv";
        public ActionResult Index(int id,int p=1)
        {
            var member=db.Members.Find(id);
            ViewData["ReviewList"] = member.Reviews.OrderByDescending(r=>r.ReviewDate)
                                           .ToPagedList(pageNumber: p, pageSize: 10);

            return View(member);
        }

        [Authorize]
        public ActionResult MyAccount()
        {
            return View("Edit", db.Members.First(m => m.Email == User.Identity.Name));
        }

        [Authorize]
        public ActionResult ReviewHistory(int p=1)
        {
            var member = db.Members.First(m=>m.Email==User.Identity.Name);
            var reviews= member.Reviews.OrderByDescending(r => r.ReviewDate)
                                       .ToPagedList(pageNumber: p, pageSize: 10);
            return View(reviews);

        }

        [Authorize]
        public ActionResult OrderHistory(int p=1,int status=0)
        {
            return View(db.Members.First(m =>m.Email==User.Identity.Name)
                                  .Orders.Where(o=>status==0|| o.OrderStatus.Id==status)
                                  .OrderByDescending(o=>o.OrderDate)
                                  .ToPagedList(p,10));
        }

        private SelectList GetOrderStatusFilter(string filter)
        {
            var items=new List<SelectListItem>();
            items.Add(new SelectListItem { Value ="All", Text = "全部" });
            foreach (var item in db.OrderStatuses)
            {
                items.Add(new SelectListItem {Value=item.Id.ToString(),Text=item.Name });
            }
            return new SelectList(items,"Value","Text", filter);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Exclude = "AuthCode,RegisterDate")]Member member)
        {

            if (CheckEmailHasBeUsed(member.Email))
            {
                ModelState.AddModelError("Email", "您輸入的Email已經有人註冊過了");
            }
            if (member.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "您的密碼長度必須大於8字元");
            }

            if (ModelState.IsValid)
            {
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + member.Password, "SHA1");
                member.ConfirmPassword = member.Password;
                member.RegisterDate = DateTime.Now;
                db.Members.Add(member);
                db.SaveChanges();
                //新增使用者的資料夾
                Directory.CreateDirectory(@"D:\Project\MvcRockShop\User\" + member.Id);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View();
            }

        }

        [Authorize]
        [HttpGet]
        [AllowEditMemberProfile]
        public ActionResult Edit()
        {
            var member = db.Members.FirstOrDefault(m => m.Email == User.Identity.Name);
            member.ConfirmPassword = member.Password;
            return View(member);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Member member,HttpPostedFileBase photoFile)
        {
            Member oldMember = db.Members.Find(member.Id);
          
            if (member.Password != oldMember.Password)
            {
                oldMember.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + member.Password, "SHA1");
                oldMember.ConfirmPassword = member.ConfirmPassword;
                if (member.Password != member.ConfirmPassword) ModelState.AddModelError("Password", "請輸入一致的密碼");
            }
            if (photoFile != null)
                if (!isPicture(photoFile.FileName)) ModelState.AddModelError("photoFile", "您上傳的格式不正確，請上傳圖檔");
            if(member.Email!=oldMember.Email && CheckEmailHasBeUsed(member.Email))
                ModelState.AddModelError("Email", "此Email已經被使用過");

            if (ModelState.IsValid)
            {
                oldMember.Name = member.Name;
                oldMember.Email = member.Email;
                oldMember.Biography = member.Biography;
                oldMember.ConfirmPassword = oldMember.Password;
                if (photoFile != null)
                {
                    Image photo = Image.FromStream(photoFile.InputStream);
                    photo.Save(@"D:\Project\MvcRockShop\User\" + member.Id + @"\photo.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(member.Email, false);
                return RedirectToAction("MyAccount");
            }

            return View(member);
        }


        private bool CheckEmailHasBeUsed(string Email)
        {
            Member member = db.Members.Where(m => m.Email == Email).FirstOrDefault();
            if (member == null)
                return false;
            else
                return true;
        }



        [Authorize]
        public ActionResult GetMember()
        {
            Member member = db.Members.Where(m=>m.Email==User.Identity.Name).FirstOrDefault();
            if (member != null)
            {
                return Content(member.Name);
            }
            else
            {
                return HttpNotFound();
            }
        }
      

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            if(ValidateUser(email,password))
            {
                FormsAuthentication.SetAuthCookie(email, false);
              
                if (String.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(returnUrl);
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index","Home");
        }

        private bool ValidateUser(string email, string password)
        {
            string hash_pw = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + password, "SHA1");
            var member = (from m in db.Members
                          where m.Email==email && m.Password == hash_pw
                          select m).FirstOrDefault();
            if (member != null)
            {
                return true;
            }
            else
            {
                ModelState.AddModelError("", "您輸入的帳號密碼有誤");
                return false;
            }
        }

    }
}
