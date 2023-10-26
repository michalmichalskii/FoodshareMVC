using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Context _context;

        public ReviewRepository(Context context)
        {
            _context = context;
        }
        public int AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review.Id;
        }

        public void DeleteReview(int reviewId, int ReviewMakerId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Review> GetAllReviews()
        {
            var reviews = _context.Reviews;
            return reviews;
        }

        public IQueryable<Review> GetAllReviewsAboutUser(int userId)
        {
            var reviews = _context.Reviews.Where(u => u.ReviewedUserId == userId.ToString());
            return reviews;
        }

        public void UpdateReview(Review review)
        {
            _context.Attach(review);
            _context.Entry(review).Property("AmountOfStars").IsModified = true;
            _context.Entry(review).Property("ReviewDescription").IsModified = true;
            _context.SaveChanges();
        }

    }
}
