using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models.ViewModels
{
    public class WarehouseProductVM
    {
        public WarehouseProduct WarehouseProduct { get; set; }
        public int WarehouseInformationId { get; set; }
        [ForeignKey("WarehouseInformationId")]
        [ValidateNever]
        public IEnumerable<SelectListItem> WarehouseInformationSelectList { get; set; }
        public int ProductSelectId { get; set; }
        [ForeignKey("ProductSelectId")]
        [ValidateNever]

        public IEnumerable<SelectListItem> ProductSelectList { get; set; }
       
        public int NumbProdInWarehouse { get; set; }
    }
}
