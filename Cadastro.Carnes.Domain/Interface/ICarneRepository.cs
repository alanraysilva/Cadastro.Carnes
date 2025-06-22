using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    /// <summary>
    /// Interface de repositório específica para a entidade Carne,
    /// incluindo operações padrão e consulta de carnes por origem.
    /// </summary>
    public interface ICarneRepository : IGenericRepository<Carne>
    {
        /// <summary>
        /// Verifica se existe alguma carne vinculada à origem informada.
        /// Retorna a carne encontrada ou null caso não exista.
        /// </summary>
        /// <param name="origemId">Identificador da origem a ser verificada.</param>
        Task<Carne> ExisteCarneComEssaOrigem(int? origemId);
    }
}