using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IGroomsRepository : IGenericRepository<Grooms>
    {
        void Update(Grooms grooms);
    }
}
