using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Data_Access_Layer.Entities;
using Business_Logic_Layer.Abstract;
using Business_Logic_Layer.Services;
using Xunit;

namespace IntegrationTests.AuctionDBIntegrationTest
{
    public class AuctionDBTest
    {
        AuctionDbContext _context;

        public AuctionDBTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AuctionDbContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=AuctionDB;Trusted_Connection=True;")
                    .UseInternalServiceProvider(serviceProvider);

            _context = new AuctionDbContext(builder.Options);
            _context.Auctions.Add(new Auction { Name = "lol", Description = "123", CreatedAt = DateTime.Now, UserId = 1 });
            _context.Auctions.Add(new Auction { Name = "lo", Description = "1234", CreatedAt = DateTime.Now, UserId = 1 });
            _context.Auctions.Add(new Auction { Name = "xdxd", Description = "12345", CreatedAt = DateTime.Now, UserId = 1 });
            _context.SaveChanges();
        }

        [Fact]
        public void QueryAuctionsFromSqlTest()
        {
            //Execute the query
            AuctionQuery query = new AuctionQuery(_context);
            var auctions = query.Execute();

            //Verify the results
            Assert.Single(auctions);
            var auction = auctions.First();
            Assert.Equal("lol", auction.Name);
        }

    }
}
