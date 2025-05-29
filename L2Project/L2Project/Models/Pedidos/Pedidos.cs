using System.ComponentModel.DataAnnotations.Schema;

namespace L2Project.Models.Pedidos
{
    [Table("Pedidos")]
    public class Pedidos
    {
        public Guid Id { get; set; }
        public int Pedido_id { get; set; }
    }
}
