using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    public interface IGenericService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int? id);
        Task<RetornoPadraoDTO> Add(T EnttiyDTO);
        Task<RetornoPadraoDTO> Update(T EnttiyDTO);
        Task<RetornoPadraoDTO> Remove(int? id);
    }
}
