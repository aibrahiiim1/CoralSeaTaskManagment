namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class PeriorityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
