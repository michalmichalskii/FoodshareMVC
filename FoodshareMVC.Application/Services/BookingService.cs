﻿using AutoMapper;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models;
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

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public int AddBooking(int postId, NewBookingVm newBooking)
        {
            var booking = _mapper.Map<Booking>(newBooking);
            var id = _bookingRepository.AddBooking(postId, booking);
            return id;
        }
    }
}
