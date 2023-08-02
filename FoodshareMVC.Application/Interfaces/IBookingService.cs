using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Models;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Interfaces
{
    public interface IBookingService
    {
        int AddBooking(int postId, NewBookingVm newBooking);
        void DeleteBooking(int id);
        void DeleteExpiredBookingAndMakePostActive(int postId);
    }
}
