using LogProxyAPI.Models.LogProxy;
using LogProxyAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<List<ExtendedLog>> Get()
        {
            try
            {
                var res = _logProxyService.GetAllLogs();
                return res.Result.ToList();
            }catch
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Log value)
        {
             _logProxyService.ForwardLog(value);
            return Ok();
        }
    }
}
