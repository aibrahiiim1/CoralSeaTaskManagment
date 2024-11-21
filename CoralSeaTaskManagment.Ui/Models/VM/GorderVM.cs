using CoralSeaTaskManagment.Model.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CoralSeaTaskManagment.Ui.Models.VM
{
    public class GorderVM
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string OtypeId { get; set; }
        public string GroomsId { get; set; }
        public string GlocationId { get; set; }
        public string GitemId { get; set; }
        public string DepartmentId { get; set; }
        public string OstatusId { get; set; }

        public string Comment { get; set; }
        [AllowNull]
        public string HotelId { get; set; }
        public string? ApplicationUserId { get; set; }
        public string GdepartmentFId { get; set; }
        public string? AssignFlag { get; set; }
    }
}
