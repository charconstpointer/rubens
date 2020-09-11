using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public class Bus : IBus
    {
        private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();

        public void Publish<T>(T @event) where T : class, IEvent
        {
            if (@event is null)
            {
                throw new ArgumentNullException();
            }
            if (!_handlers.TryGetValue(typeof(T), out var handlers))
            {
                var type = typeof(T);
                _handlers[type] = new List<Action<T>>();

            }
            foreach (var handler in (List<Action<T>>)handlers)
            {
                Task.Run(() => handler.Invoke(@event));
            }
        }

        public void Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers[type] = new List<Action<T>>();
            }
            var handlers = (List<Action<T>>)_handlers[type];
            handlers.Add(action);
        }
    }
}