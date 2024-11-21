using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class GitemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Namear { get; set; }
        public int DepartmentId { get; set; }
        public int GlocationId { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;


        // Navigation Properties
        public Hotel? Hotels { get; set; }
        public Glocation? Glocation { get; set; }
        public Department? Departments { get; set; }
    }
}
