using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class OutgoingAddDto
    {
        public int No { get; set; }
        public string CompanyName { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Price { get; set; }
        public string? Remark { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }



    }
}
