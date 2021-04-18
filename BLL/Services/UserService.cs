﻿using System.Text;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Data_Access_Layer;
using Data_Access_Layer.Entities;
using Business_Logic_Layer.Models;
using Microsoft.EntityFrameworkCore;
namespace Business_Logic_Layer.Services
{
    public class UserService
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddAsync(UserDto user)
        {
            await _dbContext.Users.AddAsync(_mapper.Map<User>(user));
            await _dbContext.SaveChangesAsync();
        }
        public async Task<UserDto> GetByIdAsync(int id)
        {
            return _mapper.Map<UserDto>(await _dbContext.Users.FindAsync(id));
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task<UserDto> FirstOrDefaultAsync(string email, string password) 
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return _mapper.Map<UserDto>(user);
        }
        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}