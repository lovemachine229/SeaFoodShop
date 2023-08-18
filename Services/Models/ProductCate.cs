using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("ProductCate")]
    public class ProductCate
    {
        public int Id { get; set; }

        public long? ProductId { get; set; }

        public int? CateId { get; set; }
    }
}
