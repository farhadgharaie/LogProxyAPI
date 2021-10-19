using LogProxyAPI.Models;
using LogProxyAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LogProxyAPI.Services.Service
{
    public class LogProxyService : ILogProxyService
    {
        private readonly IThirdParty<ThirdPartyLog> _thirdParty;
        public LogProxyService(IThirdParty<ThirdPartyLog> thirdParty)
        {
            _thirdParty = thirdParty;
        }
        public void  ForwardLog(SimpleJSON value)
        {
            var randomId = new Random();
            randomId.Next(1000);
            var extended = new ExtendedSimpleJSON
            {
                id= randomId.ToString(),
                receivedAt=DateTime.Now,
                Text=value.Text,
                Title=value.Title
            };
            _thirdParty.LogPost(extended);
        }

        IEnumerable<ExtendedSimpleJSON> ILogProxyService.GetAllLogs()
        {
            throw new NotImplementedException();
        }
    }
    public interface IThirdParty<T>
    {
        Task<IEnumerable<T>> GetAllLogs();
        Task LogPost(ExtendedSimpleJSON extendedSimpleJSON);
    }
    public class AirTable : IThirdParty<ThirdPartyLog>
    {
        private HttpClient _client;
        public AirTable()
        {
            _client = ConfigClient();
        }
        private HttpClient ConfigClient()
        {
            var accessToken = "key46INqjpp7lMzjd";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"https://api.airtable.com/v0/appD1b1YjWoXkUJwR");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return  client;
        }

        public async Task<IEnumerable<ThirdPartyLog>> GetAllLogs()
        {
            var response = await _client.GetAsync(@"/Messages");
            response.EnsureSuccessStatusCode();
            var ret= response.Content.ReadAsStringAsync();
        }

        public Task LogPost(ExtendedSimpleJSON extendedSimpleJSON)
        {
            throw new NotImplementedException();
        }
    }
}
