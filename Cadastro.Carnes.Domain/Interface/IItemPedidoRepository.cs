using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    public interface IItemPedidoRepository : IGenericRepository<ItemPedido>
    {
        Task<bool> DeletaItemPorNumeroDoPedido(int? pedidoId);
        Task<ItemPedido> ExisteItemComEssaMoeda(int? moedaId);
        Task<List<ItemPedido>> ExistePorCarneIdAsync(int? carneId);
    }
}
