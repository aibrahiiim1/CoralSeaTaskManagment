using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public DateTime CreatedTime { get; set; }



        // Navigation Properties
        public Department Departments { get; set; }
        public Hotel Hotels { get; set; }
    }
}
