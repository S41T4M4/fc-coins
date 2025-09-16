using System.ComponentModel.DataAnnotations;

namespace EAFCCoinsManager.ViewModel
{
    public class VendedorOfertaRequest
    {
        [Required]
        public int plataforma_id { get; set; }
        
        [Required]
        [Range(1000, double.MaxValue, ErrorMessage = "Quantidade mínima de 1.000 coins")]
        public decimal quantidade_coins { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal preco_por_100k { get; set; }
    }
}
