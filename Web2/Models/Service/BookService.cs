using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Web2.Models.Service
{

    public class BookService : IBook
    {
        private readonly LibContext _context;
        private readonly IWebHostEnvironment _host;
        public BookService(LibContext context , IWebHostEnvironment _hostEnvironment)
        {
            _context = context;
            _host = _hostEnvironment;
        }
        public void Add( Book book)
        {
            
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var actor = _context.Books.Find(id);
            _context.Remove(actor);
            _context.SaveChanges();
        }
        public List<Book> GetAll()
        {
            var result = _context.Books.ToList();
            return result;
        }

        public Book GetById(int Id)
        {
            var result = _context.Books.Find(Id);

            return result;
        }
        public void Update(Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
            //return book;
        }
        public bool IfExists(string bookid)
        {
            var adm = _context.Books.Where(x => x.BookId == bookid).FirstOrDefault();
            bool exists = adm != null;
            return exists;

        }
        public bool IfOtherExists(int tblid, string bookid)
        {
            var adm = _context.Books.Where(x => x.BookTblId != tblid && x.BookId == bookid).FirstOrDefault();
            if(adm == null) return false;
            return true;

        }

    }
}
