using FoodshareMVC.Domain.Models.BaseInherited;
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
        void DeleteExpiredBookingAndMakePostActive(int postId);
        int AddBookingAndMakePostNotActive(int postId, Booking booking);
        IQueryable<Booking> GetAllBookings();
    }
}
