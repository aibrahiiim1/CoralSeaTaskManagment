using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IGlocationRepository : IGenericRepository<Glocation>
    {
        void Update(Glocation glocation);
    }
}
