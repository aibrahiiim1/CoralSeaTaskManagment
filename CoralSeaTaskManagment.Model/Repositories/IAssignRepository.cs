using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IAssignRepository : IGenericRepository<Assign>
    {
        void Update(Assign assign);
    }
}
