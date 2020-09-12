using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rubens.Components.ControlPlane;

namespace Rubens.Components.Bus
{
    public class Bus : IBus
    {
        private readonly IControlPlane _controlPlane;
        private readonly IDictionary<Type, Action<object>> _handlers;

        public Bus(IControlPlane controlPlane)
        {
            _controlPlane = controlPlane;
            _handlers = new Dictionary<Type, Action<object>>();
            _controlPlane.Emit += (sender, o) =>
            {
                if (_handlers.TryGetValue(o.Type, out var handler)) handler.Invoke(o.Event);
            };
        }

        public async Task Publish<T>(T content) where T : class, IEvent
        {
            await _controlPlane.Invoke(content);
        }

        public async Task Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            await _controlPlane.Subscribe(typeof(T).Name);
            _handlers[typeof(T)] = x => action((T) x);
        }
    }
}