using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("SubscriptionEmail")]
    public class SubscriptionEmail
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
