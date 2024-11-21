namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class AssignationAddDto
    {
        public int AssignId { get; set; }

        public int OrderId { get; set; }

        public int HotelId { get; set; }

        public string? TechName { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
    }
}
