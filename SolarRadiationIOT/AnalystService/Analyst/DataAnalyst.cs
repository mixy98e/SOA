using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AnalystService.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnalystService.Analyst
{
    public class DataAnalyst
    {
        private bool _running = true;
        private bool? _night;
        private DateTime _date1 = new DateTime(0001, 1, 1, 19, 0, 0);
        private DateTime _date2 = new DateTime(0001, 1, 1, 5, 0, 0);
        private string _path = "./Log.txt";
        private string _url = "http://sensorservice:80/Sensor/";

        public bool Running
        {
            get { return _running; }
            set { _running = value; }
        }
        public bool? Night
        {
            get { return _night; }
            set { _night = value; }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }


        private DateTime _UnixTimeStampToDateTime(int unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        // will bi swaped with database----------------------------------
        private void _SaveLog(float threshold, float interval)
        {
            Console.WriteLine($"AnalystService - Log: Time={DateTime.Now}, " +
                $"New threshold={threshold}, New interval={interval} -> Sent to SensorService at uri {_url}threshold");

            if (!File.Exists(_path))
            {
                using (StreamWriter sw = File.CreateText(_path))
                {
                    sw.WriteLine($"AnalystService - Log: Time={DateTime.Now}, " +
                                    $"New threshold={threshold}, New interval={interval} " +
                                    $"-> Sent to SensorService at uri {_url}threshold");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_path))
                {
                    sw.WriteLine($"AnalystService - Log: Time={DateTime.Now}, " +
                                    $"New threshold={threshold}, New interval={interval} " +
                                    $"-> Sent to SensorService at uri {_url}threshold");
                }
            }
        }
        public List<string> ReadLog()
        {
            List<string> list = new List<string>();
            string s;
            using (StreamReader sr = File.OpenText(_path))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    list.Add(s);
                }
            }

            return list;
        }
        //----------------------------------------------------------------


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
        private async Task<IActionResult> _SetThreshold(float threshold)
        {
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
        public void testfun(float x, float y)
        {
            _SetThreshold(x);
            _SetInterval(y);
        }
        private async Task<IActionResult> _SetInterval(float interval)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(interval);
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
        //--------------------------------------------------------------------


        public async Task Analyze(SensorData sd)
        {
            if (!_running)
                return;

            float newThreshold;
            float newInterval;

            DateTime tmpTime = _UnixTimeStampToDateTime(sd.UnixTime);
            if (tmpTime.TimeOfDay > _date1.TimeOfDay
                || tmpTime.TimeOfDay < _date2.TimeOfDay)
            {
                if (_night != null && _night == true) return;
                //Night time settings
                _night = true;
                //obrisati
                SensorMetaData metaData = _GetMetaDataFromSensorAsync().Result;
                //----------

                newThreshold = metaData.Threshold * 1.5f;
                newInterval = metaData.Interval * 2f;
            }
            else
            {
                if (_night !=null && _night == false) return;
                //Day time settings
                _night = false;
                newInterval = 5000f;
                newThreshold = 25.0f;
            }

            //await _SetThreshold(newThreshold);
            //await _SetInterval(newInterval);
            _SaveLog(newThreshold, newInterval);
        }
    }
}
/*
if vetar>nesto && temperatura>nesto && vlaznost<nesto && presure>nesto && presure<nesto
{
    if(radiation > 400)
        potencijalno visoka radijacija
    else
        nije
}
else
    vreme nije pogodno
*/

/*
AnalystService      -> podaci ->     comand
            [visoko nisko nesto tako]

command vrsi pozive iz 91 - 148 iniju

*/