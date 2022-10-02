using System.Collections.Generic;
using Web2.Models;
using Web2.Models.Service;
using Web2.Models.NonDatabaseModels;

namespace Web2.Models.Service
{
    public interface IAdmin
    {
        List<Admin> GetAll();
        void Add(Admin ad);
        void AddWithAccount(NewAdmin ad);
        Admin GetById(int Id);
        Admin GetByUsername(string uname);
        Admin Update (Admin ad);   
        void Delete(int id);
        public bool IfExists(string uname);
        public bool IfOtherExists(int Id, string Username);

    }
}
