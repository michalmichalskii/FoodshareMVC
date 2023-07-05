using FoodshareMVC.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTag { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post).WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Tag).WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);

            builder.Entity<Booking>()
                .HasOne(u => u.User).WithMany(b => b.Bookings)
                .HasForeignKey(u => u.BookerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Post>()
                .HasOne(u => u.User).WithMany(u => u.Posts)
                .HasForeignKey(u => u.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
