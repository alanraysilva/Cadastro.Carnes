using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    public interface ICarneRepository : IGenericRepository<Carne>
    {
        Task<Carne> ExisteCarneComEssaOrigem(int? origemId);
    }
}
