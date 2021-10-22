using LogProxyAPI.Models;
using LogProxyAPI.Models.LogProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Services.Interface
{
    public interface ILogProxyService
    {
        void ForwardLog(SimpleJSON value);
        Task<IEnumerable<ExtendedSimpleJSON>> GetAllLogs();
    }
}
