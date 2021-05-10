using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorService.Context
{
    public class SensorContext
    {
        private static SensorContext instance;

        private int _interval;
        private float _threshold;
        private string _surcePath;

        private SensorContext()
        {
            _interval = 5000;
            _threshold = 0.01f;
            _surcePath = " ";
        }

        public static SensorContext Instance
        {
            get 
            { 
                if(instance == null)
                {
                    instance = new SensorContext();
                }
                return instance; 
            }
        }

        public void SetInterval(int value) { _interval = value;  Console.WriteLine($"Sensor interval changed to value={value}"); }
        public void SetThreshold(float value) { _threshold = value; Console.WriteLine($"Sensor threshold changed to vlaue={value}"); }
        public void SetSourcePath(string value) { _surcePath = value; Console.WriteLine($"Sensor source path changed to vlaue={value}"); }

        public int GetInterval() { return _interval; }
        public float GetThreshold() { return _threshold; }
        public string GetSourcePath() { return _surcePath; }

    }
}
