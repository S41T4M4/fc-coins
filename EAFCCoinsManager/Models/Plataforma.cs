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

        public List<Moeda> Moedas { get; set; } = new();
    }
}
