using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Model
{
    public class AnalystResult
    {
        public bool RadiationHigh { get; set; }
        public bool DayTimeDay { get; set; }
        public bool WeatherGood { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool HighRisk { get; set; }

    }
}
