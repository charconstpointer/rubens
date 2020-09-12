using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public class InMemoryServer : IServer
    {
        private readonly IDictionary<string, string> _subs;
        public InMemoryServer()
        {
            _subs = new Dictionary<string, string>();
        }

        public async Task Invoke<T>(T @event)
            => Emit.Invoke(null, new EventEmit {Type = typeof(T), Event = @event});

        public EventHandler<EventEmit> Emit { get; set; }
    }
}