namespace Cadastro.Carnes.Application.DTOs
{
    public class RetornoPadraoDTO
    {
        public bool Sucesso { get; }
        public string Mensagem { get; }

        public RetornoPadraoDTO(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
