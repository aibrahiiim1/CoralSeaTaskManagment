namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class GroomUpdateDto
    {
        public string Name { get; set; }
        public string? Building { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
