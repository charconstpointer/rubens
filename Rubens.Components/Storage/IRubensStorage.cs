using System;
using System.Threading.Tasks;

namespace Rubens.Components.Storage
{
    public interface IRubensStorage
    {
        Task Save<T>(T @event) where T : class, IEvent;
        Task AddHandler<T>(Action<T> action) where T : class, IEvent;
    }
}