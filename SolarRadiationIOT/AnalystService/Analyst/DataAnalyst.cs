using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AnalystService.Model;
using AnalystService.Mqtt;
using AnalystService.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnalystService.Analyst
{
    public class DataAnalyst
    {
        private bool _running = true;
        private bool? _night;
        private DateTime _date1 = new DateTime(0001, 1, 1, 19, 0, 0);
        private DateTime _date2 = new DateTime(0001, 1, 1, 5, 0, 0);
        private string _path = "./Log.txt";
        private IAnalystServiceRepository repository;


        public bool Running
        {
            get { return _running; }
            set { _running = value; }
        }
        public bool? Night
        {
            get { return _night; }
            set { _night = value; }
        }
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }


        private DateTime _UnixTimeStampToDateTime(int unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void _SaveLog(AnalystResult result)
        {
            repository.PostAnalystResult(result);
        }

        public IEnumerable<AnalystResult> ReadLog()
        {
            return repository.GetAnalystResults();
        }

        public async Task Analyze(SensorData sd)
        {
            Console.WriteLine("AnalystService: Analyze started");
            if (!_running)
                return;

            AnalystResult _ar = new AnalystResult();

            //-----
            DateTime tmpTime = _UnixTimeStampToDateTime(sd.UnixTime);

            if (tmpTime.TimeOfDay > _date1.TimeOfDay
                || tmpTime.TimeOfDay < _date2.TimeOfDay)
                _ar.DayTimeDay = true;//korekcija
            else
                _ar.DayTimeDay = false;

            Console.WriteLine($"time: {tmpTime.TimeOfDay.ToString()} flag(false == day): {_ar.DayTimeDay}");
            
            _ar.HighRisk = false;
            if (sd.Speed < 5.82f && sd.Temperature > 56f && sd.Humidity < 69.93f && sd.Pressure < 30.43)
            {   //vreme je pogodno
                _ar.WeatherGood = true;

                if (sd.Radiation > 229.6f)
                {
                    //visoka radijacija
                    _ar.RadiationHigh = true;
                    _ar.HighRisk = true;
                }
                else
                {
                    //niska radijacija
                    _ar.RadiationHigh = false;
                    _ar.HighRisk = false;
                }
            }
            else
            {
                //vreme nije pogodno
                _ar.WeatherGood = false;
                _ar.HighRisk = false;

                if (sd.Radiation > 229.6f)
                {
                    //visoka radijacija
                    _ar.RadiationHigh = true;
                }
                else
                {
                    //niska radijacija
                    _ar.RadiationHigh = false;
                }
            }


            //-----*/
            _ar.TimeStamp = DateTime.Now;

            var publisher = new Publisher();
            publisher.Publish(_ar, "AnalystServiceQueue");
            _SaveLog(_ar);

            Console.WriteLine("AnalystService is published data");
        }
    }
}

