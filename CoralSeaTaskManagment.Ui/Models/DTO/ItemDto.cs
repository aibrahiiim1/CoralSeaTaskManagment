using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Namear { get; set; }
        public int EcategoryId { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public string Manufacturer { get; set; }
        public DateTime InstallDate { get; set; }
        public string? Warranty { get; set; }
        public int EfamilyId { get; set; }
        public EqStatusEnum statusFlag { get; set; } = EqStatusEnum.Operation;
        public int EstatusId { get; set; } = 1;
        public int Cost { get; set; }
        public int WarrantyYear { get; set; }
        public DateTime WarrantyStart { get; set; }
        public int EclassId { get; set; }
        public string Agent { get; set; }
        public string? Pic { get; set; }
        public int HotelId { get; set; }

        //public string? ApplicationUserId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public WarrantyEnum? WarrantyFlag { get; set; } = WarrantyEnum.No;


        // Navigation Properties
        public Hotel? Hotels { get; set; }
        public Eclass? Eclasses { get; set; }
        public Efamily? Efamilies { get; set; }
        public Location? Locations { get; set; }
        public Department? Departments { get; set; }
        public Ecategory? Ecategories { get; set; }

        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }
    }
}
