using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Gorder
    {
        //[DisplayName("ID")]
        public int Id { get; set; } = 0;
        //[DisplayName("CreatedTime")]
        public DateTime CreatedTime { get; set; } 
        //[DisplayName("Order Type")]
        public int OtypeId { get; set; }

        //[DisplayName("Room#")]
        public int GroomsId { get; set; }

        //[DisplayName("Location")]
        public int GlocationId { get; set; }

        //[DisplayName("Item")]
        public int GitemId { get; set; }

        //[DisplayName("DepTo")]
        public int DepartmentId { get; set; }

        //[DisplayName("Status#")]
        public OstatusEnum OstatusId { get; set; }

        public string Comment { get; set; }
        [AllowNull]
        //[DisplayName("Hotel")]
        public int HotelId { get; set; }

        //[DisplayName("User")]
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }
        [ForeignKey(nameof(GdepartmentFrom))]
        //[DisplayName("From")]
        public int GdepartmentFId { get; set; }

        //[DisplayName("AssignFlag")]
        public AssignEnum? AssignFlag { get; set; }




        // Navigation Properties


        [ValidateNever]
        public Department? GdepartmentFrom { get; set; }
        [ValidateNever]
        public Hotel? Hotels { get; set; }

        [ValidateNever]
        public Department? Departments { get; set; }

        [ValidateNever]
        public Gitem? Gitems { get; set; }
        [ValidateNever]
        public Glocation? Glocations { get; set; }

        [ValidateNever]
        public Otype? Otypes { get; set; }
        [ValidateNever]
        public Grooms? Grooms { get; set; }
    }
}
