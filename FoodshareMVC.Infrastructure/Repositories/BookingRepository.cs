using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Context _context;

        public BookingRepository(Context context)
        {
            _context = context;
        }

        public void DeleteBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }

        public IQueryable<Booking> GetAllBookings()
        {
            var bookings = _context.Bookings;
            return bookings;
        }

        public void DeleteExpiredBookingAndMakePostActive(int postId)
        {
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
                var booking = _context.Bookings.Where(b => b.PostId == postId);
                var expiredBooking = booking.Where(eb => eb.BookingExpirationDateTime <= DateTime.Now).FirstOrDefault();
                if(expiredBooking != null)
                {
                    _context.Bookings.Remove(expiredBooking);
                    post.IsActive = true;
                    _context.SaveChanges();
                }
            }
        }

        //TODO - AFTER MAKING LOGGING SYSYEM - confirm pickup by post owner
        public int AddBookingAndMakePostNotActive(int postId, Booking booking)
        {
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();

            if (post != null)
            {
                booking.PostId = post.Id;
                booking.Post = post;
                booking.BookingExpirationDateTime = DateTime.Now.AddDays(3);
                post.IsActive = false;

                _context.Bookings.Add(booking);
                _context.SaveChanges();

                return booking.Id;
            }

            return -1;
        }
    }
}
