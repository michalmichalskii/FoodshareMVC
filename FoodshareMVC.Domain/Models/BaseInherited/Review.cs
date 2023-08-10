using FoodshareMVC.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Models.BaseInherited
{
    public class Review : BaseModel
    {
        public int ReviewerId { get; set; }
        public User Reviewer { get; set; }
        public int ReviewedUserId { get; set; }
        public User ReviewedUser { get; set; }
        public int AmountOfStars { get; set; }
        public string? ReviewDescription { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
