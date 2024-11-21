using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IEclassRepository : IGenericRepository<Eclass>
    {
        void Update(Eclass eclass);
    }
}
