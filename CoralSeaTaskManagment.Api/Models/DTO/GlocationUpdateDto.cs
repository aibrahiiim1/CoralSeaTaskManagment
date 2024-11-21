namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class GlocationUpdateDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
