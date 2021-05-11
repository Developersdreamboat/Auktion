using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Business_Logic_Layer.Models;

namespace Business_Logic_Layer.Abstract
{
    public interface IUserService
    {
        public Task AddAsync(UserDto user);
        public Task<UserDto> GetByIdAsync(int id);
        public Task<UserDto> GetByEmail(string email);
        public IEnumerable<UserDto> GetAllAsync();
        public Task<UserDto> FirstOrDefaultAsync(string email, string password);
        public Task UpdateAsync();
        public Task DeleteAsync(int id);
    }
}
