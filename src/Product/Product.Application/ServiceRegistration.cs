using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Product.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services, Type startup)
        {
            var asm = Assembly.GetExecutingAssembly();
            services.AddMediatR(asm);
            return services;
        }
    }
}
