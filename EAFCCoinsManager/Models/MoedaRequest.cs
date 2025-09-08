namespace EAFCCoinsManager.Models
{
    public class MoedaRequest
    {
        public int PlataformaId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

    public class MoedaResponse
    {
        public bool Success { get; set; }
        public int IdMoeda { get; set; }
        public int PlataformaId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string PlataformaNome { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class MoedaListResponse
    {
        public bool Success { get; set; }
        public List<MoedaResponse> Moedas { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }
}
