using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Command
{
    public class Command
    {
        /*
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

         */

    }
}
