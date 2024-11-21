namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class GitemUpdateDto
    {
        public string Name { get; set; }
        public string Namear { get; set; }
        public int DepartmentId { get; set; }
        public int GlocationId { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
