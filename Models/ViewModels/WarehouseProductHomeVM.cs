using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Warehouse.Models.ViewModels
{
    public class WarehouseProductHomeVM
    {
        public IEnumerable<WarehouseProduct> WarehouseProducts { get; set; }
        //public IEnumerable<WarehouseInformation> WarehouseInformationSelectList { get; set; }
        public SelectList ProductSelectList { get; set; }
        public SelectList WarehouseInformationSelectList { get; set; }

        public int NumbProdInWarehouse { get; set; }
    }
}
