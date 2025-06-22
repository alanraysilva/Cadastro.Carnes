using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    /// <summary>
    /// Entidade de domínio que representa a Carne. 
    /// Possui validações de negócio e encapsula suas regras.
    /// </summary>
    public class Carne
    {
        // Identificador único da carne (PK no banco)
        public int Id { get; private set; }

        // Nome da carne (ex: Picanha, Alcatra)
        public string Nome { get; private set; } = string.Empty;

        // Chave estrangeira para a origem (de onde vem essa carne)
        public int OrigemId { get; private set; }

        // Navegação para a origem (opcional, lazy load ou include)
        public Origem? Origem { get; set; }

        // Construtor vazio exigido pelo Entity Framework
        public Carne() { }

        /// <summary>
        /// Criação da carne sem o ID (novo registro).
        /// </summary>
        public Carne(string? nome, int origemId)
        {
            ValidateDomain(nome, origemId);
        }

        /// <summary>
        /// Criação da carne com ID (quando já existe no banco ou em update).
        /// </summary>
        public Carne(int id, string? nome, int origemId)
        {
            // Não aceita id negativo (regra de domínio)
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome, origemId);
        }

        /// <summary>
        /// Atualiza os campos da carne (mantendo todas as regras de validação)
        /// </summary>
        public void Update(string? nome, int origemId)
        {
            ValidateDomain(nome, origemId);
        }

        /// <summary>
        /// Validações centralizadas (DRY) para nome e origem
        /// </summary>
        private void ValidateDomain(string? nome, int origemId)
        {
            // Nome obrigatório
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            // Origem obrigatória e válida
            DomainExceptionValidation.When(origemId < 0, "Origem inválida. O Id da origem é obrigatório");

            Nome = nome!;
            OrigemId = origemId;
        }
    }
}
