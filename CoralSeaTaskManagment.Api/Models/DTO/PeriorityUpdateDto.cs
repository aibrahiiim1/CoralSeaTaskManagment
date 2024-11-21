namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class PeriorityUpdateDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
