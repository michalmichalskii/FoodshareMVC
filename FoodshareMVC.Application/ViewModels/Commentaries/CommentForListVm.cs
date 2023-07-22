﻿using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Commentaries
{
    public class CommentForListVm : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentForListVm>();
        }
    }
}
