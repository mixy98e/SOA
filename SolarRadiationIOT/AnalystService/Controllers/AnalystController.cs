using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalystService.Analyst;
using AnalystService.Model;
using AnalystService.Mqtt;

namespace AnalystService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnalystController : ControllerBase
    {
        DataAnalyst _da = new DataAnalyst();


        [HttpGet("test")]
        public ActionResult<string> GetTest()
        {
            return $"[AnalystService] I am alive (log path = {_da.Path})";
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
