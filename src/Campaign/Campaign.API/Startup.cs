using Campaign.API.IntegrationEvents.EventHandlers;
using Campaign.API.IntegrationEvents.Events;
using Campaign.Application.Interfaces;
using Campaign.Infrastructure.Data;
using Campaign.Infrastructure.Repository;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Campaign.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("configured-inner-handler").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    AllowAutoRedirect = false
                };
            });

            services.AddDbContext<CampaignDbContext>(options =>
            //options.UseSqlServer("Server=45.200.120.200;Database=TakasDb;User Id=ilker8118;Password=i.S07051997352435"));
            options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Campaign.API", Version = "v1", Description = "Campaign API" });
            });

            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "CampaignService",
                    EventBusType = EventBusType.RabbitMQ
                };

                return EventBusFactory.Create(config, sp);
            });
            services.AddTransient<OrderCreatedIntegrationEventHandler>();
            services.AddTransient<CampaignOverIntegrationEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CampaignDbContext>();
                context.Database.Migrate();
            }
            app.UseCors("CorsPolicy");
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Campaign API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
            eventBus.Subscribe<CampaignOverIntegrationEvent, CampaignOverIntegrationEventHandler>();
        }
    }
}
