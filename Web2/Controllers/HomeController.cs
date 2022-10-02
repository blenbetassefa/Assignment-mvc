using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web2.Models;
using Web2.Models.Service;
using Web2.Models.NonDatabaseModels;
using System.Web;

namespace Web2.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibContext _context;

        public HomeController(LibContext service)
        {
            _context=service;       
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(comment co)
        {
            return View("message"); 
        }
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginUser userModel)
        {
            if (!ModelState.IsValid) return View("Login", userModel);

            var ac = _context.Accounts.SingleOrDefault(a => a.UserName == userModel.UserName && a.AccountPassword == userModel.Password);
            if (ac == null)
            {
                userModel.LoginErrorMessage = "Incorrect Username or Password.";
                return View("Login", userModel);
            }
            if (ac.AccountType.Equals("Administrator") || ac.AccountType.Equals("Librarian"))
            {
                var ad = _context.Admins.Where(x => x.AdminUserName == ac.UserName).FirstOrDefault();
                if (ad != null)
                {
                    HttpContext.Session.SetString("UserName", ad.AdminUserName);
                    HttpContext.Session.SetString("Name", ad.AdminName);
                    HttpContext.Session.SetInt32("ID", ad.AdminId);
                    HttpContext.Session.SetString("AccountType", "Administrator");
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    userModel.LoginErrorMessage = "Error! Contact Administrator.";
                    return View("Login", userModel);

                }

            }
            var mb = _context.Members.Where(x => x.MemberUserName == ac.UserName).FirstOrDefault();
            if (mb != null)
            {
                HttpContext.Session.SetString("UserName", mb.MemberUserName);
                HttpContext.Session.SetString("Name", mb.MemberName);
                HttpContext.Session.SetInt32("ID", mb.MemberId);
                HttpContext.Session.SetString("AccountType", "Member");
                return RedirectToAction("Index", "Member");

            }
            else
            {
                userModel.LoginErrorMessage = "Error! Contact Administrator.";
                return View("Login", userModel);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult AdminList()
        {
            var r = _context.Admins.ToList();
            return View(r);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

}

