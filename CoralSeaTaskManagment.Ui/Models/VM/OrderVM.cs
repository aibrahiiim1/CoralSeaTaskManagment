using CoralSeaTaskManagment.Model.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CoralSeaTaskManagment.Ui.Models.VM
{
    public class OrderVM
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string OtypeId { get; set; }
        public string LocationId { get; set; }
        public string ItemId { get; set; }
        public string DepartmentId { get; set; }
        public string? OstatusId { get; set; }
        public string PeriorityId { get; set; }
        public string Comment { get; set; }
        public string? Pic { get; set; }
        [AllowNull]
        public string HotelId { get; set; }
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }
        public int? PickupId { get; set; }
        public string DepartmentFId { get; set; }
        public string? AssignFlag { get; set; }
    }
}
