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
using System.Timers;
using System.IO;


namespace SensorService
{
    public class Program
    {
        

        private static System.Timers.Timer aTimer;
        public static void Main(string[] args)
        {

            //string line = File.ReadLines(".\\DataSource\\SolarPredictionTestShort.csv").Skip(4).Take(1).First();

           // Console.WriteLine(line);
            //SetTimer(500);

            //Console.WriteLine("press to stop timer");
            /*Console.ReadLine();

            aTimer.Stop();
            aTimer.Dispose();*/






            CreateHostBuilder(args).Build().Run();

        }

        /*private static void SetTimer(int startTime)
        {
            aTimer = new System.Timers.Timer(startTime);

            aTimer.Elapsed += OnTimerEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static void ChgTimerTick(int newTime)
        {
            aTimer.Stop();

            //for(int i=0;i<newTime;i++)
            aTimer.Interval = newTime;
            aTimer.Start();
        }

        private static void OnTimerEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Tick tick tick ...", e.SignalTime);
        }*/




        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
     
    }
}


/*
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
*/