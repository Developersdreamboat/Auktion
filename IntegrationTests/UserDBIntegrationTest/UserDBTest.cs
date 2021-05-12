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

namespace IntegrationTests
{
    public class UserDBTest
    {
        AuctionDbContext _context;

        public UserDBTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AuctionDbContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=AuctionDB;Trusted_Connection=True;")
                    .UseInternalServiceProvider(serviceProvider);

            _context = new AuctionDbContext(builder.Options);
            _context.Users.Add(new User { Address = "xdxd", Email = "as@gmail.com", Password = "xdxd", Name = "xzxzx", Role = "admin", UserName = "rofl" });
            _context.Users.Add(new User { Address = "xdxdasd", Email = "adsadas@gmail.com", Password = "xddsadxd", Name = "dasxzxzx", Role = "admin", UserName = "rofl" });
            _context.Users.Add(new User { Address = "xdxd", Email = "as@gmail.com", Password = "xdxd", Name = "xzxzx", Role = "admin", UserName = "rofl" });
            _context.SaveChanges();
            _context.Database.Migrate();
        }

        [Fact]
        public void QueryUsersFromSqlTest()
        {
            //Execute the query
            UserQuery query = new UserQuery(_context);
            var users = query.Execute();

            //Verify the results
            Assert.Equal(2, users.Count());
            var user = users.First();
            Assert.Equal("xzxzx", user.Name);
        }

    }
    
}
