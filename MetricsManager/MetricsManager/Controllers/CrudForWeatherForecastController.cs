using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudForWeatherForecastController : ControllerBase
    {
        private readonly List<WeatherForecast> _storage;

        public CrudForWeatherForecastController(List<WeatherForecast> storage)
        {
            this._storage = storage;
        }

        [HttpPost("saveMetrics")]
        public IActionResult SaveMetrics([FromQuery] int? temperatureC, [FromQuery] DateTime? date)
        {
            if (temperatureC.HasValue && date.HasValue)
            {
                _storage.Add(new WeatherForecast { Date = date.Value, TemperatureC = temperatureC.Value });
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("watchSavedMetrics")]
        public IActionResult WatchSaveMetrics([FromQuery] DateTime? fromDate, [FromQuery] DateTime? byDate)
        {
            if (!fromDate.HasValue)
            {
                fromDate = DateTime.MinValue;
            }
            if (!byDate.HasValue)
            {
                byDate = DateTime.MaxValue;
            }

            var items = from weatherForecast in _storage
                        where  weatherForecast.Date >= fromDate.Value && weatherForecast.Date <= byDate.Value
                        select weatherForecast;  
                return Ok(items);
        }
        [HttpDelete("deleteSavedMetrics")]
        public IActionResult DeleteSavedMetrics([FromQuery] DateTime? fromDate, [FromQuery] DateTime? byDate)
        {
            if (fromDate.HasValue && byDate.HasValue)
            {
                var items = from weatherForecast in _storage
                           where weatherForecast.Date >= fromDate.Value && weatherForecast.Date <= byDate.Value
                           select weatherForecast;
                foreach (var item in items.ToList())
                {
                    _storage.Remove(item);
                }
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("updateMetrics")]
        public IActionResult UpdateMetrics([FromQuery] DateTime? date, [FromQuery] int? temperature)
        {
            try
            {
                var updatedWeatherForecast = _storage.Single(weatherForecast => weatherForecast.Date == date.Value);
                updatedWeatherForecast.TemperatureC = temperature.Value;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
