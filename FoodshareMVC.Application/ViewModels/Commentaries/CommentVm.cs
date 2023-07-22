using AutoMapper;
using FoodshareMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Commentaries
{
    public class CommentVm : IMapFrom<FoodshareMVC.Domain.Models.Comment>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FoodshareMVC.Domain.Models.Comment, CommentVm>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));
        }
    }
}
