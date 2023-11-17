using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    /// <summary>
    /// Класс Storekeeper,
    /// хранит информацию о грузчике
    /// </summary>
    public class Storekeeper
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Недопустимая длина строки.")]
        public string Name { get; set; }
    }
}
