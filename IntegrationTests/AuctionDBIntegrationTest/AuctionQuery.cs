using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Data_Access_Layer.Entities;

namespace IntegrationTests.AuctionDBIntegrationTest
{
    public class AuctionQuery
    {
        private AuctionDbContext _context;

        public AuctionQuery(AuctionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Auction> Execute()
        {
            return _context.Auctions
                .FromSqlRaw("SELECT * FROM Auctions WHERE Description = '123';", true);
        }

    }
}
