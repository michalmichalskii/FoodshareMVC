using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Application.ViewModels.Commentaries;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Post
{
    public class PostForListVm : IMapFrom<FoodshareMVC.Domain.Models.Post>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Creator { get; set; }
        public ICollection<CommentVm> Comments { get; set; } //czy tu może być tak po prostu commentVm???????????   
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<PostTag> PostTags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FoodshareMVC.Domain.Models.Post, PostForListVm>()
                .ForMember(d => d.Creator, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
                .ForMember(d => d.Bookings, opt => opt.Ignore())
                .ForMember(d => d.PostTags, opt => opt.Ignore());
        }
    }
}
