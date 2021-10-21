using LogProxyAPI.Models;
using LogProxyAPI.Services.Interface;
using LogProxyAPI.Services.Service;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogProxyService _logProxyService;
        public LogProxyController(ILogProxyService logProxyService)
        {
            _logProxyService = logProxyService;
        }


        [Authorize]
        [HttpGet]
        public ActionResult<List<ExtendedSimpleJSON>> Get()
        {
            var res=_logProxyService.GetAllLogs();
            return res.Result.ToList();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] SimpleJSON value)
        {
             _logProxyService.ForwardLog(value);
            return Ok();
        }
    }
}
