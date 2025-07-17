namespace ApiPruebaTecnica.Models
{
    public class LogProcesamiento
    {
        public Guid Id { get; set; }
        public string NombreOriginal { get; set; }
        public string? NuevoNombre { get; set; }
        public string Estado { get; set; } = "Unknown";
        public DateTime FechaProcesamiento { get; set; } = DateTime.Now;
    }
}