using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("plataforma")]
    public class Plataforma
    {
        [Key]
        public int id_plataforma {  get; set; }
        public string descricao_plataforma { get; set; } = string.Empty;
        public decimal taxa_por_100k { get; set; } = 0; // Taxa da plataforma por 100.000 coins
        public bool ativa { get; set; } = true; // Se a plataforma está ativa para vendas

        public List<Moeda> Moedas { get; set; } = new();
        public List<VendedorOferta> OfertasVendedores { get; set; } = new();
    }
}
