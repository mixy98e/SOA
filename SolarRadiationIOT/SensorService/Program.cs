using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorService.Runner;
using SensorService.Context;

namespace SensorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //test code
            Console.WriteLine("asdasdassadsadsasad");
            SensorContext sss = SensorContext.Instance;
            Console.WriteLine($"Singleton interval vrednost = {sss.GetInterval()}");
      
            var task = Task.Run(async () => {
                for (int i=0;i<50 ; i++)
                {
                    await Task.Delay(sss.GetInterval());
                    Console.WriteLine("1231231231231123213");

                }
            });
            //test code end

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
     
    }
}
