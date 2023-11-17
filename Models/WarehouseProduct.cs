using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Warehouse.Models
{
    /// <summary>
    /// Класс WarehouseProduct,
    /// хранит информацию о томк какой продукт хранится на определенном складе и в каком количестве
    /// </summary>
    public class WarehouseProduct
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        [ValidateNever]
        public virtual WarehouseInformation WarehouseInformation { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public virtual Product Product { get; set; }
        public int NumbProdInWarehouse { get; set; }
    }
}
