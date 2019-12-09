using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CreditCards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CreditCardsController> _logger;

        public CreditCardsController(ILogger<CreditCardsController> logger)
        {
            _logger = logger;
        }       
    }
}
