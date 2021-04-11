using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using ProjectWeather.Api.Models;
using System;

namespace ProjectWeather.Api.Controllers
{
    [FeatureGate(FeatureFlagsConst.Promotion)] // Promotion feature flag must be enabled
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(string name)
        {
            var number = Guid.NewGuid();

            return Created(nameof(Post), new { number });
        }
    }
}
