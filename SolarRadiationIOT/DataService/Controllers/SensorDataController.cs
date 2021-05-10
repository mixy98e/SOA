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
        private readonly IMongoClient client;

        public SensorDataController(IMongoClient client)
        {
            this.client = client;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<SensorData>> GetAll()
        {
            var db = client.GetDatabase("iot");
            var collection = db.GetCollection<SensorData>("sensor-data");

            return collection.Find(FilterDefinition<SensorData>.Empty).ToList();
        }

        [HttpGet("last")]
        public ActionResult<SensorData> GetLast()
        {
            var db = client.GetDatabase("iot");
            var collection = db.GetCollection<SensorData>("sensor-data");

            return collection.Find(FilterDefinition<SensorData>.Empty).ToList().Last();
        }

        [HttpPost]
        public IActionResult Post([FromBody] SensorData data)
        {
            var db = client.GetDatabase("iot");
            var collection = db.GetCollection<SensorData>("sensor-data");
            
            collection.InsertOne(data);
            return CreatedAtAction(nameof(GetAll), new { id = data.UnixTime }, data);
        }
    }
}
