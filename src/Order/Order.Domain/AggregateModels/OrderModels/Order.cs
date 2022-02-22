using Order.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.AggregateModels.OrderModels
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public string ProductCode { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public Order(string productCode, int quantity,decimal price)
        {
            if (quantity <= 0)
            {
                throw new Exception("Order quantity must be greater than 0");
            }
            ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
            Quantity = quantity;
            Price = price;
        }
    }
}
