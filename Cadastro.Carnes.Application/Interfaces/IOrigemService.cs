using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    /// <summary>
    /// Interface de serviço para operações relacionadas à entidade Origem.
    /// Herda os métodos CRUD genéricos de IGenericService.
    /// </summary>
    public interface IOrigemService : IGenericService<OrigemDTO>
    {
    }
}
