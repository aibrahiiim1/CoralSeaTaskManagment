
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class OtypeRepository : GenericRepository<Otype>, IOtypeRepository
    {
        private readonly ApplicationDbContext _context;
        public OtypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Otype otype)
        {
            var otypeindb = _context.Otypes.FirstOrDefault(x => x.Id == otype.Id);
            if (otypeindb != null) 
            {   
                otypeindb.Name = otype.Name;
                otypeindb.CreatedTime = DateTime.Now;
            }
        }
    }
}
