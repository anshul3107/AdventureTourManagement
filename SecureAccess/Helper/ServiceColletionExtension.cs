using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAccess.Helper
{
    public static class ServiceColletionExtension
    {
        public static IServiceCollection AddSecureAccessService(this IServiceCollection services)
        {
            services.AddScoped<TwoStepAuth>();
            services.AddScoped<SecureAccessFactory>();

            return services;
        }
    }
}
