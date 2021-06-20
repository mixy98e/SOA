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
        public string checkCommands(AnalystResult ar)
        {
            SensorMetaData smd =  _GetMetaDataFromSensorAsync().Result;
            string msg = string.Empty;
            //testfun(69f, 69f);

            DateTime unixTime = DateTime.Now;
            string interval = "";
            string threshold = "";
            string weatherStatus = "";
            string radiationStatus = "";
            string periodOfDay = "";

            if (ar.DayTimeDay)
            {
                Console.WriteLine("CommandProcessing: daytime day");
                periodOfDay = "Day time";
                if (ar.RadiationHigh)
                {
                    Console.WriteLine("CommandProcessing: radiation high");
                    radiationStatus = "High";
                   
                    if (ar.WeatherGood)
                    {
                        _SetInterval(5000f, smd.Interval); //interval=5s i tsh=2.5
                        Console.WriteLine("CommandProcessing: weather good");
                        weatherStatus = "Good";
                        interval = "5000ms";
                    }
                    else
                    {
                        _SetInterval(10000f, smd.Interval);//interval=15s i tsh=2.5
                        Console.WriteLine("CommandProcessing: weather bad");
                        weatherStatus = "Bad";
                        interval = "7500ms";

                    }
                    _SetThreshold(25f, smd.Threshold);
                    threshold = "25";
                }
                else
                {
                    Console.WriteLine("CommandProcessing: radiation low");
                    radiationStatus = "Low";
                    // int 10s tsh 75
                    _SetInterval(7500f, smd.Interval);
                    _SetThreshold(75f, smd.Threshold);
                    interval = "7500ms";
                    threshold = "75";
                }
            }
            else 
            {
                _SetInterval(10000f, smd.Interval);
                _SetThreshold(0.025f, smd.Threshold);//0.025
                Console.WriteLine("CommandProcessing: daytime night");
                periodOfDay = "Night time";
                interval = "10000ms";
                threshold = "0.025";

                if (ar.RadiationHigh)
                    radiationStatus = "High";
                else radiationStatus = "Low";
            }


                if (ar.WeatherGood)
                weatherStatus = "Good";
            else
                weatherStatus = "Bad";

            return unixTime + "," + interval + "," + threshold + "," + weatherStatus +
                "," + radiationStatus + "," + periodOfDay;

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
