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
    public class UserQuery
    {
        private AuctionDbContext _context;

        public UserQuery(AuctionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Execute()
        {
            return _context.Users
                .FromSqlRaw("SELECT * FROM Users WHERE Address = 'xdxd';", true);
        }

    }
}
