namespace EAFCCoinsManager.Models
{
    public class PlataformaResponse
    {
        public bool Success { get; set; }
        public int IdPlataforma { get; set; }
        public string DescricaoPlataforma { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
