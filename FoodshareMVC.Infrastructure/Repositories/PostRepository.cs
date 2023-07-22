using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models;
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

        public void DeleteComment(int commentId, int postId)
        {
            var findedPost = _context.Posts.Find(postId);
            if (findedPost != null)
            {
                var comment = _context.Comments.Find(commentId);
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    _context.SaveChanges();
                }
            }
        }

        public int AddComment(Comment comment, int postId)
        {
            var findedPost = _context.Posts.Find(postId);
            if (findedPost != null)
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return comment.Id;
            }
            return 0;
        }

        public int UpdateComment(Comment comment)//??????
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
            return comment.Id;
        }

        public IQueryable<Comment> GetAllCommentsInSecyficPost(int postId)
        {
            var comments = _context.Comments.Where(p => postId == p.PostId);
            return comments;
        }

        //Tags
        public void DeleteTag(int tagId)
        {
            var tag = _context.Tags.Find(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
        }

        public int AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return tag.Id;
        }

        public IQueryable<Tag> GetAllTags()
        {
            var tags = _context.Tags;
            return tags;
        }


    }
}
