namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Spart
    {      
        public int Id { get; set; }
        public int Q { get; set; }
        public string Name { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public double? Price { get; set; }
        public string Unit { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }



        // Navigation Properties
        public Hotel? Hotels { get; set; }
        public Item? Items { get; set; }
        public Order? Orders { get; set; }

        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }

    }
}
