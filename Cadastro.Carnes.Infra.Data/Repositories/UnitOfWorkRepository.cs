using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Implementação do padrão Unit of Work para controle transacional nas operações de banco de dados.
    /// </summary>
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        // Contexto do banco de dados
        private ApplicationDbContext _context;

        // Transação atual em uso
        private IDbContextTransaction? _transaction;

        /// <summary>
        /// Construtor recebe o contexto via injeção de dependência.
        /// </summary>
        public UnitOfWorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inicia uma nova transação no contexto do banco de dados.
        /// </summary>
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Efetiva (confirma) todas as operações realizadas durante a transação.
        /// Salva alterações no contexto e executa o commit da transação.
        /// </summary>
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
        }

        /// <summary>
        /// Desfaz todas as operações realizadas na transação, restaurando o estado anterior.
        /// </summary>
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        /// <summary>
        /// Salva todas as alterações pendentes no contexto do banco de dados.
        /// </summary>
        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

        /// <summary>
        /// Libera os recursos da transação, se houver.
        /// </summary>
        public void Dispose() => _transaction?.Dispose();
    }
}
