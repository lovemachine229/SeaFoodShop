using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("Wishlist")]
    public class Wishlist
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public long? ProductId { get; set; }

        public int? Type { get; set; }
    }
}
