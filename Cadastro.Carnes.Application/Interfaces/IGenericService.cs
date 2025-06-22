using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.Application.Interfaces
{
    /// <summary>
    /// Interface de serviço genérico para entidades do sistema.
    /// Define os métodos CRUD padrão para qualquer DTO.
    /// </summary>
    /// <typeparam name="T">Tipo do DTO a ser manipulado.</typeparam>
    public interface IGenericService<T>
    {
        /// <summary>
        /// Retorna todos os registros da entidade.
        /// Use para listagens completas.
        /// </summary>
        /// <returns>Enumerable com todos os DTOs.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Busca um registro pelo identificador.
        /// </summary>
        /// <param name="id">ID do registro.</param>
        /// <returns>DTO encontrado ou null/não encontrado.</returns>
        Task<T> GetById(int? id);

        /// <summary>
        /// Adiciona um novo registro no sistema.
        /// </summary>
        /// <param name="EnttiyDTO">DTO com os dados a serem cadastrados.</param>
        /// <returns>Retorno padrão informando sucesso ou erro.</returns>
        Task<RetornoPadraoDTO> Add(T EnttiyDTO);

        /// <summary>
        /// Atualiza um registro existente no sistema.
        /// </summary>
        /// <param name="EnttiyDTO">DTO com os dados atualizados.</param>
        /// <returns>Retorno padrão informando sucesso ou erro.</returns>
        Task<RetornoPadraoDTO> Update(T EnttiyDTO);

        /// <summary>
        /// Remove um registro pelo ID.
        /// </summary>
        /// <param name="id">ID do registro a ser removido.</param>
        /// <returns>Retorno padrão informando sucesso ou erro.</returns>
        Task<RetornoPadraoDTO> Remove(int? id);
    }
}
