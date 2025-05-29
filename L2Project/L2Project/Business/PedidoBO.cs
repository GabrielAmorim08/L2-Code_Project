using L2Project.DTO;
using L2Project.DTO.PedidoDTO;
using L2Project.Interfaces;
using L2Project.Models;
using L2Project.Models.Pedidos;
using Newtonsoft.Json;

namespace L2Project.Business
{
    public class PedidoBO : IPedido
    {
        protected readonly Db_L2_project_context _context;
        public PedidoBO(Db_L2_project_context context)
        {
            _context = context;
        }
        public ResultadoVO<List<object>> OrganizarPedidos(PedidoDTO pedidos)
        {
            try
            {

                foreach (var pedido in pedidos.Pedidos)
                {
                    Guid pedidoCadastro = CadastrarPedido(pedido);

                    foreach (var produto in pedido.produtos)
                    {
                        CadastrarProduto(produto,pedidoCadastro);
                    }
                    _context.SaveChanges();
                }
                return new ResultadoVO<List<object>>(true, "Pedidos organizados com sucesso!", new List<object>());
            }
            catch (Exception e)
            {
                return new ResultadoVO<List<object>>(false, e.Message, new List<object>());
            }
        }

        #region PRIVATES
        private Guid CadastrarPedido(Pedido_JsonDTO pedido)
        {
            var novoPedido = new Models.Pedidos.Pedidos()
            {
                Pedido_id = pedido.pedido_id
            };

            _context.Add(novoPedido);

            _context.SaveChanges();

            return novoPedido.Id;
        }
        private ResultadoVO CadastrarProduto(ProdutoDTO produto, Guid pedidoId)
        {
            var novoProduto = new Produtos()
            {
                Altura = produto.dimensoes.altura,
                Comprimento = produto.dimensoes.comprimento,
                Largura = produto.dimensoes.largura,
                NomeProduto = produto.produto_id,
                PedidoId = pedidoId,
            };

            _context.Add(novoProduto);

            return new ResultadoVO(true, "Sucesso");
        }
        #endregion
    }
}
