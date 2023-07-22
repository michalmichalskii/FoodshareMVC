using AutoMapper;
using FoodshareMVC.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.User
{
    public class UserWithPostsVm
    {
        public UserVm User { get; set; }
        public IEnumerable<PostForListVm> UserPosts{ get; set; }
    }
}
