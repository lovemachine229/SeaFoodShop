using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email không được để trống!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }
    }
}
