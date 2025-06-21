using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    public class Moeda
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Sigla { get; private set; } = string.Empty;

        public Moeda() { }

        public Moeda(int id, string nome, string sigla)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome, sigla);
        }

        public Moeda(string? nome, string sigla)
        {
            ValidateDomain(nome, sigla);
        }

        public void Update(string? nome, string sigla)
        {
            ValidateDomain(nome, sigla);
        }

        private void ValidateDomain(string? nome, string sigla)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(sigla), "Sigla inválida. O sigla é obrigatório");
            Nome = nome!;
            Sigla = sigla;
        }
    }
}
