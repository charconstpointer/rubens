using System;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public interface IControlPlane
    {
        Task Subscribe(string client, string @event);
        Task Invoke<T>(T @event);
        EventHandler<EventEmit> Emit { get; set; }
    }
}