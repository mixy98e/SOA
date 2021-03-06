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
    public static class SensorRunner
    {
        private static System.Timers.Timer _aTimer;

        private static SensorContext _sc = SensorContext.Instance;

        private static int currLine = 0; //modified from "currLine;" to "currLine = 0;" uncomment line 27
        private static float lastRadiation;
        public static void StartSensor()
        {
            //currLine = 0;
            lastRadiation = 0;
            SetTimer(_sc.GetInterval());
        }

        public static void StopSensor()
        {
            _aTimer.Stop();
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

            //kocnica
            //_aTimer.Stop();

            SendData();
        }

        public static void ChgSensorInterval(int newTickTime)
        {
            _sc.SetInterval(newTickTime);
            if (_aTimer == null)
                return;
            _aTimer.Stop();
            _aTimer.Interval = newTickTime;
            _aTimer.Start();
        }

        public static void SendData() //private -> public
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
            sensorData.Humidity = float.Parse(parsedData[6]);
            sensorData.WindDirection = float.Parse(parsedData[7]);
            sensorData.Speed = float.Parse(parsedData[8]);
            sensorData.TimeSunRise = parsedData[9];
            sensorData.TimeSunSet = parsedData[10];


            float difference;

            if (lastRadiation != 0)
                difference = Math.Abs(lastRadiation - sensorData.Radiation);
            else difference = sensorData.Radiation;

            if (difference > _sc.GetThreshold() )
                sendViaRest(sensorData);
            //else Console.WriteLine($"threshodl NOT passed - NOT SENDING DATA {line}");
        }

        //public methods
        public static void ChgSensorThreshold(float newThreshold)
        {
            _sc.SetThreshold(newThreshold);
            Console.WriteLine(_sc.GetThreshold());
        }

        //public static void ChgSensorDataPath(string newDataPath)
        //{
        //    _sc.SetSourcePath(newDataPath);
        //    Console.WriteLine(_sc.GetSourcePath());
        //}

        //public void ChgSensorSourcePath(string sourcePath)
        //{
        //    _sc.SetSourcePath(sourcePath);
        //}

        public static async Task<IActionResult> sendViaRest(SensorData sd)
        {
            Console.WriteLine(sd.UnixTime);
            Console.WriteLine(Directory.GetCurrentDirectory());
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(sd);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                Console.WriteLine($"{content.ToString()}");

                try
                {
                    using (var response = await httpClient.PostAsync("http://dataservice:80/SensorData", content))
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
                catch(HttpRequestException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                
                return null;                
            }
        }

    }

}
