namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class AssignDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;


        // Navigation Properties
        public HotelDto? Hotels { get; set; }
        public DepartmentDto? Departments { get; set; }
    }
}
