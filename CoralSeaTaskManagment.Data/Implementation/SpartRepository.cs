
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class SpartRepository : GenericRepository<Spart>, ISpartRepository
    {
        private readonly ApplicationDbContext _context;
        public SpartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Spart spart)
        {
            var spartindb = _context.Sparts.FirstOrDefault(x => x.Id == spart.Id);
            if (spartindb != null) 
            {   
                spartindb.Q = spart.Q;
                spartindb.CreatedTime = DateTime.Now;
                spartindb.Name = spart.Name;
                spartindb.HotelId = spart.HotelId;
                spartindb.ItemId = spart.ItemId;
                spartindb.Price = spart.Price;
                spartindb.Unit = spart.Unit;
                spartindb.ApplicationUserId = spart.ApplicationUserId;
                spartindb.OrderId = spart.OrderId;
            }
        }
    }
}
