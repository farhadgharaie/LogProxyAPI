using LogProxyAPI.Models;
using LogProxyAPI.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LogProxyAPI.Services.Service
{
    public class LogProxyService : ILogProxyService
    {
        private readonly IThirdParty _thirdParty;
        public LogProxyService(IThirdParty thirdParty)
        {
            _thirdParty = thirdParty;
        }
        public void ForwardLog(SimpleJSON value)
        {
            var randomId = new Random();
            randomId.Next(1000);
            var extended = new ExtendedSimpleJSON
            {
                id = randomId.Next(1000).ToString(),
                receivedAt = DateTime.Now,
                Text = value.Text,
                Title = value.Title
            };
            _thirdParty.LogPost(extended);
        }

        Task<IEnumerable<ExtendedSimpleJSON>> ILogProxyService.GetAllLogs()
        {
            var thirdPartyResult=_thirdParty.GetAll();
            return thirdPartyResult;
        }
    }
    public interface IThirdParty
    {
        Task<IEnumerable<ExtendedSimpleJSON>> GetAll();
        Task LogPost(ExtendedSimpleJSON extendedSimpleJSON);
    }
    public class AirTable : IThirdParty
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
            client.BaseAddress = new Uri(@"https://api.airtable.com/v0/appD1b1YjWoXkUJwR/Messages");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return client;
        }
        public async Task<IEnumerable<ExtendedSimpleJSON>> GetAll()
        {
            var response = await _client.GetAsync("?maxRecords=3");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserializedJson = JsonConvert.DeserializeObject<AirTableDto>(jsonString);
            var result= deserializedJson.records.Select(item => new ExtendedSimpleJSON
            {
                id = item.fields.id,
                receivedAt = item.fields.receivedAt,
                Text = item.fields.Message,
                Title = item.fields.Summary
            }
            );
            
            return result;
        }
        public async Task LogPost(ExtendedSimpleJSON extendedSimpleJSON)
        {
            var content = new AirTableRequesModel
            {
                records = new List<RecordRequetModel>
                {
                    new RecordRequetModel
                    {
                        fields= new Fields

                            {
                            receivedAt=extendedSimpleJSON.receivedAt,
                            id=extendedSimpleJSON.id,
                            Message=extendedSimpleJSON.Text,
                            Summary=extendedSimpleJSON.Title
                            }
                    }

                }
            };

            var payload = JsonConvert.SerializeObject(content);
            HttpContent httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("", httpContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
