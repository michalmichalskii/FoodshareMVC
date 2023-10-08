using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Reviews;
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
        int GetCurrentUserId(string currentUser);
        UserVm GetUserByEmail(string email);
        int AddReview(NewReviewVm model);
        UserDetailVm GetUserDetail(int id);
        UserVm GetUserWithActivePostsAndGottenReviews(int id);
        void UpdateUser(UserDetailVm userDetail);
        int AddProfileInfo(NewUserDetailVm model);
    }
}
