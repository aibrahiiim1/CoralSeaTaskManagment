namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int DepartmentId { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; }



        // Navigation Properties
        public Department Departments { get; set; }
        public Hotel Hotels { get; set; }

    }
}
