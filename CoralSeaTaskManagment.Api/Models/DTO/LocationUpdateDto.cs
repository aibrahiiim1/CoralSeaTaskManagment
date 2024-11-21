using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class LocationUpdateDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int DepartmentId { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
