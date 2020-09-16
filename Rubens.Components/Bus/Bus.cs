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
        private readonly IHandlersProvider _provider;

        public Bus(IControlPlane controlPlane, IHandlersProvider provider = null, ILogger<IBus> logger = null)
        {
            _logger = logger;
            _controlPlane = controlPlane;
            _provider = provider;
            _handlers = new ConcurrentDictionary<string, Action<object>>();
            _controlPlane.Emit += (sender, o) =>
            {
                try
                {
                    if (_handlers.TryGetValue(o.Topic, out var handler))
                    {
                        _logger.LogInformation($"emit {o.Topic}");
                        handler.Invoke(o.Event);
                    }
                }
                catch (Exception e)
                {
                    _logger?.LogError(e.Message);
                }
            };
        }

        public async Task Subscribe<T, THandler>() where T : class, IEvent where THandler : class, IEventHandler
        {
            _logger.LogInformation("veri najs");
            if (_provider is null)
            {
                throw new ApplicationException(
                    $"{nameof(IHandlersProvider)} is required for this method to work, please provide it via {nameof(IBus)}'s constructor");
            }

            var topic = typeof(T).Name;
            _logger.LogInformation(topic);
            var resolved = _provider.TryResolve<THandler>(out var instance);
            if (!resolved)
            {
                throw new ApplicationException(
                    $"Could not resolve event handler {nameof(THandler)} for type {nameof(T)}");
            }

            if (!string.IsNullOrEmpty(topic))
            {
                await _controlPlane.Subscribe(topic);
                _handlers[topic] = x =>
                {
                    var @event = JsonConvert.DeserializeObject<T>(x.ToString());
                    instance.Handle(@event);
                };
                _logger?.LogInformation($"Successfully subscribed<,,> on topic : {topic}");
            }
        }

        public async Task Publish<T>(T content) where T : class, IEvent
        {
            if (content != null)
            {
                await _controlPlane.Invoke(content);
                _logger?.LogInformation($"Published new message on topic : {typeof(T).Name}");
                return;
            }

            _logger?.LogCritical($"Cannot publish null message, {nameof(content)}");
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
                _logger?.LogInformation($"Successfully subscribed on topic : {topic}");
            }
        }
    }
}