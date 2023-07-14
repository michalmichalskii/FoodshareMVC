using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Interfaces
{
    public interface IUserService
    {
        UserVm GetUser(int id);
        UserDetailVm GetUserDetail(int id);
    }
}
