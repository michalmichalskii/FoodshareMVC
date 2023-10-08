using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
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

        public int UpdateUser(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).Property("FirstName").IsModified = true;
            _context.Entry(user).Property("LastName").IsModified = true;
            _context.Entry(user).Property("Email").IsModified = true;
            _context.Entry(user).Property("PhoneNumber").IsModified = true;
            _context.Entry(user).Property("Street").IsModified = true;
            _context.Entry(user).Property("FlatNumber").IsModified = true;
            _context.Entry(user).Property("PostalCode").IsModified = true;
            _context.Entry(user).Property("City").IsModified = true;
            _context.Entry(user).Property("Country").IsModified = true;
            _context.SaveChanges();
            return user.Id;
        }

        public User GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public User GetUserWithDetails(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }
        public IQueryable<Post> GetAllUserActivePosts(int userId)
        {
            var posts = _context.Posts.Where(p => p.CreatorId == userId);
            return posts;
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }
    }
}
