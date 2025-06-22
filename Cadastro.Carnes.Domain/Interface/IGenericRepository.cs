namespace Cadastro.Carnes.Domain.Interface
{
    /// <summary>
    /// Interface genérica para repositórios de entidades.
    /// Define operações básicas de CRUD (Create, Read, Update, Delete).
    /// </summary>
    /// <typeparam name="T">Tipo da entidade gerenciada pelo repositório.</typeparam>
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Retorna todas as entidades do tipo T.
        /// </summary>
        /// <returns>Lista de entidades encontradas.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Busca uma entidade pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da entidade.</param>
        /// <returns>Entidade encontrada ou null.</returns>
        Task<T> GetById(int? id);

        /// <summary>
        /// Cria uma nova entidade no repositório.
        /// </summary>
        /// <param name="entity">Entidade a ser criada.</param>
        /// <returns>Entidade criada (com possíveis valores gerados, como Id).</returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Atualiza uma entidade existente.
        /// </summary>
        /// <param name="entity">Entidade com dados atualizados.</param>
        /// <returns>Entidade atualizada.</returns>
        Task<T> Update(T entity);

        /// <summary>
        /// Remove uma entidade do repositório.
        /// </summary>
        /// <param name="entity">Entidade a ser removida.</param>
        /// <returns>Entidade removida.</returns>
        Task<T> Delete(T entity);
    }
}
