using L2Project.DTO;

namespace L2Project.Interfaces
{
    public interface IPedido
    {
        public ResultadoVO<List<PedidosDTO>> OrganizarPedidos(string pedidos);
    }
}
