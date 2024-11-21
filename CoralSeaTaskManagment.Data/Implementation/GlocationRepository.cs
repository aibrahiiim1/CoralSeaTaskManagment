using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class GlocationRepository : GenericRepository<Glocation>, IGlocationRepository
    {
        private readonly ApplicationDbContext _context;
        public GlocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Glocation glocation)
        {
            var glocationindb = _context.Glocations.FirstOrDefault(x => x.Id == glocation.Id);
            if (glocationindb != null) 
            {   
                glocationindb.Name = glocation.Name;
                glocationindb.NameAr = glocation.NameAr;
                glocationindb.CreatedTime = DateTime.Now;
                glocationindb.HotelId = glocation .HotelId;
            }
        }
    }
}
