
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class EfamilyRepository : GenericRepository<Efamily>, IEfamilyRepository
    {
        private readonly ApplicationDbContext _context;
        public EfamilyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Efamily efamily)
        {
            var efamilyindb = _context.Efamilies.FirstOrDefault(x => x.Id == efamily.Id);
            if (efamilyindb != null)
            {   
                efamilyindb.Name = efamily.Name;
                efamilyindb.CreatedTime = DateTime.Now;
            }
        }
    }
}
