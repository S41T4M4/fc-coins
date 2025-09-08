using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("pedido")]
    public class Pedido
    {
        [Key]
        public int id_pedido { get; set; }
        [ForeignKey("usuarios")]
        public int id_user { get; set; }
        public DateTime data_pedido { get; set; }
        public decimal total { get; set; }
        public string status { get; set; } = string.Empty;
        
        public Usuarios Usuarios { get; set; } = null!;
        public List<ItemPedido> Itens { get; set; } = new();
        public Pagamento? Pagamento { get; set; }
        
    }
}
