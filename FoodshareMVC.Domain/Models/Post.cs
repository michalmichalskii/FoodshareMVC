using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int CreatorId { get; set; } //user Id,which made a post
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
