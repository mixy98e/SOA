using CommandService.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommandService.Command
{
    public class Command
    {
        private string _url = "http://sensorservice:80/Sensor/";
        public void checkCommands(AnalystResult ar)
        {
            SensorMetaData smd =  _GetMetaDataFromSensorAsync().Result;

            //testfun(69f, 69f);

            if (ar.DayTimeDay)
            {
                Console.WriteLine("CommandProcessing: daytime day");
                if (ar.RadiationHigh)
                {
                    Console.WriteLine("CommandProcessing: radiation high");
                    if (ar.WeatherGood)
                    {
                        _SetInterval(5000f, smd.Interval); //interval=5s i tsh=2.5
                        Console.WriteLine("CommandProcessing: weather good");
                    }
                    else
                    {
                        _SetInterval(15000f, smd.Interval);//interval=15s i tsh=2.5
                        Console.WriteLine("CommandProcessing: weather bad");
                    }
                    _SetThreshold(25f, smd.Threshold);
                }
                else
                {
                    Console.WriteLine("CommandProcessing: radiation low");
                    // int 10s tsh 75
                    _SetInterval(10000f, smd.Interval);
                    _SetThreshold(75f, smd.Threshold);
                }
            }
            else 
            {
                _SetInterval(15000f, smd.Interval);
                _SetThreshold(0.025f, smd.Threshold);//0.025
                Console.WriteLine("CommandProcessing: daytime night");
            }

            

        }

  
          // Invalid rest requests will be swaped for mqtt---------------------
        public async Task<SensorMetaData> _GetMetaDataFromSensorAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync($"{_url}metadata"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        SensorMetaData metadata = JsonConvert.DeserializeObject<SensorMetaData>(apiResponse);

                        return metadata;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                return null;
            }
        }
        private async Task<IActionResult> _SetThreshold(float threshold, float oldThreshold)
        {
            if (threshold == oldThreshold)
                return null;

            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(threshold);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");

                try
                {
                    using (var response = await httpClient.PutAsync($"{_url}threshold", content))
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
        }
        private async Task<IActionResult> _SetInterval(float interval, float oldInterval)
        {
            if (interval == oldInterval)
                return null;

            int intervalTmp = (int)interval;

            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(intervalTmp);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");

                try
                {
                    using (var response = await httpClient.PutAsync($"{_url}interval", content))
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
        }
        public void testfun(float x, float y)
        {
            _SetThreshold(x,0);
            _SetInterval(y,0);
        }

    }
}
