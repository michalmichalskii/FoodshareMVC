﻿using AutoMapper;
using FluentValidation;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Post
{
    public class NewPostVm : IMapFrom<FoodshareMVC.Domain.Models.Post>
    {
        public int Id { get; set; }
        public int CreatorId { get; set; } //TODO: this should read which user is logged    
        public string Text { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public ICollection<PostTag> PostTags { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPostVm, FoodshareMVC.Domain.Models.Post>().ReverseMap()
                .ForMember(d => d.PostTags, opt => opt.Ignore());
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
