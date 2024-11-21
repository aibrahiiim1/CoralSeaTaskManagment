namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class GroomAddDto
    {
        public string Name { get; set; }
        public string? Building { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
