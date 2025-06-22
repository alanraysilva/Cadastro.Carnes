namespace Cadastro.Carnes.Domain.Validation
{
    /// <summary>
    /// Classe base para lançar exceções de domínio.
    /// Usada para validar regras de negócio e lançar mensagens customizadas.
    /// </summary>
    public class DomainExceptionValidation : Exception
    {
        /// <summary>
        /// Construtor que recebe a mensagem de erro a ser exibida na exceção.
        /// </summary>
        /// <param name="error">Mensagem detalhando a violação da regra de negócio.</param>
        public DomainExceptionValidation(string error) : base(error)
        {
        }

        /// <summary>
        /// Método auxiliar para lançar a exceção se uma condição de erro for atendida.
        /// </summary>
        /// <param name="hasError">Condição booleana que indica se há erro.</param>
        /// <param name="error">Mensagem de erro a ser lançada se a condição for verdadeira.</param>
        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }
    }
}
