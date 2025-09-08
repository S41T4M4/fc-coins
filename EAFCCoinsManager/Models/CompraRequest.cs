namespace EAFCCoinsManager.Models
{
    public class CompraRequest
    {
        public int IdCarrinho { get; set; }
        public string Email { get; set; } = string.Empty;
        public string MetodoPagamento { get; set; } = string.Empty;
        public int? TransactionId { get; set; }
    }
}
