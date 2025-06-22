using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório para operações de acesso a dados da entidade Cidade.
    /// Responsável pelas operações básicas de CRUD.
    /// </summary>
    public class CidadeRepository : ICidadeRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Injeta o contexto do banco de dados no repositório.
        /// </summary>
        public CidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova cidade no banco de dados.
        /// </summary>
        public async Task<Cidade> Create(Cidade entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove uma cidade do banco de dados.
        /// </summary>
        public async Task<Cidade> Delete(Cidade entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Retorna todas as cidades cadastradas.
        /// </summary>
        public async Task<IEnumerable<Cidade>> GetAll()
        {
            return await _context.Cidade.ToListAsync();
        }

        /// <summary>
        /// Busca uma cidade pelo Id. 
        /// Lança exceção se não encontrar.
        /// </summary>
        public async Task<Cidade> GetById(int? id)
        {
            var x = await _context.Cidade.SingleOrDefaultAsync(p => p.Id == id);
            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        /// <summary>
        /// Atualiza os dados de uma cidade existente.
        /// </summary>
        public async Task<Cidade> Update(Cidade entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
