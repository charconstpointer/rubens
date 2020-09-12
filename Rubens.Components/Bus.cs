using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public class Bus : IBus
    {
        private readonly IDictionary<Type, Action<object>> _handlers;
        private readonly Server _server;

        public Bus()
        {
            _handlers = new Dictionary<Type, Action<object>>();
            _server = new Server();
            _server.Emit += (sender, o) =>
            {
                if (_handlers.TryGetValue(o.Type, out var handler))
                {
                    handler.Invoke(o.Event);
                }
            };
        }

        public Task Publish<T>(T content) where T : class, IEvent
        {
            _server.Invoke(content);
            return Task.CompletedTask;
        }

        public Task Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            _handlers[typeof(T)] = x => action((T) x);
            return Task.CompletedTask;
        }
    }
}