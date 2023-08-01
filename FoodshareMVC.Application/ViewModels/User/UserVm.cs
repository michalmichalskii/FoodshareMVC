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
    public class UserVm : IMapFrom<Domain.Models.BaseInherited.User>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public List<PostForListVm> Posts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.BaseInherited.User, UserVm>()
                .ForMember(s => s.FullName, opt => opt.MapFrom(d => d.FirstName + " " + d.LastName))
                .ForMember(s => s.Posts, opt => opt.MapFrom(d => d.Posts.Where(p => p.CreatorId == Id)));
        }
    }
}
