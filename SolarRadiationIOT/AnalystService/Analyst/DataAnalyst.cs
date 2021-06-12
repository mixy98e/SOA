﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AnalystService.Model;
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
        private string _url = "http://sensorservice:80/Sensor/";


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


        // will bi swaped with database----------------------------------
        private void _SaveLog(float threshold, float interval)
        {
            Console.WriteLine($"AnalystService - Log: Time={DateTime.Now}, " +
                $"New threshold={threshold}, New interval={interval} -> Sent to SensorService at uri {_url}threshold");

            if (!File.Exists(_path))
            {
                using (StreamWriter sw = File.CreateText(_path))
                {
                    sw.WriteLine($"AnalystService - Log: Time={DateTime.Now}, " +
                                    $"New threshold={threshold}, New interval={interval} " +
                                    $"-> Sent to SensorService at uri {_url}threshold");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_path))
                {
                    sw.WriteLine($"AnalystService - Log: Time={DateTime.Now}, " +
                                    $"New threshold={threshold}, New interval={interval} " +
                                    $"-> Sent to SensorService at uri {_url}threshold");
                }
            }
        }
        public List<string> ReadLog()
        {
            return null;

            List<string> list = new List<string>();
            string s;
            using (StreamReader sr = File.OpenText(_path))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    list.Add(s);
                }
            }

            return list;
        }
        //----------------------------------------------------------------


        public async Task Analyze(SensorData sd)
        {
            if (!_running)
                return;

            float newThreshold;
            float newInterval;
            AnalystResult _ar = new AnalystResult();


            DateTime tmpTime = _UnixTimeStampToDateTime(sd.UnixTime);
            if (tmpTime.TimeOfDay > _date1.TimeOfDay
                || tmpTime.TimeOfDay < _date2.TimeOfDay)
            {
                if (_night != null && _night == true) 
                    return;

                //Night time settings
                _night = true;
                //obrisati
                //SensorMetaData metaData = _GetMetaDataFromSensorAsync().Result;
                //----------
                //newThreshold = metaData.Threshold * 1.5f;

                _ar.DayTimeDay = false;
            }
            else
            {
                if (_night !=null && _night == false) return;
                //Day time settings
                _night = false;
                _ar.DayTimeDay = true;
                //newThreshold = 25.6f;
            }

            //await _SetThreshold(newThreshold);
            //await _SetInterval(newInterval);
            //_SaveLog(newThreshold, newInterval);

            _ar.HighRisk = false;
            if(sd.Speed< 5.82f && sd.Temperature > 56f && sd.Humidity< 69.93f && sd.Pressure < 30.43)
            {   //vreme je pogodno
                _ar.WeatherGood = true;
                
                if(sd.Radiation > 229.6f)
                {   //visoka radijacija
                    //newInterval = 5000.0f;
                    _ar.RadiationHigh = true;
                    _ar.HighRisk = true;
                }
                else
                {   //niska radijacija
                    newInterval = 15000.0f;
                    _ar.RadiationHigh = false;
                }
            }
            else
                //vreme nije pogodno
                _ar.WeatherGood = false;

            _ar.TimeStamp = DateTime.Now;

            //command vrsi pozive iz 91 - 148 iniju
        }
    }
}

