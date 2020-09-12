using System;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public interface IServer
    {
        Task Invoke<T>(T @event);
        EventHandler<EventEmit> Emit { get; set; }
    }
}