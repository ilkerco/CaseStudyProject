using Campaign.Application.Interfaces;
using Campaign.Application.Models;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Features.Commands.IncreasedTimeCommand
{
    public class IncreasedTimeCommandHandler : IRequestHandler<IncreasedTimeCommand>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IEventBus _eventBus;

        public IncreasedTimeCommandHandler(ICampaignRepository campaignRepository,IEventBus eventBus)
        {
            _campaignRepository = campaignRepository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(IncreasedTimeCommand request, CancellationToken cancellationToken)
        {
            var productsToBeUpdate = new List<ProductUpdateModel>();
            var campaigns = await _campaignRepository.GetAll().Where(x => x.IsActive == true).ToListAsync();
            if (campaigns != null)
            {
                foreach(var campaign in campaigns)
                {
                    
                    campaign.UpdateDuration(request.IncreasedCount);
                    productsToBeUpdate.Add(new ProductUpdateModel {
                        DurationLeft = campaign.Duration,
                        PriceManipulationLimit = campaign.PriceManipulationLimit,
                        ProductCode = campaign.ProductCode
                    });
                    await _campaignRepository.UpdateAsync(campaign, campaign.Id);
                }

                await _campaignRepository.SaveChanges();
                _eventBus.Publish(new UpdateProductIntegrationEvent(productsToBeUpdate));
            }
            return Unit.Value;
        }
    }
}
