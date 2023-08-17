using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("CartItem")]
    public class CartItem
    {
        public long Id { get; set; }

        public long? ProductId { get; set; }

        public int? CustomerId { get; set; }
    }
}
