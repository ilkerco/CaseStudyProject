using Campaign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Domain.AggregateModels.CampaignModels
{
    public class Campaign:BaseEntity,IAggregateRoot
    {
        public string Name { get; private set; }
        public string ProductCode { get; private set; }
        public int Duration { get; private set; }
        public int PriceManipulationLimit { get; private set; }
        public int TargetSalesCount { get; private set; }
        public bool IsActive { get; private set; }
        public int TotalSales { get; private set; }
        public int AverageItemPrice { get; private set; }
        public decimal TotalMoney { get; private set; }
        public double TurnOverRate { get; private set; }//ortalama satış fiyatı/stock adeti

        public Campaign(
            string name,
            string productCode, 
            int duration,
            int priceManipulationLimit,
            int targetSalesCount,
            bool isActive,
            int totalSales,
            int averageItemPrice,
            double turnOverRate)
        {

            if(priceManipulationLimit>=100)
                throw new Exception("Price manipulation limit must be less than 100");

            Name = name ?? throw new ArgumentNullException(nameof(name));
            ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
            Duration = duration;
            PriceManipulationLimit = priceManipulationLimit;
            TargetSalesCount = targetSalesCount;
            IsActive = isActive;
            TotalSales = totalSales;
            AverageItemPrice = averageItemPrice;
            TurnOverRate = turnOverRate;
        }

        public void UpdateDuration(int increasedTime)
        {
            Duration -= increasedTime;
            if (Duration <= 0)
            {
                Duration = 0;
                IsActive = false;
                //AverageItemPrice = Decimal.ToInt32(TotalMoney) / TotalSales;

            }
        }
        public void UpdateProperties(decimal price,int quantity)
        {
            TotalSales += quantity;
            TotalMoney += price * quantity;
            AverageItemPrice = Decimal.ToInt32(TotalMoney) / TotalSales;
            TurnOverRate = AverageItemPrice / TotalSales;

            if(TotalSales>= TargetSalesCount)
            {
                IsActive = false;
            }

        }

        public void EndCampaign()
        {
            IsActive = false;
        }
       
    }

}
