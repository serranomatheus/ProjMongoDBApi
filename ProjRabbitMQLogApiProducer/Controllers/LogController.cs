using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjRabbitMQLogApiToMongo.Services;
using RabbitMQ.Client;

namespace ProjRabbitMQLogApiProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;
        public LogController(LogService logService)
        {
            _logService = logService;
           
        }
        [HttpPost]       
        public ActionResult<Log> Create(Log log)
        {
            _logService.Create(log);
            return Ok();

        }

    }
}
