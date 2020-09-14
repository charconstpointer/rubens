using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rubens.Components.ControlPlane;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Rubens.Components.Bus
{
    public class Bus : IBus
    {
        private readonly IControlPlane _controlPlane;
        private readonly IDictionary<string, Action<object>> _handlers;

        public Bus(IControlPlane controlPlane)
        {
            _controlPlane = controlPlane;
            _handlers = new Dictionary<string, Action<object>>();
            _controlPlane.Emit += (sender, o) =>
            {
                try
                {
                    if (_handlers.TryGetValue(o.Topic, out var handler))
                    {
                        Console.WriteLine(o.Topic);
                        handler.Invoke(o.Event);
                    };
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
            await _controlPlane.Invoke(content);
        }

        public async Task Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            Console.WriteLine($"trying to sub to {typeof(T).Name}");
            await _controlPlane.Subscribe(typeof(T).Name);
            _handlers[typeof(T).Name] = x =>
            {
                var asd = x.ToString();
                var foo = JsonConvert.DeserializeObject<T>(x.ToString());
                action(foo);
            };
            Console.WriteLine($"subbed to {typeof(T).Name}");
        }
    }
}