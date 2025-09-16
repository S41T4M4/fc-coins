namespace EAFCCoinsManager.ViewModel
{
    public class VendedorOfertaResponse
    {
        public bool Success { get; set; }
        public int IdOferta { get; set; }
        public int IdVendedor { get; set; }
        public string NomeVendedor { get; set; } = string.Empty;
        public int PlataformaId { get; set; }
        public string PlataformaNome { get; set; } = string.Empty;
        public decimal QuantidadeCoins { get; set; }
        public decimal PrecoPor100k { get; set; }
        public decimal TaxaPlataforma { get; set; }
        public decimal PrecoFinal { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataVenda { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class VendedorOfertaListResponse
    {
        public bool Success { get; set; }
        public List<VendedorOfertaResponse> Ofertas { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }

    public class CalculoPrecoResponse
    {
        public bool Success { get; set; }
        public decimal QuantidadeCoins { get; set; }
        public decimal PrecoPor100k { get; set; }
        public decimal TaxaPlataforma { get; set; }
        public decimal TotalTaxa { get; set; }
        public decimal PrecoFinal { get; set; }
        public decimal LucroVendedor { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
