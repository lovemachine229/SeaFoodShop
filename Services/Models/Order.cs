using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Services.Models
{
    [Table("Order")]
    public class Order
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public DateTime Date { get; set; }

        public decimal? TotalAmount { get; set; }

        public int Status { get; set; }

        public int Payment{ get; set; }
        [StringLength(255)]

        public string Email { get; set; }

        public virtual AddressDelivery AddressDelivery { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
