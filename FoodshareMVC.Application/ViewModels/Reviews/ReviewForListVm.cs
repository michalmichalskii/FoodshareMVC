using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Reviews
{
    public class ReviewForListVm : IMapFrom<Review>
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public string CreatorEmail { get; set; }
        public int AmountOfStars { get; set; }
        public string? ReviewDescription { get; set; }
        public DateTime CreateDateTime { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, ReviewForListVm>()
                .ForMember(d => d.Creator, opt => opt.MapFrom(s => s.Reviewer.FirstName + " " + s.Reviewer.LastName))
                .ForMember(d => d.CreatorEmail, opt => opt.MapFrom(s => s.Reviewer.Email));
        }
    }
}
