using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Bookings
{
    public class BookingForListVm
    {
        public int Id { get; set; }
        public string PickUpMethod { get; set; }
        public string PickUpAddress { get; set; }
        public int BookerId { get; set; } //user id, who actually books
        public FoodshareMVC.Domain.Models.User User { get; set; }
        public DateTime BookingExpirationDateTime { get; set; }
    }
}
