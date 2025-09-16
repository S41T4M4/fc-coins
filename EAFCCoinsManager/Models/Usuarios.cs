using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EAFCCoinsManager.Models
{
    [Table("usuarios")]
    public class Usuarios
    {
        [Key] 
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;    
        public string email { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public DateTime data_registro { get; set; }

        public List<Carrinho> Carrinhos { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();
        public List<VendedorOferta> OfertasVendedor { get; set; } = new();
    }
}
