using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalystService.Model
{
    public class SensorMetaData
    {
        public float Interval { get; set; }
        public float Threshold { get; set; }
        public string DataSource { get; set; }

        public override string ToString()
        {
            return $"Interval={Interval}, Threshold={Threshold}, DataSource={DataSource}";
        }
    }
}
