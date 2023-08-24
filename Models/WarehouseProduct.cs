using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class WarehouseProduct
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual WarehouseInformation WarehouseInformation { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int NumbProdInWarehouse { get; set; }
    }
}
