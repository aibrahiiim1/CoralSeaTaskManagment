namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Gitem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Namear { get; set; }
        public int DepartmentId { get; set; }
        public int GlocationId { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; }=DateTime.Now;


        // Navigation Properties
        public Hotel? Hotels { get; set; }
        public Glocation? Glocations { get; set; }
        public Department? Departments { get; set; }


    }
}
