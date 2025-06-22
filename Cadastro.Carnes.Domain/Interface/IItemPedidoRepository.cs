using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    /// <summary>
    /// Interface específica para operações relacionadas a itens de pedido.
    /// Herda operações básicas do repositório genérico e adiciona métodos de negócio para regras envolvendo itens de pedidos.
    /// </summary>
    public interface IItemPedidoRepository : IGenericRepository<ItemPedido>
    {
        /// <summary>
        /// Remove todos os itens associados a um pedido específico.
        /// </summary>
        /// <param name="pedidoId">Identificador do pedido.</param>
        /// <returns>True se a exclusão for bem-sucedida, false caso contrário.</returns>
        Task<bool> DeletaItemPorNumeroDoPedido(int? pedidoId);

        /// <summary>
        /// Verifica se existe algum item de pedido associado a uma determinada moeda.
        /// </summary>
        /// <param name="moedaId">Identificador da moeda.</param>
        /// <returns>O item encontrado ou null se não houver vínculo.</returns>
        Task<ItemPedido> ExisteItemComEssaMoeda(int? moedaId);

        /// <summary>
        /// Retorna a lista de itens de pedido que possuem a carne informada.
        /// </summary>
        /// <param name="carneId">Identificador da carne.</param>
        /// <returns>Lista de itens de pedido relacionados à carne.</returns>
        Task<List<ItemPedido>> ExistePorCarneIdAsync(int? carneId);
    }
}
