
using System.ComponentModel.DataAnnotations;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public enum WarrantyEnum
    {
        [Display(Name ="None")]
        None=0,
        [Display(Name = "Warranty")]
        Yes,
        [Display(Name = "No Warranty")]
        No

    }
}
