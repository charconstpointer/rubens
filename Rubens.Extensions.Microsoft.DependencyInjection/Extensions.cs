using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rubens.Components;
using System;

namespace Rubens.Extensions.Microsoft.DependencyInjection
{
    public interface IRubensBuilder
    {

    }

    public class RubensBuilder
    {
    }

    public static class RubensBuilderExtensions
    {
        private static string _connectionString;
        public static RubensBuilder WithServer(this RubensBuilder builder, string connectionString)
        {
            _connectionString = connectionString;
            return builder;
        }
    }

    public static class Extensions
    {
        public static IServiceCollection AddRubens(this IServiceCollection services, Action<RubensBuilder> options = null)
        {
            var bus = new Bus();

            services.AddSingleton<IBus>(_ => bus);
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