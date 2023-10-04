using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        public short? RoldId { get; set; }

        public string RoleName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }
    }
}
