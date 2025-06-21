using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    public interface ICompradorRepository : IGenericRepository<Comprador>
    {
        Task<Comprador> ExisteCidadePorComprador(int? cidadeId);
    }
}
