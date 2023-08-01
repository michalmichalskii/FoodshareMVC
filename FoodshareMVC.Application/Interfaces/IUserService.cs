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
        int AddReview(NewReviewVm model);
        UserDetailVm GetUserDetail(int id);
        UserWithPostsAndReviewsVm GetUserWithActivePostsAndGottenReviews(int id);
    }
}
