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

namespace IntegrationTests.LotDBIntegrationTest
{
    public class LotDBTest
    {
        AuctionDbContext _context;

        public LotDBTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AuctionDbContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=AuctionDB;Trusted_Connection=True;")
                    .UseInternalServiceProvider(serviceProvider);

            _context = new AuctionDbContext(builder.Options);
            _context.Lots.Add(new Lot { Name = "xd", Price = 1000, Expiring = DateTime.Now, Description = "aaa", BidUserId = 1, AuctionId = 1  });
            _context.Lots.Add(new Lot { Name = "xaxa", Price = 10001, Expiring = DateTime.Now, Description = "aaaa", BidUserId = 1, AuctionId = 1 });
            _context.Lots.Add(new Lot { Name = "xzxz", Price = 10010, Expiring = DateTime.Now, Description = "azaax", BidUserId = 1, AuctionId = 1 });
            _context.SaveChanges();
        }

        [Fact]
        public void QueryLotsFromSqlTest()
        {
            //Execute the query
            LotQuery query = new LotQuery(_context);
            var lots = query.Execute();

            //Verify the results
            Assert.Single(lots);
            var lot = lots.First();
            Assert.Equal("xd", lot.Name);
        }

    }
}
