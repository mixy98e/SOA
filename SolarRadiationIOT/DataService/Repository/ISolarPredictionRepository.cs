using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Repository
{
    public interface ISolarPredictionRepository
    {
        /// <summary>
        /// Returns all sensor data.
        /// </summary>
        IEnumerable<SensorData> GetSensorData();

        /// <summary>
        /// Returns the last record from the database.
        /// </summary>
        SensorData GetLastSensorData();

        /// <summary>
        /// Sends sensor data to be saved in the database.
        /// </summary>
        void PostSensorData(SensorData sensorData);
    }
}
