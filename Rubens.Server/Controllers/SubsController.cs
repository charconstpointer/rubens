using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rubens.Components;

namespace Rubens.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubsController : ControllerBase
    {
        private readonly ILogger<SubsController> _logger;

        public SubsController(ILogger<SubsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SubscribeToEvent command)
        {
            _logger.LogInformation($"{command.EventName}");
            return Ok();
        }

        [HttpPost("emit")]
        public async Task<IActionResult> Emit(EmitEvent command)
        {
            _logger.LogInformation("Emit");
            return Ok();
        }
    }
}