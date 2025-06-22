using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório para operações de acesso a dados da entidade Carne.
    /// Implementa as operações básicas de CRUD e consultas customizadas.
    /// </summary>
    public class CarneRepository : ICarneRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Injeta o contexto do banco via construtor.
        /// </summary>
        public CarneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo registro de Carne no banco de dados.
        /// </summary>
        public async Task<Carne> Create(Carne entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove um registro de Carne do banco de dados.
        /// </summary>
        public async Task<Carne> Delete(Carne entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Verifica se existe uma carne vinculada a uma determinada origem.
        /// Retorna a primeira encontrada ou null.
        /// </summary>
        public async Task<Carne> ExisteCarneComEssaOrigem(int? origemId)
        {
            var x = await _context.Carne.SingleOrDefaultAsync(p => p.OrigemId == origemId);
            return x!;
        }

        /// <summary>
        /// Retorna todas as carnes cadastradas, incluindo a origem relacionada.
        /// </summary>
        public async Task<IEnumerable<Carne>> GetAll()
        {
            return await _context.Carne.Include(x => x.Origem).ToListAsync();
        }

        /// <summary>
        /// Busca uma carne pelo Id, incluindo a origem. 
        /// Lança exceção se não encontrar o registro.
        /// </summary>
        public async Task<Carne> GetById(int? id)
        {
            var x = await _context.Carne.Include(x => x.Origem).SingleOrDefaultAsync(p => p.Id == id);
            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        /// <summary>
        /// Atualiza um registro de Carne existente no banco.
        /// </summary>
        public async Task<Carne> Update(Carne entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
