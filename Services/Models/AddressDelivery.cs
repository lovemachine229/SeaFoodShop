using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("AddressDelivery")]
    public class AddressDelivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập Họ Tên!")]
        public string FullName { get; set; }

        [EmailAddress]
        [StringLength(350)]
        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        public string Email { get; set; }

        [Phone]
        [StringLength(30)]
        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại!")]
        public string PhoneNo { get; set; }

        [StringLength(350)]
        [Required(ErrorMessage = "Vui lòng nhập Địa chỉ!")]
        public string Address { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public virtual Order Order  { get; set; }
    }
}
