using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models.ViewModels
{
    public class WarehouseProductVM
    {
        public WarehouseProduct WarehouseProduct { get; set; }
        public IEnumerable<SelectListItem> WarehouseInformationSelectList { get; set; }

        public IEnumerable<SelectListItem> ProductSelectList { get; set; }
       
        //public int NumbProdInWarehouse { get; set; }
    }
}
