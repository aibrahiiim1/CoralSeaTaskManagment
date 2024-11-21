using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class AssignationDto
    {
        public int Id { get; set; }
        public int AssignId { get; set; }

        public int OrderId { get; set; }

        public int HotelId { get; set; }

        public string? TechName { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }


        // Navigation Properties
        public Order? Orders { get; set; }
        public Hotel? Hotels { get; set; }
        public Assign? Assigns { get; set; }
    }
}
