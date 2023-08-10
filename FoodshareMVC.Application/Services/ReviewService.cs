using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Domain.Interfaces;
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

        public List<ReviewForListVm> GetAllReviewsOfUser(int userId)
        {
            var reviews = _reviewRepository.GetAllReviews()
                .Where(p => p.ReviewedUserId == userId)
                .ProjectTo<ReviewForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            return reviews;

        }
    }
}
