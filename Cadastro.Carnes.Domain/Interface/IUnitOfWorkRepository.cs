namespace Cadastro.Carnes.Domain.Interface
{
    public interface IUnitOfWorkRepository
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveChangesAsync();
    }
}
