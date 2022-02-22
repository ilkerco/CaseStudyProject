﻿using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.API.IntegrationEvents.Events
{
    public class CampaignOverIntegrationEvent:IntegrationEvent
    {
        public string ProductCode { get; set; }

        public CampaignOverIntegrationEvent(string productCode)
        {
            ProductCode = productCode;
        }
    }
}
