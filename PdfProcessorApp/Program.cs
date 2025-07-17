using System.Net.Http.Json;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

public class DocKey
{
    public Guid Id { get; set; }
    public string Clave { get; set; } = string.Empty;
    public string DocName { get; set; } = string.Empty;
}

public class LogRequest
{
    public string NombreOriginal { get; set; }
    public string? NuevoNombre { get; set; }
    public string Estado { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        string inputFolder = @"C:\PruebaEQ";
        string ocrFolder = Path.Combine(inputFolder, "OCR");
        string unknownFolder = Path.Combine(inputFolder, "UNKNOWN");

        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(ocrFolder);
        Directory.CreateDirectory(unknownFolder);

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5000/");

        var docKeys = await httpClient.GetFromJsonAsync<List<DocKey>>("api/dockey");
        if (docKeys == null || docKeys.Count == 0)
        {
            Console.WriteLine("No hay claves cargadas.");
            return;
        }

        var pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (var pdfPath in pdfFiles)
        {
            var fileName = Path.GetFileName(pdfPath);
            string extractedText = ExtractTextFromPdf(pdfPath);
            var match = docKeys.FirstOrDefault(k => extractedText.IndexOf(k.Clave, StringComparison.OrdinalIgnoreCase) >= 0);

            if (match != null)
            {
                var newFileName = $"Encontrado_{match.DocName}_{fileName}";
                var newPath = Path.Combine(ocrFolder, newFileName);
                File.Move(pdfPath, newPath);

                await httpClient.PostAsJsonAsync("api/log", new LogRequest
                {
                    NombreOriginal = fileName,
                    NuevoNombre = newFileName,
                    Estado = "Processed"
                });

                Console.WriteLine($"Archivo procesado: {fileName} → {newFileName}");
            }
            else
            {
                var newPath = Path.Combine(unknownFolder, fileName);
                File.Move(pdfPath, newPath);

                await httpClient.PostAsJsonAsync("api/log", new LogRequest
                {
                    NombreOriginal = fileName,
                    NuevoNombre = null,
                    Estado = "Unknown"
                });

                Console.WriteLine($"Archivo no reconocido: {fileName} → UNKNOWN");
            }
        }

        Console.WriteLine("Procesamiento completado.");
    }

    static string ExtractTextFromPdf(string pdfPath)
    {
        using var reader = new PdfReader(pdfPath);
        string text = "";

        for (int i = 1; i <= reader.NumberOfPages; i++)
        {
            text += PdfTextExtractor.GetTextFromPage(reader, i);
        }

        return text;
    }
}