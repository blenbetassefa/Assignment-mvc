using System.Collections.Generic;
using System.Linq;

namespace Web2.Models.Service
{
    public class AccountService : IAccount
    {
        private readonly LibContext _context;
        public AccountService(LibContext context)
        {
            _context = context;
        }
        public void Add(Account ac)
        {
            _context.Accounts.Add(ac);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var actor = _context.Accounts.Find(id);
            _context.Remove(actor);
            _context.SaveChanges();
        }
        public List<Account> GetAll()
        {
            var result = _context.Accounts.ToList();
            return result;
        }

        public Account GetById(int Id)
        {
            var result = _context.Accounts.Find(Id);

            return result;
        }
        public Account GetByUsername(string uname)
        {
            var ac = _context.Accounts.Where(x => x.UserName == uname).FirstOrDefault();
            return ac;
        }
        public Account Update(Account ac)
        {
            _context.Update(ac);
            _context.SaveChanges();
            return ac;
        }
        public bool IfExists(Account ac)
        {
            var adm = _context.Accounts.Where(x => x.UserName == ac.UserName).FirstOrDefault();
            bool exists = adm != null;
            return exists;

        }
    }
}
