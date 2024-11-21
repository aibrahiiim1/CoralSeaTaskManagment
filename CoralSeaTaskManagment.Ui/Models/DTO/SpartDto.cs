using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class SpartDto
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
        public HotelDto? Hotels { get; set; }
        public ItemDto? Items { get; set; }
        public OrderDto? Orders { get; set; }

        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }
    }
}
