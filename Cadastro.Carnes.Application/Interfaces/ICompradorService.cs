using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    /// <summary>
    /// Interface de serviço para operações relacionadas ao Comprador.
    /// Herda os métodos CRUD genéricos de IGenericService.
    /// </summary>
    public interface ICompradorService : IGenericService<CompradorDTO>
    {
        /// <summary>
        /// Obtém o total de compradores ativos no sistema.
        /// Use para dashboards, relatórios ou indicadores do sistema.
        /// </summary>
        /// <returns>Número total de compradores considerados ativos.</returns>
        Task<int> GetTotalAtivos();
    }
}
