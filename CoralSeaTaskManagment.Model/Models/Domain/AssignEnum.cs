using System.ComponentModel.DataAnnotations;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public enum AssignEnum
    {
        [Display(Name ="None")]
        None=0,
        [Display(Name = "Assigned")]
        Assigned,
        [Display(Name = "Not Assigned")]
        NotAssigned

    }
}
