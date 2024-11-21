using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class EcategoryAddDto
    {
        public string Name { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

    }
}
