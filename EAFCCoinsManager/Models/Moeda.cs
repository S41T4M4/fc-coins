using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("moeda")]
    public class Moeda
    {
        [Key]
        public int id_moeda { get; set; }
        [ForeignKey("plataforma")]
        public int plataforma_id { get; set; }
        public decimal quantidade { get; set; }
        public decimal valor { get; set; }

        public Plataforma Plataforma { get; set; } = null!;

        // Navegações
        public List<ItemCarrinho> ItensCarrinho { get; set; } = new();
        public List<ItemPedido> ItensPedido { get; set; } = new();

    }
}
