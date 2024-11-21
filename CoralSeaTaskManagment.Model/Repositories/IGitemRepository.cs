using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IGitemRepository : IGenericRepository<Gitem>
    {
        void Update(Gitem gitem);
    }
}
