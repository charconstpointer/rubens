using System;
using System.Threading.Tasks;

namespace Rubens.Components.Bus
{
    public interface IBus
    {
        Task Subscribe<T>(Action<T> action) where T : class, IEvent;
        Task Publish<T>(T @event) where T : class, IEvent;
    }
}