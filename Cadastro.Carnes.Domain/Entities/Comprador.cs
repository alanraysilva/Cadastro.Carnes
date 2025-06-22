using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    /// <summary>
    /// Entidade de domínio que representa o comprador (cliente) de carnes.
    /// Aqui só vai a regra de negócio raiz: obrigatoriedade e consistência.
    /// </summary>
    public class Comprador
    {
        // Chave primária do comprador (ID único gerado pelo banco)
        public int Id { get; private set; }

        // Nome completo do comprador (pessoa física ou jurídica)
        public string Nome { get; private set; } = string.Empty;

        // CPF ou CNPJ do comprador (sim, aceita os dois, só validar no app/service)
        public string Documento { get; private set; } = string.Empty;

        // CidadeId: referencia a cidade do comprador (FK)
        public int CidadeId { get; private set; }

        // Navegação para Cidade (lazy loading? O EF Core se vira aqui)
        public Cidade? Cidade { get; set; }

        // Construtor vazio pro Entity Framework poder materializar 
        public Comprador() { }

        /// <summary>
        /// Construtor completo usado quando já existe o ID (ex: update ou carregamento do banco)
        /// </summary>
        public Comprador(int id, string nome, string documento, int cidadeId)
        {
            // Não deixa passar id negativo
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            Nome = nome;
            Documento = documento;
            CidadeId = cidadeId;
        }

        /// <summary>
        /// Construtor para criação de novo comprador (sem ID ainda)
        /// </summary>
        public Comprador(string nome, string documento, int cidadeId)
        {
            ValidateDomain(nome, documento, cidadeId);
        }

        /// <summary>
        /// Atualiza campos do comprador (só via regras de domínio)
        /// </summary>
        public void Update(string? nome, string? documento, int cidadeid)
        {
            ValidateDomain(nome, documento, cidadeid);
        }

        /// <summary>
        /// Validação centralizada dos campos obrigatórios e regras de negócio.
        /// </summary>
        private void ValidateDomain(string? nome, string? documento, int cidadeid)
        {
            // Nome não pode ser vazio/nulo
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
            // Documento também não pode faltar 
            DomainExceptionValidation.When(string.IsNullOrEmpty(documento), "Documento inválido. O documento é obrigatório");
            // Cidade precisa existir 
            DomainExceptionValidation.When(cidadeid < 0, "Cidade inválida");

            Nome = nome!;
            Documento = documento!;
            CidadeId = cidadeid;
        }
    }
}
