using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorService.Model
{
    public class SensorData
    {
        public int UnixTime { get; set; }
        public float Radiation { get; set; }
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float WindDirection { get; set; }
        public float Speed { get; set; }
        public string TimeSunRise { get; set; }
        public string TimeSunSet { get; set; }

    }
}
