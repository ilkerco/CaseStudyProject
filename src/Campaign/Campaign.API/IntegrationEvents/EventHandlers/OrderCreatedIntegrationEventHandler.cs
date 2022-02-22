using Campaign.API.IntegrationEvents.Events;
using Campaign.API.IntegrationEvents.Models;
using Campaign.Application.Interfaces;
using EventBus.Base.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ICampaignRepository _campaignRepository;

        public OrderCreatedIntegrationEventHandler(ICampaignRepository campaignRepository, IEventBus eventBus)
        {
            _campaignRepository = campaignRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            var campaignExist = await _campaignRepository.GetAll()
                .Where(x => x.ProductCode == @event.ProductCode && x.IsActive==true).FirstOrDefaultAsync();
            if (campaignExist != null)
            {
                var list = new List<ProductUpdateModel>();
                campaignExist.UpdateProperties(@event.Price, @event.Quantity);
                if (!campaignExist.IsActive)
                {
                    //campaign is over make ur product sale price default
                    list.Add(new ProductUpdateModel { DurationLeft = 0, ProductCode = @event.ProductCode, PriceManipulationLimit = 0 });
                    _eventBus.Publish(new UpdateProductIntegrationEvent(list));
                }
                await _campaignRepository.UpdateAsync(campaignExist, campaignExist.Id);

                await _campaignRepository.SaveChanges();
            }




                await Task.CompletedTask;



            

        }
    }
}
