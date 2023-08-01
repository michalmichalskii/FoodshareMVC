﻿using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Interfaces
{
    public interface IPostRepository 
    { 
        void DeletePost(int postId);

        int AddPost(Post post);
            
        void UpdatePost(Post post);
        Post GetPost(int id);

        IQueryable<Post> GetAllPosts();
        IQueryable<Post> GetAllActivePosts();
    }
}
