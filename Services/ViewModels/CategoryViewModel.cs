﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    class CategoryViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ParentName { get; set; }
        public int NumberOfProduct { get; set; }
    }
}