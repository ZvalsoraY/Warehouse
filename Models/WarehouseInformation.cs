using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Warehouse.Models
{
    /// <summary>
    /// Класс WarehouseInformation,
    /// хранит информацию о складе
    /// </summary>
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
