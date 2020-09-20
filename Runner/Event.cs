using System;
using System.Threading.Tasks;
using Rubens.Components;

namespace Runner
{
    public class Event : IEvent
    {
        
    }
    
    public class EventHandler : IEventHandler<Event>
    {
        public async Task Handle(Event @event)
        {
            Console.WriteLine("I am handling well 🦋");
        }
    }
}