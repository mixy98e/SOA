using SOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA.Repository
{
    public interface ISolarPredictionRepository
    {
        IEnumerable<SolarPrediction> GetSolarPredictions();
    }
}
