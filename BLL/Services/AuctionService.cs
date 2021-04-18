﻿using System;
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
    public class AuctionService
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;
        public AuctionService(IMapper mapper, AuctionDbContext dbContext) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddAsync(AuctionDto category) 
        {
            await _dbContext.Auctions.AddAsync(_mapper.Map<Auction>(category));
            await _dbContext.SaveChangesAsync();
        }
        public async Task<AuctionDto> GetByIdAsync(int id) 
        {
            return _mapper.Map<AuctionDto>(await _dbContext.Auctions.FindAsync(id));
        }
        public async Task<IEnumerable<AuctionDto>> GetAllAsync()
        {
            var categories = await _dbContext.Auctions.ToListAsync();
            return _mapper.Map<IEnumerable<AuctionDto>>(categories);
        }
        public async Task UpdateAsync() 
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _dbContext.Auctions.FindAsync(id);
            _dbContext.Auctions.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}