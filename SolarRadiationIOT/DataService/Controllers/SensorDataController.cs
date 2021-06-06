using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Repository;
using DataService.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using DataService.Mqtt;

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
            // Upis u bazu
            repository.PostSensorData(data);

            var c = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
            Console.WriteLine($"{content}");

            var publisher = new Publisher();
            publisher.Publish(content.ToString(), "DataServiceQueue");


            return Ok();
        }
    }
}
