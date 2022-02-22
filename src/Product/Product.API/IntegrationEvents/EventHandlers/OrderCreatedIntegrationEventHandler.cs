using EventBus.Base.Abstraction;
using Product.API.IntegrationEvents.Events;
using Product.API.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Product.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        
        private readonly IProductRepository _productRepository;

        public OrderCreatedIntegrationEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {

            var product = await _productRepository.GetAll().Where(x => x.ProductCode == @event.ProductCode).FirstOrDefaultAsync();
            product.UpdateStock(@event.Quantity);
            await _productRepository.UpdateAsync(product, product.Id);
            await _productRepository.SaveChanges();
            await Task.CompletedTask;

        }
    }
}
