using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public List<ReviewForListVm> GetAllReviewsOfUser(string userId)
        {
            var reviews = _reviewRepository.GetAllReviews()
                .Where(p => p.ReviewedUserId == userId.ToString())
                .ProjectTo<ReviewForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            return reviews;

        }
        public int AddReview(NewReviewVm newReview)
        {
            var review = _mapper.Map<Review>(newReview);
            var id = _reviewRepository.AddReview(review);
            return id;
        }
        public void UpdateReview (NewReviewVm model)
        {
            model.CreateDateTime = DateTime.Now;
            var review = _mapper.Map<Review>(model);
            _reviewRepository.UpdateReview(review);
        }

        public ListReviewForListVm GetAllReviewsForList(int pageSize, int pageNo, string name)
        {
            var reviews = _reviewRepository.GetAllReviews()
                .Where(p => (p.ReviewedUser.FirstName + " " + p.ReviewedUser.LastName).StartsWith(name))
                .ProjectTo<ReviewForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var revewsToShow = reviews
                .Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ToList();

            var reviewList = new ListReviewForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                Reviews = revewsToShow,
                Count = reviews.Count
            };
            return reviewList;
        }

        public decimal GetStarAverage(UserVm model)
        {
            var reviews = model.MyReviews.Reviews;
            int countOfReviews = reviews.Count;
            decimal sumOfStars = 0;

            foreach (var review in reviews)
            {
                sumOfStars += review.AmountOfStars;
            }
            if (countOfReviews > 0)
            {
                var starAverage = sumOfStars / countOfReviews;
                model.StarAverage = starAverage;
            }

            return model.StarAverage;
        }
    }
}
