using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
namespace Data_Access_Layer.Entities
{
    public class Auction
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
