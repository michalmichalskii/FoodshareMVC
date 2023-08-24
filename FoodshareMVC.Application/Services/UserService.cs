﻿using AutoMapper;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Interfaces;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Domain.Models.BaseInherited;

namespace FoodshareMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public UserVm GetUserWithActivePostsAndGottenReviews(int id)
        {
            var user = _userRepository.GetUser(id);
            var mappedUser = _mapper.Map<UserVm>(user);

            var posts = _userRepository.GetAllUserActivePosts(id)
                .Where(p => p.IsActive == true)
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.CreateDateTime)
                .ToList();

            var reviews = _reviewRepository.GetAllReviewsAboutUser(id)
                .ProjectTo<ReviewForListVm>(_mapper.ConfigurationProvider)
                .OrderByDescending(r => r.CreateDateTime)
                .ToList();

            var model = new UserVm()
            {
                Id = id,
                City = mappedUser.City,
                FullName = mappedUser.FullName,
                Rewievs = reviews,
                UserPosts = posts
            };
            return model;
        }

        public UserDetailVm GetUserDetail(int id)
        {
            var user = _userRepository.GetUserWithDetails(id);
            var userVm = _mapper.Map<UserDetailVm>(user);
            return userVm;
        }

        public int AddReview(NewReviewVm newReview)
        {
            var review = _mapper.Map<Review>(newReview);
            var id = _reviewRepository.AddReview(review);
            return id;
        }

        public void UpdateUser(UserDetailVm userDetail)
        {
            var user =_mapper.Map<User>(userDetail);
            _userRepository.UpdateUser(user);
        }
    }

}
