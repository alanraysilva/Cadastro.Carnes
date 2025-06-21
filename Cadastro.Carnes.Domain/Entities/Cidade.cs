using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    public class Cidade
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Estado { get; private set; } = string.Empty;

        public Cidade() { }

        public Cidade(int id, string nome, string estado)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome, estado);
        }

        public Cidade(string nome, string estado)
        {
            ValidateDomain(nome, estado);
        }

        public void Update(string nome, string estado)
        {
            ValidateDomain(nome, estado);
        }

        private void ValidateDomain(string? nome, string? estado)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(estado), "Estado inválido. O estado é obrigatório");

            Nome = nome!;
            Estado = estado!;
        }
    }
}
