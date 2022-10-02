using System.Collections.Generic;
using System.Linq;
using Web2.Models;
using Web2.Models.Service;
using Web2.Models.NonDatabaseModels;

namespace Web2.Models.Service
{
    public class MemberService : IMember
    {
        private readonly LibContext _context;
         public MemberService(LibContext context)
        {
            _context = context;
        }
        public void Add(Member mb)
        {
            _context.Members.Add(mb);
            _context.SaveChanges();
        }

        public void AddWithAccount(NewMember mb)
        {
            Member mb2 = new Member();
            mb2.MemberId = mb.MemberId;
            mb2.MemberUserName = mb.MemberUserName;
            mb2.MemberName = mb.MemberName;
            mb2.MemberGender = mb.MemberGender;
            mb2.MemberJoinedDate = mb.MemberJoinedDate;
            mb2.MemberPhone = mb.MemberPhone;
            mb2.MemberEmail = mb.MemberEmail;
            mb2.MemberDob = mb.MemberDob;

            Account ac = new Account();
            ac.UserName= mb.MemberUserName;
            ac.AccountPassword = mb.Password;
            ac.AccountType = "Member";

            _context.Members.Add(mb2);
            _context.Accounts.Add(ac);
            _context.SaveChanges();


        }

        public void Delete(int id)
        {
            var actor = _context.Members.Find(id);
            _context.Remove(actor);
            _context.SaveChanges();
        }
        public List<Member> GetAll()
        {
            var result = _context.Members.ToList();
            return result;
        }

        public Member GetById(int Id)
        {
            var result = _context.Members.Find(Id);

            return result;
        }
        public Member GetByUsername(string uname)
        {
            var ad = _context.Members.Where(x => x.MemberUserName == uname).FirstOrDefault();
            return ad;
        }
        public Member Update(Member mb)
        {
            _context.Update(mb);
            _context.SaveChanges();
            return mb;
        }
         public bool IfExists(string uname)
        {
            var adm = _context.Accounts.Where(x => x.UserName == uname).FirstOrDefault();
            bool exists = adm != null;
            return exists;

        }
        public bool IfOtherExists(int id, string username)
        {
            var adm = _context.Members.Where(x => x.MemberId != id && x.MemberUserName == username).FirstOrDefault();
            var mbr = _context.Admins.Where(x => x.AdminUserName == username).FirstOrDefault();
            if (adm == null && mbr == null) return false;
            return true;

        }

    }
}
