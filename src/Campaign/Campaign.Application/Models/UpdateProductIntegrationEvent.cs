using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Models
{
    public class UpdateProductIntegrationEvent : IntegrationEvent
    {
        public List<ProductUpdateModel> list;

        public UpdateProductIntegrationEvent(List<ProductUpdateModel> list)
        {
            this.list = list;
        }
    }

    public class ProductUpdateModel
    {
        public string ProductCode { get; set; }
        public int DurationLeft { get; set; }
        public int PriceManipulationLimit { get; set; }

    }
}
