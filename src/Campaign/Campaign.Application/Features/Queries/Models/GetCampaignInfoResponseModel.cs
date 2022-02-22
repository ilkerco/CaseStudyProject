using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Features.Queries.Models
{
    public class GetCampaignInfoResponseModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int TargetSales { get; set; }
        public int TotalSales { get; set; }
        public double Turnover { get; set; }
        public int AverageItemPrice { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrMsg { get; set; }
    }
}
