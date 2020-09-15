using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rubens.Components.ControlPlane;

namespace Rubens.Components.Bus
{
    public class Bus : IBus
    {
        private readonly IControlPlane _controlPlane;
        private readonly IDictionary<string, Action<object>> _handlers;

        public Bus(IControlPlane controlPlane)
        {
            _controlPlane = controlPlane;
            _handlers = new ConcurrentDictionary<string, Action<object>>();
            _controlPlane.Emit += (sender, o) =>
            {
                try
                {
                    if (_handlers.TryGetValue(o.Topic, out var handler))
                    {
                        handler.Invoke(o.Event);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            };
        }

        public async Task Publish<T>(T content) where T : class, IEvent
        {
            if (content != null)
            {
                await _controlPlane.Invoke(content);
            }
        }

        public async Task Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            var topic = typeof(T).Name;
            if (!string.IsNullOrEmpty(topic))
            {
                await _controlPlane.Subscribe(topic);
                _handlers[topic] = x =>
                {
                    var @event = JsonConvert.DeserializeObject<T>(x.ToString());
                    action(@event);
                };
            }
        }
    }
}