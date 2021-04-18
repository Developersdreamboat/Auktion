using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Business_Logic_Layer.Models;

namespace Business_Logic_Layer.Abstract
{
    public interface IAuctionService
    {
        public Task AddAsync(AuctionDto category);
        public Task<AuctionDto> GetByIdAsync(int id);
        public Task<IEnumerable<AuctionDto>> GetAllAsync();
        public Task UpdateAsync();
        public Task DeleteAsync(int id);
    }
}
