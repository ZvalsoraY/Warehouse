﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Warehouse.Models
{
    /// <summary>
    /// Класс ApplicationType
    /// хранит информацию о типе товара
    /// </summary>
    public class ApplicationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Недопустимая длина строки.")]
        public string Name { get; set; }        
    }
}
