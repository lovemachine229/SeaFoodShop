using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    class SearchOrderViewModel
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
    }
}
