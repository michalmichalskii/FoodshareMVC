using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.User
{
    public class UserVm : IMapFrom<FoodshareMVC.Domain.Models.User>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public List<PostForListVm> Posts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FoodshareMVC.Domain.Models.User, UserVm>()
                .ForMember(s => s.FullName, opt => opt.MapFrom(d => d.FirstName + " " + d.LastName))
                .ForMember(d => d.Posts, opt => opt.Ignore());
        }
    }
}
