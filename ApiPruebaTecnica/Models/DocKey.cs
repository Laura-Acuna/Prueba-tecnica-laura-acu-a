namespace ApiPruebaTecnica.Models
{
    public class DocKey
    {
        public Guid Id { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string DocName { get; set; } = string.Empty;
    }
}