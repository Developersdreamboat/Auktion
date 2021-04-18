using AutoMapper;
using Data_Access_Layer.Entities;
using Business_Logic_Layer.Models;

namespace Business_Logic_Layer
{
    public class MapperConfigurationClass : Profile
    {
        public MapperConfigurationClass()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Auction, AuctionDto>().ReverseMap();
            CreateMap<Lot, LotDto>().ReverseMap();
        }
    }
}
