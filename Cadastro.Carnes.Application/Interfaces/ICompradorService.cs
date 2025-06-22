using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    public interface ICompradorService : IGenericService<CompradorDTO>
    {
        Task<int> GetTotalAtivos();
    }
}
