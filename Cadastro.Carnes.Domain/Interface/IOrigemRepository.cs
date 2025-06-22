using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Domain.Interface
{
    /// <summary>
    /// Interface de repositório para operações de acesso à entidade Origem.
    /// Herda métodos CRUD padrão do repositório genérico.
    /// </summary>
    public interface IOrigemRepository : IGenericRepository<Origem>
    {
    }
}
