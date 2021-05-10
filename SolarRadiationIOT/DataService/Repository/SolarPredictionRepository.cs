using MongoDB.Driver;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Repository
{
    public class SolarPredictionRepository : ISolarPredictionRepository
    {
        private readonly IMongoClient client;

        public SolarPredictionRepository(IMongoClient client)
        {
            this.client = client;
        }

        public IEnumerable<SensorData> GetSensorData()
        {
            var db = client.GetDatabase("iot");
            var collection = db.GetCollection<SensorData>("sensor-data");

            return collection.Find(FilterDefinition<SensorData>.Empty).ToList();
        }

        public void PostSensorData(SensorData sensorData)
        {
            var db = client.GetDatabase("iot");
            var collection = db.GetCollection<SensorData>("sensor-data");

            collection.InsertOne(sensorData);
        }
    }
}
