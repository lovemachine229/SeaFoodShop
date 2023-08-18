using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public short RoleId { get; set; }
        public bool IsActive { get; set; }
        public string IsActived { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Role Role { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
