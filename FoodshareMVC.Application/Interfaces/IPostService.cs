using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Interfaces
{
    public interface IPostService
    {
        List<PostForListVm> GetAllUserPosts(string userId);
        ApplicationUser GetPostOwner(int postId);
        ListPostForListVm GetAllActivePostsForList(int pageSize, int pageNo, string searchCreator, string city, string pickupMethod);
        ListPostForListVm GetAllUserPostsForList(int pageSize, int pageNo, string user);
        int AddPost(NewPostVm newPost);
        NewPostVm GetPost(int id);
        void UpdatePost(NewPostVm model);
        void DeletePost(int id);
        ListPostForListVm GetAllPostsForList();
        List<PostForListVm> GetAllActivePostsByCity(string city);
        void SetPostActiveAfterBookingExpirationDateHasPassed();
        int SetPostNotActive(int postId);
    }
}
