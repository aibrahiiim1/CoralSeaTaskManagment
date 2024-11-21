using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IEfamilyRepository : IGenericRepository<Efamily>
    {
        void Update(Efamily efamily);
    }
}
