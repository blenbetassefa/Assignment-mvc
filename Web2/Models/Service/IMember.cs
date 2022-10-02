using System.Collections.Generic;
using Web2.Models;
using Web2.Models.Service;
using Web2.Models.NonDatabaseModels;

namespace Web2.Models.Service
{
    public interface IMember
    {
        List<Member> GetAll();
        void Add(Member mb);
        void AddWithAccount(NewMember mb);
        Member GetById(int Id);
        Member GetByUsername(string uname);
        Member Update (Member mb);   
        void Delete(int id);
        public bool IfExists(string uname);
        public bool IfOtherExists(int id, string username);

    }
}
