using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorService.Model;
using SensorService.Context;
using System.Timers;

namespace SensorService.Runner
{
    public class SensorRunner
    {
        SensorContext sss = SensorContext.Instance;

        //const int x = 5000;
        //private static System.Timers.Timer aTimer;

        int y = 1000;

        public void test() {
            var task = Task.Run(async () => {
                for (; ; )
                {
                    await Task.Delay(y);
                    throw new Exception();
                }
            });
        }
     
    }

}
