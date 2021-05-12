using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using UnitTests;
using Business_Logic_Layer;
using Business_Logic_Layer.Abstract;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Entities;

namespace UnitTests.ServiceTests
{
    [TestFixture]
    public class LotServiceTests
    {
        private IMapper _mapper;
        private ILotService _lotService;
        private AuctionDbContext _dbContext;
        [SetUp]
        public void Setup() 
        {
            _mapper = MappingClass.CreateMapper();
            var options = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            using var context = new AuctionDbContext(options);
            context.Users.Add(new User { Id = 3, Email = "haha@gmail.com", Password = "111", Role = "admin", Address = "lala", Name = "lol", UserName = "haha" });
            context.Users.Add(new User { Id = 4, Email = "xdxd@gmail.com", Password = "112", Role = "admin", Address = "lal231a", Name = "lo123l", UserName = "haha" });
            context.Auctions.Add(new Auction { Id = 1, Name = "A", Description = "AA", CreatedAt = DateTime.Now, UserId = 3 });
            context.Auctions.Add(new Auction { Id = 2, Name = "B", Description = "BB", CreatedAt = DateTime.Now, UserId = 4 });
            context.Auctions.Add(new Auction { Id = 3, Name = "C", Description = "CC", CreatedAt = DateTime.Now, UserId = 3 });
            context.Lots.Add(new Lot { Id = 1, Name = "AA", Description = "AAA", Expiring=DateTime.Now, Price = 1000, BidUserId = 3, AuctionId = 1 });
            context.Lots.Add(new Lot { Id = 2, Name = "BB", Description = "BBB", Expiring = DateTime.Now, Price = 1111, BidUserId = 4, AuctionId = 1 });
            context.Lots.Add(new Lot { Id = 3, Name = "CC", Description = "CCC", Expiring = DateTime.Now, Price = 10000, BidUserId = 3, AuctionId = 2 });
            context.Lots.Add(new Lot { Id = 4, Name = "DD", Description = "DDD", Expiring = DateTime.MaxValue, Price = 100011, BidUserId = 4, AuctionId = 2 });
            context.SaveChanges();
            _dbContext = new AuctionDbContext(options);
            _lotService = new LotService(_mapper, _dbContext);
        }
        [Test]
        public void GetAllAsync_NumberOfLots() 
        {
            var value = _dbContext.Lots.Count();
            _lotService.GetAllAsync().Count().Should().Be(value);
        }
        [Test]
        public void GetByIdAsync_AreEqual() 
        {
            var lot = _dbContext.Lots.Find(1).Name;
            _lotService.GetByIdAsync(1).Result.Name.Should().Be(lot);
        }
        [Test]
        public void GetByAuctionId_AreSameAuctions() 
        {
            var value = _dbContext.Lots.AsNoTracking().Where(x=>x.AuctionId==1).Count();
            _lotService.GetByAuctionId(1).Count().Should().Be(value);
        }
        [Test]
        public void AddAsync_Add() 
        {
            var newLot = new LotDto { Id = 5, Name = "HAHAHA", Description = "HAHAHA", Expiring = DateTime.Now, Price = 10001111, BidUserId = 4, AuctionId = 2 };
            _lotService.AddAsync(newLot);
            _dbContext.Lots.Count().Should().Be(5);
        }
        [Test]
        public void UpdateBidAsync_ChangeBid() 
        {
            var startPrice = _dbContext.Lots.Find(4).Price;
            _dbContext.Lots.Find(4).Price.Should().NotBe(200000);
            _lotService.UpdateBidAsync(4,3,200000);
            _dbContext.Lots.Find(4).Price.Should().Be(200000);
        }
    }
}
