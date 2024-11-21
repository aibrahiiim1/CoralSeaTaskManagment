using CoralSeaTaskManagment.Model.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class OtypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public FronthousEnum? Fronthousflag { get; set; }


        // Navigation Properties
        public Hotel? Hotels { get; set; }
        public IEnumerable<SelectListItem> otypeEnumList { get; set; }
    }
}
