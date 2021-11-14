using LandingRocket.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingRocket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LandingRocketController : ControllerBase
    {
        private readonly ILandingRocketService _landingRocketService;
        private readonly ILogger<LandingRocketController> _logger;

        public LandingRocketController(ILogger<LandingRocketController> logger, ILandingRocketService landingRocketService)
        {
            _landingRocketService = landingRocketService;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> IsLaningAvailable (int landing_x, int landing_y)
        {
            var result = await _landingRocketService.LandingAvailability(landing_x, landing_y);
            return Ok(result);
        }
    }
}
