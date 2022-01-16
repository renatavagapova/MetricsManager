using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly MetricsRepository _repository;

        public MetricsController(MetricsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            _repository.Add(date, temperature);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult ReadTimeInterval([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            List<Metrics> list = _repository.Read(fromDate, toDate);
            return Ok(list);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temp)
        {
            _repository.Update(date, temp);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteTimeInterval([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            _repository.Delete(fromDate, toDate);
            return Ok();
        }
    }
}
