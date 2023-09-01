using FoodshareMVC.Domain.Models;
using FoodshareMVC.Domain.Models.BaseInherited;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Infrastructure
{
    public class Context : IdentityDbContext<IdentityUser>
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .HasOne(p => p.Booking).WithOne(b => b.Post)
                .HasForeignKey<Booking>(b => b.PostId);

            builder.Entity<Booking>()
                .HasOne(u => u.User).WithMany(b => b.Bookings)
                .HasForeignKey(u => u.BookerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Post>()
                .HasOne(u => u.User).WithMany(u => u.Posts)
                .HasForeignKey(u => u.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Review>()
                .HasOne(u => u.Reviewer).WithMany(o => o.WrittenReviews)
                .HasForeignKey(u => u.ReviewerId);

            builder.Entity<Review>()
               .HasOne(u => u.ReviewedUser).WithMany(o => o.MyReviews)
               .HasForeignKey(u => u.ReviewedUserId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
