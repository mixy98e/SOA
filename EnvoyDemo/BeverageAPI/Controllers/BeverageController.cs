using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BeverageController : ControllerBase
    {
        private static readonly string[] Drinks = new[]
        {
            "Beer", "Rakija", "Whiskey", "Coffee", "Tea", "Vodka", "Vine"
        };

        [HttpGet]
        public IActionResult Get()
        {
            var rand = new Random();
            return Ok(Drinks[rand.Next(Drinks.Length)]);
        }
    }
}
