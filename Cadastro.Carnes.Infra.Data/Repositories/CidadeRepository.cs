using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private ApplicationDbContext _context;

        public CidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cidade> Create(Cidade entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Cidade> Delete(Cidade entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Cidade>> GetAll()
        {
            return await _context.Cidade.ToListAsync();
        }

        public async Task<Cidade> GetById(int? id)
        {
            var x = await _context.Cidade.SingleOrDefaultAsync(p => p.Id == id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");

        }

        public async Task<Cidade> Update(Cidade entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
