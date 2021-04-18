using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Data_Access_Layer;
using Data_Access_Layer.Entities;
using Business_Logic_Layer.Models;
using Microsoft.EntityFrameworkCore;
namespace Business_Logic_Layer.Services
{
    public class LotService
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
        public async Task<IEnumerable<LotDto>> GetAllAsync()
        {
            var lots = await _dbContext.Lots.ToListAsync();
            return _mapper.Map<IEnumerable<LotDto>>(lots);
        }
        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(int id)
        {
            var lot = await _dbContext.Lots.FindAsync(id);
            _dbContext.Lots.Remove(lot);
            await _dbContext.SaveChangesAsync();
        }
    }
}
