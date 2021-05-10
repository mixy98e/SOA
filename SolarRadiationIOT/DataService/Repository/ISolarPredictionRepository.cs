using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Repository
{
    public interface ISolarPredictionRepository
    {
        IEnumerable<SensorData> GetSensorData();
        void PostSensorData(SensorData sensorData);
    }
}
