namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class SpartAddDto
    {
        public int Q { get; set; }
        public string Name { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public double? Price { get; set; }
        public string Unit { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
    }
}
