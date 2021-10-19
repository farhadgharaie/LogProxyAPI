using LogProxyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Services.Interface
{
    public interface ILogProxyService
    {
        void ForwardLog(SimpleJSON value);
        IEnumerable<ExtendedSimpleJSON> GetAllLogs();
    }
}
