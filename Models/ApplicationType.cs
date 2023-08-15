﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Warehouse.Models
{
    public class ApplicationType
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Тип товара")]
        [Required]
        [StringLength(200, ErrorMessage = "Недопустимая длина строки.")]
        public string Name { get; set; }
    }
}