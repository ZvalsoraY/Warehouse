using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warehouse.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        //public int ApplicationTypeId { get; set; }
        //public virtual ApplicationType? ApplicationType { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set; }
    }
}
