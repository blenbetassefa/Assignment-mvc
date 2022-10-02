using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Web2.Models.Service;

namespace Web2.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBook _service;
        static int userId;          // Used to store user id.
        static string userName;
        public BorrowController(IBook service)
        {
            _service = service;
        }
        public IActionResult Index(int userId, string userName)
        {
            if (userId == null)
            {
                return View("NotFound");
            }
            var user = _service.GetById(userId);
            if (user == null)
            {
                return View("NotFound");
            }

            BorrowController.userId = (int)userId;
            BorrowController.userName = userName;
            var hold = _service.GetAll();
            return View(hold);
        }

        public ActionResult Requested()
        {
            return RedirectToAction("Requested", "UserTransaction", new { userId = userId });
        }
    }
}
