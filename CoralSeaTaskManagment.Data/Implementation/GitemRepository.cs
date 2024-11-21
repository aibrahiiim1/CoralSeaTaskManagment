using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class GitemRepository : GenericRepository<Gitem>, IGitemRepository
    {
        private readonly ApplicationDbContext _context;
        public GitemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Gitem gitem)
        {
            var gitemindb = _context.Gitems.FirstOrDefault(x => x.Id == gitem.Id);
            if (gitemindb != null) 
            {
                gitemindb.CreatedTime = DateTime.Now;
                gitemindb.Name = gitem.Name;
                gitemindb.Namear = gitem.Namear;
                gitemindb.DepartmentId = gitem.DepartmentId;
                gitemindb.HotelId = gitem.HotelId;
                gitemindb.GlocationId = gitem.GlocationId;

            }
        }
    }
}
