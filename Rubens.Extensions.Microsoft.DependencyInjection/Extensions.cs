using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rubens.Components;
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
            var type = typeof(IEventHandler);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass)
                .Where(p => type.IsAssignableFrom(p));
            foreach (var handler in types)
            {
                services.AddSingleton(handler);
            }

            services.AddSingleton<IBus, Bus>();
            services.AddSingleton(ControlPlaneFactory.Create(cfg));
            services.AddSingleton<IHandlersProvider, MicrosoftProvider>();
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