using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public DateTime CreatedTime { get; set; }



        // Navigation Properties
        public DepartmentDto Departments { get; set; }
        public HotelDto Hotels { get; set; }
    }
}
