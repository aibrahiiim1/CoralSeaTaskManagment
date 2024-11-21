namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Glocation
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;


        // Navigation Properties
        public Hotel? Hotels { get; set; }

    }
}
