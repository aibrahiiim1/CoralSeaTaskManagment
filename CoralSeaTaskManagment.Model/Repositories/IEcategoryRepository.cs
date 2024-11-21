using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IEcategoryRepository :IGenericRepository<Ecategory>
    {
        void Update(Ecategory ecategory);
    }
}
