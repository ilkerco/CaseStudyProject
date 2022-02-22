using Campaign.Application.Interfaces;
using Campaign.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Infrastructure.Repository
{
    public class CampaignRepository : Repository<Campaign.Domain.AggregateModels.CampaignModels.Campaign>, ICampaignRepository
    {
        public CampaignRepository(CampaignDbContext context) : base(context)
        {

        }
    }
}
