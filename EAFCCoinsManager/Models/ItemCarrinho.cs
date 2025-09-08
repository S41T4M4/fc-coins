using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("item_carrinho")]
    public class ItemCarrinho
    {
        [Key]
        public int id_item { get; set; }
        [ForeignKey("carrinho")]
        public int id_carrinho { get; set; }
        [ForeignKey("moeda")]
        public int id_moeda { get; set; }
        public int quantidade { get; set; }

        public Carrinho Carrinho { get; set; } = null!;
        public Moeda Moeda { get; set; } = null!;
    }
}
