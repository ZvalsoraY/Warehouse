using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warehouse.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set; }
    }
}
