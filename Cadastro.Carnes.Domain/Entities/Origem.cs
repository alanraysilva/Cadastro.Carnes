using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    /// <summary>
    /// Entidade responsável por representar a origem de uma carne, como país, fornecedor ou frigorífico.
    /// </summary>
    public class Origem
    {
        /// <summary>
        /// Identificador único da origem.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Nome descritivo da origem.
        /// </summary>
        public string Nome { get; private set; } = string.Empty;

        /// <summary>
        /// Construtor vazio para uso do Entity Framework.
        /// </summary>
        public Origem() { }

        /// <summary>
        /// Construtor utilizado para criação de uma nova origem (sem Id).
        /// </summary>
        public Origem(string? nome)
        {
            ValidateDomain(nome);
        }

        /// <summary>
        /// Construtor utilizado para instanciar a origem já com Id definido.
        /// </summary>
        public Origem(int id, string? nome)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome);
        }

        /// <summary>
        /// Atualiza o nome da origem, aplicando validação.
        /// </summary>
        public void Update(string? nome)
        {
            ValidateDomain(nome);
        }

        /// <summary>
        /// Valida se o nome da origem é válido e não está vazio.
        /// </summary>
        private void ValidateDomain(string? nome)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            Nome = nome!;
        }
    }
}
