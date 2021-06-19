using CommandService.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Command
{
    public interface ICommandService
    {
        void checkCommands(AnalystResult ar);
        Task<SensorMetaData> _GetMetaDataFromSensorAsync();
        void testfun(float x, float y);
        Task Notify(string msg);
    }
}
