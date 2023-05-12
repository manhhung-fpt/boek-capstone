using Boek.Core.Entities;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Levels;
using Boek.Infrastructure.ViewModels.Orders;

namespace Boek.Infrastructure.ViewModels.Users.Customers
{
    public class CustomerOrdersViewModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [Int]
        public int? LevelId { get; set; }
        [DateRange]
        public DateTime? Dob { get; set; }
        [Boolean]
        public bool? Gender { get; set; }
        [Int]
        public int? Point { get; set; }
        public UserViewModel User { get; set; }
        public BasicLevelViewModel Level { get; set; }
        public int TotalOrders { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public List<OrderViewModel> Orders { get; set; }

        public void UpdateTotal(bool? IsDescendingData = null, bool? IsDescendingTimeLine = null)
        {
            if (Orders != null)
            {
                if (Orders.Any())
                {
                    TotalOrders = Orders.Count();
                    Orders.ForEach(o =>
                    {
                        if (o.Total.HasValue)
                            Total += o.Total;
                    });
                    var result = !IsDescendingTimeLine.HasValue || false.Equals(IsDescendingTimeLine);
                    if (IsDescendingData.HasValue && result)
                    {
                        if ((bool)IsDescendingData)
                            Orders = Orders.OrderByDescending(item => item.Total).ToList();
                        else
                            Orders = Orders.OrderBy(item => item.Total).ToList();
                    }
                }
            }
        }
    }
}
