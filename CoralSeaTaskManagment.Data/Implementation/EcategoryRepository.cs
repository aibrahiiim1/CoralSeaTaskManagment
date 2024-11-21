
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class EcategoryRepository : GenericRepository<Ecategory>, IEcategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public EcategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Ecategory ecategory)
        {
            var ecategoryindb = _context.Ecategories.FirstOrDefault(x => x.Id == ecategory.Id);
            if (ecategoryindb != null)
            {
                ecategoryindb.Name = ecategory.Name;
                ecategoryindb.CreatedTime = DateTime.Now;
            }
        }
    }
}
