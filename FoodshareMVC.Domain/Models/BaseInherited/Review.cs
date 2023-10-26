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
        public string ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }
        public string ReviewedUserId { get; set; }
        public ApplicationUser ReviewedUser { get; set; }
        public int AmountOfStars { get; set; }
        public string? ReviewDescription { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
