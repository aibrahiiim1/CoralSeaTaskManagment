namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class SchedualeAddDto
    {
        public string Reason { get; set; }
        public int OrderId { get; set; }
        public int HotelId { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }
    }
}
