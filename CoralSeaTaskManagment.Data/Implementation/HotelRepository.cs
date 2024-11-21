using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class HotelRepository: GenericRepository<Hotel>, IHotelRepository
    {
        private readonly ApplicationDbContext _context;
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public  async void Update(int id, Hotel hotel)
        {
            await Task.Run(() => {

                var hotelindb = _context.Hotels.FirstOrDefault(x => x.Id == id);
                if (hotelindb != null)
                {
                    hotelindb.Name = hotel.Name;
                }
            });  
            
        }
    }
}
