using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rubens.Components;
using System;

namespace Rubens.Extensions.Microsoft.DependencyInjection
{
    public interface IRubensBuilder
    {

    }

    public class RubensBuilder : IRubensBuilder
    {
        private readonly IBus _bus;

        public RubensBuilder(IBus bus)
        {
            _bus = bus;
        }

    }

    public static class Extensions
    {
        public static IServiceCollection AddRubens(this IServiceCollection services)
        {
            services.AddSingleton<IBus, Bus>();
            return services;
        }

        public static IApplicationBuilder UseRubens(this IApplicationBuilder app, Action<IBus> action)
        {
            var bus = app.ApplicationServices.GetService<IBus>();
            if (bus is null)
            {
                throw new ApplicationException(
                    "Please make sure that you've registered your bus before trying to use it");
            }
            action(bus);
            return app;
        }
    }
}