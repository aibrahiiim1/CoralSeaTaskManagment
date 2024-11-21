using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        void Update(Department department);
    }
}
