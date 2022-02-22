using EventBus.Base.Abstraction;
using MediatR;
using Order.Application.Events;
using Order.Application.Features.Commands.Models;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,IEventBus eventBus)
        {
            _eventBus = eventBus;
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = new Order.Domain.AggregateModels.OrderModels.Order(
                    request.ProductCode, request.Quantity, request.ProductPrice * request.Quantity);
                await _orderRepository.AddAsync(order);
                var isSuccess = await _orderRepository.SaveChanges() > 0;
                if (isSuccess)
                {
                    _eventBus.Publish(new OrderCreatedIntegrationEvent(request.ProductCode, request.Quantity,request.ProductPrice));
                    return new CreateOrderResponse { IsSuccess = true, ProductCode = order.ProductCode, Quantity = order.Quantity };
                }
                return new CreateOrderResponse { IsSuccess = false, ErrMsg = "Error while creating product" };

            }
            catch (Exception ex)
            {
                return new CreateOrderResponse { IsSuccess = false, ErrMsg = ex.Message };
            }
        }
    }
}
