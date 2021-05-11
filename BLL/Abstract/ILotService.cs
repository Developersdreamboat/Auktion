using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Business_Logic_Layer.Models;
namespace Business_Logic_Layer.Abstract
{
    public interface ILotService
    {
        public Task AddAsync(LotDto lot);
        public Task<LotDto> GetByIdAsync(int id);
        public IEnumerable<LotDto> GetAllAsync();
        public IEnumerable<LotDto> GetByAuctionId(int id);
        public void UpdateBidAsync(int lotId, int userId, int bidValue);
        public Task DeleteAsync(int id);
    }
}
