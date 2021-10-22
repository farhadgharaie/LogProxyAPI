using LogProxyAPI.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Services.Service
{
    public class LoginService:ILoginService
    {
        private IConfiguration _configuration;
        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool verify(string username, string password)
        {
            var configuredUserName = _configuration["usrname"];
            var configuredPassword = _configuration["password"];
            return configuredUserName == username && configuredPassword == password;
        }
    }
}
