using AutoMapper;
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

namespace FoodshareMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserWithPostsVm GetUserWithActivePosts(int id)
        {
            var user = _userRepository.GetUser(id);
            var mappedUser = _mapper.Map<UserVm>(user);
                
            var posts = _userRepository.GetAllUserActivePosts(id)
                .Where(p => p.IsActive == true)
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider).ToList();
            var model = new UserWithPostsVm()
            {
                User = mappedUser,
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
    }

}
