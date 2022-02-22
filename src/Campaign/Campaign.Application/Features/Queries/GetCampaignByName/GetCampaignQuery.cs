using Campaign.Application.Features.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Features.Queries.GetCampaignByName
{
    public class GetCampaignQuery:IRequest<GetCampaignInfoResponseModel>
    {
        public string CampaignName { get; set; }

        public GetCampaignQuery(string campaignName)
        {
            CampaignName = campaignName;
        }
    }
}
