using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ContactUs
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập Tiêu đề!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Nội dung.")]
        public string Content { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập Họ & Tên.")]
        public string FullName { get; set; }

        [EmailAddress]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }
    }
}
