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
    public class MemberController : Controller
    {
        private readonly IMember _service;
        private readonly LibContext _context;
        public MemberController(IMember service, LibContext context)
        {
            _service = service;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View();
        }
        public IActionResult ManageMembers()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var Mbs = _service.GetAll();
            return View(Mbs);
        }

        public IActionResult MemberCreate()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            return View();

        }
        [HttpPost]
        public IActionResult MemberCreate2(NewMember mb)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            if (!ModelState.IsValid) return View("MemberCreate", mb);

            if (_service.IfExists(mb.MemberUserName))
            {
                mb.AccountExistsErrorMessage = "There is already an account with such a username";
                return View("MemberCreate", mb);
            }
            _service.AddWithAccount(mb);
            return RedirectToAction("ManageMembers", "Member");
        }


        public IActionResult MemberDetails(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var Mbr = _service.GetById(id);
            if (Mbr == null) return RedirectToAction("ManageMembers", "Member");
            return View(Mbr);
        }
        public IActionResult MemberEdit(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var Mbr = _service.GetById(id);
            if (Mbr == null) return RedirectToAction("ManageMembers", "Member");
            return View(Mbr);

        }
        [HttpPost]
        public IActionResult MemberEdit2(Member mb)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");

            if (!ModelState.IsValid) return View("MemberEdit", mb);
            if (_service.IfOtherExists(mb.MemberId, mb.MemberUserName))
            {
                mb.Error = "There is already an account with this Id.";
                return View("MemberEdit", mb);
            }


            _service.Update(mb);
            return RedirectToAction("ManageMembers", "Member");
        }
        public IActionResult MemberDelete(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var Mbr = _service.GetById(id);
            if (Mbr == null) return RedirectToAction("ManageMembers", "Member");
            return View(Mbr);

        }
        [HttpPost]
        public IActionResult MemberDelete2(Member mb)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            _service.Delete(mb.MemberId);
            return RedirectToAction("ManageMembers", "Member");
        }

        public IActionResult UserAccount()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var Mb = _service.GetByUsername(ViewBag.Username);
            return View(Mb);
        }
        [HttpPost]
        public IActionResult ProfileEdit(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var Mb = _service.GetById(id);
            if (Mb == null) return RedirectToAction("UserAccount", "Member");
            return View(Mb);

        }
        [HttpPost]
        public IActionResult ProfileEdit2(Member mb)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");

            if (!ModelState.IsValid) return View("ProfileEdit", mb);
            if (_service.IfOtherExists(mb.MemberId, mb.MemberUserName))
            {
                mb.Error = "There is already an account with this Id.";
                return View("ProfileEdit", mb);
            }

            _service.Update(mb);
            return RedirectToAction("UserAccount", "Member");
        }


        public IActionResult ChangePassword()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View();
        }

        [HttpPost]
        public IActionResult ChangePasswords(ChangePassword cp)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Name = HttpContext.Session.GetString("Name");

            if (cp.OldPassword == null || cp.NewPassword1 == null || cp.NewPassword2 == null) return View("ChangePassword", cp);
            if (cp.NewPassword1.Equals(cp.NewPassword2))
            {
                var AccountDetails = _context.Accounts.Where(x => x.UserName == HttpContext.Session.GetString("UserName")).FirstOrDefault();
                if (AccountDetails.AccountPassword.Equals(cp.OldPassword))
                {
                    AccountDetails.AccountPassword = cp.NewPassword1;
                    _context.Update(AccountDetails);
                    _context.SaveChanges();
                    return RedirectToAction("UserAccount", "Member");
                }
                else
                {
                    cp.CPErrorMessage = "Wrong Password";
                    return View("ChangePassword", cp);
                }
            }
            else
            {
                cp.CPErrorMessage = "The Passwords should be equal";
                return View("ChangePassword", cp);
            }

        }

    }

}
