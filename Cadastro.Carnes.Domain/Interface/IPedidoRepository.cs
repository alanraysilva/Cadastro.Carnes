using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    /// <summary>
    /// Interface de repositório para operações relacionadas à entidade Pedido.
    /// Herda operações CRUD do repositório genérico.
    /// </summary>
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        /// <summary>
        /// Verifica se existe algum pedido relacionado a um determinado comprador (vendedor).
        /// </summary>
        /// <param name="compradorId">ID do comprador a ser verificado</param>
        /// <returns>Retorna o pedido encontrado ou null, caso não exista.</returns>
        Task<Pedido> ExistePedidoComVendedor(int? compradorId);
    }
}
