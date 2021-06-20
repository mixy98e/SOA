using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalystService.Analyst;
using AnalystService.Model;
using AnalystService.Mqtt;
using AnalystService.Repository;

namespace AnalystService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnalystController : ControllerBase
    {
        private IAnalystServiceRepository repository;

        DataAnalyst _da = new DataAnalyst();

        public AnalystController(IAnalystServiceRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            repository.PostAnalystResult(new AnalystResult()
            {
                DayTimeDay = true,
                HighRisk = false,
                RadiationHigh = false,
                TimeStamp = DateTime.Now,
                WeatherGood = true
            });
            return Ok();
        }

        [HttpGet("startstop")]
        public IActionResult Get()
        {
            _da.Running = !_da.Running;
            return Ok();
        }
        
        [HttpGet("log")]
        public IEnumerable<AnalystResult> GetLog()
        {
            return _da.ReadLog();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SensorData sd)
        {
            await _da.Analyze(sd);
            return Ok();
        }

    }
}
