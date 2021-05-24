using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private static readonly string[] Food = new[]
        {
            "Meat", "Toast", "Burger", "Pizza", "Pasta", "Ice Cream", "Cake"
        };

        [HttpGet]
        public IActionResult Get()
        {
            var rand = new Random();
            return Ok(Food[rand.Next(Food.Length)]);
        }
    }
}
