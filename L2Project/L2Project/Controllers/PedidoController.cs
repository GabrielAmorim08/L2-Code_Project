using L2Project.DTO;
using L2Project.DTO.PedidoDTO;
using L2Project.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace L2Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        protected IPedido _business;
        public PedidoController(IPedido business) { _business = business; }

        [HttpPost]
        [Route("OrdenarPedidos")]
        public ResultadoVO<List<object>> OrdenarPedidos([FromBody] PedidoDTO pedidos) => _business.OrganizarPedidos(pedidos);
    }
}
