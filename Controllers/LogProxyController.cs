using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogProxyController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Ok");
        }
    }
}
