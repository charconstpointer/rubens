using System;

namespace Rubens.Components
{
    public interface IBus
    {
        void Subscribe<T>(Action<T> action) where T : class, IEvent;
        void Publish<T>(T @event) where T : class, IEvent;
    }
}