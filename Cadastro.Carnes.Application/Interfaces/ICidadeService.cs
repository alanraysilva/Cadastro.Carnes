using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    /// <summary>
    /// Interface de serviço para operações relacionadas à entidade Cidade.
    /// Herda os métodos CRUD genéricos definidos em IGenericService.
    /// </summary>
    public interface ICidadeService : IGenericService<CidadeDTO>
    {
    }
}
