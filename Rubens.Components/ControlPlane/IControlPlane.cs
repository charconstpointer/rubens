using System;
using System.Threading.Tasks;

namespace Rubens.Components.ControlPlane
{
    public interface IControlPlane
    {
        EventHandler<EventEmit> Emit { get; set; }
        Task Subscribe(string @event);
        Task Invoke<T>(T @event) where T : class, IEvent;
    }
}