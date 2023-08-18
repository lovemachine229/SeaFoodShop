using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class ChangePasswordViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email không được để trống!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu hiện tại!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu mới!")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Xác thực mật khẩu mới chưa chính xác!")]
        [Required(ErrorMessage = "Vui lòng xác thực mật khẩu mới!")]
        public string ConfirmNewPassword { get; set; }
    }
}
