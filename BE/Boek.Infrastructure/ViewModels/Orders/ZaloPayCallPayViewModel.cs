using Boek.Infrastructure.Responds.Orders;

namespace Boek.Infrastructure.ViewModels.Orders
{
    public class ZaloPayCallPayViewModel
    {
        public ZaloPayCallBackResponseModel zaloPayCallBackResponseModel { get; set; }
        public List<OrderUpdateModel> orderUpdateModels { get; set; }

        public bool IsValid()
        {
            if (orderUpdateModels != null)
                return orderUpdateModels.Any();
            return false;
        }
    }
}