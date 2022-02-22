using Campaign.API.Models;
using Campaign.Application.Features.Commands.CreateCampaignCommand;
using Campaign.Application.Features.Commands.IncreasedTimeCommand;
using Campaign.Application.Features.Queries.GetCampaignByName;
using Campaign.Application.Models;
using Campaign.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Campaign.API.Controllers
{
    [Route("api/[controller]")]
    public class CampaignController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;

        public CampaignController(IMediator mediator,IHttpClientFactory clientFactory)
        {
            _mediator = mediator;
            _clientFactory = clientFactory;
        }

        [HttpPost("increase_time")]
        public async Task<IActionResult> IncreaseTime([FromBody] IncreaseTimeModel increaseTimeModel)
        {
            TimerService.AddTime(increaseTimeModel.Hour);
            if (increaseTimeModel.Hour != 0)
            {
                var res = await _mediator.Send(new IncreasedTimeCommand(increaseTimeModel.Hour));
            }
            return Ok("time is " + TimerService.GetTimeStr());
        }
        [HttpGet("get_campaign_info/{campaignName}")]
        public async Task<IActionResult> GetCampaignInfo(string campaignName)
        {
            var res = await _mediator.Send(new GetCampaignQuery(campaignName));
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res.ErrMsg);
        }

        [HttpPost("create_campaign")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignRequest createCampaignRequest)
        {
            try
            {
                HttpClient httpClient = _clientFactory.CreateClient();
                Uri url = new Uri("http://ocelot/api/product/Product/get_product_info/" + createCampaignRequest.ProductCode);
                //Uri url = new Uri("http://localhost:8000/api/product/get_product_info/" + createCampaignRequest.ProductCode);
                HttpResponseMessage response = await httpClient.GetAsync(url);

                var content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest(content);
                }

                var productResponse = JsonConvert.DeserializeObject<GetProductResponseModel>(content);
                if (productResponse.Stock < createCampaignRequest.TargetSalesCount)
                {
                    return BadRequest("Target Sales Count must be less than product's Stock");
                }

                var res = await _mediator.Send(new CreateCampaignCommand(createCampaignRequest.CampaignName, createCampaignRequest.ProductCode,
                    createCampaignRequest.Duration, createCampaignRequest.PriceManipulationLimit, createCampaignRequest.TargetSalesCount));

                if (res.IsSuccess)
                    return Ok(res);
                return BadRequest(res.ErrMsg);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
            
        }
    }
}
