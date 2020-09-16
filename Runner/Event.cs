using System;
using System.Threading.Tasks;
using Rubens.Components;

namespace Runner
{
    public class Event : IEvent
    {
        
    }
    
    public class EventHandler : IEventHandler
    {
        public async Task Handle<T>(T @event) where T : class, IEvent
        {
            Console.WriteLine("I am handling well 🦋");
        }
    }
}