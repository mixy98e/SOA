using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Mqtt
{
    public interface ISubscriber
    {
        void Subscribe(string qName);
    }
}
