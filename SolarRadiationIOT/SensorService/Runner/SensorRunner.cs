using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorService.Model;
using SensorService.Context;
using System.Timers;
using System.IO;
using SensorService.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SensorService.Runner
{
    public class SensorRunner
    {
        private static System.Timers.Timer _aTimer;

        private static SensorContext _sc = SensorContext.Instance;

        private static int currLine;
        public static void StartSensor()
        {
           currLine = 0;
           SetTimer(_sc.GetInterval());
        }

        private static void SetTimer(int initTickTime)
        {
            _aTimer = new System.Timers.Timer(initTickTime);

            _aTimer.Elapsed += OnTimerEvent;
            _aTimer.AutoReset = true;
            _aTimer.Enabled = true;
        }

        private static void OnTimerEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Tick tick tick ...", e.SignalTime);
            SendData();
        }

        public static void ChgSensorInterval(int newTickTime)
        {
            _sc.SetInterval(newTickTime);
            _aTimer.Stop();
            _aTimer.Interval = newTickTime;
            _aTimer.Start();
        }

        private static void SendData()
        {
            Console.WriteLine($"uso0, {_sc.GetSourcePath()}");
            string line = File.ReadLines(_sc.GetSourcePath()).Skip(currLine).Take(1).First();
            currLine++;

            string[] parsedData = line.Split(',');

            SensorData sensorData = new SensorData();
            sensorData.UnixTime = int.Parse(parsedData[0]);
            sensorData.Radiation = float.Parse(parsedData[3]);
            sensorData.Temperature = float.Parse(parsedData[4]);
            sensorData.Pressure = float.Parse(parsedData[5]);
            sensorData.WindDirection = float.Parse(parsedData[6]);
            sensorData.Speed = float.Parse(parsedData[7]);
            sensorData.TimeSunRise = parsedData[8];
            sensorData.TimeSunSet = parsedData[9];

            sendViaRest(sensorData);

            float x = 0.15f;
            if (x > _sc.GetThreshold())
                Console.WriteLine($"threshodl passed - SENDING DATA {line}");
            else Console.WriteLine($"threshodl NOT passed - NOT SENDING DATA {line}");
        }

        //public methods
        public static void ChgSensorThreshold(float newThreshold)
        {
            _sc.SetThreshold(newThreshold);
            Console.WriteLine(_sc.GetThreshold());
        }

        //public void ChgSensorSourcePath(string sourcePath)
        //{
        //    _sc.SetSourcePath(sourcePath);
        //}
        
        public static async Task<IActionResult> sendViaRest(SensorData sd)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(sd);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:5000/SensorData", content))
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
        }

    }

}
