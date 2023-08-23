using AutoMapper;
using FluentValidation;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Post
{
    public class NewPostVm : IMapFrom<Domain.Models.BaseInherited.Post>
    {
        public int Id { get; set; }
        public int CreatorId { get; set; } //TODO - AFTER MAKING LOGGING SYSYEM - this should read which user is logged    
        public string Text { get; set; }
        public string PossibilityPickUpMethod { get; set; }
        public string? PickUpAddress { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public IFormFile? Image { get; set; }
        public string City { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPostVm, Domain.Models.BaseInherited.Post>().ReverseMap();
        }
    }
    public class NewPostValidation : AbstractValidator<NewPostVm>
    {
        public NewPostValidation()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.Text).MaximumLength(255);            
        }
    }
}
