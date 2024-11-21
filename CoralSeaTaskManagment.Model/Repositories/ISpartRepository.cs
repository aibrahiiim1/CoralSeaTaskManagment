using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface ISpartRepository : IGenericRepository<Spart>
    {
        void Update(Spart spart);
    }
}
