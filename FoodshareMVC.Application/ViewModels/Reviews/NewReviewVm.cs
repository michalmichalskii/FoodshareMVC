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
    public class NewReviewVm : IMapFrom<Review>
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int ReviewedUserId { get; set; }
        public float AmountOfStars { get; set; }
        public string ReviewDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, NewReviewVm>()
                .ReverseMap();
        }
    }
}
