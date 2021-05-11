﻿using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private SensorContext _sc = SensorContext.Instance;

        [HttpGet("start")]
        public IActionResult GetStart()
        {
            SensorRunner.StartSensor();
            
            return Ok();
        }

        [HttpGet("interval/{newInterval}")]
        public IActionResult GetInt(int newInterval)
        {
            //if (sr == null) return NotFound();
            SensorRunner.ChgSensorInterval(newInterval);
            return Ok();
        }

        [HttpGet("thrshold/{newThreshold}")]
        public IActionResult GetThrs(float newThreshold)
        {
            //if (sr == null) return NotFound();
            SensorRunner.ChgSensorThreshold(newThreshold);
            return Ok();
        }
    }
}
