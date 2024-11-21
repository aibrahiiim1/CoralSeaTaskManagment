using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IOtypeRepository : IGenericRepository<Otype>
    {
        void Update(Otype otype);
    }
}
