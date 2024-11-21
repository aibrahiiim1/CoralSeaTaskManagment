using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        void Update(Item item);
    }
}
