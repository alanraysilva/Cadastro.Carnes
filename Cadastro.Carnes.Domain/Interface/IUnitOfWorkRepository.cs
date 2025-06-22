namespace Cadastro.Carnes.Domain.Interface
{
    /// <summary>
    /// Interface responsável pelo controle de transações no padrão Unit of Work.
    /// Centraliza o commit, rollback e controle das operações no contexto do banco.
    /// </summary>
    public interface IUnitOfWorkRepository
    {
        /// <summary>
        /// Inicia uma nova transação no contexto atual.
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Efetiva (confirma) todas as alterações realizadas durante a transação.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Desfaz todas as alterações realizadas durante a transação em andamento.
        /// </summary>
        Task RollbackAsync();

        /// <summary>
        /// Salva todas as alterações pendentes no contexto do banco de dados.
        /// </summary>
        /// <returns>Quantidade de registros afetados</returns>
        Task<int> SaveChangesAsync();
    }
}
