using LogProxyAPI.Models;
using LogProxyAPI.Models.LogProxy;
using LogProxyAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThirdPartyAPI.ACL.Interface;

namespace LogProxyAPI.Services.Service
{
    public class LogProxyService : ILogProxyService
    {
        private readonly IThirdParty _thirdParty;
        public LogProxyService(IThirdParty thirdParty)
        {
            _thirdParty = thirdParty;
        }
        public void ForwardLog(Log value)
        {
            var randomId = new Random();
            randomId.Next(1000);
            var extended = new ExtendedLog
            {
                id = randomId.Next(1000).ToString(),
                receivedAt = DateTime.Now,
                Text = value.Text,
                Title = value.Title
            };
            _thirdParty.LogPost(extended);
        }
        public Task<IEnumerable<ExtendedLog>> GetAllLogs()
        {
            var thirdPartyResult=_thirdParty.GetAll();
            return thirdPartyResult;
        }
    }
}
