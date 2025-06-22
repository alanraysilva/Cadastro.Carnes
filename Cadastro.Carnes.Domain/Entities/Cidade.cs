using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    /// <summary>
    /// Entidade de domínio que representa uma Cidade.
    /// Guarda nome, sigla do estado e faz validação de regras de negócio.
    /// </summary>
    public class Cidade
    {
        // Identificador único da cidade (PK no banco)
        public int Id { get; private set; }

        // Nome da cidade (ex: Campinas, Americana)
        public string Nome { get; private set; } = string.Empty;

        // Sigla do estado, ex: SP, RJ
        public string Estado { get; private set; } = string.Empty;

        // Construtor vazio obrigatório pro Entity Framework não reclamar
        public Cidade() { }

        /// <summary>
        /// Construtor completo: usado quando já se sabe o ID (update, re-hidratação)
        /// </summary>
        public Cidade(int id, string nome, string estado)
        {
            // Se vier com id negativo, estoura exceção
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome, estado);
        }

        /// <summary>
        /// Construtor pra criar cidade nova, sem ID ainda (vai ser gerado pelo banco)
        /// </summary>
        public Cidade(string nome, string estado)
        {
            ValidateDomain(nome, estado);
        }

        /// <summary>
        /// Atualiza os campos (segue as regras igualzinho a criação)
        /// </summary>
        public void Update(string nome, string estado)
        {
            ValidateDomain(nome, estado);
        }

        /// <summary>
        /// Validação centralizada para não repetir código
        /// </summary>
        private void ValidateDomain(string? nome, string? estado)
        {
            // Nome não pode ser nulo/vazio
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            // Estado também não pode faltar (sigla obrigatória)
            DomainExceptionValidation.When(string.IsNullOrEmpty(estado), "Estado inválido. O estado é obrigatório");

            Nome = nome!;
            Estado = estado!;
        }
    }
}
