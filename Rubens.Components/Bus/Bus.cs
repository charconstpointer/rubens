using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rubens.Components.ControlPlane;

namespace Rubens.Components.Bus
{
    public class Bus : IBus
    {
        private readonly IControlPlane _controlPlane;
        private readonly IDictionary<string, Action<object>> _handlers;
        private readonly ILogger<IBus> _logger;
        public Bus(IControlPlane controlPlane, ILogger<IBus> logger = null)
        {
            _logger = logger;
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
                    _logger.LogError(e.Message);
                }
            };

        }

        public async Task Publish<T>(T content) where T : class, IEvent
        {
            if (content != null)
            {
                await _controlPlane.Invoke(content);
                _logger.LogInformation($"Published new message on topic : {typeof(T).Name}");
                return;
            }
            _logger.LogCritical($"Cannot publish null message, {nameof(content)}");
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
                _logger.LogInformation($"Successfully subscribed on topic : {topic}");
            }
        }
    }
}