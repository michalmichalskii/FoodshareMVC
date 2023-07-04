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
        public void DeletePost(int postId)
        {
            var post = _context.Users.Find(postId);
            if (post != null)
            {
                _context.Users.Remove(post);
                _context.SaveChanges();
            }
        }

        public int AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post.Id;
        }

        public int UpdatePost(Post post)//??????
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
            return post.Id;
        }

        public IQueryable<Post> GetAllPosts()
        {
            var posts = _context.Posts;
            return posts;
        }

        public IQueryable<Post> GetAllSpecyficUserPosts(User user)
        {
            var posts = _context.Posts.Where(p => p.User == user);
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

        //Booking
        public void DeleteBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }

        public int AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking.Id;
        }

        public IQueryable<Booking> GetAllBookings()
        {
            var bookings = _context.Bookings;
            return bookings;
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
