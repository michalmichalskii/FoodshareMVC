using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Post
{
    public class PostForListVm : IMapFrom<Domain.Models.BaseInherited.Post>
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Text { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Creator { get; set; }
        public int CreatorId { get; set; }
        public string PossibilityPickUpMethod { get; set; }
        public string? PickUpAddress { get; set; }
        public ICollection<Booking> Bookings { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.BaseInherited.Post, PostForListVm>()
                .ForMember(d => d.Creator, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
                .ForMember(d => d.Bookings, opt => opt.Ignore());        }
    }
}
