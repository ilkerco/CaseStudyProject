using Campaign.Application.Features.Queries.Models;
using Campaign.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Features.Queries.GetCampaignByName
{
    public class GetCampaignQueryHandler : IRequestHandler<GetCampaignQuery, GetCampaignInfoResponseModel>
    {
        private ICampaignRepository _campaignRepository;

        public GetCampaignQueryHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<GetCampaignInfoResponseModel> Handle(GetCampaignQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var campaign = await _campaignRepository.GetAll().Where(x => x.Name == request.CampaignName).FirstOrDefaultAsync();
                if (campaign == null)
                {
                    return new GetCampaignInfoResponseModel { IsSuccess = false, ErrMsg = "There is no campaign with this Name", };
                }
                return new GetCampaignInfoResponseModel
                {
                    AverageItemPrice = campaign.AverageItemPrice,
                    Name = campaign.Name,
                    Status = campaign.IsActive ? "Active" : "Ended",
                    TargetSales = campaign.TargetSalesCount,
                    TotalSales = campaign.TotalSales,
                    Turnover = campaign.TurnOverRate,
                    IsSuccess = true
                };

            }
            catch(Exception ex)
            {
                return new GetCampaignInfoResponseModel { IsSuccess = false, ErrMsg = ex.Message };
            }
        }
    }
}
