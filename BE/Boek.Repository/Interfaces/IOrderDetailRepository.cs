using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        public double GetTotal(OrderDetail orderDetail);
        public double GetSubTotal(OrderDetail orderDetail);
    }
}
