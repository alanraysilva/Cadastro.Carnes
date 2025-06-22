using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    public interface IPedidoService : IGenericService<PedidoDTO>
    {
        Task<int> GetTotalCount();
    }
}
