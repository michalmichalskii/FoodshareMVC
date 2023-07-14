using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Bookings
{
    public class ListBookingForListVm
    {
        public List<BookingForListVm> Bookings { get; set; }
        public int Count { get; set; }
    }
}
