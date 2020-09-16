using System;
using System.Threading.Tasks;

namespace Rubens.Components.Bus
{
    public interface IBus
    {
        Task Subscribe<T>(Action<T> action) where T : class, IEvent;
        Task Subscribe<T, TH>() where T : class, IEvent where TH : class, IEventHandler;
        Task Publish<T>(T @event) where T : class, IEvent;
    }
}