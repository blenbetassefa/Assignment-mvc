using System.Collections.Generic;

namespace Web2.Models.Service
{
    public interface IBook
    {
        List<Book> GetAll();
        void Add(Book book);        
      
        Book GetById(int Id);
        void Update (Book book);   
        void Delete(int id);
        public bool IfExists(string bookid);
        public bool IfOtherExists(int tblid, string bookid);


    }
}
