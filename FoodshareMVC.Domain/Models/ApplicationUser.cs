using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodshareMVC.Domain.Models.Base;
using FoodshareMVC.Domain.Models.BaseInherited;
using Microsoft.AspNetCore.Identity;

namespace FoodshareMVC.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public int? FlatNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Review> WrittenReviews { get; set; }
        public ICollection<Review> MyReviews { get; set; }
        public ICollection<Booking> Bookings { get; set; }


    }
}
