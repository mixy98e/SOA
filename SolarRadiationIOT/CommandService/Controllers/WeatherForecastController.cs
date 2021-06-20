using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public ActionResult<string> GetTest()
        {
            Command.Command cs = new Command.Command();
            cs.testfun(6900f, 69f);
            return $"[CommandService] I am alive";
        }
    }
}
