using Microsoft.AspNetCore.Mvc;
using ApiPruebaTecnica.Models;

namespace ApiPruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private static List<LogProcesamiento> _logs = new();

        [HttpGet]
        public IActionResult Get() => Ok(_logs.OrderByDescending(l => l.FechaProcesamiento));

        [HttpPost]
        public IActionResult Add([FromBody] LogRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NombreOriginal) || string.IsNullOrWhiteSpace(request.Estado))
                return BadRequest("Datos incompletos");

            var log = new LogProcesamiento
            {
                Id = Guid.NewGuid(),
                NombreOriginal = request.NombreOriginal,
                NuevoNombre = request.NuevoNombre,
                Estado = request.Estado,
                FechaProcesamiento = DateTime.Now
            };

            _logs.Add(log);
            return Ok();
        }
    }
}