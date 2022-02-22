using Campaign.Application.Features.Commands.Models;
using Campaign.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Features.Commands.CreateCampaignCommand
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CreateCampaignResponseModel>
    {
        private readonly ICampaignRepository _campaignRepository;

        public CreateCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<CreateCampaignResponseModel> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var campaign = new Campaign.Domain.AggregateModels.CampaignModels.Campaign(
                    request.Name, request.ProductCode, request.Duration, request.PriceManipulationLimit, request.TargetSalesCount,
                    true,0,0,0
                    );
                await _campaignRepository.AddAsync(campaign);
                var isSuccess = await _campaignRepository.SaveChanges()>0;
                if (isSuccess)
                {
                    return new CreateCampaignResponseModel
                    {
                        Name = campaign.Name,
                        PriceManipulationLimit = campaign.PriceManipulationLimit,
                        ProductCode = campaign.ProductCode,
                        TargetSalesCount = campaign.TargetSalesCount,
                        Duration = campaign.Duration,
                        IsSuccess = true,
                    };
                }

                return new CreateCampaignResponseModel { ErrMsg = "Error while creating campaign", IsSuccess = false };

            }
            catch(Exception ex)
            {
                return new CreateCampaignResponseModel { ErrMsg = ex.Message, IsSuccess = false };
            }
        }
    }
}
