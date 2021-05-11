using System;
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
    public class AuctionService : IAuctionService
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;
        public AuctionService(IMapper mapper, AuctionDbContext dbContext) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddAsync(AuctionDto auction) 
        {
            await _dbContext.Auctions.AddAsync(_mapper.Map<Auction>(auction));
            await _dbContext.SaveChangesAsync();
        }
        public async Task<AuctionDto> GetByIdAsync(int id) 
        {
            return _mapper.Map<AuctionDto>(await _dbContext.Auctions.FindAsync(id));
        }
        public async Task<IEnumerable<AuctionDto>> GetAllAsync()
        {
            var auctions = await _dbContext.Auctions.ToListAsync();
            return _mapper.Map<IEnumerable<AuctionDto>>(auctions);
        }
        public async Task UpdateAsync() 
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(int id)
        {
            var auction = await _dbContext.Auctions.FindAsync(id);
            _dbContext.Auctions.Remove(auction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
