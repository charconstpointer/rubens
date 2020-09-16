using System;
using Microsoft.Extensions.DependencyInjection;
using Rubens.Components;

namespace Rubens.Extensions.Microsoft.DependencyInjection
{
    public class MicrosoftProvider : IHandlersProvider
    {
        private readonly IServiceProvider _services;

        public MicrosoftProvider(IServiceProvider services)
        {
            _services = services;
        }

        public bool TryResolve<T>(out T instance) where T : class, IEventHandler
        {
            instance = _services.GetRequiredService<T>();
            return instance != null;
        }
    }
}