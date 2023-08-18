using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("Role")]
    public class Role
    {
        public short Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
