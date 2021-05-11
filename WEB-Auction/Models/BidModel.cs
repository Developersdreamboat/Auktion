using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_Auction.Models
{
    public class BidModel
    {
        public int LotId { get; set; }
        public int UserName { get; set; }
        public int BidValue { get; set; }
    }
}
