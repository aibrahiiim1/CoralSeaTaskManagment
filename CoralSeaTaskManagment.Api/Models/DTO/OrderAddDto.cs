using CoralSeaTaskManagment.Model.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class OrderAddDto
    {
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public int OtypeId { get; set; }
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public int DepartmentId { get; set; }
        public OstatusEnum OstatusId { get; set; }
        public int PeriorityId { get; set; }
        public string Comment { get; set; }
        public string? Pic { get; set; }
        [AllowNull]
        public int HotelId { get; set; }
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }
        public int? PickupId { get; set; }
        public int DepartmentFId { get; set; }
        public AssignEnum? AssignFlag { get; set; }

    }
}
