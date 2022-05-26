using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {

            services.AddTransient<ICurrencyExchange, CurrencyExchange>();
            services.AddTransient<IExceptionLogging, ExceptionLogging>();


            return services;
        }

    }
}
