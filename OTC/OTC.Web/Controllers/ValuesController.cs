using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OTC.Data.Sys;
using OTC.Repository.Sys;

namespace OTC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILogRepository _logRepository;
        public ValuesController(ILogger<ValuesController> logger,ILogRepository logRepository)
        {
            _logger = logger;
            _logRepository = logRepository;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<List<Log>> Get()
        {
            return _logRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                _logger.LogInformation(10000, "Getting item {ID}", id);
                _logger.LogError("aaaa");
                if (id == 0)
                {
                    _logger.LogWarning(40000, "GetById({ID}) NOT FOUND", id);
                    throw new Exception("test");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return Ok(id.ToString());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
