using LogProxyAPI.Models;
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
        [Authorize]
        [HttpGet]
        public ActionResult<string> Get()
        {
            // get all logs from third-party
            return Ok("Ok");
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] SimpleJSON value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = "";// _service.Add(value);
            return CreatedAtAction("Get", new { id = item }, item);
        }
    }
}
