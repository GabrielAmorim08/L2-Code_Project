namespace L2Project.DTO.PedidoDTO
{
    public class Pedido_JsonDTO
    {
        public int pedido_id { get; set; }
        public List<ProdutoDTO> produtos { get; set; }
    }
}
