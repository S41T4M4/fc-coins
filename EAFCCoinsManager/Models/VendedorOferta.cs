using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("vendedor_oferta")]
    public class VendedorOferta
    {
        [Key]
        public int id_oferta { get; set; }
        
        [ForeignKey("usuarios")]
        public int id_vendedor { get; set; }
        
        [ForeignKey("plataforma")]
        public int plataforma_id { get; set; }
        
        public decimal quantidade_coins { get; set; }
        public decimal preco_por_100k { get; set; } // Preço por 100.000 coins
        public decimal taxa_plataforma { get; set; } // Taxa da plataforma por 100k
        public decimal preco_final { get; set; } // Preço final calculado
        public string status { get; set; } = "ativo"; // ativo, pausado, vendido, cancelado
        public DateTime data_criacao { get; set; }
        public DateTime? data_atualizacao { get; set; }
        public DateTime? data_venda { get; set; }
        
        // Navegações
        public Usuarios Vendedor { get; set; } = null!;
        public Plataforma Plataforma { get; set; } = null!;
        public List<Pedido> Pedidos { get; set; } = new();
    }
}
