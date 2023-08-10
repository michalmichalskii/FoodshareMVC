using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FoodshareMVC.Domain.Interfaces
{
    public interface IReviewRepository
    {
        void DeleteReview(int reviewId, int reviewMakerId);

        int AddReview(Review review);

        int UpdateReview(Review review);

        IQueryable<Review> GetAllReviewsAboutUser(int userId);
        IQueryable<Review> GetAllReviews();
    }
}
