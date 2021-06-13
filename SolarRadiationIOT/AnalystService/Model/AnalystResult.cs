using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalystService.Model
{
    public class AnalystResult
    {
        public ObjectId Id { get; set; }
        public bool RadiationHigh { get; set; } = false;
        public bool DayTimeDay { get; set; } = false;
        public bool WeatherGood { get; set; } = false;
        public DateTime TimeStamp { get; set; }
        public bool HighRisk { get; set; } = false;

    }
}
