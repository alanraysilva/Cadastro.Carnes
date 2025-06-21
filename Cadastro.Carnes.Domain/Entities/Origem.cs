using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    public class Origem
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;

        public Origem() { }


        public Origem(string? nome)
        {
            ValidateDomain(nome);
        }


        public Origem(int id, string? nome)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(nome);
        }

        public void Update(string? nome)
        {
            ValidateDomain(nome);
        }

        private void ValidateDomain(string? nome)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            Nome = nome!;
        }
    }
}
