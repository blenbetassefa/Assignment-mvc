using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.Models;
using Web2.Models.NonDatabaseModels;
using Web2.Models.Service;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace Web2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _service;
        public AccountController(IAccount service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var ac = _service.GetAll();
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            return View(ac);

        }

        public IActionResult CreateNewAdminAccount()
        {
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var ad = TempData.Peek<Admin>("admindata");
            //Admin ad = TempData["admindata"] as Admin;
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Section = "Create New Administrators";
            ViewBag.AccountType = "Administrator";
            ViewBag.AccountUsername = ad.AdminUserName;
            ViewBag.Error = TempData["CreateAccountError"] as string;
            return View();

        }
        [HttpPost]
        public IActionResult CreateNewAdminAccount2(string Username, string AccountType, string Password, string ConfirmPassword)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");

            TempData["CreateAccountError"] = "";

            if (Username == null || AccountType == null || Password == null || ConfirmPassword == null)
            {
                TempData["CreateAccountError"] = "Please Fill in all the inputs.";
                return RedirectToAction("CreateNewAdminAccount", "Account");
            }
            if (!Password.Equals(ConfirmPassword))
            {
                TempData["CreateAccountError"] = "Please Fill in all the inputs.";
                return RedirectToAction("CreateNewAdminAccount", "Account");

            }
            Account ac = new Account();
            ac.UserName = Username;
            ac.AccountPassword = Password;
            ac.AccountType = AccountType;
            _service.Add(ac);
            return RedirectToAction("AdminAdd3", "Admin");

        }
        public IActionResult CreateNewMemberAccount()
        {
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var mb = TempData.Peek<Member>("memberdata");
            //Member mb = TempData["memberdata"] as Member;
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Section = "Create New Member Account";
            ViewBag.AccountType = "Member";
            ViewBag.AccountUsername = mb.MemberUserName;
            ViewBag.Error = TempData["CreateAccountError"] as string;
            return View();

        }
        public IActionResult CreateCreateNewMemberAccount2(string Username, string AccountType, string Password, string ConfirmPassword)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");

            TempData["CreateAccountError"] = "";

            if (Username == null || AccountType == null || Password == null || ConfirmPassword == null)
            {
                TempData["CreateAccountError"] = "Please Fill in all the inputs.";
                return RedirectToAction("CreateNewMemberAccount", "Account");
            }
            if (!Password.Equals(ConfirmPassword))
            {
                TempData["CreateAccountError"] = "Please Fill in all the inputs.";
                return RedirectToAction("CreateNewMemberAccount", "Account");

            }
            Account ac = new Account();
            ac.UserName = Username;
            ac.AccountPassword = Password;
            ac.AccountType = AccountType;
            _service.Add(ac);
            return RedirectToAction("MemberAdd3", "Member");

        }

    }
}
