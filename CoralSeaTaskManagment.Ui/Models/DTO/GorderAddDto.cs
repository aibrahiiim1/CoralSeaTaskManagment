using CoralSeaTaskManagment.Model.Models.Domain;
using System.Diagnostics.CodeAnalysis;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class GorderAddDto
    {        //[DisplayName("CreatedTime")]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
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

        //[DisplayName("From")]
        public int GdepartmentFId { get; set; }

        //[DisplayName("AssignFlag")]
        public AssignEnum? AssignFlag { get; set; }
    }
}
