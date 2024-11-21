using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        void Update(Order order);
    }
}
