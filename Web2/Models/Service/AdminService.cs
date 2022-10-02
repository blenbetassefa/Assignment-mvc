using System.Collections.Generic;
using System.Linq;
using Web2.Models;
using Web2.Models.Service;
using Web2.Models.NonDatabaseModels;

namespace Web2.Models.Service
{
    public class AdminService : IAdmin
    {
        private readonly LibContext _context;
         public AdminService(LibContext context)
        {
            _context = context;
        }
        public void Add(Admin ad)
        {
            _context.Admins.Add(ad);
            _context.SaveChanges();
        }

        public void AddWithAccount(NewAdmin ad)
        {
            Admin ad2 = new Admin();
            ad2.AdminId = ad.AdminId;
            ad2.AdminName = ad.AdminName;
            ad2.AdminGender = ad.AdminGender;
            ad2.AdminJoinedDate = ad.AdminJoinedDate;
            ad2.AdminEmail = ad.AdminEmail;
            ad2.AdminPhone = ad.AdminPhone;
            ad2.AdminUserName = ad.AdminUserName;
            ad2.AdminDob = ad.AdminDob;

            Account ac = new Account();
            ac.UserName=ad.AdminUserName;
            ac.AccountPassword = ad.Password;
            ac.AccountType = "Administrator";
            _context.Admins.Add(ad2);
            _context.Accounts.Add(ac);
            _context.SaveChanges();


        }

        public void Delete(int id)
        {
            var actor = _context.Admins.Find(id);
            _context.Remove(actor);
            _context.SaveChanges();
        }
        public List<Admin> GetAll()
        {
            var result = _context.Admins.ToList();
            return result;
        }

        public Admin GetById(int Id)
        {
            var result = _context.Admins.Find(Id);

            return result;
        }
        public Admin GetByUsername(string uname)
        {
            var ad = _context.Admins.Where(x => x.AdminUserName == uname).FirstOrDefault();
            return ad;
        }
        public Admin Update(Admin ad)
        {
            _context.Update(ad);
            _context.SaveChanges();
            return ad;
        }
         public bool IfExists(string uname)
        {
            var adm = _context.Accounts.Where(x => x.UserName == uname).FirstOrDefault();
            bool exists = adm != null;
            return exists;

        }
        public bool IfOtherExists(int id, string username)
        {
            var adm = _context.Admins.Where(x => x.AdminId != id && x.AdminUserName == username).FirstOrDefault();
            var mbr = _context.Members.Where(x => x.MemberUserName == username).FirstOrDefault();
            if (adm == null && mbr == null) return false;
            return true;

        }

    }
}
