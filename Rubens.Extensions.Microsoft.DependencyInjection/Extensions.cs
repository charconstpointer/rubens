using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rubens.Components.Bus;
using Rubens.Components.Configuration;
using Rubens.Components.ControlPlane;

namespace Rubens.Extensions.Microsoft.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddRubens(this IServiceCollection services,
            Action<RubensConfiguration> options = null)
        {
            var cfg = new RubensConfiguration();
            options?.Invoke(cfg);
            services.AddSingleton<IBus, Bus>();
            services.AddSingleton<IControlPlane, ControlPlane>();
            services.AddSingleton(cfg);
            return services;
        }

        public static IApplicationBuilder UseRubens(this IApplicationBuilder app, Action<IBus> action)
        {
            var bus = app.ApplicationServices.GetService<IBus>();
            if (bus is null)
                throw new ApplicationException(
                    "Please make sure that you've registered your bus before trying to use it");
            action(bus);
            return app;
        }
    }
}