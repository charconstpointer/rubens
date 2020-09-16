using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rubens.Components;
using System.Threading.Tasks;

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
            _logger.LogInformation($"Subscription request for topic : {topic}");
        }

        public async Task SendMessage(EventEmit emitEvent)
        {
            var group = emitEvent.Topic;
            if (!string.IsNullOrEmpty(group))
            {
                _logger.LogInformation($"Send message for topic : {group}");
                await Clients.Group(group).SendAsync("NewMessage", emitEvent);
            }
        }
    }
}