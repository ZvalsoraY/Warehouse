using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Warehouse.Models
{
    public class WarehouseInformation
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Название склада")]
        [Required]
        [StringLength(200, ErrorMessage = "Недопустимая длина строки.")]
        public string Name { get; set; }
    }
}
