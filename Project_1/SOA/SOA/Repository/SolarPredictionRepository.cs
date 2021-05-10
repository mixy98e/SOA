using MongoDB.Driver;
using SOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA.Repository
{
    public class SolarPredictionRepository : ISolarPredictionRepository
    {
        private readonly IMongoClient client;

        public SolarPredictionRepository(IMongoClient client)
        {
            this.client = client;
        }

        public IEnumerable<SolarPrediction> GetSolarPredictions()
        {
            return client.
        }
    }
}
