using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório responsável pelo acesso e manipulação dos dados de Comprador.
    /// Implementa as operações básicas de CRUD e consultas específicas.
    /// </summary>
    public class CompradorRepository : ICompradorRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Construtor recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        public CompradorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo comprador ao banco de dados.
        /// </summary>
        public async Task<Comprador> Create(Comprador entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove um comprador do banco de dados.
        /// </summary>
        public async Task<Comprador> Delete(Comprador entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Retorna o primeiro comprador que pertence à cidade informada.
        /// </summary>
        public async Task<Comprador> ExisteCidadePorComprador(int? cidadeId)
        {
            var x = await _context.Comprador
                                  .Include(x => x.Cidade)
                                  .SingleOrDefaultAsync(p => p.CidadeId == cidadeId);
            return x!;
        }

        /// <summary>
        /// Lista todos os compradores, incluindo a cidade relacionada.
        /// </summary>
        public async Task<IEnumerable<Comprador>> GetAll()
        {
            return await _context.Comprador
                                 .Include(x => x.Cidade)
                                 .ToListAsync();
        }

        /// <summary>
        /// Busca um comprador pelo Id, trazendo também os dados da cidade.
        /// Lança exceção se não encontrar.
        /// </summary>
        public async Task<Comprador> GetById(int? id)
        {
            var x = await _context.Comprador
                                  .Include(x => x.Cidade)
                                  .SingleOrDefaultAsync(p => p.Id == id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        /// <summary>
        /// Atualiza os dados de um comprador existente.
        /// </summary>
        public async Task<Comprador> Update(Comprador entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
