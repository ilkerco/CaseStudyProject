using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Order.Application.Features.Commands.CreateOrder;
using Order.Application.Interfaces;
using Order.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;
        public OrderController(IMediator mediator ,IHttpClientFactory clientFactory)
        {
            _mediator = mediator;
            _clientFactory = clientFactory;
        }

        [HttpPost("create_order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            HttpClient httpClient = _clientFactory.CreateClient();
            Uri url = new Uri("http://ocelot/api/product/Product/get_product_info/"+createOrderRequest.ProductCode);
            //Uri url = new Uri("http://localhost:8000/api/product/get_product_info/" + createOrderRequest.ProductCode);
            HttpResponseMessage response = await httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(content); 
            }

            var productResponse = JsonConvert.DeserializeObject<GetProductResponse>(content);
            if (productResponse.Stock < createOrderRequest.Quantity)
            {
                return BadRequest("Stock is " + productResponse.Stock);
            }

            var res = await _mediator.Send(new CreateOrderCommand(
                createOrderRequest.ProductCode, createOrderRequest.Quantity, productResponse.Price));
            //var data = await _orderService.CreateOrder(createOrderRequest.ProductCode,createOrderRequest.Quantity,productResponse.Price);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res.ErrMsg);
        }
    }
}
