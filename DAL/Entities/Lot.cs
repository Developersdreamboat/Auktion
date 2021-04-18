using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
    public class Lot
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public Auction Auction { get; set; }
    }
}
