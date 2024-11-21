
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class EclassRepository : GenericRepository<Eclass>, IEclassRepository
    {
        private readonly ApplicationDbContext _context;
        public EclassRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Eclass eclass)
        {
            var eclassindb = _context.Eclasses.FirstOrDefault(x => x.Id == eclass.Id);
            if (eclassindb != null)
            {
                eclassindb.Name = eclass.Name;
                eclassindb.CreatedTime = DateTime.Now;
            }
        }
    }
}
