using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    public class Comprador
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public int CidadeId { get; private set; }

        public Cidade? Cidade { get; set; }

        public Comprador() { }

        public Comprador(int id, string nome, string documento, int cidadeId)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            Nome = nome;
            Documento = documento;
            CidadeId = cidadeId;
        }

        public Comprador(string nome, string documento, int cidadeId)
        {
            ValidateDomain(nome, documento, cidadeId);
        }

        public void Update(string? nome, string? documento, int cidadeid)
        {
            ValidateDomain(nome, documento, cidadeid);
        }

        private void ValidateDomain(string? nome, string? documento, int cidadeid)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(documento), "Documento inválido. O documento é obrigatório");
            DomainExceptionValidation.When(cidadeid < 0, "Cidade inválida");

            Nome = nome!;
            Documento = documento!;
            CidadeId = cidadeid;
        }
    }
}
