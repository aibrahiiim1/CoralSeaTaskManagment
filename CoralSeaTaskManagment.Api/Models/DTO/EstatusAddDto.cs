using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class EstatusAddDto
    {
        public string Name { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

    }
}
