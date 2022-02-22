using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Interfaces
{
    public interface ICampaignRepository : IRepository<Campaign.Domain.AggregateModels.CampaignModels.Campaign>
    {
    }
}
