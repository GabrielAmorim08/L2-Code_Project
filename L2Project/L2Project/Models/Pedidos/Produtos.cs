using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L2Project.Models.Pedidos
{
    [Table("Pedidos_Produtos")]
    public class Produtos
    {
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string NomeProduto { get; set; }
        public Guid PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedidos Pedido { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }

    }
}
