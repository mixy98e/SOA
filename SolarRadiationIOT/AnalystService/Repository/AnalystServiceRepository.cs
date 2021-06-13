using AnalystService.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalystService.Repository
{
    public class AnalystServiceRepository : IAnalystServiceRepository
    {
        private readonly IMongoClient client;

        public AnalystServiceRepository(IMongoClient client)
        {
            this.client = client;
        }

        public IEnumerable<AnalystResult> GetAnalystResults()
        {
            var db = client.GetDatabase("iot");
            var collection = db.GetCollection<AnalystResult>("sensor-analyst");

            return collection.Find(FilterDefinition<AnalystResult>.Empty).ToList();
        }

        public void PostAnalystResult(AnalystResult analystResult)
        {
            var db = client.GetDatabase("iot");
            var collection = db.GetCollection<AnalystResult>("sensor-analyst");

            collection.InsertOne(analystResult);
        }
    }
}
