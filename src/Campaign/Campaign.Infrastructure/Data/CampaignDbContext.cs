using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Infrastructure.Data
{
    public class CampaignDbContext : DbContext
    {
        public CampaignDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Campaign.Domain.AggregateModels.CampaignModels.Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /*builder.Entity<Campaign.Domain.AggregateModels.CampaignModels.Campaign>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");*/
        }
    }
}
