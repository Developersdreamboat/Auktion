using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Business_Logic_Layer.Models;

namespace WEB_Auction.Models
{
    public class AuctionCreateModel
    {
        public AuctionCreateModel() 
        {
            CreatedAt = DateTime.Now;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
