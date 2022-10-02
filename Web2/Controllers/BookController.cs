using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using Web2.Models;
using Web2.Models.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web2.Models.NonDatabaseModels;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace Web2.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _service;
        private readonly IWebHostEnvironment _host;
        public BookController(IBook service , IWebHostEnvironment webHostEnvironment )
        {
            _service = service;
            _host=webHostEnvironment;   
        }
        [HttpGet]
        public IActionResult Index()
        {
            var hold = _service.GetAll();
            return View(hold);

        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }
        public IActionResult AddBook()
        {
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            return View();

        }
        [HttpPost]
        public IActionResult AddBook2(Book book)
        {
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            if (ModelState.IsValid)
            {
                string uiqueFileName = null;
                if(book.Img != null)
                {
                  string upload= Path.Combine(_host.WebRootPath, "images");
                    uiqueFileName= Guid.NewGuid().ToString() + "_" + book.Img.FileName;
                    string filepath = Path.Combine(upload, uiqueFileName);
                    book.Img.CopyTo(new FileStream(filepath,FileMode.Create));  
                }
                if (_service.IfExists(book.BookId))
                {
                    book.Error = "There is already a book with this Id.";
                    return View("AddBook", book);
                }
                book.BookImg = uiqueFileName;
                _service.Add(book);
                return RedirectToAction(nameof(Index));
            }

            return View("AddBook", book);
        }

        public IActionResult Edit(int id)

        {
            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            var data = _service.GetById(id);
            if (data == null) return RedirectToAction("Index", "Book");
            return View(data);

        }
        
        [HttpPost]
        public IActionResult Edit2(Book book)
        {


            var AccountType = HttpContext.Session.GetString("AccountType");
            if (!AccountType.Equals("Administrator") || AccountType == null) return RedirectToAction("Logout", "Home");
            if (!ModelState.IsValid) return View("Edit", book);
            
                string uiqueFileName = null;
            if (book.Img != null)
                {
                    string upload = Path.Combine(_host.WebRootPath, "images");
                    uiqueFileName = Guid.NewGuid().ToString() + "_" + book.Img.FileName;
                    string filepath = Path.Combine(upload, uiqueFileName);
                    book.Img.CopyTo(new FileStream(filepath, FileMode.Create));
                }
                
                if (_service.IfOtherExists(book.BookTblId, book.BookId))
                {
                    book.Error = "There is already a book with this Id.";
                    return View("Edit", book);
                }
                
                book.BookImg = uiqueFileName;

                _service.Update(book);
                return RedirectToAction(nameof(Index));
            
            
        }

        public IActionResult Delete(int id)
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }
        [HttpPost, ActionName("Delete")]

        public IActionResult Deleteconfirm(int id)
        {
            var AccountType2 = HttpContext.Session.GetString("AccountType");
            if (!AccountType2.Equals("Administrator") || AccountType2 == null) return RedirectToAction("Logout", "Home");
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MemberViewBook()
        {
            var hold = _service.GetAll();
            return View(hold);

        }

        public IActionResult MemberViewBookDetails(int id)
        {
            var hold = _service.GetById(id);
            return View(hold);

        }


    }
}
