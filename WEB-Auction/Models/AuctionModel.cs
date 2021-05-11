using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_Logic_Layer.Models;

namespace WEB_Auction.Models
{
    public class AuctionModel
    {
        public AuctionModel(IEnumerable<AuctionDto> auctions) 
        {
            Auctions = auctions;
        }
        public IEnumerable<AuctionDto> Auctions { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
