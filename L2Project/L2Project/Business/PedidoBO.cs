using L2Project.DTO;
using L2Project.DTO.CaixasDTO;
using L2Project.DTO.PedidoDTO;
using L2Project.Interfaces;
using L2Project.Models;
using L2Project.Models.Pedidos;
using Newtonsoft.Json;
using System.Linq;

namespace L2Project.Business
{
    public class PedidoBO : IPedido
    {
        protected readonly Db_L2_project_context _context;
        public PedidoBO(Db_L2_project_context context)
        {
            _context = context;
        }
        public ResultadoVO<List<PedidosCaixasJsonDTO>> OrganizarPedidos(PedidoDTO pedidos)
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

                var produtosOrganizados = OrganizarProdutosCaixas();
                return new ResultadoVO<List<PedidosCaixasJsonDTO>>(true, "Pedidos organizados com sucesso!", produtosOrganizados);
            }
            catch (Exception e)
            {
                return new ResultadoVO<List<PedidosCaixasJsonDTO>>(false, e.Message, new List<PedidosCaixasJsonDTO>());
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

        private List<PedidosCaixasJsonDTO> OrganizarProdutosCaixas()
        {
            var caixasDisponiveis = GetCaixasDisponiveis();
            var pedidosOrganizados = new List<PedidosCaixasJsonDTO>();

            var pedidos = _context.Pedidos.ToList();

            foreach (var pedido in pedidos)
            {
                var produtos = _context.Produtos
                    .Where(x => x.PedidoId == pedido.Id)
                    .Select(p => new
                    {
                        Nome = p.NomeProduto,
                        Altura = p.Altura,
                        Largura = p.Largura,
                        Comprimento = p.Comprimento,
                        Volume = p.Altura * p.Largura * p.Comprimento
                    })
                    .ToList();

                var pedidoOrganizado = new PedidosCaixasJsonDTO { pedido_id = pedido.Pedido_id };

                var produtosNaoAlocados = new HashSet<string>(produtos.Select(p => p.Nome));

                foreach (var caixa in caixasDisponiveis)
                {
                    var caixaAtual = new CaixasDTO { Caixa_id = caixa.Id };
                    double volumeCaixa = caixa.Volume;
                    double volumeUsado = 0;

                    foreach (var produto in produtos.Where(p => produtosNaoAlocados.Contains(p.Nome)))
                    {
                        if (produto.Altura <= caixa.Altura &&
                            produto.Largura <= caixa.Largura &&
                            produto.Comprimento <= caixa.Comprimento)
                        {
                            if (volumeUsado + produto.Volume <= volumeCaixa)
                            {
                                caixaAtual.Produtos.Add(produto.Nome);
                                volumeUsado += produto.Volume;
                                produtosNaoAlocados.Remove(produto.Nome);
                            }
                        }
                    }

                    if (caixaAtual.Produtos.Any())
                        pedidoOrganizado.Caixas.Add(caixaAtual);
                }

                foreach (var nome in produtosNaoAlocados)
                {
                    pedidoOrganizado.Caixas.Add(new CaixasDTO
                    {
                        Caixa_id = null,
                        Produtos = new List<string> { nome },
                        Observacao = "Produto não cabe em nenhuma caixa disponível."
                    });
                }

                pedidosOrganizados.Add(pedidoOrganizado);
            }

            return pedidosOrganizados;
        }

        private List<Caixa_Parametros> GetCaixasDisponiveis() =>
            new()
            {
        new() { Id = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
        new() { Id = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
        new() { Id = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
            };
        #endregion
    }
}
