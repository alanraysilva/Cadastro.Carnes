using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    /// <summary>
    /// Interface de serviço para operações relacionadas ao Item do Pedido.
    /// Herda todos os métodos CRUD genéricos de IGenericService.
    /// </summary>
    public interface IItemPedidoService : IGenericService<ItemPedidoDTO>
    {
    }
}
