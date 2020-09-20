using System.Threading.Tasks;

namespace Rubens.Components
{
    public interface IEventHandler<T> where T : class, IEvent
    {
        Task Handle(T @event);
    }
}