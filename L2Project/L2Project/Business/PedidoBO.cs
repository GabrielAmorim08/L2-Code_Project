using L2Project.DTO;
using L2Project.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace L2Project.Business
{
    public class PedidoBO : IPedido
    {
        public PedidoBO()
        {

        }
        public ResultadoVO<List<PedidosDTO>> OrganizarPedidos(string pedidos)
        {
            try
            {
                return new ResultadoVO<List<PedidosDTO>>(true, "Pedidos organizados com sucesso!", new List<PedidosDTO>());
            }
            catch (Exception e)
            {
                return new ResultadoVO<List<PedidosDTO>>(false,e.Message, new List<PedidosDTO>());
            }
        }
    }
}
