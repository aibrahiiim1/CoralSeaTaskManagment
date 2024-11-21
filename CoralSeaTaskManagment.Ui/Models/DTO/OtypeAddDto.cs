using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class OtypeAddDto
    {
        public string Name { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public FronthousEnum? Fronthousflag { get; set; }
    }
}
