namespace Rubens.Components
{
    public interface IHandlersProvider
    {
        bool TryResolve<T>(out T instance) where T : class, IEventHandler;
    }
}