using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("item_pedido")]
    public class ItemPedido
    {
        [Key]
        public int id_item_pedido { get; set; }
        [ForeignKey("pedido")]
        public int id_pedido { get; set; }
        [ForeignKey("moeda")]
        public int id_moeda { get; set; }
        public int quantidade { get; set; }
        public decimal Preco_unitario { get; set; }

        public Pedido Pedido { get; set; } = null!;
        public Moeda Moeda { get; set; } = null!;

    }
}
