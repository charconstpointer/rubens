using System;
using System.Collections.Generic;

namespace Rubens.Components
{
    public class Server
    {
        public EventHandler<EventEmit> Emit;
        private readonly IDictionary<string, string> _subs;

        public Server()
        {
            _subs = new Dictionary<string, string>();
        }

        public void Invoke<T>(T @event)
            => Emit.Invoke(null, new EventEmit {Type = typeof(T), Event = @event});
    }
}