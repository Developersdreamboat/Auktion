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
    public class AuctionServiceTests
    {
        private IMapper _mapper;
        private IAuctionService _auctionService;
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
            context.SaveChanges();
            _dbContext = new AuctionDbContext(options);
            _auctionService = new AuctionService(_mapper, _dbContext);
        }
        [Test]
        public void GetAllAsync_NumberOfAuctions()
        {
            var value = _dbContext.Auctions.Count();
            _auctionService.GetAllAsync().Result.Count().Should().Be(value);
        }
        [Test]
        public void GetById_AreEqual()
        {
            var value = _dbContext.Auctions.Find(1).Name;
            _auctionService.GetByIdAsync(1).Result.Name.Should().Be(value);
        }
        [Test]
        public void AddAsync_Add() 
        {
            var auction = new AuctionDto() { Id = 4, Name = "D", Description = "DD", CreatedAt = DateTime.Now, UserId = 4 };
            _auctionService.AddAsync(auction);
            _dbContext.Auctions.Count().Should().Be(4);
        }
    }
}
