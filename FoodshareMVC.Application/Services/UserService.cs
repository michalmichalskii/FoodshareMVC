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
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Domain.Models.BaseInherited;
using System.Reflection.Metadata.Ecma335;
using FoodshareMVC.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodshareMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public UserService(IMapper mapper, UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userStore = userStore;
        }

        public UserDetailVm GetUserDetail(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            var mappedUser = _mapper.Map<UserDetailVm>(user);
            return mappedUser;
        }

        public int UpdateUser(UserDetailVm userDetail)
        {
            var user = _userManager.FindByIdAsync(userDetail.Id).Result;
            user.PhoneNumber = userDetail.PhoneNumber;
            user.FirstName = userDetail.FirstName;
            user.LastName = userDetail.LastName;
            user.Street = userDetail.Street;
            user.City = userDetail.City;
            user.FlatNumber = userDetail.FlatNumber;
            user.Country = userDetail.Country;
            user.PostalCode = userDetail.PostalCode;
            var updatedUser = Task.WaitAny(_userStore.UpdateAsync(user, CancellationToken.None));
            return updatedUser;
        }

        public UserVm GetUserVmByEmail(string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            var mappedUser = _mapper.Map<UserVm>(user);
            return mappedUser;
        }
        public UserDetailVm GetUserDetailVmByEmail(string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            var mappedUser = _mapper.Map<UserDetailVm>(user);
            return mappedUser;
        }

        public UserVm GetUserVm(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            var mappedUser = _mapper.Map<UserVm>(user);
            return mappedUser;
        }

        public bool IsCompletedRegister(string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (user.City == null
                || user.Country == null
                || user.FirstName == null
                || user.LastName == null
                || user.FlatNumber == null
                || user.PostalCode == null
                || user.Street == null)
            {
                return false;
            }
            return true;
        }
    }

}
