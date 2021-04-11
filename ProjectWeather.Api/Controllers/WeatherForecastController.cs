using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;
using ProjectWeather.Api.Models;
using System;
using System.Linq;
using System.Threading;

namespace ProjectWeather.Api.Controllers
{
    //[Authorize]    
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [FeatureGate(FeatureFlagsConst.WeatherForecastGet)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Startup.WeatherForecasts);
        }

        [FeatureGate(FeatureFlagsConst.WeatherForecastPost)]
        [HttpPost]
        public IActionResult Post(WeatherForecast weatherForecast)
        {
            weatherForecast.Id = Guid.NewGuid();

            Startup.WeatherForecasts.Add(weatherForecast);

            return Created(nameof(Post), weatherForecast);
        }

        [FeatureGate(FeatureFlagsConst.WeatherForecastGetById)]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var weatherForecast = Startup.WeatherForecasts.FirstOrDefault(t => t.Id == id);
            return Ok(weatherForecast);
        }

        [FeatureGate(FeatureFlagsConst.WeatherForecastGetByIdTest)]
        [HttpGet("test/{id}")]
        public IActionResult GetById(Guid id)
        {
            Thread.Sleep(1000);

            var weatherForecast = Startup.WeatherForecasts.FirstOrDefault(t => t.Id == id);
            return Ok(weatherForecast);
        }
    }
}
