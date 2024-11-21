using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface ISchedualeRepository : IGenericRepository<Scheduale>
    {
        void Update(Scheduale scheduale);
    }
}
