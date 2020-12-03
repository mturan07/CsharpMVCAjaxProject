using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult UserControl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserControl(string UserName, string Password)
        {
            Kullanici kullanici = new _Giris().IsLoginSuccess(UserName, Password);
            if (kullanici != null)
            {
                return RedirectToAction("Index", "Portal");
            }
            return RedirectToAction("UserControl", "Login");
        }

        public ActionResult LogOut(int userId)
        {
            Session.Clear();
            
            new _Kullanici().LogCikis(userId);

            return RedirectToAction("UserControl", "Login");
        }
    }
}