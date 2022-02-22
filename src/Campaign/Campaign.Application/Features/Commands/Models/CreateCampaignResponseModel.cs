using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Features.Commands.Models
{
    public class CreateCampaignResponseModel
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrMsg { get; set; }
    }
}
