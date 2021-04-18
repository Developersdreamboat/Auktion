using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Entities;

namespace Data_Access_Layer
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Lot> Lots { get; set; }
    }
}
