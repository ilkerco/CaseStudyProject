using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.API.Models;
using Product.API.Services.Interfaces;
using Product.Application.Features.Commands.CreateProduct;
using Product.Application.Features.Queries.GetProductByProductCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get_product_info/{productCode}")]
        public async Task<IActionResult> GetProductInfo(string productCode)
        {
            var res = await _mediator.Send(new GetProductQuery(productCode));
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res.ErrMsg);
        }
        [HttpPost("create_product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
        {
            var res = await _mediator.Send(new CreateProductCommand(createProductRequest.ProductCode, createProductRequest.Price, createProductRequest.Stock));
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res.ErrMsg);
        }
    }
}
