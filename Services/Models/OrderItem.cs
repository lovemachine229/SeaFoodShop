using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public long ProductId { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        public decimal? LastPrice { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
