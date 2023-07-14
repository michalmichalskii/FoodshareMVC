using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Interfaces
{
    public interface IPostRepository
    {
        //Post
        void DeletePost(int postId);

        int AddPost(Post post);
            
        void UpdatePost(Post post);
        Post GetPost(int id);

        IQueryable<Post> GetAllPosts();
        IQueryable<Post> GetAllActivePosts();

        IQueryable<Post> GetAllSpecyficUserPosts(User user);


        //Comments
        void DeleteComment(int commentId, int postId);

        int AddComment(Comment comment, int postId);

        int UpdateComment(Comment comment);

        IQueryable<Comment> GetAllCommentsInSecyficPost(int postId);

        //Tags
        void DeleteTag(int tagId);

        int AddTag(Tag tag);

        IQueryable<Tag> GetAllTags();

    }
}
