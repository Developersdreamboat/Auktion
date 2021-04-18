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
        public Task<IEnumerable<LotDto>> GetAllAsync();
        public Task UpdateAsync();
        public Task DeleteAsync(int id);
    }
}
