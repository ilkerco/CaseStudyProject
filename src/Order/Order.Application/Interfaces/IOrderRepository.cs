using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Interfaces
{
    public interface IOrderRepository : IRepository<Order.Domain.AggregateModels.OrderModels.Order>
    {
    }
}
