using Product.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.AggregateModels.ProductModels
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string ProductCode { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public decimal SalePrice { get; private set; }

        public Product(string productCode, decimal price, int stock)
        {
            if (price <= 0)
                throw new Exception("Product price must be greater than 0");
            if (stock <= 0)
                throw new Exception("Product stock must be greater than 0");

            ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
            Price = price;
            Stock = stock;
            SalePrice = price;
        }

        public void UpdateStock(int soldCount)
        {
            Stock -= soldCount;
        }
        public void ResetPrice()
        {
            SalePrice = Price;
        }
        public void DecreasePrice(int manipulationLimit)
        {
            var limitPrice = Price - (Price * manipulationLimit/100);
            if (SalePrice == Price)
            {
                var piece = (Price - limitPrice) / 6;
                SalePrice = Price - piece;
            }
            else
            {
                var diff = (Price - SalePrice)*100/Price;
                SalePrice -= (SalePrice * diff / 100) * (decimal)(1.3);
            }

            if (SalePrice < limitPrice)
            {
                SalePrice = Price;
            }

        }
    }
}
