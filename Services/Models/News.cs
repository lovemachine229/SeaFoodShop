using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class News
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "Tiêu đề quá dài!")]
        [Required(ErrorMessage = "Vui lòng nhập Tiêu đề!")]
        public string Title { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Vui lòng nhập Mô tả ngắn!")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Vui lòng nhập Nội dung!")]
        public string Detail { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Ảnh đại diện!")]
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Xuất bản!")]
        public bool? Published { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Type { get; set; }
    }
}
