using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAFCCoinsManager.Models
{
    [Table("pagamento")]
    public class Pagamento
    {
        [Key]
        public int id_pagamento { get; set; }
        [ForeignKey("pedido")]
        public int id_pedido { get; set; }
        public DateTime data_pag {  get; set; } 
        public decimal valor_pago { get; set; }
        public string metodo { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public int transaction_id { get; set; }

        public Pedido Pedido { get; set; } = null!;

    }
}
