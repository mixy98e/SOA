using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorService.Model;
using System.Net.Http;
using SensorService.Runner;
using SensorService.Context;

namespace SensorService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private SensorContext _sc = SensorContext.Instance;

        [HttpGet("metadata")]
        public ActionResult<Object> GetMetaData()
        {
            return new
            {
                interval = _sc.GetInterval(),
                threshold = _sc.GetThreshold(),
                dataSource = _sc.GetSourcePath()
            };
        }

        [HttpGet("start")]
        public IActionResult GetStart()
        {
            SensorRunner.StartSensor();
            
            return Ok();
        }

        [HttpGet("stop")]
        public IActionResult GetStop()
        {
            SensorRunner.StopSensor();
            return Ok();
        }

        [HttpPut("interval")]
        public IActionResult PutInt([FromBody]int newInterval)
        {
            //if (sr == null) return NotFound();
            SensorRunner.ChgSensorInterval(newInterval);
            return Ok();
        }

        [HttpPut("threshold")]
        public IActionResult PutThrs([FromBody]float newThreshold)
        {
            //if (sr == null) return NotFound();
            SensorRunner.ChgSensorThreshold(newThreshold);
            return Ok();
        }

        //[HttpGet("datapath/{newPath}")]
        //public IActionResult GetPath(string newPath)
        //{
        //    //if (sr == null) return NotFound();
        //    SensorRunner.ChgSensorDataPath(newPath);
        //    return Ok();
        //}
    }
}
