using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodshareMVC.Application.Helpers;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Tasks;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public int AddPost(NewPostVm newPost)
        {
            var post = _mapper.Map<Post>(newPost);
            var id = _postRepository.AddPost(post);
            return id;
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

        public ListPostForListVm GetAllActivePostsInYourCityForList(int pageSize, int pageNo, string searchString, string city)
        {
            
            var posts = _postRepository.GetAllActivePosts()
                .Where(p => p.City == city)
                .Where(p => p.Text.StartsWith(searchString))
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            var postsToShow = posts
                .Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ToList();
            var postList = new ListPostForListVm()
            {
                City = city,
                PageSize = pageSize,
                SearchString = searchString,
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

        public NewPostVm GetPostForEdit(int id)
        {
            var post = _postRepository.GetPost(id);
            var postVm = _mapper.Map<NewPostVm>(post);
            return postVm;
        }

        public void UpdatePost(NewPostVm model)
        {
            var post = _mapper.Map<Post>(model);
            _postRepository.UpdatePost(post);
        }
    }
}
