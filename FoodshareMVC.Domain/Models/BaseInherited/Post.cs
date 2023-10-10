using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodshareMVC.Domain.Models.Base;

namespace FoodshareMVC.Domain.Models.BaseInherited
{
    public class Post : BaseModel
    {
        public string? Image { get; set; }
        public string Text { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string PossibilityPickUpMethod { get; set; }
        public int CreatorId { get; set; }
        public User User { get; set; }
        public bool IsActive { get; set; }
        public string? PickUpAddress { get; set; }
        public string City { get; set; }
        public Booking Booking { get; set; }
    }
}
