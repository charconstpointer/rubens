using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public class Bus : IBus
    {
        private readonly IDictionary<Type, Action<object>> _handlers;
        private readonly IControlPlane _controlPlane;

        public Bus()
        {
            _handlers = new Dictionary<Type, Action<object>>();
            _controlPlane = new ControlPlane("https://localhost:5001");
            _controlPlane.Emit += (sender, o) =>
            {
                if (_handlers.TryGetValue(o.Type, out var handler))
                {
                    handler.Invoke(o.Event);
                }
            };
        }

        public async Task Publish<T>(T content) where T : class, IEvent
        {
            Console.WriteLine("Publish");
            await _controlPlane.Invoke(content);
        }

        public async Task Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            Console.WriteLine("Subscribe");
            await _controlPlane.Subscribe("", typeof(T).Name);
            _handlers[typeof(T)] = x => action((T) x);
        }
    }
}