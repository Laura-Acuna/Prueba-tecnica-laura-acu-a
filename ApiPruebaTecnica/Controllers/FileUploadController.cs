using Microsoft.AspNetCore.Mvc;

namespace ApiPruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly string _uploadPath = @"C:\PruebaEQ";

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || !file.FileName.EndsWith(".pdf"))
                return BadRequest("Solo se permiten archivos PDF");

            if (file.Length > 10 * 1024 * 1024)
                return BadRequest("El archivo excede el tamaño máximo permitido (10MB)");

            Directory.CreateDirectory(_uploadPath);
            var filePath = Path.Combine(_uploadPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Archivo subido con éxito");
        }
    }
}