﻿using FoodshareMVC.Application.ViewModels.Post.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Post
{
    public class ListPostForListVm
    {
        //TODO - AFTER MAKING LOGGING SYSYEM - user should can edit only his own posts
        public int CurrentUserId { get; set; }        //TODO - Im not really sure that there should be prop like this
        public List<PostForListVm> Posts { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public FilterVm Filter { get; set; } = new FilterVm();
    }
}
