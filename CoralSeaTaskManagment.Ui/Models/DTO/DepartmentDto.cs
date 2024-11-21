
using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{

  
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; }



        // Navigation Properties
        public HotelDto Hotels { get; set; }
    }
}
