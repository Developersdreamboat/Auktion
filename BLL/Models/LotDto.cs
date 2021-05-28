using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class LotDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public float Price { get; set; }
        public int BidUserId { get; set; }
        public DateTime Expiring { get; set; }
        public int AuctionId { get; set; }
    }
}
