namespace Rubens.Components
{
    public interface IHandlersProvider
    {
        bool TryResolve<T, TH>(out T instance) where TH : class, IEvent where T : class, IEventHandler<TH>;
    }
}