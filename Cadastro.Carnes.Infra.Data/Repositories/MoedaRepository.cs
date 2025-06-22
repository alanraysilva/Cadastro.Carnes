using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório responsável pelas operações de persistência da entidade Moeda.
    /// Implementa métodos CRUD básicos.
    /// </summary>
    public class MoedaRepository : IMoedaRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Recebe o contexto de banco de dados via injeção de dependência.
        /// </summary>
        public MoedaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova moeda ao banco de dados.
        /// </summary>
        public async Task<Moeda> Create(Moeda entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove uma moeda existente do banco de dados.
        /// </summary>
        public async Task<Moeda> Delete(Moeda entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Retorna todas as moedas cadastradas no banco.
        /// </summary>
        public async Task<IEnumerable<Moeda>> GetAll()
        {
            return await _context.Moeda.ToListAsync();
        }

        /// <summary>
        /// Busca uma moeda pelo seu Id.
        /// Lança exceção se não encontrar.
        /// </summary>
        public async Task<Moeda> GetById(int? id)
        {
            var x = await _context.Moeda.SingleOrDefaultAsync(p => p.Id == id);
            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        /// <summary>
        /// Atualiza os dados de uma moeda existente no banco de dados.
        /// </summary>
        public async Task<Moeda> Update(Moeda entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
