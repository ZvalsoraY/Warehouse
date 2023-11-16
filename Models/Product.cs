using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Warehouse.Models
{
    /// <summary>
    /// Класс Product,
    /// хранит данные оп продукте, его описание, типе.
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Недопустимая длина строки.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Недопустимая длина строки.")]
        public string ShortDesc { get; set; }

        [Display(Name = "Application Type")]
        public int ApplicationTypeId { get; set; }
        [ForeignKey("ApplicationTypeId")]
        [ValidateNever]
        public virtual ApplicationType ApplicationType { get; set; }
    }
}
