using Microsoft.AspNetCore.Mvc;
using ApiPruebaTecnica.Models;

namespace ApiPruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocKeyController : ControllerBase
    {
        private static List<DocKey> _docKeys = new();

        [HttpGet]
        public IActionResult Get() => Ok(_docKeys);

        [HttpPost]
        public IActionResult Add(DocKey key)
        {
            key.Id = Guid.NewGuid();
            _docKeys.Add(key);
            return Ok(key);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, DocKey updated)
        {
            var existing = _docKeys.FirstOrDefault(k => k.Id == id);
            if (existing == null) return NotFound();

            existing.Clave = updated.Clave;
            existing.DocName = updated.DocName;
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var key = _docKeys.FirstOrDefault(k => k.Id == id);
            if (key == null) return NotFound();

            _docKeys.Remove(key);
            return NoContent();
        }
    }
}