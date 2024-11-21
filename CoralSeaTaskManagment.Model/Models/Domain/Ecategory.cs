namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Ecategory
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;


        // Navigation Properties
        public Hotel? Hotels { get; set; }

    }
}
