using L2Project.DTO;
using L2Project.DTO.CaixasDTO;
using L2Project.DTO.PedidoDTO;

namespace L2Project.Interfaces
{
    public interface IPedido
    {
        public ResultadoVO<List<PedidosCaixasJsonDTO>> OrganizarPedidos(PedidoDTO pedidos);
    }
}
