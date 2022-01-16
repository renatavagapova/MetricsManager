using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsAgentController : ControllerBase
    {
        private readonly IRamInfoProvider _ramInfoProvider;

        [HttpGet("available")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            double freeRam = _ramInfoProvider.GetFreeRam();
            return Ok(freeRam);
        }
    }
}
