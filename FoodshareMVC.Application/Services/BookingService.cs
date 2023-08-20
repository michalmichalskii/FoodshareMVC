using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepsitory;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IPostRepository postRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _postRepsitory = postRepository;
        }


        //TODO - AFTER MAKING LOGGING SYSYEM - confirm pickup by post owner
        public int AddBooking(int postId, NewBookingVm newBooking)
        {
            var booking = _mapper.Map<Booking>(newBooking);
            var post = _postRepsitory.GetPost(postId);
            if (post != null)
            {
                booking.BookingExpirationDateTime = DateTime.Now.AddDays(2);
                var id = _bookingRepository.AddBooking(postId, booking);
                return id;
            }
            else
            {
                return -1;
            }
        }
        public void DeleteExpiredBooking()
        {
            var expriredBookings = _bookingRepository.GetAllBookings().Where(p => p.BookingExpirationDateTime <= DateTime.Now);

            _bookingRepository.DeleteRangeOfBookings(expriredBookings);
        }
    }
}
