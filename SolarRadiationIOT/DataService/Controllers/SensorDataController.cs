using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Repository;
using DataService.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private ISolarPredictionRepository repository;

        public SensorDataController(ISolarPredictionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("all")]
        public IEnumerable<SensorData> GetAll()
        {
            return repository.GetSensorData();
        }

        [HttpGet("last")]
        public ActionResult<SensorData> GetLast()
        {
            return repository.GetLastSensorData();
        }

        [HttpPost]
        public IActionResult Post([FromBody] SensorData data)
        {
            repository.PostSensorData(data);

            return Ok();//CreatedAtAction(nameof(GetAll), new { id = data.UnixTime }, data);
        }
    }
}
