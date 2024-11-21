using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Order order)
        {
            var orderindb = _context.Orders.FirstOrDefault(x => x.Id == order.Id);
            if (orderindb != null) 
            {   
                orderindb.CreatedTime = DateTime.Now;
                orderindb.ApplicationUserId = order.ApplicationUserId;
                orderindb.Comment=order.Comment;
                orderindb.DepartmentFId=order.DepartmentFId;
                orderindb.HotelId=order.HotelId;
                orderindb.ItemId=order.ItemId;
                orderindb.DepartmentId=order.DepartmentId;
                orderindb.LocationId=order.LocationId;
                orderindb.OstatusId=order.OstatusId;
                orderindb.OtypeId=order.OtypeId;
                orderindb.Pic=order.Pic;
                orderindb.PeriorityId=order.PeriorityId;
            }
        }
    }
}
