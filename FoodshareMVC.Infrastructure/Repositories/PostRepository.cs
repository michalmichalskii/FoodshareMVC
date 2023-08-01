using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly Context _context;

        public PostRepository(Context context)
        {
            _context = context;
        }

        //Post
        public void DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }

        public int AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post.Id;
        }

        public void UpdatePost(Post post)
        {
            _context.Attach(post);
            _context.Entry(post).Property("Text").IsModified = true;
            _context.SaveChanges();
        }
        public Post GetPost(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id).FirstOrDefault();
            return post;
        }

        public IQueryable<Post> GetAllPosts()
        {
            var posts = _context.Posts;
            return posts;
        }

        public IQueryable<Post> GetAllActivePosts()
        {
            var posts = _context.Posts.Where(post => post.IsActive);
            return posts;
        }

        //Comments

       

        //Tags
        


    }
}
