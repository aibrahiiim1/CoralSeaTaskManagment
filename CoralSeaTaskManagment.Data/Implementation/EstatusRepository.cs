
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class EstatusRepository : GenericRepository<Estatus>, IEstatusRepository
    {
        private readonly ApplicationDbContext _context;
        public EstatusRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Estatus estatus)
        {
            var estatusindb = _context.Estatuses.FirstOrDefault(x => x.Id == estatus.Id);
            if (estatusindb != null)
            {  
                estatusindb.Name = estatus.Name;
                estatusindb.CreatedTime = DateTime.Now;
            }
        }
    }
}
