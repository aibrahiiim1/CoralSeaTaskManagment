using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IHotelRepository:IGenericRepository<Hotel>
    {
        void Update(int id, Hotel hotel);
    }
}
