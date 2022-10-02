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
    public class TransactionController : Controller
    {
        // GET: TransactionController

        private readonly ITransaction _service;
        private readonly LibContext _context;
        public TransactionController(ITransaction service)
        {
            _service = service;
            _context = new LibContext();

        }

        public ActionResult AdminViewAll()
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.ToList();
            return View(tr);
        }

        public ActionResult AdminViewNew()
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Requested")).ToList();
            return View(tr);
        }

        public ActionResult AdminViewUnreturned()
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Accepted")).ToList();
            return View(tr);
        }

        public ActionResult AdminViewRejected()
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Rejected")).ToList();
            return View(tr);
        }

        public ActionResult AdminViewReturned()
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Returned")).ToList();
            return View(tr);
        }

        public ActionResult ApplyRequest(int id)
        {
            Book book = _context.Books.Where(x => x.BookTblId.Equals(id)).FirstOrDefault();
            if (book == null || book.BookStatus == "Unavailable") return RedirectToAction("MemberViewBook", "Book");
            return View(book);

        }

        public ActionResult ApplyRequest2(int id)
        {
            Book book = _context.Books.Where(x => x.BookTblId.Equals(id)).FirstOrDefault();
            if (book == null || book.BookStatus == "Unavailable") return RedirectToAction("MemberViewRejected", "Transaction");
            var uid = HttpContext.Session.GetInt32("ID");
            Member mb = _context.Members.Where(x => x.MemberId.Equals(uid)).FirstOrDefault();
            if (mb == null) return RedirectToAction("MemberViewReturned", "Transaction");
            Transaction ts = new Transaction();
            ts.MemberId = mb.MemberId;
            ts.MemberName = mb.MemberName;
            ts.TranStatus = "Requested";
            ts.TranDate = DateTime.Now;
            ts.BookIsbn = book.BookIsbn;
            ts.BookId = book.BookId;
            ts.BookTitle = book.BookTitle;
            _context.Transactions.Add(ts);
            _context.SaveChanges();

            return RedirectToAction("MemberViewBook", "Book");

        }


        public ActionResult MemberViewAll()
        {
            var userId = HttpContext.Session.GetInt32("ID");
            var tr = _context.Transactions.Where(x => x.MemberId.Equals(userId)).ToList();
            return View(tr);
        }

        public ActionResult MemberViewNew()
        {
            var uid = HttpContext.Session.GetInt32("ID");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Requested") && x.MemberId.Equals(uid)).ToList();
            return View(tr);
        }

        public ActionResult MemberViewUnreturned()
        {
            var uid = HttpContext.Session.GetInt32("ID");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Accepted") && x.MemberId.Equals(uid)).ToList();
            return View(tr);
        }

        public ActionResult MemberViewRejected()
        {
            var uid = HttpContext.Session.GetInt32("ID");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Rejected") && x.MemberId.Equals(uid)).ToList();
            return View(tr);
        }

        public ActionResult MemberViewReturned()
        {
            var uid = HttpContext.Session.GetInt32("ID");
            var tr = _context.Transactions.Where(x => x.TranStatus.Equals("Returned") && x.MemberId.Equals(uid)).ToList();
            return View(tr);
        }

        public ActionResult AcceptRequest(int id)
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.Where(x => x.TranId.Equals(id)).FirstOrDefault();
            var br = _context.Books.Where(x=> x.BookId.Equals(tr.BookId)).FirstOrDefault();
            if(tr == null || br == null) return RedirectToAction("AdminViewAll", "Transaction");
            br.BookStatus = "Unavailable";
            tr.TranStatus = "Accepted";
            _context.Books.Update(br);
            _context.Transactions.Update(tr);
            _context.SaveChanges();
            return RedirectToAction("AdminViewAll", "Transaction");
        }

        public ActionResult RejectRequest(int id)
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.Where(x => x.TranId.Equals(id)).FirstOrDefault();
            if (tr == null) return RedirectToAction("AdminViewAll", "Transaction");
            tr.TranStatus = "Rejected";
            _context.Transactions.Update(tr);
            _context.SaveChanges();
            return RedirectToAction("AdminViewAll", "Transaction");
        }

        public ActionResult MarkReturned(int id)
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var tr = _context.Transactions.Where(x => x.TranId.Equals(id)).FirstOrDefault();
            var br = _context.Books.Where(x => x.BookId.Equals(tr.BookId)).FirstOrDefault();
            if (tr == null || br == null) return RedirectToAction("AdminViewAll", "Transaction");
            br.BookStatus = "Available";
            tr.TranStatus = "Returned";
            _context.Books.Update(br);
            _context.Transactions.Update(tr);
            _context.SaveChanges();
            return RedirectToAction("AdminViewAll", "Transaction");
        }



    }
}
