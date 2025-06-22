using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Responsável pelas operações de acesso e manipulação dos dados da entidade Origem.
    /// </summary>
    public class OrigemRepository : IOrigemRepository
    {
        // Contexto do banco de dados utilizado para as operações
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor recebe o contexto via injeção de dependência.
        /// </summary>
        public OrigemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova origem ao banco de dados.
        /// </summary>
        public async Task<Origem> Create(Origem entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove uma origem existente do banco de dados.
        /// </summary>
        public async Task<Origem> Delete(Origem entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Retorna todas as origens cadastradas.
        /// </summary>
        public async Task<IEnumerable<Origem>> GetAll()
        {
            return await _context.Origem.ToListAsync();
        }

        /// <summary>
        /// Busca uma origem pelo Id. Lança exceção se não encontrar.
        /// </summary>
        public async Task<Origem> GetById(int? id)
        {
            var x = await _context.Origem.SingleOrDefaultAsync(p => p.Id == id);
            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        /// <summary>
        /// Atualiza os dados de uma origem existente no banco de dados.
        /// </summary>
        public async Task<Origem> Update(Origem entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
