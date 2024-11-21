
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class SchedualeRepository : GenericRepository<Scheduale>, ISchedualeRepository
    {
        private readonly ApplicationDbContext _context;
        public SchedualeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Scheduale scheduale)
        {
            var schedualeindb = _context.Scheduales.FirstOrDefault(x => x.Id == scheduale.Id);
            if (schedualeindb != null) 
            {   
                schedualeindb.CreatedTime = DateTime.Now;
                schedualeindb.Reason = scheduale.Reason;
                schedualeindb.HotelId = scheduale.HotelId;
                schedualeindb.OrderId = scheduale.OrderId;
                schedualeindb.ReturnDate = scheduale.ReturnDate;
                schedualeindb.ApplicationUserId = scheduale.ApplicationUserId;
            }
        }
    }
}
