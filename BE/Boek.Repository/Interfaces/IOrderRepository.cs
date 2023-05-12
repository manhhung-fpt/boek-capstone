using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public double GetTotal(Order order);
        public double GetSubTotal(Order order);
        Task<Order> AddAsyncCustom(Order entity);
        IQueryable<Order> GetOrdersByCustomer(Guid? id);
    }
}
