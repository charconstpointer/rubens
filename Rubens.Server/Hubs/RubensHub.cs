using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rubens.Components;

namespace Rubens.Server.Hubs
{
    public class RubensHub : Hub
    {
        private readonly ILogger<RubensHub> _logger;

        public RubensHub(ILogger<RubensHub> logger)
        {
            _logger = logger;
        }

        public async Task Subscribe(string topic)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, topic);
            _logger.LogInformation($"Sub request @ {topic}");
        }

        public async Task SendMessage(EventEmit emitEvent)
        {
            var group = emitEvent.Topic;
            _logger.LogInformation($"Received new request @ {group}");
            await Clients.Group(group).SendAsync("NewMessage", emitEvent);
        }
    }
}
