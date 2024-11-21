using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IAssignationRepository : IGenericRepository<Assignation>
    {
        void Update(Assignation assignation);
    }
}
