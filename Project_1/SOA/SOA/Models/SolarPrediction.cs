using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA.Models
{
    public class SolarPrediction
    {
        public long UNIXTime { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public float Radiation { get; set; }
        public int Temperature { get; set; }
        public float Pressure { get; set; }
        public int Humidity { get; set; }
        public float WindDirection { get; set; }
        public float Speed { get; set; }
        public DateTime TimeSunRise { get; set; }
        public DateTime TimeSunSet { get; set; }
    }
}
