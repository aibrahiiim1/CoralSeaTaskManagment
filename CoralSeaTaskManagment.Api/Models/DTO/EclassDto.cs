using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class EclassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;


        // Navigation Properties
        public Hotel? Hotels { get; set; }
    }
}
