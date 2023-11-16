using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class Storekeeper
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Недопустимая длина строки.")]
        public string Name { get; set; }
    }
}
