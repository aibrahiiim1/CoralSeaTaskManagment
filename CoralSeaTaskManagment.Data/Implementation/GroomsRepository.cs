using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class GroomsRepository : GenericRepository<Grooms>, IGroomsRepository
    {
        private readonly ApplicationDbContext _context;
        public GroomsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Grooms grooms)
        {
            var groomsindb = _context.Grooms.FirstOrDefault(x => x.Id == grooms.Id);
            if (groomsindb != null) 
            {   
                groomsindb.Name = grooms.Name;
                groomsindb.Building = grooms.Building;
                groomsindb.CreatedTime = DateTime.Now;
                groomsindb.HotelId = grooms.HotelId;
            }
        }
    }
}
