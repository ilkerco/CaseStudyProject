using Campaign.Application.Features.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Features.Commands.CreateCampaignCommand
{
    public class CreateCampaignCommand:IRequest<CreateCampaignResponseModel>
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }

        public CreateCampaignCommand(string name, string productCode, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            Name = name;
            ProductCode = productCode;
            Duration = duration;
            PriceManipulationLimit = priceManipulationLimit;
            TargetSalesCount = targetSalesCount;
        }
    }
}
