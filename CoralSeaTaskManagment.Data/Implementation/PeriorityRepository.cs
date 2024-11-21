using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class PeriorityRepository : GenericRepository<Periority>, IPeriorityRepository
    {
        private readonly ApplicationDbContext _context;
        public PeriorityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Periority periority)
        {
            var periorityindb = _context.Periorities.FirstOrDefault(x => x.Id == periority.Id);
            if (periorityindb != null) 
            {   
                periorityindb.Name = periority.Name;
                periorityindb.CreatedTime = DateTime.Now;
            }
        }
    }
}
