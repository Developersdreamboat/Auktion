using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class LotDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BuyNowPrice { get; set; }
        public int Bid { get; set; }
        public string BidUser { get; set; }
        public DateTime Expiring { get; set; }
        public int CategoryId { get; set; }
        public string User { get; set; }
    }
}
