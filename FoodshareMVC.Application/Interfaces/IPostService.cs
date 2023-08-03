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
        ListPostForListVm GetAllActivePostsInYourCityForList(int pageSize, int pageNo, string searchString, string city);
        int AddPost(NewPostVm newPost);
        NewPostVm GetPostForEdit(int id);
        void UpdatePost(NewPostVm model);
        void DeletePost(int id);
        ListPostForListVm GetAllPostsForList();
        List<PostForListVm> GetAllActivePostsByCity(string city);
    }
}
