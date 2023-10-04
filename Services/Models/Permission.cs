﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Module { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Statistic { get; set; }
        public bool Config { get; set; }
    }
}
