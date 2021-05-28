using System;
using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
    public class Lot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public int BidUserId { get; set; }
        public DateTime Expiring { get; set; }
        public int AuctionId { get; set; }
    }
}
