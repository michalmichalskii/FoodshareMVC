using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Interfaces
{
    public interface IBookingRepository
    {
        void DeleteBooking(int bookingId);

        int AddBooking(Booking booking);

        IQueryable<Booking> GetAllBookings();
    }
}
