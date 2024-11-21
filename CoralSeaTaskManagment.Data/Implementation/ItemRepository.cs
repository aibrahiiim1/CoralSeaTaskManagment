
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Item item)
        {
            var itemindb = _context.Items.FirstOrDefault(x => x.Id == item.Id);
            if (itemindb != null) 
            {
                itemindb.Code = item.Code;
                itemindb.CreatedTime = DateTime.Now;
                itemindb.Name = item.Name;
                itemindb.HotelId = item.HotelId;
                //itemindb.ApplicationUserId = item.ApplicationUserId;
                itemindb.Namear = item.Namear;
                itemindb.EcategoryId = item.EcategoryId;
                itemindb.DepartmentId = item.DepartmentId;
                itemindb.LocationId = item.LocationId;
                itemindb.Manufacturer = item.Manufacturer;
                itemindb.InstallDate = item.InstallDate;
                itemindb.Warranty = item.Warranty;
                itemindb.EfamilyId = item.EfamilyId;
                itemindb.EstatusId = item.EstatusId;
                itemindb.Cost = item.Cost;
                itemindb.WarrantyYear = item.WarrantyYear;
                itemindb.WarrantyStart = item.WarrantyStart;
                itemindb.EclassId = item.EclassId;
                itemindb.Agent = item.Agent;
                itemindb.Pic = item.Pic;

            }
    }
    }
}
