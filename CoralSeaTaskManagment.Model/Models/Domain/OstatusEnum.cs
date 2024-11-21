using System.ComponentModel.DataAnnotations;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public enum OstatusEnum
    {
        [Display(Name ="None")]
        None=0,
        [Display(Name = "Solved")]
        Solved,
        [Display(Name = "In Progress")]
        Inprogress,
        [Display(Name = "Open")]
        Open,
        [Display(Name = "OutGoing")]
        OutGoing,
        [Display(Name = "Scheduale")]
        Schedualed,
        [Display(Name = "Closed")]
        Closed,
        [Display(Name = "Canceled")]
        Canceled,
        [Display(Name = "ReOpen")]
        ReOpen,
        [Display(Name = "Pending")]
        Pending

    }
}
