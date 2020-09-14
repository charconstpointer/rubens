using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Rubens.Components.Configuration;

namespace Rubens.Components.ControlPlane
{
    public class ControlPlane : IControlPlane
    {
        private readonly HubConnection _connection;

        public ControlPlane(RubensConfiguration rubensConfiguration)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"{rubensConfiguration.ConnectionString}/rubens")
                .Build();
            _connection.StartAsync().GetAwaiter().GetResult();
        }

        public EventHandler<EventEmit> Emit { get; set; }

        public async Task Subscribe(string @event)
        {
            await _connection.InvokeAsync("Subscribe", @event);
            _connection.On<EventEmit>("NewMessage", emit => { Emit(null, emit); });
        }

        public async Task Invoke<T>(T @event) where T : class, IEvent
        {
            var message = new EventEmit
            {
                Event = @event,
                Topic = typeof(T).Name
            };

            await _connection.InvokeAsync("SendMessage", message);
        }
    }
}