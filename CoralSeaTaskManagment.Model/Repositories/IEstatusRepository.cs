using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IEstatusRepository : IGenericRepository<Estatus>
    {
        void Update(Estatus estatus);
    }
}
