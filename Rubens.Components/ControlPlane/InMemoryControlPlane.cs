using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rubens.Components.ControlPlane
{
    public class InMemoryControlPlane : IControlPlane
    {
        public EventHandler<EventEmit> Emit { get; set; }
        public async Task Subscribe(string @event)
        {
        }

        public async Task Invoke<T>(T @event) where T : class, IEvent
        {
            var message = new EventEmit
            {
                Event = JsonConvert.SerializeObject(@event),
                Topic = typeof(T).Name
            };
            Emit.Invoke(null, message);
        }
    }
}