using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    [Table("Product")]
    public class Product
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public long Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public int Code { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public string Images { get; set; }

        public string Avatar { get; set; }

        public int? Qualtity { get; set; }

        public string Unit { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal Price { get; set; }

        public decimal SaleOff { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? Published { get; set; }

        public bool? IsHot { get; set; }

        public int? View { get; set; }

        public decimal SellPrice
        {
            set { SellPrice = value; }
            get
            {
                if (SaleOff > 0 && DateTime.Now >= StartDate && DateTime.Now <= EndDate)
                {
                    return SaleOff;
                }
                else
                {
                    return Price;
                }
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
