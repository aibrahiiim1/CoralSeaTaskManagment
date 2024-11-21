using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class EfamilyAddDto
    {
        public string Name { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

    }
}
