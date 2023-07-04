using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Interfaces
{
    public interface IUserRepository
    {
        void DeleteUser(int userId);

        int AddUser(User user);

        int UpdateUser(User user);

        User GetUserById(int id);
    }
}
