using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rubens.Components.Storage
{
    public class InMemoryRubensStorage : IRubensStorage
    {
        private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
        public Task Save<T>(T @event) where T : class, IEvent
        {
            if (!_handlers.TryGetValue(typeof(T), out var handlers))
            {
                var type = typeof(T);
                _handlers[type] = new List<Action<T>>();

            }
            foreach (var handler in (List<Action<T>>)handlers)
            {
                Task.Run(() => handler.Invoke(@event));
            }
            return Task.CompletedTask;
        }

        public Task AddHandler<T>(Action<T> action) where T : class, IEvent
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers[type] = new List<Action<T>>();
            }
            var handlers = (List<Action<T>>)_handlers[type];
            handlers.Add(action);
            return Task.CompletedTask;
        }
    }
}