using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Services.Interface
{
    public interface ILoginService
    {
        bool verify(string username, string password);
    }
}
