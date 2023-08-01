using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.User
{
    public class UserWithPostsAndReviewsVm
    {
        public UserVm User { get; set; }
        public IEnumerable<ReviewForListVm> Rewievs { get; set; }
        public IEnumerable<PostForListVm> UserPosts { get; set; }
        public NewReviewVm NewReview { get; set; }
    }
}
