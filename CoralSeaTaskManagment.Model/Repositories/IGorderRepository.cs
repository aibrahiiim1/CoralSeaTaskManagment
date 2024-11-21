using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IGorderRepository : IGenericRepository<Gorder>
    {
        void Update(Gorder gorder);
    }
}
