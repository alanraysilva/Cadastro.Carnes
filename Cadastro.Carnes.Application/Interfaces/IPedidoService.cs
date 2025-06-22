using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    /// <summary>
    /// Interface de serviço para operações relacionadas à entidade Pedido.
    /// Herda os métodos CRUD genéricos de IGenericService.
    /// </summary>
    public interface IPedidoService : IGenericService<PedidoDTO>
    {
        /// <summary>
        /// Obtém o total de pedidos cadastrados no sistema.
        /// Útil para indicadores, dashboards ou relatórios.
        /// </summary>
        /// <returns>Quantidade total de pedidos.</returns>
        Task<int> GetTotalCount();
    }
}
