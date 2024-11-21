namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class GlocationAddDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
