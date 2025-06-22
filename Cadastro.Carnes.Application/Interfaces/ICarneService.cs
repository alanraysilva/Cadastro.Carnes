using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    public interface ICarneService : IGenericService<CarneDTO>
    {
        Task<int> GetTotalCount();
    }
}
