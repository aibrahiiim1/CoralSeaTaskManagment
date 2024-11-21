

using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Location location)
        {
            var locationindb = _context.Locations.FirstOrDefault(x => x.Id == location.Id);
            if (locationindb != null) 
            {   
                locationindb.Name = location.Name;
                locationindb.NameAr = location.NameAr;
                locationindb.CreatedTime = DateTime.Now;
                locationindb.DepartmentId = location.DepartmentId;
                locationindb.HotelId = location.HotelId;
            }
        }
    }
}
