using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Models
{
    public class CreateCampaignRequest
    {
        public string CampaignName { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
    }
}
