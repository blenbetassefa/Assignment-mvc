using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.Models;
using Web2.Models.Service;
using Web2.Models.NonDatabaseModels;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace Web2.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _service;
        private readonly LibContext _context;
        public AdminController(IAdmin service)
        {
            _service = service;
            _context = new LibContext();
        }

        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if(!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            return View();
        }

        public IActionResult UserAccount()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var AdminDetails = _service.GetByUsername(ViewBag.Username);
            return View(AdminDetails);
        }
        [HttpPost]
        public IActionResult ProfileEdit(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var Lib = _service.GetById(id);
            if (Lib == null) return RedirectToAction("UserAccount", "Admin");
            return View(Lib);

        }
        [HttpPost]
        public IActionResult ProfileEdit2(Web2.Models.Admin ad)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");

            if (!ModelState.IsValid) return View("ProfileEdit", ad);
            if (_service.IfOtherExists(ad.AdminId, ad.AdminUserName))
            {
                ad.Error = "There is already an account with this Id.";
                return View("ProfileEdit", ad);
            }

            _service.Update(ad);
            return RedirectToAction("UserAccount", "Admin");
        }


        public IActionResult ChangePassword()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult ChangePasswords(ChangePassword cp)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");

            if (cp.OldPassword == null || cp.NewPassword1 == null || cp.NewPassword2 == null) return View("ChangePassword", cp);
            if (cp.NewPassword1.Equals(cp.NewPassword2))
            {
                var AccountDetails = _context.Accounts.Where(x => x.UserName == HttpContext.Session.GetString("UserName")).FirstOrDefault();
                if (AccountDetails.AccountPassword.Equals(cp.OldPassword))
                {
                    AccountDetails.AccountPassword = cp.NewPassword1;
                    _context.Update(AccountDetails);
                    _context.SaveChanges();
                    return View("ChangePassword", cp);
                }
                else
                {
                    cp.CPErrorMessage = "Wrong Password";
                    return View("ChangePassword", cp);
                }
            }
            else
            {
                cp.CPErrorMessage = "";
                return View("ChangePassword", cp);
            }

        }

        public IActionResult ManageAdministrators()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var Libs = _service.GetAll();
            return View(Libs);
        }

        public IActionResult AdminCreate()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            return View();

        }
        [HttpPost]
        public IActionResult AdminCreate2(NewAdmin ad)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            if (!ModelState.IsValid) return View("AdminCreate", ad);

            if (_service.IfExists(ad.AdminUserName))
            {
                ad.AccountExistsErrorMessage = "There is already an account with such a username";
                return View("AdminCreate", ad);
            }
            _service.AddWithAccount(ad);
            return RedirectToAction("ManageAdministrators", "Admin");
        }


        public IActionResult AdminDetails(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var Lib = _service.GetById(id);
            if (Lib == null) return RedirectToAction("ManageAdministrators", "Admin");
            return View(Lib);
        }
        public IActionResult AdminEdit(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var Lib = _service.GetById(id);
            if (Lib == null) return RedirectToAction("ManageAdministrators", "Admin");
            return View(Lib);

        }
        [HttpPost]
        public IActionResult AdminEdit2(Web2.Models.Admin ad)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            if (!ModelState.IsValid) return View("AdminEdit", ad);
            if (_service.IfOtherExists(ad.AdminId, ad.AdminUserName))
            {
                ad.Error = "There is already an account with this Id.";
                return View("AdminEdit", ad);
            }
            _service.Update(ad);
            return RedirectToAction("ManageAdministrators", "Admin");
        }
        public IActionResult AdminDelete(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var Lib = _service.GetById(id);
            if (Lib == null) return RedirectToAction("ManageAdministrators", "Admin");
            return View(Lib);

        }
        [HttpPost]
        public IActionResult AdminDelete2(Web2.Models.Admin ad)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            _service.Delete(ad.AdminId);
            return RedirectToAction("ManageAdministrators", "Admin");
        }

    }
}
