using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("CustomerInfor")]
    public class CustomerInfor
    {
        [Key]
        public Guid GuidId { get; set; }

        [StringLength(20)]
        public string PhoneNo { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }
    }
}
