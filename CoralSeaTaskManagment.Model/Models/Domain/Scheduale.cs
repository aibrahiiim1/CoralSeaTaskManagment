using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Scheduale
    {      
        public int Id { get; set; }
        public string Reason { get; set; }
        public int OrderId { get; set; }
        public int HotelId { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }


        // Navigation Properties
        public Hotel? Hotels { get; set; }
        public Order? Orders { get; set; }
    }
}
