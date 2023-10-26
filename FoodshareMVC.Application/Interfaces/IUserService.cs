using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Interfaces
{
    public interface IUserService
    {
        UserDetailVm GetUserDetailVmByEmail(string email);
        bool IsCompletedRegister(string id);
        UserVm GetUserVm(string id);
        UserVm GetUserVmByEmail(string email);
        UserDetailVm GetUserDetail(string id);
        int UpdateUser(UserDetailVm userDetail);
    }
}
