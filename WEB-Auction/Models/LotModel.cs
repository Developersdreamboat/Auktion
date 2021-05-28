using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_Logic_Layer.Models;

namespace WEB_Auction.Models
{
    public class LotModel
    {
        public LotModel() { }
        public LotModel(IEnumerable<LotDto> lots) 
        {
            Lots = lots;
        }
        public IEnumerable<LotDto> Lots { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImgUrl { get; set; }
        public int Bid { get; set; }
        public string BidUser { get; set; }
        public DateTime Expiring { get; set; }
        public int AuctionId { get; set; }
        public string User { get; set; }
    }
}
