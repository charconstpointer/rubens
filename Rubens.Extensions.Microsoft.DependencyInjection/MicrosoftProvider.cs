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

        public bool TryResolve<T, TH>(out T instance) where TH : class, IEvent where T : class, IEventHandler<TH>
        {
            instance = _services.GetRequiredService<T>();
            return instance != null;
        }
    }
}