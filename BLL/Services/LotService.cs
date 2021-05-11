using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Data_Access_Layer;
using Data_Access_Layer.Entities;
using Business_Logic_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Business_Logic_Layer.Abstract;

namespace Business_Logic_Layer.Services
{
    public class LotService : ILotService
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;
        public LotService(IMapper mapper, AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddAsync(LotDto lot)
        {
            await _dbContext.Lots.AddAsync(_mapper.Map<Lot>(lot));
            await _dbContext.SaveChangesAsync();
        }
        public async Task<LotDto> GetByIdAsync(int id)
        {
            return _mapper.Map<LotDto>(await _dbContext.Lots.FindAsync(id));
        }
        public IEnumerable<LotDto> GetByAuctionId(int id) 
        {
            var lots = _dbContext.Lots.AsNoTracking().ToList().Where(x => x.AuctionId == id);
            return _mapper.Map<IEnumerable<LotDto>>(lots);
        }
        public IEnumerable<LotDto> GetAllAsync()
        {
            var lots = _dbContext.Lots;
            return _mapper.Map<IEnumerable<LotDto>>(lots);
        }
        public void UpdateBidAsync(int lotId, int userId, int bidValue)
        {
            var bid = _dbContext.Lots.FindAsync(lotId);
            if (bid.Result.Price <= bidValue && bid.Result.Expiring >= DateTime.Now) 
            {
                bid.Result.Price = bidValue;
                bid.Result.BidUserId = userId;
                _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var lot = await _dbContext.Lots.FindAsync(id);
            _dbContext.Lots.Remove(lot);
            await _dbContext.SaveChangesAsync();
        }
    }
}
