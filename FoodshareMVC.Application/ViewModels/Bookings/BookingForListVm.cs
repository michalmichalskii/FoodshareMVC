using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Bookings
{
    public class BookingForListVm : IMapFrom<Booking>
    {
        public int Id { get; set; }
        public string PickUpMethod { get; set; }
        public string PickUpAddress { get; set; }
        public int BookerId { get; set; } //user id, who actually books
        public ApplicationUser User { get; set; }
        public DateTime BookingExpirationDateTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, BookingForListVm>();
        }
    }
}
