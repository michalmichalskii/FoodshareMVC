using AutoMapper;
using FluentValidation;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Reviews
{
    public class NewReviewVm : IMapFrom<Review>
    {
        public int Id { get; set; }
        public string ReviewerId { get; set; }
        public string ReviewedUserId { get; set; }
        public int AmountOfStars { get; set; }
        public string? ReviewDescription { get; set; }
        public DateTime CreateDateTime { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, NewReviewVm>()
                .ReverseMap();
        }
    }

    public class NewReviewValidation : AbstractValidator<NewReviewVm>
    {
        public NewReviewValidation()
        {
            RuleFor(x => x.AmountOfStars).GreaterThan(0).WithMessage("Enter amount of stars");
        }
    }
}
