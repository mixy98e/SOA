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
        public async Task<ActionResult> Post([FromBody] SensorData data)
        {
            repository.PostSensorData(data);

            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                Console.WriteLine($"{content.ToString()}");

                try
                {
                    using (var response = await httpClient.PostAsync("http://host.docker.internal:5002/Analyst/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return new JsonResult(
                            new
                            {
                                resp = apiResponse,
                                message = "Data successfully sent",
                            }
                        );
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                return null;
            }

            return Ok();//CreatedAtAction(nameof(GetAll), new { id = data.UnixTime }, data);
        }
    }
}
