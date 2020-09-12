using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rubens.Components.Storage;

namespace Rubens.Components
{
    public class Bus : IBus
    {
        private readonly IRubensStorage _storage;
        public Bus(IRubensStorage storage)
        {
            _storage = storage;
        }

        public async Task Publish<T>(T @event) where T : class, IEvent
        {
            await _storage.Save(@event);
        }

        public async Task Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            await _storage.AddHandler(action);
        }
    }
}