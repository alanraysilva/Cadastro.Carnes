namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// DTO para retorno padrão de operações (criação, edição, exclusão, etc).
    /// Usado para padronizar as respostas das controllers (sucesso/erro + mensagem).
    /// </summary>
    public class RetornoPadraoDTO
    {
        /// <summary>
        /// Indica se a operação foi bem-sucedida (true) ou não (false).
        /// </summary>
        public bool Sucesso { get; }

        /// <summary>
        /// Mensagem de retorno ao usuário (informativa, de sucesso ou de erro).
        /// </summary>
        public string Mensagem { get; }

        /// <summary>
        /// Construtor padrão que define o status e a mensagem da operação.
        /// </summary>
        /// <param name="sucesso">True para sucesso, false para erro.</param>
        /// <param name="mensagem">Mensagem informativa para o usuário.</param>
        public RetornoPadraoDTO(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
