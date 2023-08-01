using AutoMapper;
using FoodshareMVC.Application.Mapping;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Bookings
{
    public class NewBookingVm : IMapFrom<Booking>
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int BookerId { get; set; }
        public string PickUpMethod { get; set; }
        public string PickUpAddress { get; set; }
        public DateTime BookingExpirationDateTime { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, NewBookingVm>().ReverseMap();
        }
    }
}
