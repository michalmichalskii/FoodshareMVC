﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models;
using FoodshareMVC.Domain.Models.BaseInherited;
using FoodshareMVC.Infrastructure.Helpers.PhotoManage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoManager _photoManager;
        public PostService(IPostRepository postRepository, IMapper mapper, IPhotoManager photoManager)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _photoManager = photoManager;
        }

        public int AddPost(NewPostVm newPost)
        {
            var result = _photoManager.AddPhoto(newPost.Image);
            newPost.CreateDateTime = DateTime.Now;
            newPost.UpdateDateTime = null;
            var post = _mapper.Map<Post>(newPost);
            if (post.Image != null)
            {
                post.Image = result.Url.ToString();
            }
            var id = _postRepository.AddPost(post);
            return id;
        }
        public void UpdatePost(NewPostVm model)
        {
            var result = _photoManager.AddPhoto(model.Image);
            model.UpdateDateTime = DateTime.Now;
            var post = _mapper.Map<Post>(model);
            if (post.Image != null)
            {
                post.Image = result.Url.ToString();
            }
            _postRepository.UpdatePost(post);
        }
        public void DeletePost(int id)
        {
            _postRepository.DeletePost(id);
        }

        public List<PostForListVm> GetAllActivePostsByCity(string city)
        {
            var posts = _postRepository.GetAllActivePosts().Where(p => p.City == city)
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            return posts;
        }
        
        //TODO - change all parameters to one filter class
        public ListPostForListVm GetAllActivePostsForList(int pageSize, int pageNo, string searchCreator, string city, string pickupMethod)
        {
            var posts = _postRepository.GetAllActivePosts()
                .Where(p => p.City.StartsWith(city))
                .Where(p => p.PossibilityPickUpMethod.Contains(pickupMethod.ToString()))
                .Where(p => (p.User.FirstName + " " + p.User.LastName).StartsWith(searchCreator))
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            var postsToShow = posts
                .Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ToList();
            var postList = new ListPostForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                Posts = postsToShow,
                Count = posts.Count
            };
            return postList;
        }

        public ListPostForListVm GetAllUserPostsForList(int pageSize, int pageNo, string user)
        {
            var posts = _postRepository.GetAllActivePosts()
                .Where(p => (p.User.FirstName + " " + p.User.LastName).StartsWith(user))
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            var postsToShow = posts
                .Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ToList();
            var postList = new ListPostForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                Posts = postsToShow,
                Count = posts.Count
            };
            return postList;
        }

        public ListPostForListVm GetAllPostsForList()
        {
            var posts = _postRepository.GetAllPosts()
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            var postList = new ListPostForListVm()
            {
                Posts = posts
            };
            return postList;
        }

        public NewPostVm GetPost(int id)
        {
            var post = _postRepository.GetPost(id);
            var postVm = _mapper.Map<NewPostVm>(post);
            return postVm;
        }

        public void SetPostActiveAfterBookingExpirationDateHasPassed()
        {
            var posts = _postRepository.GetAllPosts()
                .Where(post => post.IsActive == false)
                .Where(post => post.Booking == null)
                .ToList();
            foreach (var post in posts)
            {
                _postRepository.SetPostActive(post.Id);
            }
        }
        public int SetPostNotActive(int postId)
        {
            _postRepository.SetPostNotActive(postId);
            return postId;
        }

        public ApplicationUser GetPostOwner(int postId)
        {
            var post = _postRepository.GetPost(postId);
            var postOwner = post.User;
            return postOwner;
        }

        public List<PostForListVm> GetAllUserPosts(string userId)
        {
            var listOfUserPosts = _postRepository.GetAllPosts()
                .Where(u => u.User.Id == userId.ToString())
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            return listOfUserPosts;
        }
    }
}
