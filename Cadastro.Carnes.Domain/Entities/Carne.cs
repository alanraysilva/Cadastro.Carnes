using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    public class Carne
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public int OrigemId { get; private set; }

        public Origem? Origem { get; set; }

        public Carne() { }

        public Carne(string? nome, int origemId)
        {
            ValidateDomain(nome, origemId);
        }

        public Carne(int id, string? nome, int origemId)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome, origemId);
        }

        public void Update(string? nome, int origemId)
        {
            ValidateDomain(nome, origemId);
        }

        private void ValidateDomain(string? nome, int origemId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");

            DomainExceptionValidation.When(origemId < 0, "Origem inválida. O Id da origem é obrigatório");

            Nome = nome!;
            OrigemId = origemId;
        }
    }
}
