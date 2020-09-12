using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public class InMemoryControlPlane : IControlPlane
    {
        private readonly IDictionary<string, string> _subs;
        public InMemoryControlPlane()
        {
            _subs = new Dictionary<string, string>();
        }

        public async Task Subscribe(string client, string @event)
        {
            throw new NotImplementedException();
        }

        public async Task Invoke<T>(T @event)
            => Emit.Invoke(null, new EventEmit {Type = typeof(T), Event = @event});

        public EventHandler<EventEmit> Emit { get; set; }
    }
}