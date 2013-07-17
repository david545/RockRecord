using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RockRecord.Models;
namespace RockRecord.Models
{
    public class RockRecordContext:DbContext
    {
        public RockRecordContext() : base("name=DefaultConnection") { }

        
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderHeader> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
              
            modelBuilder.Entity<Member>().HasMany(m => m.Reviews)
                                         .WithRequired(r => r.Member)
                                         .WillCascadeOnDelete(true);

            modelBuilder.Entity<Member>().HasMany(m => m.Orders)
                                         .WithRequired(o => o.Member)
                                         .WillCascadeOnDelete(true);

            modelBuilder.Entity<Genre>().HasMany(c => c.Albums)
                                                .WithRequired(a => a.Genre)
                                                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Album>().HasMany(a => a.Reviews)
                                        .WithRequired(r => r.Album)
                                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Album>().HasMany(a => a.Songs)
                                        .WithRequired(s => s.Album)
                                        .WillCascadeOnDelete(true);


            modelBuilder.Entity<Artist>().HasMany(art => art.Albums)
                                         .WithRequired(alb => alb.Artist)
                                         .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderHeader>().HasMany(o => o.OrderDetails)
                                              .WithRequired(od => od.OrderHeader)
                                              .WillCascadeOnDelete(true);
 
        }
    }
}