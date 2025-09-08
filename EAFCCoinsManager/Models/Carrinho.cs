using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("carrinho")]
    public class Carrinho
    {
        [Key]
        public int id_carrinho { get; set; }
        [ForeignKey("usuarios")]
        public int id_user { get; set; }
        public DateTime create_time { get; set; }
        public Usuarios Usuario { get; set; } = null!;
        public List<ItemCarrinho> Itens { get; set; } = new();
       
    }
}
