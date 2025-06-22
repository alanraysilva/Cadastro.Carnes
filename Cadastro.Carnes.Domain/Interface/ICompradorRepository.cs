using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    /// <summary>
    /// Interface de repositório para a entidade Comprador.
    /// Define operações específicas além das operações genéricas.
    /// </summary>
    public interface ICompradorRepository : IGenericRepository<Comprador>
    {
        /// <summary>
        /// Verifica se existe algum comprador associado a uma determinada cidade.
        /// Retorna o comprador encontrado, se houver.
        /// </summary>
        /// <param name="cidadeId">ID da cidade para consulta.</param>
        /// <returns>Comprador associado à cidade, se existir.</returns>
        Task<Comprador> ExisteCidadePorComprador(int? cidadeId);
    }
}
