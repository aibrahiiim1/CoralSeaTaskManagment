using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        void Update(Location location);
    }
}
