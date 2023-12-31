﻿using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.User
{
    public class NewUserDetailVm : IMapFrom<ApplicationUser>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }  
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public int FlatNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, NewUserDetailVm>().ReverseMap();
        }
    }
}
