using Order.Application.Interfaces;
using Order.Domain.SeedWork;
using Order.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Repository
{
    public class OrderRepository : Repository<Order.Domain.AggregateModels.OrderModels.Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context) : base(context)
        {

        }
    }
}
