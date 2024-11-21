namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; }



        // Navigation Properties
        public Hotel Hotels { get; set; }
    }
}
