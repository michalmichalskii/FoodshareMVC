using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Commentaries;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Tasks;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ListPostForListVm GetAllPostsForList(int pageSize, int pageNo, string searchString)
        {
            var posts = _postRepository.GetAllActivePosts()
                .Where(p => p.Text.StartsWith(searchString))
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            var postsToShow = posts
                .Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ToList();
            var postList = new ListPostForListVm()
            {
                PageSize = pageSize,
                SearchString = searchString,
                CurrentPage = pageNo,
                Posts = postsToShow,
                Count = posts.Count
            };
            return postList;
        }
        public NewPostVm GetPostForEdit(int id)
        {
            var post = _postRepository.GetPost(id);
            var postVm = _mapper.Map<NewPostVm>(post);
            return postVm;
        }

        public void UpdateCustomer(NewPostVm model)
        {
            var post = _mapper.Map<Post>(model);
            _postRepository.UpdatePost(post);
        }
    }
}
