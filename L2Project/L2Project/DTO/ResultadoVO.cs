namespace L2Project.DTO
{
    public class ResultadoVO
    {
        public bool Sucesso;
        public string Mensagem;
        public object Retorno;
        public ResultadoVO()
        {
        }
        public ResultadoVO(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }

    public class ResultadoVO<T>
    {
        public bool Sucesso;
        public string Mensagem;
        public T Retorno;
        public ResultadoVO(bool sucesso, string mensagem, T retorno)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Retorno = retorno;
        }
    }
}
