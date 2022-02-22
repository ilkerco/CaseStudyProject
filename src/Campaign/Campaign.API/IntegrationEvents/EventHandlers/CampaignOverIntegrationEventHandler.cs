using Campaign.API.IntegrationEvents.Events;
using Campaign.Application.Interfaces;
using EventBus.Base.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.API.IntegrationEvents.EventHandlers
{
    public class CampaignOverIntegrationEventHandler : IIntegrationEventHandler<CampaignOverIntegrationEvent>
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignOverIntegrationEventHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task Handle(CampaignOverIntegrationEvent @event)
        {
            var campaign = await _campaignRepository.GetAll().Where(x => x.ProductCode == @event.ProductCode).FirstOrDefaultAsync();
            if (campaign != null)
            {
                campaign.EndCampaign();
                await _campaignRepository.UpdateAsync(campaign, campaign.Id);
                await _campaignRepository.SaveChanges();
            }

            await Task.CompletedTask;
        }
    }
}
