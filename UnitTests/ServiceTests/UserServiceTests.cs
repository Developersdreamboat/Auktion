using System;
using System.Linq;
using System.Threading;
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
    public class UserServiceTests
    {
        private IMapper _mapper;
        private IUserService _userService;
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
            context.SaveChanges();
            _dbContext = new AuctionDbContext(options);
            _userService = new UserService(_mapper,_dbContext);
        }
        [Test]
        public void GetAllAsync_NumberOfUsers()
        {
            var value = _dbContext.Users.Count();
            _userService.GetAllAsync().Count().Should().Be(value);
        }
        [Test]
        public void GetByEmail_IsEqual() 
        {
            var userEmail = "haha@gmail.com";
            _userService.GetByEmail("haha@gmail.com").Result.Email.Should().Be(userEmail);
        }
        [Test]
        public void GetById_IsNull() 
        {
            var result = _userService.GetByIdAsync(100000).Result;
            result.Should().BeNull();
        }
        [Test]
        public void AddUserAsync() 
        {
            var newUser = new UserDto { Id = 5, Email = "xdxdXDA@gmail.com", Password = "1123", Role = "admin", Address = "lal231a", Name = "lo123l", UserName = "hahaxdxd" };
            _userService.AddAsync(newUser);
            _dbContext.Users.Count().Should().Be(3);
        }
    }
}
