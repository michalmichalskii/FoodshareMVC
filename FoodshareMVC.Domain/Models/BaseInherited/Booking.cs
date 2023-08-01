using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodshareMVC.Domain.Models.Base;

namespace FoodshareMVC.Domain.Models.BaseInherited
{
    public class Booking : BaseModel
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string PickUpMethod { get; set; }
        public string PickUpAddress { get; set; }
        public int BookerId { get; set; }
        public User User { get; set; }
        public DateTime BookingExpirationDateTime { get; set; }
    }
}
