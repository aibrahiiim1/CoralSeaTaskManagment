using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Model.Repositories
{
    public interface IOutgoingRepository : IGenericRepository<Outgoing>
    {
        void Update(Outgoing outgoing);
    }
}
