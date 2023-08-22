using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(200)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]

        public string Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNo { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
