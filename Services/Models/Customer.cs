using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("Customer")]
    public class Customer
    {
        public int Id { get; set; }

        public Guid GuidId { get; set; }

        [StringLength(350)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
