using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    /// <summary>
    /// Interface de serviço específica para Carne.
    /// Herda métodos CRUD genéricos de IGenericService.
    /// </summary>
    public interface ICarneService : IGenericService<CarneDTO>
    {
        /// <summary>
        /// Obtém a quantidade total de carnes cadastradas no sistema.
        /// Útil para dashboards, paginação ou relatórios.
        /// </summary>
        /// <returns>Número total de registros de carne.</returns>
        Task<int> GetTotalCount();
    }
}
