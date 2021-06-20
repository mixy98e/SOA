using AnalystService.Model;
using System.Collections.Generic;

namespace AnalystService.Repository
{
    public interface IAnalystServiceRepository
    {
        /// <summary>
        /// Returns all analyst results.
        /// </summary>
        IEnumerable<AnalystResult> GetAnalystResults();

        /// <summary>
        /// Sends analyst result to be saved in the database.
        /// </summary>
        void PostAnalystResult(AnalystResult analystResult);
    }
}
