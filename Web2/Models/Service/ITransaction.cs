using System.Collections.Generic;
namespace Web2.Models.Service
{
    public interface ITransaction
    {
        List<Account> GetAll();
        void Add(Account ac);

        Account GetById(int Id);
        Account GetByUsername(string uname);
        Account Update(Account ac);
        void Delete(int id);
        public bool IfExists(Account ac);

    }
}

