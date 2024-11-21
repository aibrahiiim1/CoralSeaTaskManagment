using System.ComponentModel.DataAnnotations;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public enum EqStatusEnum
    {
        [Display(Name ="None")]
        None=0,
        [Display(Name = "Operation")]
        Operation,
        [Display(Name = "Maintenance")]
        Maintenance,
        [Display(Name = "Outgoing")]
        Outgoing,
        [Display(Name = "Depreciated")]
        Depreciated
    }
}
