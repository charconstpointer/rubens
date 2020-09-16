using System.Threading.Tasks;

namespace Rubens.Components
{
    public interface IEventHandler
    {
        Task Handle<T>(T @event) where T : class, IEvent;
    }
}