using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubReceiber.Service
{
    public interface IEventHubReceiberService
    {
        Task StartProcessingAsync();
        Task StopProcessingAsync();
    }
}
