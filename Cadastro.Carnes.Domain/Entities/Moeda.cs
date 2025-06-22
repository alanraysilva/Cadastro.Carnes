using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma moeda utilizada nos itens dos pedidos.
    /// </summary>
    public class Moeda
    {
        /// <summary>
        /// Identificador único da moeda.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Nome descritivo da moeda (Exemplo: Dólar Americano, Real Brasileiro).
        /// </summary>
        public string Nome { get; private set; } = string.Empty;

        /// <summary>
        /// Sigla da moeda (Exemplo: USD, BRL, EUR).
        /// </summary>
        public string Sigla { get; private set; } = string.Empty;

        /// <summary>
        /// Construtor vazio utilizado pelo Entity Framework.
        /// </summary>
        public Moeda() { }

        /// <summary>
        /// Construtor completo para instanciar a moeda com todos os campos, incluindo o Id.
        /// </summary>
        public Moeda(int id, string nome, string sigla)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome, sigla);
        }

        /// <summary>
        /// Construtor para criar uma nova moeda sem informar o Id (usado na criação).
        /// </summary>
        public Moeda(string? nome, string sigla)
        {
            ValidateDomain(nome, sigla);
        }

        /// <summary>
        /// Atualiza os campos da moeda, aplicando as regras de validação.
        /// </summary>
        public void Update(string? nome, string sigla)
        {
            ValidateDomain(nome, sigla);
        }

        /// <summary>
        /// Realiza as validações obrigatórias nos campos da moeda.
        /// </summary>
        private void ValidateDomain(string? nome, string sigla)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(sigla), "Sigla inválida. O sigla é obrigatório");
            Nome = nome!;
            Sigla = sigla;
        }
    }
}
