using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string PickUpMethod { get; set; }
        public string PickUpAddress { get; set; }
        public int UserId { get; set; } //booker id
        public User User { get; set; }
        public DateTime BookingExpirationDateTime { get; set; }
    }
}
