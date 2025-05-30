using L2Project.DTO.PedidoDTO;

namespace L2Project.DTO.CaixasDTO
{
    public class PedidosCaixasJsonDTO
    {
        public int pedido_id { get; set; }
        public List<CaixasDTO> Caixas { get; set; }
    }
}
