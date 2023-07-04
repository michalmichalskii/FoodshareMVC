using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FoodshareMVC.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public int UpdateUser(User user)//??????
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user.Id;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
