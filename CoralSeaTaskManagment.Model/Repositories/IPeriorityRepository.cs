using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IPeriorityRepository : IGenericRepository<Periority>
    {
        void Update(Periority periority);
    }
}
