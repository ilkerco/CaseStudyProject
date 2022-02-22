using EventBus.Base.Abstraction;
using Microsoft.EntityFrameworkCore;
using Product.API.IntegrationEvents.Events;
using Product.API.Models;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.IntegrationEvents.EventHandlers
{
    public class UpdateProductIntegrationEventHandler : IIntegrationEventHandler<UpdateProductIntegrationEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventBus _eventBus;

        public UpdateProductIntegrationEventHandler(IProductRepository productRepository,IEventBus eventBus)
        {
            _productRepository = productRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(UpdateProductIntegrationEvent @event)
        {
            foreach(var product in @event.list)
            {
                var productEntity = await _productRepository.GetAll().Where(x => x.ProductCode == product.ProductCode).FirstOrDefaultAsync();
                if(product.DurationLeft == 0)
                {
                    productEntity.ResetPrice();
                }
                else
                {
                    productEntity.DecreasePrice(product.PriceManipulationLimit);
                    if(productEntity.SalePrice == productEntity.Price)
                    {
                        //reached price limit campaign is over
                        _eventBus.Publish(new CampaignOverIntegrationEvent(product.ProductCode));
                    }
                }

                await _productRepository.UpdateAsync(productEntity, productEntity.Id);
                
            }
            await _productRepository.SaveChanges();

            await Task.CompletedTask;
        }
    }
}
