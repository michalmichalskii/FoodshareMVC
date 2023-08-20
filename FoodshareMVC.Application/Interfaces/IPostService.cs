using FoodshareMVC.Application.ViewModels.Post;
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
        ListPostForListVm GetAllActivePostsForList(int pageSize, int pageNo, string searchCreator, string city, string pickupMethod);
        ListPostForListVm GetAllActivePostsInYourCityForList(int pageSize, int pageNo, string? searchCreator, string? city, string? pickupMethod);
        int AddPost(NewPostVm newPost);
        NewPostVm GetPostForEdit(int id);
        void UpdatePost(NewPostVm model);
        void DeletePost(int id);
        ListPostForListVm GetAllPostsForList();
        List<PostForListVm> GetAllActivePostsByCity(string city);
        void SetPostActiveAfterBookingExpirationDateHasPassed();
        void SetPostNotActive(int postId);
    }
}
