using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        Task<Pedido> ExistePedidoComVendedor(int? compradorId);
    }
}
