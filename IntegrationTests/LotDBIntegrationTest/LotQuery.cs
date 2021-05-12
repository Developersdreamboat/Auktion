using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Data_Access_Layer.Entities;

namespace IntegrationTests.LotDBIntegrationTest
{
    public class LotQuery
    {
        private AuctionDbContext _context;

        public LotQuery(AuctionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lot> Execute()
        {
            return _context.Lots
                .FromSqlRaw("SELECT * FROM Lots WHERE Description = 'aaa';", true);
        }

    }
}
